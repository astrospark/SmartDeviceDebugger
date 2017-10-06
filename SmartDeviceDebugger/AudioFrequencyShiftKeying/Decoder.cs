using System;
using System.Collections.Generic;
using System.Linq;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace SmartDevice.AudioFrequencyShiftKeying
{
	internal class Decoder
	{
		public Decoder()
		{
			_buffer = new List<byte>();
		}

		public EventHandler<DataReceivedEventArgs> DataReceived;

		public void Start(string deviceID, float volume)
		{
			if (_audioIn != null) return;

			var deviceEnumerator = new MMDeviceEnumerator();
			_captureDevice = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).FirstOrDefault(endpoint => endpoint.ID == deviceID);
			if (_captureDevice == null)
			{
				throw new ArgumentException(@"The requested device ID could not be found.", nameof(deviceID));
			}

			var waveFormat = new WaveFormat(SampleFrequency, 16, 2);
			if (!_captureDevice.AudioClient.IsFormatSupported(AudioClientShareMode.Exclusive, waveFormat))
			{
				throw new ArgumentException(@"The requested device does not support the required stream format.", nameof(deviceID));
			}

			_sampleCount = 0;
			_previousSample = 0;
			_previousCycleType = CycleType.Processed;

			_audioIn = new WasapiCapture(_captureDevice, true, 50)
			{
				ShareMode = AudioClientShareMode.Exclusive,
				WaveFormat = waveFormat
			};
			_previousVolume = _captureDevice.AudioEndpointVolume.MasterVolumeLevel;
			_captureDevice.AudioEndpointVolume.MasterVolumeLevelScalar = volume;
			_audioIn.DataAvailable += audioIn_DataAvailable;
			_audioIn.StartRecording();
		}

		public void Stop()
		{
			if (_audioIn == null) return;

			_audioIn.StopRecording();
			_captureDevice.AudioEndpointVolume.MasterVolumeLevel = _previousVolume;
			_audioIn.Dispose();
			_audioIn = null;
		}

		private void audioIn_DataAvailable(object sender, WaveInEventArgs e)
		{
			for (var n = 0; n < e.BytesRecorded; n += 2)
			{
				if (n % 4 == 0) continue; // skip even samples (left channel)

				var sample = BitConverter.ToUInt16(e.Buffer, n) - 32768;

				if (ZeroCrossed(_previousSample, sample))
				{
					ProcessCycle(_sampleCount);
					_sampleCount = 0;
				}

				_sampleCount++;
				_previousSample = sample;
			}

			// ReSharper disable once InvertIf
			if (_buffer.Count > 0)
			{
				DataReceived(this, new DataReceivedEventArgs(_buffer));
				_buffer.Clear();
			}
		}

		private void ProcessCycle(int samples)
		{
			CycleType cycleType;

			if (MatchSpace(samples))
			{
				cycleType = CycleType.Space;
			}
			else if (MatchMark(samples))
			{
				cycleType = CycleType.Mark;
			}
			else
			{
				cycleType = CycleType.Error;
				// Debug.WriteLine($"Invalid cycle length. {samples} samples");
			}

			if (_previousCycleType == CycleType.Space && cycleType == CycleType.Space)
			{
				_buffer.Add(0);
				cycleType = CycleType.Processed;
			}
			else if (_previousCycleType == CycleType.Mark && cycleType == CycleType.Mark)
			{
				_buffer.Add(1);
				cycleType = CycleType.Processed;
			}
			else if (cycleType == CycleType.Error)
			{
				_buffer.Add(2);
			}
			else if ((_previousCycleType == CycleType.Space && cycleType == CycleType.Mark) ||
			         (_previousCycleType == CycleType.Mark && cycleType == CycleType.Space))
			{
				_buffer.Add(3); // Phase error
				// Debug.WriteLine("Phase error.");
			}

			_previousCycleType = cycleType;
		}

		private static bool ZeroCrossed(int previous, int current)
		{
			return (previous > 0 && current < 0) ||
			       (previous < 0 && current > 0) ||
			       current == 0;
		}

		private static bool MatchMark(int samples)
		{
			return samples > HalfMarkSamplesMin && samples < HalfMarkSamplesMax;
		}

		private static bool MatchSpace(int samples)
		{
			return samples > HalfSpaceSamplesMin && samples < HalfSpaceSamplesMax;
		}

		private const int SampleFrequency = 48000;

		// Mark 2,000Hz, 12 samples
		private const int HalfMarkSamplesMin = 9;
		private const int HalfMarkSamplesMax = 48;

		// Space 4,000Hz 6 samples
		private const int HalfSpaceSamplesMin = 2;
		private const int HalfSpaceSamplesMax = 8;

		private readonly List<byte> _buffer;
		private MMDevice _captureDevice;
		private WasapiCapture _audioIn;
		private float _previousVolume;
		private int _sampleCount;
		private int _previousSample;
		private CycleType _previousCycleType;

		private enum CycleType
		{
			Space,
			Mark,
			Processed,
			Error
		}
	}
}