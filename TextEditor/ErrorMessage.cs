using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor
{
    //Form1クラスが乱雑になるのを防ぐべく
    //例外に対する処理を分離して記述するため
    //ErrorMessageクラスを作成した。
    internal static class ErrorMessage
    {

        //例外に対する扱いはその種類に関わらずメッセージボックスを出すことで統一
        //対応すべき例外は2種類から増える可能性がある
        //各例外に対応するメッセージを、例外の数が増えた時の拡張性が高いswitch文による記述で分岐させて表示
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
                    break;
            }
        }
    }
}
