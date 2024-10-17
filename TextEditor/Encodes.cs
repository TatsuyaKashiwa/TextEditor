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
        static Encoding ChangeEncode(int i) => encodings[i];
    }
}
