using System;
using System.Windows.Forms;
using SmartDevice.SmartDeviceProtocol;

namespace SmartDevice
{
	internal partial class VariablesForm : Form
	{
		public VariablesForm()
		{
			InitializeComponent();

			_timer = new Timer
			{
				Interval = 300
			};
			_timer.Tick += timer_Tick;
		}

		public VariablesForm(Encoder encoder, Decoder decoder)
			: this()
		{
			_encoder = encoder ?? throw new ArgumentNullException(nameof(encoder));
			_decoder = decoder ?? throw new ArgumentNullException(nameof(decoder));
			_decoder.BlockReceived += decoder_BlockReceived;
		}

		private void VariablesForm_Load(object sender, EventArgs e)
		{
			variablesListView.Items.Clear();
			for (var variable = 0; variable <= 0x35; variable++)
			{
				var item = variablesListView.Items.Add($"0x{variable:X2}");
				item.SubItems.Add(VariableName.Get((byte) variable));
				item.SubItems.Add(string.Empty);
			}
		}

		private void getAllButton_Click(object sender, EventArgs e)
		{
			if (_getAllActive)
			{
				GetAllStop();
			}
			else
			{
				GetAllStart();
			}
		}

		private void decoder_BlockReceived(object sender, BlockReceivedEventArgs e)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new EventHandler<BlockReceivedEventArgs>(decoder_BlockReceived), sender, e);
				return;
			}

			var block = e.Block;
			if (block.BlockType != 0x20 || block.Data.Count < 2 || !block.ChecksumValid) return;

			var variable = block.Data[0];
			UpdateVariable(variable, block.Data[1]);

			if (_getAllActive && variable == _getAllVariable)
			{
				_getAllVariable++;
				GetAll();
			}
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			GetAll();
		}

		private void GetAllStart()
		{
			_getAllActive = true;
			_getAllVariable = 0;
			GetAll();
			getAllButton.Text = "&Stop";
		}

		private void GetAllStop()
		{
			_getAllActive = false;
			_timer.Stop();
			getAllButton.Text = "&Get All";
		}

		private void GetAll()
		{
			if (!_getAllActive) return;
			if (_getAllVariable > 0x35) GetAllStop();

			var readVariableBlock = new Block(0xA0); // READ-VARIABLE
			readVariableBlock.Data.Add(_getAllVariable);
			_encoder.Send(readVariableBlock);

			_timer.Start();
		}

		private void UpdateVariable(byte variable, byte value)
		{
			if (variablesListView.Items.Count < variable + 1) return;
			var item = variablesListView.Items[variable];
			if (item.SubItems.Count < 3) return;
			item.SubItems[2].Text = $@"0x{value:X2}";
		}

		private readonly Timer _timer;
		private readonly Encoder _encoder;
		private readonly Decoder _decoder;
		private bool _getAllActive;
		private byte _getAllVariable;
	}
}
