using System.Collections.Generic;

namespace CodeWizard.Plugins.CodeGeneration
{
    public class FileNames
    {
        public const string UsartInteruptHandler = "usart_interupt_handler.c";
        public const string UsartInteruptInit    = "usart_interupt_init.c";
        public const string UsartSerialDefines = "usart_serial_def.h";
        public const string UsartSerialInit = "usart_serial_init.c";
        public const string MainFileName = "main.c";
        public const string PortInit = "PortConfig.c";
        public const string SpiInit = "spi_init.c";
        public const string SpiDefine = "spi_def.h";

        public static List<string> List
        {
            get
            {
                return new List<string>()
                    {
                        UsartInteruptHandler,
                        UsartInteruptInit,
                        UsartSerialDefines,
                        UsartSerialInit,
                        MainFileName,
                        PortInit,
                        SpiInit,
                        SpiDefine
                    };
            }
        }
    }
}
