using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor;

/// <summary>
/// エラーメッセージ表示のためのクラス
/// </summary>
internal static class ErrorMessageShower
{

    /// <summary>
    /// catchされた例外に対応するエラーメッセージを表示させるメソッド
    /// </summary>
    /// <param name="e">catchされた例外</param>
    /// <remarks>
    /// 例外が発生しうるファイル入出力時の例外に対するエラーメッセージを表示
    ///</remarks>
    internal static void ShowErrorMessage(Exception e) 
    {
        switch (e) 
        {
            case System.ArgumentException:
                MessageBox.Show("ファイルが選択されていません");
                break;
            case System.IO.FileNotFoundException:
                MessageBox.Show("該当するファイル名のファイルが見つかりませんでした");
                break;
            default:
                MessageBox.Show(e.ToString());
                break;
        }
    }
}
