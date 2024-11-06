using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor
{
    /// <summary>
    /// エラーメッセージ表示のためのクラス
    /// </summary>
    /// <remarks>
    /// Form1クラスが乱雑になるのを防ぐべく
    ///例外に対する処理を分離して記述するためのクラス
    ///</remarks>
    internal static class ErrorMessage
    {

        /// <summary>
        /// catchされた例外に対応するエラーメッセージを表示させるメソッド
        /// </summary>
        /// <param name="e">catchされた例外</param>
        /// <remarks>
        /// 例外に対する扱いはその種類に関わらずメッセージボックスを出すことで統一
        ///対応すべき例外は2種類から増える可能性がある
        ///各例外に対応するメッセージを、例外の数が増えた時の拡張性が高いswitch文による記述で分岐させて表示
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
}
