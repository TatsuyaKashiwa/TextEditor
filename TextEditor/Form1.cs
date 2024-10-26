using System.Text;
using System.Text.RegularExpressions;

namespace TextEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.InitializeComponent();
        }

        private string _loadedFilePath = "";
        private string _savingFilePath = "";
        private int _encodeNum = 0;
        private Encoding _encodeLoad = Encodes.GetEncode(0);
        private Encoding _encodeSave = Encodes.GetEncode(0);
        private Color _tagColor = Color.Blue;

        /// <summary>
        /// XML�t�@�C���ł��邩�𔻒肷�郁�\�b�h
        /// </summary>
        /// <param name="path">����Ώۂ̃t�@�C���p�X</param>
        /// <returns>XML�t�@�C���ł����true��Ԃ�</returns>
        /// <remarks>
        ///xml�t�@�C���̃^�O�ɐF��t���邽�߂�xml�t�@�C���ł��邩�𔻒肷��K�v������
        ///���茋�ʂ� bool�Ƃ����ق����A�������ɂ��̂܂ܑg�ݍ��߂�̂�
        ///�����̃t�@�C���p�X��.xml�ŏI��邩�̔����IsMatch���\�b�h�Ŏ�������
        /// </remarks>
        private bool IsXML(string path) => Regex.IsMatch(path, ".xml$");

        /// <summary>
        /// (XML)�^�O�v�f�̐F��ύX
        /// </summary>
        ///<remarks>
        ///�^�O�̗v�f�݂̂�ʂ̐F�֕ύX����K�v������
        ///�^�O�ɍ��v����\���������o���āA�v�f������F�t������K�v�����邽��
        ///�^�O�̏����ɍ��v����\����z��Ɋi�[��
        ///�v�f�����������^�O�F�Ŏw�肵���F�֕ύX�������B
        ///</remarks>
        private void ColoringTag()
        {
            var tags = Regex.Matches(this.richTextBox.Text, @"<([^<>]+)>");
            foreach (Match tag in tags)
            {
                var index = tag.Groups[1].Index;
                var tagLength = tag.Groups[1].Length;
                this.richTextBox.Select(index, tagLength);
                this.richTextBox.SelectionColor = this._tagColor;
            }
        }

        /// <summary>
        /// �t�@�C���ǂݍ���(load�{�^���������̓���)�ɑΉ����郁�\�b�h
        /// </summary>
        /// <param name="ex">catch������O</param>
        /// <remarks>
        /// �t�@�C����I�������G���R�[�h�Ŏ�荞�݁AXML�t�@�C���ł���ΐF��t������
        ///���ׂĎ�荞��ReadAllLines���\�b�h�ł͓��삪�d���Ȃ肤�邽��
        ///�e�L�X�g�̓ǂݍ��݂ƃ^�O�̐F�t���͈�A�̓���Ƃ���������
        ///��s���\���̈�ɒǉ�������A�t�@�C����荞�݈ȍ~�̑����try�߂ň͂�
        ///</remarks>
        ///<exception cref="System.ArgumentException">�t�@�C�����I���̏ꍇ</exception>
        ///<exception cref="System.IO.FileNotFoundException">���݂��Ȃ��t�@�C��������͂����ꍇ</exception>
        private void load_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this._loadedFilePath = this.openFileDialog.FileName;
            }
            this._encodeLoad = Encodes.GetEncode(this._encodeNum);
            try
            {
                var lines = File.ReadLines(this._loadedFilePath, this._encodeLoad);
                foreach (var line in lines)
                {
                    this.richTextBox.Text += line + Environment.NewLine;
                }
                if (this.IsXML(this._loadedFilePath))
                {
                    this.ColoringTag();
                    this.richTextBox.SelectionColor = this._tagColor;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// �t�@�C���ۑ�(save�{�^������)�ɑΉ����郁�\�b�h
        /// </summary>
        /// <param name="ex">catch������O</param>
        /// <remarks>
        /// �t�@�C�����w�肵���G���R�[�h�ŕۑ��A�t�@�C����V���ɍ��o�����Ƃ��s������
        ///�t�@�C���̃G���R�[�h�͕ۑ���A�t�@�C���̗L���̓e�L�X�g�{�b�N�X����f�[�^���t�@�C���ɏ����o�����ɍs���K�v�����邽��
        ///Encodes�N���X�̃��\�b�h�ŃG���R�[�h��ϊ����A�t�@�C���ւ̃f�[�^�����o���̂�try�߂ň͂�
        ///</remarks>
        ///<exception cref="System.ArgumentException">�t�@�C�����I���̏ꍇ</exception>
        private void save_Click(object sender, EventArgs e)
        {
            this._encodeSave = Encodes.GetEncode(this._encodeNum);
            this.richTextBox.Text = Encodes.ChangeEncode(this._encodeLoad, this._encodeSave, this.richTextBox.Text);
            if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                this._savingFilePath = this.saveFileDialog.FileName;
            }
            if (!File.Exists(this._savingFilePath) && this._savingFilePath != "")
            {
                FileStream fileStream = File.Open(this._savingFilePath, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Close();
            }
            try
            {
                File.WriteAllText(this._savingFilePath, this.richTextBox.Text, this._encodeSave);
            }
            catch (Exception ex)
            {
                ErrorMessage.ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// ���W�I�{�^���ɑΉ�����G���R�[�h�i�ɑΉ�����C���f�b�N�X�j��I�����郁�\�b�h
        /// </summary>
        /// <remarks>
        /// ���W�I�{�^���̑I���œǂݍ���/�ۑ��̃G���R�[�h��ς�����悤�ɂ�����
        ///���W�I�{�^���̑I�����G���R�[�h��Ԃ����\�b�h�ɓn���K�v�����邽��
        ///�e�{�^���̃G���R�[�h�ɑΉ�����int�^�̒l��Ԃ��悤�ɂ���
        ///</remarks>
        private void utf8_CheckedChanged(object sender, EventArgs e) => this._encodeNum = 0;

        private void utf16le_CheckedChanged(object sender, EventArgs e) => this._encodeNum = 1;

        private void utf16be_CheckedChanged(object sender, EventArgs e) => this._encodeNum = 2;

        private void utf32_CheckedChanged(object sender, EventArgs e) => this._encodeNum = 3;

        /// <summary>
        /// �e�L�X�g�F�ύX�̃��\�b�h
        /// </summary>
        /// <remarks>
        /// �^�O�v�f�̐F��ύX�����Ƀe�L�X�g�̐F��ύX������
        ///�Ȃ��Ȃ�A�^�O�v�f�̐F�t�����ꎞ�I�Ȃ��̂ƂȂ��Ă��܂�����
        ///�e�L�X�g�F�ύX��Ƀ^�O�̐F��t���Ȃ�����
        ///</remarks>
        private void changeTextColor_Click(object sender, EventArgs e)
        {
            if (this.colorDialogText.ShowDialog() == DialogResult.OK)
            {
                this.richTextBox.ForeColor = this.colorDialogText.Color;
                this.ColoringTag();
            }
        }

        /// <summary>
        /// �^�O�v�f�̐F��ύX���郁�\�b�h
        /// </summary>
        /// <remarks>
        ///  �^�O�v�f�̐F��ύX������
        ///�����v���p�e�B��ύX���邾���ł͐F�͕ς��Ȃ�����
        ///�v�f�̐F�����肵����^�O�F��ύX�����郁�\�b�h����p������
        ///</remarks>
        private void changeTagColor_Click(object sender, EventArgs e)
        {
            if (this.IsXML(this._loadedFilePath) && (this.colorDialogTag.ShowDialog() == DialogResult.OK))
            {
                this._tagColor = this.colorDialogTag.Color;
                this.ColoringTag();
            }
        }

        /// <summary>
        /// �e�L�X�g���͎��Ƀ^�O�̌`���ɂȂ��Ă���΃^�O�v�f�𒅐F���郁�\�b�h
        /// </summary>
        /// <remarks>
        /// �e�L�X�g���͎��Ƀ^�O�̌`���ɂȂ��Ă���Ύ����ŐF�����Ăق���
        ///�^�O�v�f�̐F�Ńe�L�X�g�����͂���Ă͂Ȃ�Ȃ��A�����͏ꏊ���ω����Ă͂Ȃ�Ȃ�����
        ///�ʏ�̃^�O�v�f�F�ύX���\�b�h�ɉ����ē��͈ʒu�����ɖ߂��A�^�O�̑I������������L�q��ǉ�����
        ///</remarks>
        private void textChanged(object sender, EventArgs e)
        {
            if (this.IsXML(this._loadedFilePath))
            {
                int currentPosition = this.richTextBox.SelectionStart;
                this.ColoringTag();
                this.richTextBox.SelectionStart = currentPosition;
                this.richTextBox.SelectionLength = 0;
            }
        }

    }
}
