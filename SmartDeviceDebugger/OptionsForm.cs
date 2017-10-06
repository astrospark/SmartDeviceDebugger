using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;
using NAudio.CoreAudioApi;

namespace SmartDevice
{
	public partial class OptionsForm : Form
	{
		public OptionsForm()
		{
			InitializeComponent();

			PopulateInputDevices();
			PopulateOutputDevices();
		}

		public string SelectedInputDeviceID
		{
			get => (string) inputDeviceComboBox.SelectedValue;
			set => inputDeviceComboBox.SelectedValue = value;
		}

		public string SelectedOutputDeviceID
		{
			get => (string) outputDeviceComboBox.SelectedValue;
			set => outputDeviceComboBox.SelectedValue = value;
		}

		private void PopulateInputDevices()
		{
			var deviceEnumerator = new MMDeviceEnumerator();
			inputDeviceComboBox.DataSource = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active).ToList();
			inputDeviceComboBox.ValueMember = nameof(MMDevice.ID);
			inputDeviceComboBox.DisplayMember = nameof(MMDevice.FriendlyName);
		}

		private void PopulateOutputDevices()
		{
			var deviceEnumerator = new MMDeviceEnumerator();
			outputDeviceComboBox.DataSource = deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active).ToList();
			outputDeviceComboBox.ValueMember = nameof(MMDevice.ID);
			outputDeviceComboBox.DisplayMember = nameof(MMDevice.FriendlyName);
		}
	}
}
