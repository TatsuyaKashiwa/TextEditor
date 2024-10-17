namespace TextEditor
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
            flowLayoutPanel1 = new FlowLayoutPanel();
            load = new Button();
            save = new Button();
            changeTextColor = new Button();
            changeTagColor = new Button();
            flowLayoutPanel2 = new FlowLayoutPanel();
            utf8 = new RadioButton();
            utf16le = new RadioButton();
            utf16be = new RadioButton();
            utf32 = new RadioButton();
            richTextBox = new RichTextBox();
            openFileDialog = new OpenFileDialog();
            colorDialogText = new ColorDialog();
            colorDialogTag = new ColorDialog();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(load);
            flowLayoutPanel1.Controls.Add(save);
            flowLayoutPanel1.Controls.Add(changeTextColor);
            flowLayoutPanel1.Controls.Add(changeTagColor);
            flowLayoutPanel1.Location = new Point(10, 6);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(0, 9, 0, 9);
            flowLayoutPanel1.Size = new Size(786, 54);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // load
            // 
            load.Location = new Point(0, 12);
            load.Margin = new Padding(0, 3, 60, 3);
            load.Name = "load";
            load.Size = new Size(150, 30);
            load.TabIndex = 0;
            load.Text = "load";
            load.UseVisualStyleBackColor = true;
            load.Click += load_Click;
            // 
            // save
            // 
            save.Location = new Point(210, 12);
            save.Margin = new Padding(0, 3, 60, 3);
            save.Name = "save";
            save.Size = new Size(150, 30);
            save.TabIndex = 1;
            save.Text = "save";
            save.UseVisualStyleBackColor = true;
            save.Click += save_Click;
            // 
            // changeTextColor
            // 
            changeTextColor.Location = new Point(420, 12);
            changeTextColor.Margin = new Padding(0, 3, 60, 3);
            changeTextColor.Name = "changeTextColor";
            changeTextColor.Size = new Size(150, 30);
            changeTextColor.TabIndex = 2;
            changeTextColor.Text = "文字色変更";
            changeTextColor.UseVisualStyleBackColor = true;
            changeTextColor.Click += changeTextColor_Click;
            // 
            // changeTagColor
            // 
            changeTagColor.Location = new Point(633, 12);
            changeTagColor.Name = "changeTagColor";
            changeTagColor.Size = new Size(150, 30);
            changeTagColor.TabIndex = 3;
            changeTagColor.Text = "タグ色変更(XML)";
            changeTagColor.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.BackgroundImageLayout = ImageLayout.Center;
            flowLayoutPanel2.Controls.Add(utf8);
            flowLayoutPanel2.Controls.Add(utf16le);
            flowLayoutPanel2.Controls.Add(utf16be);
            flowLayoutPanel2.Controls.Add(utf32);
            flowLayoutPanel2.Location = new Point(10, 79);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Padding = new Padding(0, 7, 0, 0);
            flowLayoutPanel2.Size = new Size(786, 54);
            flowLayoutPanel2.TabIndex = 1;
            // 
            // utf8
            // 
            utf8.AutoSize = true;
            utf8.Checked = true;
            utf8.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            utf8.Location = new Point(3, 10);
            utf8.Name = "utf8";
            utf8.Padding = new Padding(0, 0, 131, 0);
            utf8.Size = new Size(210, 29);
            utf8.TabIndex = 0;
            utf8.TabStop = true;
            utf8.Text = "UTF-8";
            utf8.UseVisualStyleBackColor = true;
            utf8.CheckedChanged += utf8_CheckedChanged;
            // 
            // utf16le
            // 
            utf16le.AutoSize = true;
            utf16le.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            utf16le.Location = new Point(219, 10);
            utf16le.Name = "utf16le";
            utf16le.Padding = new Padding(0, 0, 80, 0);
            utf16le.Size = new Size(200, 29);
            utf16le.TabIndex = 1;
            utf16le.Text = "UTF-16(LE)";
            utf16le.UseVisualStyleBackColor = true;
            utf16le.CheckedChanged += utf16le_CheckedChanged;
            // 
            // utf16be
            // 
            utf16be.AutoSize = true;
            utf16be.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            utf16be.Location = new Point(425, 10);
            utf16be.Name = "utf16be";
            utf16be.Padding = new Padding(0, 0, 88, 0);
            utf16be.Size = new Size(210, 29);
            utf16be.TabIndex = 2;
            utf16be.Text = "UTF-16(BE)";
            utf16be.UseVisualStyleBackColor = true;
            utf16be.CheckedChanged += utf16be_CheckedChanged;
            // 
            // utf32
            // 
            utf32.AutoSize = true;
            utf32.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            utf32.Location = new Point(641, 10);
            utf32.Name = "utf32";
            utf32.Size = new Size(89, 29);
            utf32.TabIndex = 3;
            utf32.Text = "UTF-32";
            utf32.UseVisualStyleBackColor = true;
            utf32.CheckedChanged += utf32_CheckedChanged;
            // 
            // richTextBox
            // 
            richTextBox.Dock = DockStyle.Bottom;
            richTextBox.Location = new Point(0, 139);
            richTextBox.Name = "richTextBox";
            richTextBox.Size = new Size(800, 622);
            richTextBox.TabIndex = 2;
            richTextBox.Text = "";
            // 
            // openFileDialog
            // 
            openFileDialog.CheckFileExists = false;
            openFileDialog.FileName = "openFileDialog";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 761);
            Controls.Add(richTextBox);
            Controls.Add(flowLayoutPanel2);
            Controls.Add(flowLayoutPanel1);
            Name = "Form1";
            Text = "Form1";
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Button load;
        private Button save;
        private Button changeTextColor;
        private Button changeTagColor;
        private FlowLayoutPanel flowLayoutPanel2;
        private RadioButton utf8;
        private RadioButton utf16le;
        private RadioButton utf16be;
        private RadioButton utf32;
        private RichTextBox richTextBox;
        private OpenFileDialog openFileDialog;
        private ColorDialog colorDialogText;
        private ColorDialog colorDialogTag;
    }
}
