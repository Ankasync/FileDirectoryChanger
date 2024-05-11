namespace FileDirectoryChanger
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            notifyIcon1 = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            gösterToolStripMenuItem = new ToolStripMenuItem();
            gizleToolStripMenuItem = new ToolStripMenuItem();
            çıkışToolStripMenuItem = new ToolStripMenuItem();
            tableLayoutPanel1 = new TableLayoutPanel();
            label3 = new Label();
            SourceFolder = new TextBox();
            label2 = new Label();
            DestinationFolder = new TextBox();
            comboBox1 = new ComboBox();
            label1 = new Label();
            button1 = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            contextMenuStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // notifyIcon1
            // 
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { gösterToolStripMenuItem, gizleToolStripMenuItem, çıkışToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(109, 70);
            // 
            // gösterToolStripMenuItem
            // 
            gösterToolStripMenuItem.Name = "gösterToolStripMenuItem";
            gösterToolStripMenuItem.Size = new Size(108, 22);
            gösterToolStripMenuItem.Text = "Göster";
            gösterToolStripMenuItem.Click += gösterToolStripMenuItem_Click;
            // 
            // gizleToolStripMenuItem
            // 
            gizleToolStripMenuItem.Name = "gizleToolStripMenuItem";
            gizleToolStripMenuItem.Size = new Size(108, 22);
            gizleToolStripMenuItem.Text = "Gizle";
            gizleToolStripMenuItem.Click += gizleToolStripMenuItem_Click;
            // 
            // çıkışToolStripMenuItem
            // 
            çıkışToolStripMenuItem.Name = "çıkışToolStripMenuItem";
            çıkışToolStripMenuItem.Size = new Size(108, 22);
            çıkışToolStripMenuItem.Text = "Çıkış";
            çıkışToolStripMenuItem.Click += çıkışToolStripMenuItem_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(SourceFolder, 1, 2);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(DestinationFolder, 1, 1);
            tableLayoutPanel1.Controls.Add(comboBox1, 1, 0);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Size = new Size(511, 100);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Location = new Point(3, 66);
            label3.Name = "label3";
            label3.Size = new Size(249, 34);
            label3.TabIndex = 5;
            label3.Text = "Source Directory";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // SourceFolder
            // 
            SourceFolder.Dock = DockStyle.Fill;
            SourceFolder.Location = new Point(258, 69);
            SourceFolder.Name = "SourceFolder";
            SourceFolder.Size = new Size(250, 29);
            SourceFolder.TabIndex = 3;
            SourceFolder.DoubleClick += textBox1_DoubleClick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(3, 33);
            label2.Name = "label2";
            label2.Size = new Size(249, 33);
            label2.TabIndex = 4;
            label2.Text = "DestinationFolder";
            label2.TextAlign = ContentAlignment.TopRight;
            label2.Click += label2_Click;
            // 
            // DestinationFolder
            // 
            DestinationFolder.Dock = DockStyle.Fill;
            DestinationFolder.Location = new Point(258, 36);
            DestinationFolder.Name = "DestinationFolder";
            DestinationFolder.Size = new Size(250, 29);
            DestinationFolder.TabIndex = 1;
            DestinationFolder.DoubleClick += textBox2_DoubleClick;
            // 
            // comboBox1
            // 
            comboBox1.Dock = DockStyle.Fill;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "F12", "F11", "F10", "F9", "F8", "F8", "F7", "F6", "F5", "F4", "F3", "F2", "F1", "" });
            comboBox1.Location = new Point(258, 3);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(250, 29);
            comboBox1.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(249, 33);
            label1.TabIndex = 2;
            label1.Text = "Key";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // button1
            // 
            button1.Location = new Point(409, 106);
            button1.Name = "button1";
            button1.Size = new Size(99, 46);
            button1.TabIndex = 2;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(511, 229);
            Controls.Add(button1);
            Controls.Add(tableLayoutPanel1);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            Shown += Form1_Shown;
            contextMenuStrip1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem gösterToolStripMenuItem;
        private ToolStripMenuItem gizleToolStripMenuItem;
        private ToolStripMenuItem çıkışToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox DestinationFolder;
        private Label label1;
        private ComboBox comboBox1;
        private Button button1;
        private System.Windows.Forms.Timer timer1;
        private Label label2;
        private TextBox SourceFolder;
        private Label label3;
    }
}
