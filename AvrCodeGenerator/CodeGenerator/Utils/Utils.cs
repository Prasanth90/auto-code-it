using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenerator.Utils
{
	public static class Utils
	{
        public static void PerformReplacementInFileContents(Dictionary<string, string> replacementDict, ref string fileContents)
        {
            foreach (KeyValuePair<string, string> replacement in replacementDict)
            {
              fileContents =  fileContents.Replace(replacement.Key, replacement.Value);
            }
        }
	}
}
