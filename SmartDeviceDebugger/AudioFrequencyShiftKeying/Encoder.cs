using System;
using System.Collections.Generic;
using System.Linq;
using CSCore;
using CSCore.CoreAudioAPI;
using CSCore.SoundOut;
using CSCore.Streams;
using CSCore.Streams.SampleConverter;

namespace SmartDevice.AudioFrequencyShiftKeying
{
	internal class Encoder : ISampleSource
	{
		public Encoder()
		{
			_queue = new Queue<byte>();
		}

		public bool PhaseInvert { get; set; }

		public bool CanSeek => false;

		public WaveFormat WaveFormat => new WaveFormat(48000, 32, 1, AudioEncoding.IeeeFloat);

		public long Position { get => 0; set => throw new InvalidOperationException(); }

		public long Length => 0;

		public void Start(string deviceID, float volume)
		{
			if (_audioOut != null) return;

			var deviceEnumerator = new MMDeviceEnumerator();
			_renderDevice = deviceEnumerator.EnumAudioEndpoints(DataFlow.Render, DeviceState.Active).FirstOrDefault(endpoint => endpoint.DeviceID == deviceID);
			if (_renderDevice == null)
			{
				throw new ArgumentException(@"The requested device ID could not be found.", nameof(deviceID));
			}

			var monoToStereoSource = new MonoToStereoSource(this);
			var sampleToPcm16 = new SampleToPcm16(monoToStereoSource);
			var audioClient = AudioClient.FromMMDevice(_renderDevice);
			if (!audioClient.IsFormatSupported(AudioClientShareMode.Exclusive, sampleToPcm16.WaveFormat))
			{
				throw new ArgumentException(@"The requested device does not support the required stream format.", nameof(deviceID));
			}

			_audioOut = new WasapiOut(false, AudioClientShareMode.Exclusive, 50);

			var audioEndpointVolume = AudioEndpointVolume.FromDevice(_renderDevice);
			_previousVolume = audioEndpointVolume.MasterVolumeLevel;
			audioEndpointVolume.MasterVolumeLevelScalar = volume;

			_audioOut.Initialize(sampleToPcm16);
			_audioOut.Play();
		}

		public void Stop()
		{
			if (_audioOut == null) return;

			_audioOut.Stop();
			AudioEndpointVolume.FromDevice(_renderDevice).MasterVolumeLevel = _previousVolume;
			_audioOut.Dispose();
			_audioOut = null;
		}

		public void Send(IEnumerable<byte> values)
		{
			foreach (var value in values)
			{
				_queue.Enqueue(value);
			}
		}

		public int Read(float[] buffer, int offset, int sampleCount)
		{
			var sampleRate = WaveFormat.SampleRate;
			for (var n = 0; n < sampleCount; n++)
			{
				if (_currentSample >= _fullCycleSamples) // We've finished a full cycle of the current value
				{
					var value = _queue.Count > 0 // Get the next value
						? _queue.Dequeue()
						: (byte) 1; // Send a mark if we have nothing else to send
					_frequency = value == 0 ? 4000 : 2000;
					_fullCycleSamples = sampleRate / _frequency;
					_currentSample = 0;
				}
				buffer[n + offset] = (float) Math.Sin(2 * Math.PI * _currentSample * _frequency / sampleRate) * (PhaseInvert ? -1.0f : 1.0f);
				_currentSample++;
			}
			return sampleCount;
		}

		public void Dispose()
		{
		}

		private readonly Queue<byte> _queue;
		private MMDevice _renderDevice;
		private WasapiOut _audioOut;
		private float _previousVolume;
		private int _frequency;
		private int _fullCycleSamples;
		private int _currentSample;
	}
}
