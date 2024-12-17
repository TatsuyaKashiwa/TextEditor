using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor;

/// <summary>
/// エンコードに関わるクラス
/// </summary>
internal static class Encodes
{
/// <summary>
/// 利用エンコードをまとめた配列
/// </summary>
///  <remarks>
///エンコードを変化・選択させるのにインデックス・int32型の値として扱えれば便利である
/// </remarks>
private static Encoding[] s_encodings = [Encoding.UTF8, Encoding.Unicode, Encoding.BigEndianUnicode, Encoding.UTF32];

/// <summary>
/// エンコード名に対応する列挙値をEncoding型に変換
/// </summary>
/// <param name="i">エンコードに対応するenum(をintへキャストしたもの)</param>
/// <returns>引数(ラジオボタンの選択)に対応したエンコード</returns>
/// <remarks>
///MainForm.csで定義したエンコード名に対応する列挙値(をintにキャストしたもの)をEncoding型のエンコードに変換
///</remarks>
internal static Encoding GetEncode(int i) => s_encodings[i];

/// <summary>
/// 入力文字のエンコードを選択したエンコードへ変換
/// </summary>
/// <param name="previousEncode">現在のエンコード</param>
/// <param name="afterwardEncode">変換先のエンコード</param>
/// <param name="text">入力テキスト（エンコード変換前）</param>
/// <returns>入力テキスト(エンコード変換後)</returns>
/// <remarks>
/// 入力文字のエンコードを一旦バイト配列の形にして選択エンコードへ変換
///(エンコード変換のConvertメソッドがバイト配列にのみ対応しているため)
///</remarks>
internal static string ChangeEncode(Encoding previousEncode,
    　　　　　　　　　　　　　　　　Encoding afterwardEncode,
    　　　　　　　　　　　　　　　　string text) 
{
    byte[] previousByte = previousEncode.GetBytes(text);
    byte[] afterwardByte = Encoding.Convert(previousEncode,
                                            afterwardEncode,
                                            previousByte);
    return afterwardEncode.GetString(afterwardByte);
}


}
