namespace CodeWizard.DataModel.UsarModel
{
    public class UsartSettings
    {
        public string SelectedMode
        {
            get;
            set;
        }

        public string SelectedDataBitLength
        {
            get;
            set;
        }

        public string SelectedBaudRate
        {
            get;
            set;
        }

        public string SelectedParityMode
        {
            get;
            set;
        }

        public bool IsTwoStopBits { get; set; }

        public bool RxCompleteIntEnabled
        {
            get;
            set;
        }

        public bool TxCompleteIntEnabled
        {
            get;
            set;
        }

        public bool DataReceivedIntEnabled
        {
            get;
            set;
        }

        public string SelectedRxInteruptLevel
        {
            get;
            set;
        }

        public string SelectedTxInteruptLevel
        {
            get;
            set;
        }

        public string SelectedDreInteruptLevel
        {
            get;
            set;
        }

        public string SelectedDemo
        {
            get;
            set;
        }
    }
}
