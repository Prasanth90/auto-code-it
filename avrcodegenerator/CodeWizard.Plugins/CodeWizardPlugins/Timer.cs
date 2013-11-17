using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using CodeWizard.DataModel;
using CodeWizard.DataModel.ICodeWizardPlugin;
using CodeWizard.DataModel.Timer;
using CodeWizard.DataModel.UsarModel;
using CodeWizard.Plugins.CodeGeneration;
using CodeWizard.Plugins.CodeGeneration.CodeGenerators;
using CodeWizard.Plugins.View.Usart;

namespace CodeWizard.Plugins.CodeWizardPlugins
{
    [Export(typeof(ICodeWizardPlugin))]
    [ExportMetadata(CodeWizardPluginType.General, CodeWizardPluginNames.Timer)]
    public class Timer : ICodeWizardPlugin
    {
        private TimerModel _timerModel;
        public PluginInfo GetPluginInfo()
        {
            var pluginInfo = new PluginInfo()
                {
                    Icon = "mexico",
                    Name = CodeWizardPluginNames.Timer
                };
            return pluginInfo;
        }

        public Dictionary<string, UserControl> CreateUserControl(string name)
        {
             var userControls = new Dictionary<string, UserControl>();
             var timerModel = new TimerModel();
             _timerModel = timerModel;
             foreach (var timer in timerModel.Timers)
             {
                 userControls.Add(timer.TimerName, new View.Timers.TimerControl(timer));
             }
             return userControls;
        }

        public ICodeGenerator CodeGenerator()
        {
             var filesContentStore = new FilesContentStore();
            new CreateRawInput(filesContentStore).LoadResourceFile();
           return new TimerCodeGenerator(_timerModel,filesContentStore);
        }
    }
}
