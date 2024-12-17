using System.Text;
using System.Text.RegularExpressions;

namespace TextEditor;

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
    /// XMLファイルであるかを判定するメソッド
    /// </summary>
    /// <param name="path">判定対象のファイルパス</param>
    /// <returns>XMLファイルであればtrueを返す</returns>
    /// <remarks>
    ///xmlファイルのタグに色を付けるためにxmlファイルであるかを判定する必要がある
    ///判定結果を boolとしたほうが、条件式にそのまま組み込めるので
    ///引数のファイルパスが.xmlで終わるかの判定をIsMatchメソッドで実現した
    /// </remarks>
    private bool IsXML(string path) => Regex.IsMatch(path, ".xml$");

    /// <summary>
    /// (XML)タグ要素の色を変更
    /// </summary>
    ///<remarks>
    ///タグの要素のみを別の色へ変更する必要がある
    ///タグに合致する表現を見つけ出して、要素だけを色付けする必要があるため
    ///タグの条件に合致する表現を配列に格納し
    ///要素部分だけをタグ色で指定した色へ変更させた。
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
    /// ファイル読み込み(loadボタン押下時の動作)に対応するメソッド
    /// </summary>
    /// <param name="ex">catchした例外</param>
    /// <remarks>
    /// ファイルを選択したエンコードで取り込み、XMLファイルであれば色を付けたい
    ///すべて取り込むReadAllLinesメソッドでは動作が重くなりうるため
    ///テキストの読み込みとタグの色付けは一連の動作としたいため
    ///一行ずつ表示領域に追加した後、ファイル取り込み以降の操作をtry節で囲んだ
    ///</remarks>
    ///<exception cref="System.ArgumentException">ファイル未選択の場合</exception>
    ///<exception cref="System.IO.FileNotFoundException">存在しないファイル名を入力した場合</exception>
    private void Load_OnClick(object sender, EventArgs e)
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
            ErrorMessageShower.ShowErrorMessage(ex);
        }
    }

    /// <summary>
    /// ファイル保存(saveボタン押下)に対応するメソッド
    /// </summary>
    /// <param name="ex">catchした例外</param>
    /// <remarks>
    /// ファイルを指定したエンコードで保存、ファイルを新たに作り出すことも行いたい
    ///ファイルのエンコードは保存先、ファイルの有無はテキストボックスからデータをファイルに書き出す時に行う必要があるため
    ///Encodesクラスのメソッドでエンコードを変換し、ファイルへのデータ書き出しのみtry節で囲んだ
    ///</remarks>
    ///<exception cref="System.ArgumentException">ファイル未選択の場合</exception>
    private void Save_OnClick(object sender, EventArgs e)
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
            ErrorMessageShower.ShowErrorMessage(ex);
        }
    }

    /// <summary>
    /// ラジオボタンに対応するエンコード（に対応するインデックス）を選択するメソッド
    /// </summary>
    /// <remarks>
    /// ラジオボタンの選択で読み込み/保存のエンコードを変えられるようにしたい
    ///ラジオボタンの選択をエンコードを返すメソッドに渡す必要があるため
    ///各ボタンのエンコードに対応するint型の値を返すようにした
    ///</remarks>
    private void Utf8_OnCheckedChanged(object sender, EventArgs e) => this._encodeNum = Encode.Utf8;

    private void Utf16le_OnCheckedChanged(object sender, EventArgs e) => this._encodeNum = Encode.Utf16LE;

    private void Utf16be_OnCheckedChanged(object sender, EventArgs e) => this._encodeNum = Encode.Utf16BE;

    private void Utf32_OnCheckedChanged(object sender, EventArgs e) => this._encodeNum = Encode.Utf32;

    /// <summary>
    /// テキスト色変更のメソッド
    /// </summary>
    private void ChangeTextColor_OnClick(object sender, EventArgs e)
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
    /// タグ要素の色を変更するメソッド
    /// </summary>
    /// <remarks>
    ///  タグ要素の色を変更したい
    ///ただプロパティを変更するだけでは色は変わらないため
    ///要素の色を決定した後タグ色を変更させるメソッドを作用させた
    ///</remarks>
    private void ChangeTagColor_OnClick(object sender, EventArgs e)
    {
        if (this.IsXML(this._loadedFilePath) && (this.ColorDialogForTag.ShowDialog() == DialogResult.OK))
        {
            this._tagColor = this.ColorDialogForTag.Color;
            this.ColoringTag();
        }
    }

    /// <summary>
    /// テキスト入力時にタグの形式になっていればタグ要素を着色するメソッド
    /// </summary>
    /// <remarks>
    /// テキスト入力時にタグの形式になっていれば自動で色がついてほしい
    ///タグ要素の色でテキストが入力されてはならない、かつ入力場所も変化してはならないため
    ///通常のタグ要素色変更メソッドに加えて入力位置を元に戻し、タグの選択を解除する記述を追加した
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
}
