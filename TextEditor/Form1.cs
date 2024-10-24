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

        //xmlファイルのタグに色を付けるためにxmlファイルであるかを判定する必要がある
        //判定結果を boolとしたほうが、条件式にそのまま組み込めるので
        //引数のファイルパスが.xmlで終わるかの判定をIsMatchメソッドで実現した
        bool isXML(string path) => Regex.IsMatch(path, ".xml$");

        //タグの要素のみを別の色へ変更する必要がある
        //タグに合致する表現を見つけ出して、要素だけを色付けする必要があるため
        //タグの条件に合致する表現を配列に格納し
        //要素部分だけをタグ色で指定した色へ変更させた。
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

        //ファイルを選択したエンコードで取り込み、XMLファイルであれば色を付けたい
        //すべて取り込むReadAllLinesメソッドでは動作が重くなりうるため
        //テキストの読み込みとタグの色付けは一連の動作としたいため
        //一行ずつ表示領域に追加した後、ファイル取り込み以降の操作をtry節で囲んだ
        private void load_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                loadedfilepath = openFileDialog.FileName;
            }
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

        //ファイルを指定したエンコードで保存、ファイルを新たに作り出すことも行いたい
        //ファイルのエンコードは保存先、ファイルの有無はテキストボックスからデータをファイルに書き出す時に行う必要があるため
        //Encodesクラスのメソッドでエンコードを変換し、ファイルへのデータ書き出しのみtry節で囲んだ
        private void save_Click(object sender, EventArgs e)
        {
            encodeSave = Encodes.GetEncode(encodeNum);
            richTextBox.Text = Encodes.ChangeEncode(encodeLoad, encodeSave, richTextBox.Text);
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                savingfilepath = saveFileDialog.FileName;
            }
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

        //ラジオボタンの選択で読み込み/保存のエンコードを変えられるようにしたい
        //ラジオボタンの選択をエンコードを返すメソッドに渡す必要があるため
        //各ボタンのエンコードに対応するint型の値を返すようにした
        private void utf8_CheckedChanged(object sender, EventArgs e) => encodeNum = 0;

        private void utf16le_CheckedChanged(object sender, EventArgs e) => encodeNum = 1;

        private void utf16be_CheckedChanged(object sender, EventArgs e) => encodeNum = 2;

        private void utf32_CheckedChanged(object sender, EventArgs e) => encodeNum = 3;

        //タグ要素の色を変更せずにテキストの色を変更したい
        //なぜなら、タグ要素の色付けが一時的なものとなってしまうため
        //テキスト色変更後にタグの色を付けなおした
        private void changeTextColor_Click(object sender, EventArgs e)
        {
            if (colorDialogText.ShowDialog() == DialogResult.OK)
            {
                richTextBox.ForeColor = colorDialogText.Color;
                ColoringTag();
            }
        }

        //タグ要素の色を変更したい
        //ただプロパティを変更するだけでは色は変わらないため
        //要素の色を決定した後タグ色を変更させるメソッドを作用させた
        private void changeTagColor_Click(object sender, EventArgs e)
        {
            if (isXML(loadedfilepath) && (colorDialogTag.ShowDialog() == DialogResult.OK))
            {
                tagColor = colorDialogTag.Color;
                ColoringTag();
            }
        }

        //テキスト入力時にタグの形式になっていれば自動で色がついてほしい
        //タグ要素の色でテキストが入力されてはならない、かつ入力場所も変化してはならないため
        //通常のタグ要素色変更メソッドに加えて入力位置を元に戻し、タグの選択を解除する記述を追加した
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
