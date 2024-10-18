using System.Text;
using System.Text.RegularExpressions;

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
        static Color tagColor = Color.Blue;
       
        bool isXML(string path) => Regex.IsMatch(path, ".xml$");

        void ColoringTag()
        {
            var tags = Regex.Matches(richTextBox.Text, @"<([^<>]+)>");
            foreach (Match tag in tags)
            {
                var index = tag.Groups[1].Index;
                var tagLength = tag.Groups[1].Length;
                richTextBox.Select(index, tagLength);
                richTextBox.SelectionColor = tagColor;
            }
        }

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
            try
            {
                var lines = File.ReadLines(loadedfilepath, encodeLoad);
                foreach (var line in lines)
                {
                    richTextBox.Text += line + Environment.NewLine;
                    defaultText += line + Environment.NewLine;
                }
                if (isXML(loadedfilepath)) 
                {
                    ColoringTag();
                    richTextBox.SelectionColor = tagColor;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage.ShowErrorMessage(ex);
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            encodeSave = Encodes.GetEncode(encodeNum);
            richTextBox.Text = Encodes.ChangeEncode(encodeLoad, encodeSave, richTextBox.Text);
            savingfilepath = GetFilepath();
            if (!File.Exists(savingfilepath) && savingfilepath != "")
            {
                using (File.Create(savingfilepath)) ;
            }
            try 
            {
                File.WriteAllText(savingfilepath, richTextBox.Text, encodeSave);
                if (loadedfilepath != "")
                {
                    File.WriteAllText(loadedfilepath, defaultText, encodeLoad);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage.ShowErrorMessage(ex);
            }
        }

        private void utf8_CheckedChanged(object sender, EventArgs e) => encodeNum = 0;

        private void utf16le_CheckedChanged(object sender, EventArgs e) => encodeNum = 1;

        private void utf16be_CheckedChanged(object sender, EventArgs e) => encodeNum = 2;

        private void utf32_CheckedChanged(object sender, EventArgs e) => encodeNum = 3;

        private void changeTextColor_Click(object sender, EventArgs e)
        {
            if (colorDialogText.ShowDialog() == DialogResult.OK)
            {
                richTextBox.ForeColor = colorDialogText.Color;
                ColoringTag();
            }
        }

        private void changeTagColor_Click(object sender, EventArgs e)
        {
            if (isXML(loadedfilepath) && (colorDialogTag.ShowDialog() == DialogResult.OK)) 
            {
                tagColor = colorDialogTag.Color;
                ColoringTag();
            }
        }

        private void textChanged(object sender, EventArgs e)
        {
            if (isXML(loadedfilepath))
            {
                int currentPosition = richTextBox.SelectionStart;
                string alltext = richTextBox.Text;
                var tags = Regex.Matches(richTextBox.Text, @"<([^<>]+)>");
                foreach (Match tag in tags)
                {
                    var index = tag.Groups[1].Index;
                    var tagLength = tag.Groups[1].Length;
                    richTextBox.Select(index, tagLength);
                    richTextBox.SelectionColor = tagColor;
                }
                richTextBox.SelectionStart = currentPosition;
                richTextBox.SelectionLength = 0;
            }
        }
    }
}
