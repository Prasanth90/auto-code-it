﻿using System.Collections.Generic;
using System.Text;

namespace CodeWizard.DataModel.ICodeWizardPlugin
{
    public class CodeBlock
    {
        public CodeBlock(string codeBlockName)
        {
            CodeBlockName = codeBlockName;
            CodeGenerationInfos = new List<CodeGenerationInfo>();
        }
        public string CodeBlockName { get; set; }

        public List<CodeGenerationInfo> CodeGenerationInfos { get; set; }
    }

    public class CodeGenerationInfo
    {
        public CodeGenerationInfo(string pheripheralName)
        {
            SourceCodeBlock = new StringBuilder();
            HashDefineBlock = new StringBuilder();
            InteruptHandlerBlock = new StringBuilder();
            FunctionCallsBlock = new StringBuilder();
            FunctionDeclarationBlock = new StringBuilder();
            PeripheralName = pheripheralName;
        }
        public string PeripheralName { get; set; }
        public StringBuilder SourceCodeBlock { get; set; }
        public StringBuilder HashDefineBlock { get; set; }
        public StringBuilder InteruptHandlerBlock { get; set; }
        public StringBuilder FunctionCallsBlock { get; set; }
        public StringBuilder FunctionDeclarationBlock { get; set; }
    }
}
