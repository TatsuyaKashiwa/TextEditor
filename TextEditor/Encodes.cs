using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor
{
    internal static class Encodes
    {
        static Encoding[] encodings = {Encoding.UTF8,Encoding.Unicode,Encoding.BigEndianUnicode,Encoding.UTF32 };
        internal static Encoding GetEncode(int i) => encodings[i];

        internal static string ChangeEncode(Encoding previousEncode, Encoding afterwardEncode, string text) 
        {
            byte[] previousByte = previousEncode.GetBytes(text);
            byte[] afterwardByte = Encoding.Convert(previousEncode, afterwardEncode, previousByte);
            return afterwardEncode.GetString(afterwardByte);
        }
        
        
    }
}
