namespace SmartDevice
{
	partial class MainForm
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
			this.startStopButton = new System.Windows.Forms.Button();
			this.detailsTextBox = new System.Windows.Forms.TextBox();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.blocksListView = new System.Windows.Forms.ListView();
			this.blockTypeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.blockDataColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.blockChecksumColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.blocksListViewContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.includeBlockTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.excludeBlockTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.autoScrollCheckBox = new System.Windows.Forms.CheckBox();
			this.includeLabel = new System.Windows.Forms.Label();
			this.includeTextBox = new System.Windows.Forms.TextBox();
			this.excludeTextBox = new System.Windows.Forms.TextBox();
			this.excludeLabel = new System.Windows.Forms.Label();
			this.clearButton = new System.Windows.Forms.Button();
			this.variablesButton = new System.Windows.Forms.Button();
			this.optionsButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.blocksListViewContextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// startStopButton
			// 
			this.startStopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.startStopButton.Location = new System.Drawing.Point(629, 12);
			this.startStopButton.Name = "startStopButton";
			this.startStopButton.Size = new System.Drawing.Size(75, 23);
			this.startStopButton.TabIndex = 4;
			this.startStopButton.Text = "&Start";
			this.startStopButton.UseVisualStyleBackColor = true;
			this.startStopButton.Click += new System.EventHandler(this.startStopButton_Click);
			// 
			// detailsTextBox
			// 
			this.detailsTextBox.BackColor = System.Drawing.SystemColors.Window;
			this.detailsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.detailsTextBox.Location = new System.Drawing.Point(0, 0);
			this.detailsTextBox.Multiline = true;
			this.detailsTextBox.Name = "detailsTextBox";
			this.detailsTextBox.ReadOnly = true;
			this.detailsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.detailsTextBox.Size = new System.Drawing.Size(716, 139);
			this.detailsTextBox.TabIndex = 2;
			// 
			// splitContainer
			// 
			this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer.Location = new System.Drawing.Point(0, 93);
			this.splitContainer.Name = "splitContainer";
			this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.blocksListView);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.detailsTextBox);
			this.splitContainer.Size = new System.Drawing.Size(716, 345);
			this.splitContainer.SplitterDistance = 202;
			this.splitContainer.TabIndex = 6;
			// 
			// blocksListView
			// 
			this.blocksListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.blockTypeColumnHeader,
            this.blockDataColumnHeader,
            this.blockChecksumColumnHeader});
			this.blocksListView.ContextMenuStrip = this.blocksListViewContextMenuStrip;
			this.blocksListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.blocksListView.FullRowSelect = true;
			this.blocksListView.HideSelection = false;
			this.blocksListView.Location = new System.Drawing.Point(0, 0);
			this.blocksListView.Name = "blocksListView";
			this.blocksListView.Size = new System.Drawing.Size(716, 202);
			this.blocksListView.TabIndex = 0;
			this.blocksListView.UseCompatibleStateImageBehavior = false;
			this.blocksListView.View = System.Windows.Forms.View.Details;
			this.blocksListView.SelectedIndexChanged += new System.EventHandler(this.blocksListView_SelectedIndexChanged);
			// 
			// blockTypeColumnHeader
			// 
			this.blockTypeColumnHeader.Text = "BType";
			this.blockTypeColumnHeader.Width = 180;
			// 
			// blockDataColumnHeader
			// 
			this.blockDataColumnHeader.Text = "BData";
			this.blockDataColumnHeader.Width = 280;
			// 
			// blockChecksumColumnHeader
			// 
			this.blockChecksumColumnHeader.Text = "BSum";
			// 
			// blocksListViewContextMenuStrip
			// 
			this.blocksListViewContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.toolStripSeparator1,
            this.selectAllToolStripMenuItem,
            this.toolStripSeparator2,
            this.includeBlockTypeToolStripMenuItem,
            this.excludeBlockTypeToolStripMenuItem});
			this.blocksListViewContextMenuStrip.Name = "blocksListViewContextMenuStrip";
			this.blocksListViewContextMenuStrip.Size = new System.Drawing.Size(175, 104);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.ShortcutKeyDisplayString = "";
			this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.copyToolStripMenuItem.Text = "&Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(171, 6);
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.selectAllToolStripMenuItem.Text = "Select &All";
			this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(171, 6);
			// 
			// includeBlockTypeToolStripMenuItem
			// 
			this.includeBlockTypeToolStripMenuItem.Name = "includeBlockTypeToolStripMenuItem";
			this.includeBlockTypeToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.includeBlockTypeToolStripMenuItem.Text = "&Include Block Type";
			this.includeBlockTypeToolStripMenuItem.Click += new System.EventHandler(this.includeBlockTypeToolStripMenuItem_Click);
			// 
			// excludeBlockTypeToolStripMenuItem
			// 
			this.excludeBlockTypeToolStripMenuItem.Name = "excludeBlockTypeToolStripMenuItem";
			this.excludeBlockTypeToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.excludeBlockTypeToolStripMenuItem.Text = "Exclude Block Type";
			this.excludeBlockTypeToolStripMenuItem.Click += new System.EventHandler(this.excludeBlockTypeToolStripMenuItem_Click);
			// 
			// autoScrollCheckBox
			// 
			this.autoScrollCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.autoScrollCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
			this.autoScrollCheckBox.Checked = true;
			this.autoScrollCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.autoScrollCheckBox.Location = new System.Drawing.Point(629, 444);
			this.autoScrollCheckBox.Name = "autoScrollCheckBox";
			this.autoScrollCheckBox.Size = new System.Drawing.Size(75, 23);
			this.autoScrollCheckBox.TabIndex = 10;
			this.autoScrollCheckBox.Text = "Auto Scroll";
			this.autoScrollCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.autoScrollCheckBox.UseVisualStyleBackColor = true;
			// 
			// includeLabel
			// 
			this.includeLabel.Location = new System.Drawing.Point(12, 44);
			this.includeLabel.Name = "includeLabel";
			this.includeLabel.Size = new System.Drawing.Size(85, 13);
			this.includeLabel.TabIndex = 6;
			this.includeLabel.Text = "Include:";
			this.includeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// includeTextBox
			// 
			this.includeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.includeTextBox.Location = new System.Drawing.Point(103, 41);
			this.includeTextBox.Name = "includeTextBox";
			this.includeTextBox.Size = new System.Drawing.Size(520, 20);
			this.includeTextBox.TabIndex = 7;
			this.includeTextBox.Leave += new System.EventHandler(this.filterTextBox_Leave);
			// 
			// excludeTextBox
			// 
			this.excludeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.excludeTextBox.Location = new System.Drawing.Point(103, 67);
			this.excludeTextBox.Name = "excludeTextBox";
			this.excludeTextBox.Size = new System.Drawing.Size(520, 20);
			this.excludeTextBox.TabIndex = 9;
			this.excludeTextBox.Leave += new System.EventHandler(this.filterTextBox_Leave);
			// 
			// excludeLabel
			// 
			this.excludeLabel.Location = new System.Drawing.Point(12, 70);
			this.excludeLabel.Name = "excludeLabel";
			this.excludeLabel.Size = new System.Drawing.Size(85, 13);
			this.excludeLabel.TabIndex = 8;
			this.excludeLabel.Text = "Exclude:";
			this.excludeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// clearButton
			// 
			this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.clearButton.Location = new System.Drawing.Point(548, 444);
			this.clearButton.Name = "clearButton";
			this.clearButton.Size = new System.Drawing.Size(75, 23);
			this.clearButton.TabIndex = 5;
			this.clearButton.Text = "&Clear";
			this.clearButton.UseVisualStyleBackColor = true;
			this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
			// 
			// variablesButton
			// 
			this.variablesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.variablesButton.Enabled = false;
			this.variablesButton.Location = new System.Drawing.Point(93, 12);
			this.variablesButton.Name = "variablesButton";
			this.variablesButton.Size = new System.Drawing.Size(75, 23);
			this.variablesButton.TabIndex = 11;
			this.variablesButton.Text = "&Variables";
			this.variablesButton.UseVisualStyleBackColor = true;
			this.variablesButton.Click += new System.EventHandler(this.variablesButton_Click);
			// 
			// optionsButton
			// 
			this.optionsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.optionsButton.Location = new System.Drawing.Point(12, 12);
			this.optionsButton.Name = "optionsButton";
			this.optionsButton.Size = new System.Drawing.Size(75, 23);
			this.optionsButton.TabIndex = 12;
			this.optionsButton.Text = "&Options";
			this.optionsButton.UseVisualStyleBackColor = true;
			this.optionsButton.Click += new System.EventHandler(this.optionsButton_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(716, 479);
			this.Controls.Add(this.optionsButton);
			this.Controls.Add(this.variablesButton);
			this.Controls.Add(this.clearButton);
			this.Controls.Add(this.excludeTextBox);
			this.Controls.Add(this.excludeLabel);
			this.Controls.Add(this.includeTextBox);
			this.Controls.Add(this.includeLabel);
			this.Controls.Add(this.autoScrollCheckBox);
			this.Controls.Add(this.splitContainer);
			this.Controls.Add(this.startStopButton);
			this.Name = "MainForm";
			this.Text = "Smart Device Debugger";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.mainForm_FormClosed);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.blocksListViewContextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button startStopButton;
		private System.Windows.Forms.TextBox detailsTextBox;
		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.ListView blocksListView;
		private System.Windows.Forms.ColumnHeader blockTypeColumnHeader;
		private System.Windows.Forms.ColumnHeader blockDataColumnHeader;
		private System.Windows.Forms.ColumnHeader blockChecksumColumnHeader;
		private System.Windows.Forms.CheckBox autoScrollCheckBox;
		private System.Windows.Forms.Label includeLabel;
		private System.Windows.Forms.TextBox includeTextBox;
		private System.Windows.Forms.TextBox excludeTextBox;
		private System.Windows.Forms.Label excludeLabel;
		private System.Windows.Forms.Button clearButton;
		private System.Windows.Forms.ContextMenuStrip blocksListViewContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem includeBlockTypeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem excludeBlockTypeToolStripMenuItem;
		private System.Windows.Forms.Button variablesButton;
		private System.Windows.Forms.Button optionsButton;
	}
}

