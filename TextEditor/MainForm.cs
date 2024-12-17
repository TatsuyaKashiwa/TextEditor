using System.Text;
using System.Text.RegularExpressions;

namespace TextEditor;

/// <summary>
/// エンコード名をEncodesクラスの配列のインデックスに対応させるenum
/// </summary>
enum EncodeName 
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

    /// <summary>
    /// 読み込みファイルのファイルパス
    /// </summary>
    private string _loadedFilePath = "";
    /// <summary>
    /// 保存先ファイルのファイルパス
    /// </summary>
    private string _savingFilePath = "";
    /// <summary>
    /// 選択されたエンコード名の列挙値
    /// </summary>
    private EncodeName _encodeName = EncodeName.Utf8;
    /// <summary>
    /// 読み込み時エンコード
    /// </summary>
    private Encoding _encodeLoad = Encodes.GetEncode((int)EncodeName.Utf8);
    /// <summary>
    /// 保存時エンコード
    /// </summary>
    private Encoding _encodeSave = Encodes.GetEncode((int)EncodeName.Utf8);
    /// <summary>
    /// XMLタグ要素色
    /// </summary>
    private Color _tagColor = Color.Blue;

    /// <summary>
    /// XMLファイルであるかを判定するメソッド
    /// </summary>
    /// <param name="path">判定対象のファイルパス</param>
    /// <returns>XMLファイルであればtrueを返す</returns>
    /// <remarks>
    ///xmlファイル(パスの末尾が.xml)であればtrueを返すメソッド
    /// </remarks>
    private bool IsXML(string path) => Regex.IsMatch(path, ".xml$");

    /// <summary>
    /// (XML)タグ要素の色を変更
    /// </summary>
    ///<remarks>
    ///XMLタグ要素(/を含む)のみ指定した色へ変更する
    ///正規表現のGroupを用いることで正規表現と合致した部分以外がタグ要素の文字色となることを防ぐ
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
    /// ファイル読み込みに対応するメソッド(loadボタン押下時のイベントハンドラ)
    /// </summary>
    /// <param name="ex">catchした例外</param>
    /// <remarks>
    /// ダイアログで選択したファイルを読み込み、xmlファイルであればタグ要素は指定色で表示させる。
    /// 例外発生時はエラーメッセージをユーザに向けて表示させる。
    ///</remarks>
    ///<exception cref="System.ArgumentException">ファイル未選択の場合</exception>
    ///<exception cref="System.IO.FileNotFoundException">存在しないファイル名を入力した場合</exception>
    private void Load_OnClick(object sender, EventArgs e)
    {
        if (this.OpenFileDialog.ShowDialog() == DialogResult.OK)
        {
            this._loadedFilePath = this.OpenFileDialog.FileName;
        }

        this._encodeLoad = Encodes.GetEncode((int)this._encodeName);

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
    /// ファイル保存に対応するメソッド(saveボタン押下のイベントハンドラ)
    /// </summary>
    /// <param name="ex">catchした例外</param>
    /// <remarks>
    ///ファイルを指定したエンコードで保存する。
    ///ディレクトリ内に指定した名前のファイルが存在しない場合は新規にファイル作成を行う
    ///例外発生時はエラーメッセージをユーザに向けて表示させる。
    ///</remarks>
    ///<exception cref="System.ArgumentException">ファイル未選択の場合</exception>
    private void Save_OnClick(object sender, EventArgs e)
    {

        this._encodeSave = Encodes.GetEncode((int)this._encodeName);
        this.RichTextBox.Text = Encodes.ChangeEncode(this._encodeLoad,
                                                     this._encodeSave,
                                                     this.RichTextBox.Text);

        if (this.SaveFileDialog.ShowDialog() == DialogResult.OK)
        {
            this._savingFilePath = this.SaveFileDialog.FileName;
        }
        if (!File.Exists(this._savingFilePath) && this._savingFilePath != "")
        {
            using FileStream fileStream = File.Open(this._savingFilePath,
                                                    FileMode.Create,
                                                    FileAccess.ReadWrite);
        }
        try
        {
            File.WriteAllText(this._savingFilePath,
                              this.RichTextBox.Text,
                              this._encodeSave);
        }
        catch (Exception ex)
        {
            ErrorMessageShower.ShowErrorMessage(ex);
        }
    }

    /// <summary>
    /// ラジオボタンに対応するエンコードを選択するメソッド
    /// </summary>
    /// <remarks>
    /// ラジオボタンを選択すると、対応するエンコードの列挙値をエンコード名として設定される
    ///</remarks>
    private void Utf8_OnCheckedChanged(object sender, EventArgs e) => this._encodeName = EncodeName.Utf8;

    private void Utf16le_OnCheckedChanged(object sender, EventArgs e) => this._encodeName = EncodeName.Utf16LE;

    private void Utf16be_OnCheckedChanged(object sender, EventArgs e) => this._encodeName = EncodeName.Utf16BE;

    private void Utf32_OnCheckedChanged(object sender, EventArgs e) => this._encodeName = EncodeName.Utf32;

    /// <summary>
    /// テキスト色変更のメソッド(文字色変更ボタンのイベントハンドラ)
    /// </summary>
    /// <remarks>
    /// カラーダイアログで選択された色へテキストの文字色を変更する
    /// タグ要素色も同時に変更されるため、文字色変更後にタグ色を元に戻す
    /// </remarks>
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
    /// タグ要素の色を変更するメソッド（タグ色変更ボタンのイベントハンドラ）
    /// </summary>
    /// <remarks>
    ///プロパティの変更のみではxmlタグが指定された色へ変わらないため
    ///プロパティ変更後、タグ色を変更させるメソッドを作用させた
    ///</remarks>
    private void ChangeTagColor_OnClick(object sender, EventArgs e)
    {
        if (this.IsXML(this._loadedFilePath)
            && (this.ColorDialogForTag.ShowDialog() == DialogResult.OK))
        {
            this._tagColor = this.ColorDialogForTag.Color;
            this.ColoringTag();
        }
    }

    /// <summary>
    /// xmlファイル読み込み時のテキスト入力で、入力がタグの形式になっていればタグ要素を着色するメソッド
    /// </summary>
    /// <remarks>
    ///xmlファイル読み込み時に入力がタグの形式となっていれば自動的にタグ要素の色に変更する
    ///色変更で入力位置が変更されたり、無関係の場所がタグ要素の色にならないよう
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
