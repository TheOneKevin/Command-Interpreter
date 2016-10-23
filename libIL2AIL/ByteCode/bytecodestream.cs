using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace libIL2AIL.ByteCode
{
    class bytecodestream
    {
        public static bool AppendAllBytes(string path, byte[] bytes)
        {
            if (!File.Exists(path))
                return false;

            using (var stream = new FileStream(path, FileMode.Append))
            {
                stream.Write(bytes, 0, bytes.Length);
            }

            return true;
        }
    }
}
