using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataModel.UsarModel
{
    public class UsartModel 
    {
        public UsartModel(string usartName)
        {
            this.UsartName = usartName;
            UsartSettings = new UsartSettings();
        }
        public string UsartName { get; set; }

        public UsartSettings UsartSettings { get; set; }
    }
}
