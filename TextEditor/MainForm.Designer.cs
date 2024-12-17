namespace TextEditor
{
    partial class MainForm
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
            this.flowLayoutPanel1 = new FlowLayoutPanel();
            this.Load = new Button();
            this.Save = new Button();
            this.ChangeTextColor = new Button();
            this.ChangeTagColor = new Button();
            this.flowLayoutPanel2 = new FlowLayoutPanel();
            this.Utf8 = new RadioButton();
            this.Utf16le = new RadioButton();
            this.Utf16be = new RadioButton();
            this.Utf32 = new RadioButton();
            this.RichTextBox = new RichTextBox();
            this.OpenFileDialog = new OpenFileDialog();
            this.ColorDialogForText = new ColorDialog();
            this.ColorDialogForTag = new ColorDialog();
            this.SaveFileDialog = new SaveFileDialog();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.Load);
            this.flowLayoutPanel1.Controls.Add(this.Save);
            this.flowLayoutPanel1.Controls.Add(this.ChangeTextColor);
            this.flowLayoutPanel1.Controls.Add(this.ChangeTagColor);
            this.flowLayoutPanel1.Location = new Point(10, 6);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new Padding(0, 9, 0, 9);
            this.flowLayoutPanel1.Size = new Size(786, 54);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // Load
            // 
            this.Load.Location = new Point(0, 12);
            this.Load.Margin = new Padding(0, 3, 60, 3);
            this.Load.Name = "Load";
            this.Load.Size = new Size(150, 30);
            this.Load.TabIndex = 0;
            this.Load.Text = "load";
            this.Load.UseVisualStyleBackColor = true;
            this.Load.Click += this.Load_OnClick;
            // 
            // Save
            // 
            this.Save.Location = new Point(210, 12);
            this.Save.Margin = new Padding(0, 3, 60, 3);
            this.Save.Name = "Save";
            this.Save.Size = new Size(150, 30);
            this.Save.TabIndex = 1;
            this.Save.Text = "save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += this.Save_OnClick;
            // 
            // ChangeTextColor
            // 
            this.ChangeTextColor.Location = new Point(420, 12);
            this.ChangeTextColor.Margin = new Padding(0, 3, 60, 3);
            this.ChangeTextColor.Name = "ChangeTextColor";
            this.ChangeTextColor.Size = new Size(150, 30);
            this.ChangeTextColor.TabIndex = 2;
            this.ChangeTextColor.Text = "文字色変更";
            this.ChangeTextColor.UseVisualStyleBackColor = true;
            this.ChangeTextColor.Click += this.ChangeTextColor_OnClick;
            // 
            // ChangeTagColor
            // 
            this.ChangeTagColor.Location = new Point(633, 12);
            this.ChangeTagColor.Name = "ChangeTagColor";
            this.ChangeTagColor.Size = new Size(150, 30);
            this.ChangeTagColor.TabIndex = 3;
            this.ChangeTagColor.Text = "タグ色変更(XML)";
            this.ChangeTagColor.UseVisualStyleBackColor = true;
            this.ChangeTagColor.Click += this.ChangeTagColor_OnClick;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BackgroundImageLayout = ImageLayout.Center;
            this.flowLayoutPanel2.Controls.Add(this.Utf8);
            this.flowLayoutPanel2.Controls.Add(this.Utf16le);
            this.flowLayoutPanel2.Controls.Add(this.Utf16be);
            this.flowLayoutPanel2.Controls.Add(this.Utf32);
            this.flowLayoutPanel2.Location = new Point(10, 79);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Padding = new Padding(0, 7, 0, 0);
            this.flowLayoutPanel2.Size = new Size(786, 54);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // Utf8
            // 
            this.Utf8.AutoSize = true;
            this.Utf8.Checked = true;
            this.Utf8.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            this.Utf8.Location = new Point(3, 10);
            this.Utf8.Name = "Utf8";
            this.Utf8.Padding = new Padding(0, 0, 131, 0);
            this.Utf8.Size = new Size(210, 29);
            this.Utf8.TabIndex = 0;
            this.Utf8.TabStop = true;
            this.Utf8.Text = "UTF-8";
            this.Utf8.UseVisualStyleBackColor = true;
            this.Utf8.CheckedChanged += this.Utf8_OnCheckedChanged;
            // 
            // Utf16le
            // 
            this.Utf16le.AutoSize = true;
            this.Utf16le.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            this.Utf16le.Location = new Point(219, 10);
            this.Utf16le.Name = "Utf16le";
            this.Utf16le.Padding = new Padding(0, 0, 80, 0);
            this.Utf16le.Size = new Size(200, 29);
            this.Utf16le.TabIndex = 1;
            this.Utf16le.Text = "UTF-16(LE)";
            this.Utf16le.UseVisualStyleBackColor = true;
            this.Utf16le.CheckedChanged += this.Utf16le_OnCheckedChanged;
            // 
            // Utf16be
            // 
            this.Utf16be.AutoSize = true;
            this.Utf16be.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            this.Utf16be.Location = new Point(425, 10);
            this.Utf16be.Name = "Utf16be";
            this.Utf16be.Padding = new Padding(0, 0, 88, 0);
            this.Utf16be.Size = new Size(210, 29);
            this.Utf16be.TabIndex = 2;
            this.Utf16be.Text = "UTF-16(BE)";
            this.Utf16be.UseVisualStyleBackColor = true;
            this.Utf16be.CheckedChanged += this.Utf16be_OnCheckedChanged;
            // 
            // Utf32
            // 
            this.Utf32.AutoSize = true;
            this.Utf32.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            this.Utf32.Location = new Point(641, 10);
            this.Utf32.Name = "Utf32";
            this.Utf32.Size = new Size(89, 29);
            this.Utf32.TabIndex = 3;
            this.Utf32.Text = "UTF-32";
            this.Utf32.UseVisualStyleBackColor = true;
            this.Utf32.CheckedChanged += this.Utf32_OnCheckedChanged;
            // 
            // RichTextBox
            // 
            this.RichTextBox.Dock = DockStyle.Bottom;
            this.RichTextBox.Font = new Font("Yu Gothic UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            this.RichTextBox.Location = new Point(0, 142);
            this.RichTextBox.Name = "RichTextBox";
            this.RichTextBox.Size = new Size(784, 580);
            this.RichTextBox.TabIndex = 2;
            this.RichTextBox.Text = "";
            this.RichTextBox.TextChanged += this.TextChanging;
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.CheckFileExists = false;
            this.OpenFileDialog.FileName = "openFileDialog";
            this.OpenFileDialog.Filter = "テキストドキュメント(*.txt)|*.txt|XMLファイル(*.xml)|*.xml|全てのファイル(*.*)|*.*";
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.Filter = "テキストドキュメント(*.txt)|*.txt|XMLファイル(*.xml)|*.xml|全てのファイル(*.*)|*.*";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(784, 722);
            this.Controls.Add(this.RichTextBox);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Button Load;
        private Button Save;
        private Button ChangeTextColor;
        private Button ChangeTagColor;
        private FlowLayoutPanel flowLayoutPanel2;
        private RadioButton Utf8;
        private RadioButton Utf16le;
        private RadioButton Utf16be;
        private RadioButton Utf32;
        private RichTextBox RichTextBox;
        private OpenFileDialog OpenFileDialog;
        private ColorDialog ColorDialogForText;
        private ColorDialog ColorDialogForTag;
        private SaveFileDialog SaveFileDialog;
    }
}
