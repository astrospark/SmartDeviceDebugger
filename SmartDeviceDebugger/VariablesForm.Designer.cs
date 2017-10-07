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
			this.components = new System.ComponentModel.Container();
			this.variablesListView = new System.Windows.Forms.ListView();
			this.variableColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.hexidecimalColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.decimalColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.binaryColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.variablesListViewContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.getSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.getAllButton = new System.Windows.Forms.Button();
			this.getSelectedButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.variablesListViewContextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// variablesListView
			// 
			this.variablesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.variablesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.variableColumnHeader,
            this.nameColumnHeader,
            this.hexidecimalColumnHeader,
            this.decimalColumnHeader,
            this.binaryColumnHeader});
			this.variablesListView.ContextMenuStrip = this.variablesListViewContextMenuStrip;
			this.variablesListView.FullRowSelect = true;
			this.variablesListView.GridLines = true;
			this.variablesListView.HideSelection = false;
			this.variablesListView.Location = new System.Drawing.Point(0, 70);
			this.variablesListView.Name = "variablesListView";
			this.variablesListView.Size = new System.Drawing.Size(431, 335);
			this.variablesListView.TabIndex = 0;
			this.variablesListView.UseCompatibleStateImageBehavior = false;
			this.variablesListView.View = System.Windows.Forms.View.Details;
			// 
			// variableColumnHeader
			// 
			this.variableColumnHeader.Text = "Var";
			this.variableColumnHeader.Width = 40;
			// 
			// nameColumnHeader
			// 
			this.nameColumnHeader.Text = "Name";
			this.nameColumnHeader.Width = 180;
			// 
			// hexidecimalColumnHeader
			// 
			this.hexidecimalColumnHeader.Text = "Hex";
			this.hexidecimalColumnHeader.Width = 40;
			// 
			// decimalColumnHeader
			// 
			this.decimalColumnHeader.Text = "Dec";
			this.decimalColumnHeader.Width = 40;
			// 
			// binaryColumnHeader
			// 
			this.binaryColumnHeader.Text = "Bin";
			this.binaryColumnHeader.Width = 120;
			// 
			// variablesListViewContextMenuStrip
			// 
			this.variablesListViewContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getSelectedToolStripMenuItem,
            this.toolStripSeparator2,
            this.copyToolStripMenuItem,
            this.toolStripSeparator1,
            this.selectAllToolStripMenuItem});
			this.variablesListViewContextMenuStrip.Name = "variablesListViewContextMenuStrip";
			this.variablesListViewContextMenuStrip.Size = new System.Drawing.Size(165, 104);
			// 
			// getSelectedToolStripMenuItem
			// 
			this.getSelectedToolStripMenuItem.Name = "getSelectedToolStripMenuItem";
			this.getSelectedToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.getSelectedToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.getSelectedToolStripMenuItem.Text = "&Get Selected";
			this.getSelectedToolStripMenuItem.Click += new System.EventHandler(this.getSelectedButton_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(161, 6);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.copyToolStripMenuItem.Text = "&Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
			this.selectAllToolStripMenuItem.Text = "Select &All";
			this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
			// 
			// getAllButton
			// 
			this.getAllButton.Location = new System.Drawing.Point(118, 12);
			this.getAllButton.Name = "getAllButton";
			this.getAllButton.Size = new System.Drawing.Size(100, 23);
			this.getAllButton.TabIndex = 1;
			this.getAllButton.Text = "Get &All";
			this.getAllButton.UseVisualStyleBackColor = true;
			this.getAllButton.Click += new System.EventHandler(this.getAllButton_Click);
			// 
			// getSelectedButton
			// 
			this.getSelectedButton.Location = new System.Drawing.Point(12, 12);
			this.getSelectedButton.Name = "getSelectedButton";
			this.getSelectedButton.Size = new System.Drawing.Size(100, 23);
			this.getSelectedButton.TabIndex = 2;
			this.getSelectedButton.Text = "&Get Selected";
			this.getSelectedButton.UseVisualStyleBackColor = true;
			this.getSelectedButton.Click += new System.EventHandler(this.getSelectedButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.Location = new System.Drawing.Point(344, 41);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 3;
			this.cancelButton.Text = "&Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Visible = false;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// progressBar
			// 
			this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar.Location = new System.Drawing.Point(12, 41);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(326, 23);
			this.progressBar.TabIndex = 4;
			this.progressBar.Visible = false;
			// 
			// VariablesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(431, 405);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.getSelectedButton);
			this.Controls.Add(this.getAllButton);
			this.Controls.Add(this.variablesListView);
			this.Name = "VariablesForm";
			this.Text = "Variables";
			this.Load += new System.EventHandler(this.VariablesForm_Load);
			this.variablesListViewContextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView variablesListView;
		private System.Windows.Forms.ColumnHeader variableColumnHeader;
		private System.Windows.Forms.ColumnHeader nameColumnHeader;
		private System.Windows.Forms.ColumnHeader hexidecimalColumnHeader;
		private System.Windows.Forms.Button getAllButton;
		private System.Windows.Forms.ColumnHeader decimalColumnHeader;
		private System.Windows.Forms.ColumnHeader binaryColumnHeader;
		private System.Windows.Forms.Button getSelectedButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.ContextMenuStrip variablesListViewContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem getSelectedToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	}
}