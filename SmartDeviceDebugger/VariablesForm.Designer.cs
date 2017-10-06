namespace SmartDevice
{
	partial class VariablesForm
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
			this.variablesListView = new System.Windows.Forms.ListView();
			this.numberColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.valueColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.getAllButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// variablesListView
			// 
			this.variablesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.variablesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.numberColumnHeader,
            this.nameColumnHeader,
            this.valueColumnHeader});
			this.variablesListView.FullRowSelect = true;
			this.variablesListView.GridLines = true;
			this.variablesListView.Location = new System.Drawing.Point(0, 41);
			this.variablesListView.Name = "variablesListView";
			this.variablesListView.Size = new System.Drawing.Size(339, 432);
			this.variablesListView.TabIndex = 0;
			this.variablesListView.UseCompatibleStateImageBehavior = false;
			this.variablesListView.View = System.Windows.Forms.View.Details;
			// 
			// numberColumnHeader
			// 
			this.numberColumnHeader.Text = "Num";
			// 
			// nameColumnHeader
			// 
			this.nameColumnHeader.Text = "Name";
			this.nameColumnHeader.Width = 180;
			// 
			// valueColumnHeader
			// 
			this.valueColumnHeader.Text = "Value";
			// 
			// getAllButton
			// 
			this.getAllButton.Location = new System.Drawing.Point(12, 12);
			this.getAllButton.Name = "getAllButton";
			this.getAllButton.Size = new System.Drawing.Size(75, 23);
			this.getAllButton.TabIndex = 1;
			this.getAllButton.Text = "&Get All";
			this.getAllButton.UseVisualStyleBackColor = true;
			this.getAllButton.Click += new System.EventHandler(this.getAllButton_Click);
			// 
			// VariablesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(339, 473);
			this.Controls.Add(this.getAllButton);
			this.Controls.Add(this.variablesListView);
			this.Name = "VariablesForm";
			this.Text = "Variables";
			this.Load += new System.EventHandler(this.VariablesForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView variablesListView;
		private System.Windows.Forms.ColumnHeader numberColumnHeader;
		private System.Windows.Forms.ColumnHeader nameColumnHeader;
		private System.Windows.Forms.ColumnHeader valueColumnHeader;
		private System.Windows.Forms.Button getAllButton;
	}
}