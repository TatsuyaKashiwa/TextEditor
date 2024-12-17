using System.Text;
using System.Text.RegularExpressions;

namespace TextEditor
{
    enum Encode 
    {
        Utf8,
        Utf16LE,
        Utf16BE,
        Utf32,
    }
    public partial class MainForm : Form
    {
        public MainForm()
        {
            this.InitializeComponent();
        }

        private string _loadedFilePath = "";
        private string _savingFilePath = "";
        private Encode _encodeNum = Encode.Utf8;
        private Encoding _encodeLoad = Encodes.GetEncode((int)Encode.Utf8);
        private Encoding _encodeSave = Encodes.GetEncode((int)Encode.Utf8);
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
            var tags = Regex.Matches(this.RichTextBox.Text, @"<([^<>]+)>");

            foreach (Match tag in tags)
            {
                var index = tag.Groups[1].Index;
                var tagLength = tag.Groups[1].Length;
                this.RichTextBox.Select(index, tagLength);
                this.RichTextBox.SelectionColor = this._tagColor;
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
        private void Load_Click(object sender, EventArgs e)
        {
            if (this.OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                this._loadedFilePath = this.OpenFileDialog.FileName;
            }
            
            this._encodeLoad = Encodes.GetEncode((int)this._encodeNum);

            try
            {
                var lines = File.ReadLines(this._loadedFilePath, this._encodeLoad);

                foreach (var line in lines)
                {
                    this.RichTextBox.Text += line + Environment.NewLine;
                }
                if (this.IsXML(this._loadedFilePath))
                {
                    this.ColoringTag();
                    this.RichTextBox.SelectionColor = this._tagColor;
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
        private void Save_Click(object sender, EventArgs e)
        {

            this._encodeSave = Encodes.GetEncode((int)this._encodeNum);
            this.RichTextBox.Text = Encodes.ChangeEncode(this._encodeLoad, this._encodeSave, this.RichTextBox.Text);

            if (this.SaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                this._savingFilePath = this.SaveFileDialog.FileName;
            }
            if (!File.Exists(this._savingFilePath) && this._savingFilePath != "")
            {
                FileStream fileStream = File.Open(this._savingFilePath, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Close();
            }
            try
            {
                File.WriteAllText(this._savingFilePath, this.RichTextBox.Text, this._encodeSave);
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
        private void Utf8_CheckedChanged(object sender, EventArgs e) => this._encodeNum = Encode.Utf8;

        private void Utf16le_CheckedChanged(object sender, EventArgs e) => this._encodeNum = Encode.Utf16LE;

        private void Utf16be_CheckedChanged(object sender, EventArgs e) => this._encodeNum = Encode.Utf16BE;

        private void Utf32_CheckedChanged(object sender, EventArgs e) => this._encodeNum = Encode.Utf32;

        /// <summary>
        /// �e�L�X�g�F�ύX�̃��\�b�h
        /// </summary>
        private void ChangeTextColor_Click(object sender, EventArgs e)
        {
            if (this.ColorDialogForText.ShowDialog() == DialogResult.OK)
            {
                this.RichTextBox.ForeColor = this.ColorDialogForText.Color;
                if (this.IsXML(this._loadedFilePath)) 
                {
                    this.ColoringTag();
                }
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
        private void ChangeTagColor_Click(object sender, EventArgs e)
        {
            if (this.IsXML(this._loadedFilePath) && (this.ColorDialogForTag.ShowDialog() == DialogResult.OK))
            {
                this._tagColor = this.ColorDialogForTag.Color;
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
        private void TextChanging(object sender, EventArgs e)
        {
            if (this.IsXML(this._loadedFilePath))
            {
                int currentPosition = this.RichTextBox.SelectionStart;
                this.ColoringTag();

                this.RichTextBox.SelectionStart = currentPosition;
                this.RichTextBox.SelectionLength = 0;
            }
        }

    /// <summary>
    /// WriteAllText�̎���Ń��\�b�h
    /// </summary>
    /// <param name="path">�ۑ��t�@�C���p�X</param>
    /// <param name="savetext">�ۑ�����e�L�X�g</param>
    /// <param name="encode">�G���R�[�h</param>
    /// <remarks>
    /// StreamWriter��new���邱�Ƃ�(�t�@�C���쐬�E)FileStream��Open����
    /// �����Ƃ��Ď󂯂��ۑ��e�L�X�g���AStreamWriter.Write�Ńt�@�C���֏�������
    /// </remarks>
    private void MyWriteAllText(string path, string savetext, Encoding encode) 
    {
        using (var sw = new StreamWriter(path,false,encode)) 
        {
            sw.Write(savetext);
        }
    }

}
