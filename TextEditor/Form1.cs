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

        private string _loadedFilePath = "";
        private string _savingFilePath = "";
        private int _encodeNum = 0;
        private Encoding _encodeLoad = Encodes.GetEncode(0);
        private Encoding _encodeSave = Encodes.GetEncode(0);
        private Color _tagColor = Color.Blue;

        //xml�t�@�C���̃^�O�ɐF��t���邽�߂�xml�t�@�C���ł��邩�𔻒肷��K�v������
        //���茋�ʂ� bool�Ƃ����ق����A�������ɂ��̂܂ܑg�ݍ��߂�̂�
        //�����̃t�@�C���p�X��.xml�ŏI��邩�̔����IsMatch���\�b�h�Ŏ�������
        private bool IsXML(string path) => Regex.IsMatch(path, ".xml$");

        //�^�O�̗v�f�݂̂�ʂ̐F�֕ύX����K�v������
        //�^�O�ɍ��v����\���������o���āA�v�f������F�t������K�v�����邽��
        //�^�O�̏����ɍ��v����\����z��Ɋi�[��
        //�v�f�����������^�O�F�Ŏw�肵���F�֕ύX�������B
        private void ColoringTag()
        {
            var tags = Regex.Matches(richTextBox.Text, @"<([^<>]+)>");
            foreach (Match tag in tags)
            {
                var index = tag.Groups[1].Index;
                var tagLength = tag.Groups[1].Length;
                richTextBox.Select(index, tagLength);
                richTextBox.SelectionColor = _tagColor;
            }
        }

        //�t�@�C����I�������G���R�[�h�Ŏ�荞�݁AXML�t�@�C���ł���ΐF��t������
        //���ׂĎ�荞��ReadAllLines���\�b�h�ł͓��삪�d���Ȃ肤�邽��
        //�e�L�X�g�̓ǂݍ��݂ƃ^�O�̐F�t���͈�A�̓���Ƃ���������
        //��s���\���̈�ɒǉ�������A�t�@�C����荞�݈ȍ~�̑����try�߂ň͂�
        private void load_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _loadedFilePath = openFileDialog.FileName;
            }
            _encodeLoad = Encodes.GetEncode(_encodeNum);
            try
            {
                var lines = File.ReadLines(_loadedFilePath, _encodeLoad);
                foreach (var line in lines)
                {
                    richTextBox.Text += line + Environment.NewLine;
                }
                if (IsXML(_loadedFilePath))
                {
                    ColoringTag();
                    richTextBox.SelectionColor = _tagColor;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage.ShowErrorMessage(ex);
            }
        }

        //�t�@�C�����w�肵���G���R�[�h�ŕۑ��A�t�@�C����V���ɍ��o�����Ƃ��s������
        //�t�@�C���̃G���R�[�h�͕ۑ���A�t�@�C���̗L���̓e�L�X�g�{�b�N�X����f�[�^���t�@�C���ɏ����o�����ɍs���K�v�����邽��
        //Encodes�N���X�̃��\�b�h�ŃG���R�[�h��ϊ����A�t�@�C���ւ̃f�[�^�����o���̂�try�߂ň͂�
        private void save_Click(object sender, EventArgs e)
        {
            _encodeSave = Encodes.GetEncode(_encodeNum);
            richTextBox.Text = Encodes.ChangeEncode(_encodeLoad, _encodeSave, richTextBox.Text);
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                _savingFilePath = saveFileDialog.FileName;
            }
            if (!File.Exists(_savingFilePath) && _savingFilePath != "")
            {
                FileStream fileStream = File.Open(_savingFilePath, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Close();
            }
            try
            {
                File.WriteAllText(_savingFilePath, richTextBox.Text, _encodeSave);
            }
            catch (Exception ex)
            {
                ErrorMessage.ShowErrorMessage(ex);
            }
        }

        //���W�I�{�^���̑I���œǂݍ���/�ۑ��̃G���R�[�h��ς�����悤�ɂ�����
        //���W�I�{�^���̑I�����G���R�[�h��Ԃ����\�b�h�ɓn���K�v�����邽��
        //�e�{�^���̃G���R�[�h�ɑΉ�����int�^�̒l��Ԃ��悤�ɂ���
        private void utf8_CheckedChanged(object sender, EventArgs e) => this._encodeNum = 0;

        private void utf16le_CheckedChanged(object sender, EventArgs e) => this._encodeNum = 1;

        private void utf16be_CheckedChanged(object sender, EventArgs e) => this._encodeNum = 2;

        private void utf32_CheckedChanged(object sender, EventArgs e) => this._encodeNum = 3;

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
            if (IsXML(_loadedFilePath) && (colorDialogTag.ShowDialog() == DialogResult.OK))
            {
                _tagColor = colorDialogTag.Color;
                ColoringTag();
            }
        }

        //�e�L�X�g���͎��Ƀ^�O�̌`���ɂȂ��Ă���Ύ����ŐF�����Ăق���
        //�^�O�v�f�̐F�Ńe�L�X�g�����͂���Ă͂Ȃ�Ȃ��A�����͏ꏊ���ω����Ă͂Ȃ�Ȃ�����
        //�ʏ�̃^�O�v�f�F�ύX���\�b�h�ɉ����ē��͈ʒu�����ɖ߂��A�^�O�̑I������������L�q��ǉ�����
        private void textChanged(object sender, EventArgs e)
        {
            if (IsXML(_loadedFilePath))
            {
                int currentPosition = richTextBox.SelectionStart;
                ColoringTag();
                richTextBox.SelectionStart = currentPosition;
                richTextBox.SelectionLength = 0;
            }
        }

    }
}
