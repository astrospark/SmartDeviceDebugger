namespace SmartDevice
{
	partial class OptionsForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.outputDeviceLabel = new System.Windows.Forms.Label();
			this.inputDeviceLabel = new System.Windows.Forms.Label();
			this.outputDeviceComboBox = new System.Windows.Forms.ComboBox();
			this.inputDeviceComboBox = new System.Windows.Forms.ComboBox();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.phaseInvertCheckBox = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// outputDeviceLabel
			// 
			this.outputDeviceLabel.Location = new System.Drawing.Point(11, 43);
			this.outputDeviceLabel.Name = "outputDeviceLabel";
			this.outputDeviceLabel.Size = new System.Drawing.Size(85, 13);
			this.outputDeviceLabel.TabIndex = 2;
			this.outputDeviceLabel.Text = "Ou&tput Device:";
			this.outputDeviceLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// inputDeviceLabel
			// 
			this.inputDeviceLabel.Location = new System.Drawing.Point(11, 15);
			this.inputDeviceLabel.Name = "inputDeviceLabel";
			this.inputDeviceLabel.Size = new System.Drawing.Size(85, 13);
			this.inputDeviceLabel.TabIndex = 0;
			this.inputDeviceLabel.Text = "&Input Device:";
			this.inputDeviceLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// outputDeviceComboBox
			// 
			this.outputDeviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.outputDeviceComboBox.FormattingEnabled = true;
			this.outputDeviceComboBox.Location = new System.Drawing.Point(102, 40);
			this.outputDeviceComboBox.Name = "outputDeviceComboBox";
			this.outputDeviceComboBox.Size = new System.Drawing.Size(340, 21);
			this.outputDeviceComboBox.TabIndex = 3;
			// 
			// inputDeviceComboBox
			// 
			this.inputDeviceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.inputDeviceComboBox.FormattingEnabled = true;
			this.inputDeviceComboBox.Location = new System.Drawing.Point(102, 12);
			this.inputDeviceComboBox.Name = "inputDeviceComboBox";
			this.inputDeviceComboBox.Size = new System.Drawing.Size(340, 21);
			this.inputDeviceComboBox.TabIndex = 1;
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(286, 80);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 5;
			this.okButton.Text = "&OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(367, 80);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 6;
			this.cancelButton.Text = "&Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// phaseInvertCheckBox
			// 
			this.phaseInvertCheckBox.AutoSize = true;
			this.phaseInvertCheckBox.Location = new System.Drawing.Point(102, 68);
			this.phaseInvertCheckBox.Name = "phaseInvertCheckBox";
			this.phaseInvertCheckBox.Size = new System.Drawing.Size(86, 17);
			this.phaseInvertCheckBox.TabIndex = 4;
			this.phaseInvertCheckBox.Text = "Invert &Phase";
			this.phaseInvertCheckBox.UseVisualStyleBackColor = true;
			// 
			// OptionsForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(454, 115);
			this.Controls.Add(this.phaseInvertCheckBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.outputDeviceLabel);
			this.Controls.Add(this.inputDeviceLabel);
			this.Controls.Add(this.outputDeviceComboBox);
			this.Controls.Add(this.inputDeviceComboBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionsForm";
			this.Text = "Options";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label outputDeviceLabel;
		private System.Windows.Forms.Label inputDeviceLabel;
		private System.Windows.Forms.ComboBox outputDeviceComboBox;
		private System.Windows.Forms.ComboBox inputDeviceComboBox;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.CheckBox phaseInvertCheckBox;
	}
}