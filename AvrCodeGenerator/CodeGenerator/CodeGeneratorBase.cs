using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel;

namespace CodeGenerator
{
    public abstract class CodeGeneratorBase : ICodeGenerator
    {
        protected McuModel McuModel { get; set; }

        protected FilesContentStore FilesContentStore { get; set; }

        protected CodeGeneratorBase(McuModel mcuModel, FilesContentStore filesContentStore)
        {
            McuModel = mcuModel;
            FilesContentStore = filesContentStore;
        }

        public abstract CodeBlock GetCode();
        

        protected StringBuilder GetCommentSection(string message)
        {
            var commentsection = new StringBuilder();
            commentsection.AppendLine();
            commentsection.Append("/**");
            commentsection.AppendLine();
            commentsection.Append("* \\brief " + message);
            commentsection.AppendLine();
            commentsection.Append("*/");
            commentsection.AppendLine();
            return commentsection;
        }
    }

    public class CodeGenerationInfo
    {
        public CodeGenerationInfo(string pheripheralName)
        {
            CodeBlock = new StringBuilder();
            HashDefineBlock = new StringBuilder();
            InteruptHandlerBlock = new StringBuilder();
            FunctionCallsBlock = new StringBuilder();
            FunctionDeclarationBlock = new StringBuilder();
            PeripheralName = pheripheralName;
        }
        public string PeripheralName { get; set; }
        public StringBuilder CodeBlock { get; set; }
        public StringBuilder HashDefineBlock { get; set; }
        public StringBuilder InteruptHandlerBlock { get; set; }
        public StringBuilder FunctionCallsBlock { get; set; }
        public StringBuilder FunctionDeclarationBlock { get; set; }
    }

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
}
