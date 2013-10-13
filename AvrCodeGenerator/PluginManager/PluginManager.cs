using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using DataModel;
using DataModel.DataProvider.PeripheralInfoProviders;
using DataModel.ICodeWizardPlugin;

namespace PluginManager
{
    public class PluginManager
    {
        [ImportMany(typeof(ICodeWizardPlugin))]
        public System.Lazy<ICodeWizardPlugin, IDictionary<string, object>>[] Plugins { get; set; }

        public static System.Lazy<ICodeWizardPlugin, IDictionary<string, object>>[] ModulePlugins { get; set; } 

        public static List<ICodeWizardPlugin> GeneralPlugins
        {
            get
            {
                if (ModulePlugins != null)
                {
                    var plugins = new List<ICodeWizardPlugin>();
                    foreach (Lazy<ICodeWizardPlugin, IDictionary<string, object>> modulePlugin in ModulePlugins)
                    {
                        object value;
                        if (modulePlugin.Metadata.TryGetValue(CodeWizardPluginType.General, out value))
                        {
                            plugins.Add( modulePlugin.Value);
                        }
                    }
                    return plugins;
                }
                return null;
            }
        }

        public PluginManager()
        {
            McuModel.PeripheralInfoProvider = new XmegaPeripheralInfoProvider("xmega128a1");
            AssembleComponents();
        }

        /// <summary>
        /// Assembles the calculator components
        /// </summary>
        public void AssembleComponents()
        {
            try
            {
                //Creating an instance of aggregate catalog. It aggregates other catalogs
                var aggregateCatalog = new AggregateCatalog();

                //Build the directory path where the parts will be available
                var directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                //Load parts from the available dlls in the specified path using the directory catalog
                var directoryCatalog = new DirectoryCatalog(directoryPath, "*.dll");

                //Load parts from the current assembly if available
                var asmCatalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());

                //Add to the aggregate catalog
                aggregateCatalog.Catalogs.Add(directoryCatalog);
                aggregateCatalog.Catalogs.Add(asmCatalog);

                //Crete the composition container
                var container = new CompositionContainer(aggregateCatalog);

                // Composable parts are created here i.e. the Import and Export components assembles here
                container.ComposeParts(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ICodeWizardPlugin GetPlugins(string type, string name)
        {
            var plugins = new List<ICodeWizardPlugin>();
            foreach (var modulePlugin in ModulePlugins)
            {
                object value;
                if (modulePlugin.Metadata.TryGetValue(type, out value))
                {
                    if ((value.ToString()).Equals(name))
                    {
                        plugins.Add(modulePlugin.Value);
                    }
                }
            }
            if(plugins.Any())
            {
                return plugins[0];
            }
            return null;
        }
    }
}
