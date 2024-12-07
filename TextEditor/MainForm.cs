using System.Text;
using System.Text.RegularExpressions;

namespace TextEditor;

public partial class MainForm : Form
{
    public MainForm()
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
    ///すべて取り込むReadAllLinesメソッドでは動作が重くなりうるため(ファイルサイズが莫大ならば→現状はインスタンス生成コストのほうが重いのでAllLinesで)
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
        this._encodeLoad = Encodes.GetEncode(this._encodeNum);
        try
        {
            this.RichTextBox.Text = this.MyReadAllText(this._loadedFilePath, this._encodeLoad);
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
    ///ファイルのエンコードはファイルを適切なエンコードとして保存するために必要であるので
    ///Encodesクラスのメソッドでエンコードを変換し、
    ///WriteAllTextはファイルが存在しない場合は作成するため、この部分をtry節で囲んだ
    ///</remarks>
    ///<exception cref="System.ArgumentException">ファイル未選択の場合</exception>
    private void Save_OnClick(object sender, EventArgs e)
    {
        this._encodeSave = Encodes.GetEncode(this._encodeNum);
        this.RichTextBox.Text = Encodes.ChangeEncode(this._encodeLoad, this._encodeSave, this.RichTextBox.Text);
        if (this.SaveFileDialog.ShowDialog() == DialogResult.OK)
        {
            this._savingFilePath = this.SaveFileDialog.FileName;
        }
        try
        {
                this.MyWriteAllText(this._savingFilePath, this.RichTextBox.Text, this._encodeSave);
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
    private void Utf8_OnCheckedChanged(object sender, EventArgs e) => this._encodeNum = 0;

    private void Utf16le_OnCheckedChanged(object sender, EventArgs e) => this._encodeNum = 1;

    private void Utf16be_OnCheckedChanged(object sender, EventArgs e) => this._encodeNum = 2;

    private void Utf32_OnCheckedChanged(object sender, EventArgs e) => this._encodeNum = 3;

    /// <summary>
    /// テキスト色変更のメソッド
    /// </summary>
    /// <remarks>
    /// タグ要素の色を変更せずにテキストの色を変更したい
    ///なぜなら、タグ要素の色付けが一時的なものとなってしまうため
    ///テキスト色変更後にタグの色を付けなおした
    ///</remarks>
    private void ChangeTextColor_OnClick(object sender, EventArgs e)
    {
        if (this.ColorDialogText.ShowDialog() == DialogResult.OK)
        {
            this.RichTextBox.ForeColor = this.ColorDialogText.Color;
            this.ColoringTag();
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
        if (this.IsXML(this._loadedFilePath) && (this.ColorDialogTag.ShowDialog() == DialogResult.OK))
        {
            this._tagColor = this.ColorDialogTag.Color;
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


    /// <summary>
    /// ReadAllTextの自作版メソッド
    /// </summary>
    /// <param name="path">読み込みファイルのフルパス</param>
    /// <param name="encode">読み込みエンコード</param>
    /// <returns>ファイル内容(string型)</returns>
    /// <remarks>
    /// ReadAllTextをStreamを明記して作成
    /// StreamReaderをnewすることでFileStreamをOpenし、
    /// StreamReaderのReadToEnd()メソッドで最終行まで読み込み
    /// </remarks>
    private string MyReadAllText(string path, Encoding encode) 
    {
        using (var sreader = new StreamReader(path,encode)) 
        {
            return sreader.ReadToEnd();
        }

    }

    /// <summary>
    /// WriteAllTextの自作版メソッド
    /// </summary>
    /// <param name="path">保存ファイルパス</param>
    /// <param name="savetext">保存するテキスト</param>
    /// <param name="encode">エンコード</param>
    /// <remarks>
    /// StreamWriterをnewすることで(ファイル作成・)FileStreamをOpenして
    /// 引数として受けた保存テキストを、StreamWriter.Writeでファイルへ書き込み
    /// </remarks>
    private void MyWriteAllText(string path, string savetext, Encoding encode) 
    {
        using (var sw = new StreamWriter(path,false,encode)) 
        {
            sw.Write(savetext);
        }
    }

}
