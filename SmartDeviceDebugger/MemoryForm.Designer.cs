namespace SmartDevice
{
	partial class MemoryForm
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
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.cancelButton = new System.Windows.Forms.Button();
			this.getAllButton = new System.Windows.Forms.Button();
			this.memoryTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// progressBar
			// 
			this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar.Location = new System.Drawing.Point(12, 41);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(365, 23);
			this.progressBar.TabIndex = 7;
			this.progressBar.Visible = false;
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.Location = new System.Drawing.Point(383, 41);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 6;
			this.cancelButton.Text = "&Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Visible = false;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// getAllButton
			// 
			this.getAllButton.Location = new System.Drawing.Point(12, 12);
			this.getAllButton.Name = "getAllButton";
			this.getAllButton.Size = new System.Drawing.Size(100, 23);
			this.getAllButton.TabIndex = 5;
			this.getAllButton.Text = "Get &All";
			this.getAllButton.UseVisualStyleBackColor = true;
			this.getAllButton.Click += new System.EventHandler(this.getAllButton_Click);
			// 
			// memoryTextBox
			// 
			this.memoryTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoryTextBox.BackColor = System.Drawing.SystemColors.Window;
			this.memoryTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.memoryTextBox.Location = new System.Drawing.Point(0, 70);
			this.memoryTextBox.Multiline = true;
			this.memoryTextBox.Name = "memoryTextBox";
			this.memoryTextBox.ReadOnly = true;
			this.memoryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.memoryTextBox.Size = new System.Drawing.Size(470, 336);
			this.memoryTextBox.TabIndex = 8;
			// 
			// MemoryForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(470, 406);
			this.Controls.Add(this.memoryTextBox);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.getAllButton);
			this.Name = "MemoryForm";
			this.Text = "Memory";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button getAllButton;
		private System.Windows.Forms.TextBox memoryTextBox;
	}
}