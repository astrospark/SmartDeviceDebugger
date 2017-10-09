using System.Linq;
using System.Windows.Forms;
using CSCore.CoreAudioAPI;

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

		public bool PhaseInvert
		{
			get => phaseInvertCheckBox.Checked;
			set => phaseInvertCheckBox.Checked = value;
		}

		private void PopulateInputDevices()
		{
			var deviceEnumerator = new MMDeviceEnumerator();
			inputDeviceComboBox.DataSource = deviceEnumerator.EnumAudioEndpoints(DataFlow.Capture, DeviceState.Active).ToList();
			inputDeviceComboBox.ValueMember = nameof(MMDevice.DeviceID);
			inputDeviceComboBox.DisplayMember = nameof(MMDevice.FriendlyName);
		}

		private void PopulateOutputDevices()
		{
			var deviceEnumerator = new MMDeviceEnumerator();
			outputDeviceComboBox.DataSource = deviceEnumerator.EnumAudioEndpoints(DataFlow.Render, DeviceState.Active).ToList();
			outputDeviceComboBox.ValueMember = nameof(MMDevice.DeviceID);
			outputDeviceComboBox.DisplayMember = nameof(MMDevice.FriendlyName);
		}
	}
}
