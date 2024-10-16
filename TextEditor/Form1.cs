namespace TextEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static string filepath = "";

        void getFilepath()
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filepath = openFileDialog.FileName;
            }
        }

        private void load_Click(object sender, EventArgs e)
        {
            getFilepath();
            var lines = File.ReadLines(filepath);
            foreach (var line in lines)
            {
                richTextBox.Text += line + Environment.NewLine;
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            getFilepath();
            File.WriteAllText(filepath, richTextBox.Text);
        }
    }
}
