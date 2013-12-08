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

        public const string TimerInit = "timer_init.c";
        public const string TimerChannelInit = "timer_ch_init.c";
        public const string TimerChannelInitFuncCall = "timer_ch_init_funccall.c";
        public const string TimerChannelInteruppt = "timerchannel_interrupt.c";
        public const string TimerInteruptHandler = "timer_interupt_handler.c";
        public const string TimerModeInitFile = "timer_mode_init.c";
        public const string TimerInteruptCallBackDec = "timer_interupt_handler_dec.c";

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
                        SpiDefine,
                        TimerInit,
                        TimerChannelInit,
                        TimerChannelInitFuncCall,
                        TimerChannelInteruppt,
                        TimerInteruptHandler,
                        TimerModeInitFile,
                        TimerInteruptCallBackDec
                    };
            }
        }
    }
}
