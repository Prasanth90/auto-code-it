using System;
using System.Text;

namespace CodeGenerator.CodeGeneratorUtils
{
    public class CodeGeneratorUtils
    {
        public static  String GetHashDefine(string symbol , string value)
        {
            return string.Format("#define {0} {1}", symbol, value);
        }
    }
}
