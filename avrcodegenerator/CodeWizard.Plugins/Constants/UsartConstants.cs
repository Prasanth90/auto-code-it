namespace PeripheralConfig.Constants
{
	public class UsartConstants
	{
        public const string UsartName = "$UsartName$";
        public const string BaudRate = "$BaudRate$";
        public const string CharLength = "$UsartCharSize$";
        public const string Parity = "$UsartParityMode$";
        public const string StopBit = "$UsartStopBit$";
        public const string InteruptLevel = "$InteruptLevel$";
        public const string InteruptType = "$IntType$";
        public const string UsartSerialInit = "$UsartSerialInit$";
        public const string InteruptInit = "$InteruptInit$";
	}

    public class MainFileConstants
    {
        public const string HashDefines = "$HashDefines$";
        public const string FunctionDeclaration = "$FunctionDeclarations$";
        public const string FunctionCalls = "$FunctionCalls$";
        public const string FunctionDefines = "$FunctionDefines$";
        public const string InteruptHandler = "$InteruptHandler$";
    }
}
