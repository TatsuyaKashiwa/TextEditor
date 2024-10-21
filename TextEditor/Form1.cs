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
        static int encodeNum = 0;
        static Encoding encodeLoad = Encodes.GetEncode(0);
        static Encoding encodeSave = Encodes.GetEncode(0);
        static Color tagColor = Color.Blue;

        //xml�t�@�C���̃^�O�ɐF��t���邽�߂�xml�t�@�C���ł��邩�𔻒肷��K�v������
        //���茋�ʂ� bool�Ƃ����ق����A�������ɂ��̂܂ܑg�ݍ��߂�̂�
        //�����̃t�@�C���p�X��.xml�ŏI��邩�̔����IsMatch���\�b�h�Ŏ�������
        bool isXML(string path) => Regex.IsMatch(path, ".xml$");

        //�^�O�̗v�f�݂̂�ʂ̐F�֕ύX����K�v������
        //�^�O�ɍ��v����\���������o���āA�v�f������F�t������K�v�����邽��
        //�^�O�̏����ɍ��v����\����z��Ɋi�[��
        //�v�f�����������^�O�F�Ŏw�肵���F�֕ύX�������B
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

        //���́E�o�͋��Ƀt�@�C���p�X�𓾂邱�Ƃ͋��ʂł���̂Ń��\�b�h�Ƃ��ċ��ʉ����ׂ��ł���
        //���o�͐�̃p�X�͕ʂɂȂ蓾�邽�߁A����ꂽ�t�@�C���p�X��Ԃ�l(string�^)�Ƃ��ĕԂ��K�v������
        //�p�X�𓾂邽�߂�if�������łȂ��Aelse���Ƃ��ăt�@�C���I������Ȃ������ۂ̕Ԃ�l��ݒ肵���`�Ƃ���
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
            }
            catch (Exception ex)
            {
                ErrorMessage.ShowErrorMessage(ex);
            }
        }

        //���W�I�{�^���̑I���œǂݍ���/�ۑ��̃G���R�[�h��ς�����悤�ɂ�����
        //���W�I�{�^���̑I�����G���R�[�h��Ԃ����\�b�h�ɓn���K�v�����邽��
        //�e�{�^���̃G���R�[�h�ɑΉ�����int�^�̒l��Ԃ��悤�ɂ���
        private void utf8_CheckedChanged(object sender, EventArgs e) => encodeNum = 0;

        private void utf16le_CheckedChanged(object sender, EventArgs e) => encodeNum = 1;

        private void utf16be_CheckedChanged(object sender, EventArgs e) => encodeNum = 2;

        private void utf32_CheckedChanged(object sender, EventArgs e) => encodeNum = 3;

        //�^�O�v�f�̐F��ύX�����Ƀe�L�X�g�̐F��ύX������
        //�Ȃ��Ȃ�A�^�O�v�f�̐F�t�����ꎞ�I�Ȃ��̂ƂȂ��Ă��܂�����
        //�e�L�X�g�F�ύX��Ƀ^�O�̐F��t���Ȃ�����
        private void changeTextColor_Click(object sender, EventArgs e)
        {
            if (colorDialogText.ShowDialog() == DialogResult.OK)
            {
                richTextBox.ForeColor = colorDialogText.Color;
                ColoringTag();
            }
        }

        //�^�O�v�f�̐F��ύX������
        //�����v���p�e�B��ύX���邾���ł͐F�͕ς��Ȃ�����
        //�v�f�̐F�����肵����^�O�F��ύX�����郁�\�b�h����p������
        private void changeTagColor_Click(object sender, EventArgs e)
        {
            if (isXML(loadedfilepath) && (colorDialogTag.ShowDialog() == DialogResult.OK)) 
            {
                tagColor = colorDialogTag.Color;
                ColoringTag();
            }
        }

        //�e�L�X�g���͎��Ƀ^�O�̌`���ɂȂ��Ă���Ύ����ŐF�����Ăق���
        //�^�O�v�f�̐F�Ńe�L�X�g�����͂���Ă͂Ȃ�Ȃ��A�����͏ꏊ���ω����Ă͂Ȃ�Ȃ�����
        //�ʏ�̃^�O�v�f�F�ύX���\�b�h�ɉ����ē��͈ʒu�����ɖ߂��A�^�O�̑I������������L�q��ǉ�����
        private void textChanged(object sender, EventArgs e)
        {
            if (isXML(loadedfilepath))
            {
                int currentPosition = richTextBox.SelectionStart;
                ColoringTag();
                richTextBox.SelectionStart = currentPosition;
                richTextBox.SelectionLength = 0;
            }
        }
    }
}
