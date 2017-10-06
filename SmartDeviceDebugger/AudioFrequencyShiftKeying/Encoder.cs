using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace SmartDevice.AudioFrequencyShiftKeying
{
	internal class Encoder : WaveProvider32
	{
		public Encoder()
		{
			_queue = new Queue<byte>();
		}

		public void Start(string deviceID, float volume)
		{
			if (_audioOut != null) return;

			var deviceEnumerator = new MMDeviceEnumerator();
			var renderDevice = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active).FirstOrDefault(endpoint => endpoint.ID == deviceID);
			if (renderDevice == null)
			{
				throw new ArgumentException(@"The requested device ID could not be found.", nameof(deviceID));
			}

			SetWaveFormat(48000, 1);
			var wave16Provider = new WaveFloatTo16Provider(this);
			var stereoProvider = new MonoToStereoProvider16(wave16Provider);
			if (!renderDevice.AudioClient.IsFormatSupported(AudioClientShareMode.Exclusive, stereoProvider.WaveFormat))
			{
				throw new ArgumentException(@"The requested device does not support the required stream format.", nameof(deviceID));
			}

			_audioOut = new WasapiOut(renderDevice, AudioClientShareMode.Exclusive, true, 50);
			_previousVolume = _audioOut.Volume;
			_audioOut.Volume = volume;
			_audioOut.Init(stereoProvider);
			_audioOut.Play();
		}

		public void Stop()
		{
			if (_audioOut == null) return;

			_audioOut.Stop();
			_audioOut.Volume = _previousVolume;
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

		public override int Read(float[] buffer, int offset, int sampleCount)
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
				buffer[n + offset] = (float) Math.Sin(2 * Math.PI * _currentSample * _frequency / sampleRate) * -1.0f;
				_currentSample++;
			}
			return sampleCount;
		}

		private readonly Queue<byte> _queue;
		private WasapiOut _audioOut;
		private float _previousVolume;
		private int _frequency;
		private int _fullCycleSamples;
		private int _currentSample;
	}
}
