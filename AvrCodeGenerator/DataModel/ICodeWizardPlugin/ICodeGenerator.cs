﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModel;
using DataModel.ICodeWizardPlugin;

namespace CodeGenerator
{
    public  interface ICodeGenerator
    {
        CodeBlock GetCode();
    }
}
