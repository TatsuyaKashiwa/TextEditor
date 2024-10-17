using System.Text;

namespace TextEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static string loadedfilepath = "";
        static string savingfilepath = "";
        static string defaultText = "";
        static int encodeNum = 0;
        static Encoding encodeLoad = Encodes.GetEncode(0);
        static Encoding encodeSave = Encodes.GetEncode(0);

        string GetFilepath()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            else
            {
                return "";
            }
        }

        private void load_Click(object sender, EventArgs e)
        {
            loadedfilepath = GetFilepath();
            encodeLoad = Encodes.GetEncode(encodeNum);
            var lines = File.ReadLines(loadedfilepath, encodeLoad);
            foreach (var line in lines)
            {
                richTextBox.Text += line + Environment.NewLine;
                defaultText += line + Environment.NewLine;
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            richTextBox.Text = Encodes.ChangeEncode(encodeLoad,encodeSave, richTextBox.Text);
            savingfilepath = GetFilepath();
            if (!File.Exists(savingfilepath))
            {
                using (File.Create(savingfilepath)) ;
            }
            File.WriteAllText(loadedfilepath, richTextBox.Text, Encodes.GetEncode(encodeNum));
            File.Copy(loadedfilepath, savingfilepath, overwrite: true);
            File.WriteAllText(loadedfilepath, defaultText, Encodes.GetEncode(encodeNum));
        }

        private void utf8_CheckedChanged(object sender, EventArgs e) => encodeNum = 0;

        private void utf16le_CheckedChanged(object sender, EventArgs e) => encodeNum = 1;

        private void utf16be_CheckedChanged(object sender, EventArgs e) => encodeNum = 2;

        private void utf32_CheckedChanged(object sender, EventArgs e) => encodeNum = 3;
    }
}
