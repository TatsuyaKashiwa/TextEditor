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
        static string defaultText="";

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
            var lines = File.ReadLines(loadedfilepath);
            foreach (var line in lines)
            {
                richTextBox.Text += line + Environment.NewLine;
                defaultText += line + Environment.NewLine;
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            savingfilepath = GetFilepath();
            if (!File.Exists(savingfilepath)) 
            {
                using (File.Create(savingfilepath)) ;
            }
            File.WriteAllText(loadedfilepath, richTextBox.Text);
            File.Copy(loadedfilepath, savingfilepath, overwrite:true);
            File.WriteAllText(loadedfilepath, defaultText);
        }
    }
}
