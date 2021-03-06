﻿using System;
using System.Collections.Generic;

namespace CodeWizard.Plugins.Utils
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

	    public static String GetHashDefine(string symbol, string value)
	    {
	        return String.Format("#define {0} {1}", symbol, value);
	    }
	}
}
