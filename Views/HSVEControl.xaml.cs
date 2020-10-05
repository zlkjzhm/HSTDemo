using System.Windows.Controls;

namespace HSTDemo
{
    /// <summary>
    /// HSVEControl.xaml 的交互逻辑
    /// </summary>
    public partial class HSVEControl : UserControl
    {
        public double XDSD { get; set; }
        public string sXDSD { get; set; }

        public HSVEControl(double xdsd)
        {

            InitializeComponent();
            XDSD = xdsd * 100;
            sXDSD = XDSD.ToString("0") + "%";
            DataContext = this;
        }
    }
}
