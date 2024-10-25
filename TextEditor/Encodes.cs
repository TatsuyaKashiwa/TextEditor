using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor
{
    //エンコードに関することはForm1クラス内の様々な所に現れている
    //なので、エンコード関連の処理をひとまとめにしたほうが効率が良く読みやすくなると思われる
    //エンコード関連のことをまとめたユーティリティクラスを作成した
    internal static class Encodes
    {
        //エンコードを変化・選択させるのにインデックス・int32型の値として扱えれば便利である
        //ただし、選択するエンコードの種類はアプリケーション利用中に追加することはないため
        //(通常の)配列としてアプリケーションで用いるエンコードを格納した
        //(下記メソッドを介して取り扱われるためprivateとした)
       private static Encoding[] _encodings = {Encoding.UTF8,Encoding.Unicode,Encoding.BigEndianUnicode,Encoding.UTF32 };
        
        //上記の配列からエンコードを取り出す仕組みが必要となる
        //外部のコントロールの選択と上記の配列を対応させる必要があるため
        //引数にコントロールの選択に対応する値を取るメソッドとして定義した
        internal static Encoding GetEncode(int i) => _encodings[i];

        //エンコードを変化させただけでは文字化けとなり、ファイルの内容を保持したエンコード変換ができない。
        //内容を保持したままエンコードを変換するには一旦バイト配列に変換する必要がある
        //バイト配列への変換→エンコード変換→String型に変換して返す操作を一元化したメソッドを作成
        internal static string ChangeEncode(Encoding previousEncode, Encoding afterwardEncode, string text) 
        {
            byte[] previousByte = previousEncode.GetBytes(text);
            byte[] afterwardByte = Encoding.Convert(previousEncode, afterwardEncode, previousByte);
            return afterwardEncode.GetString(afterwardByte);
        }
        
        
    }
}
