using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HSTDemo
{
    /// <summary>
    /// XDSDVEControl.xaml 的交互逻辑
    /// </summary>
    public partial class XDSDVEControl : UserControl
    {
        public double XDSD { get; set; }
        public string sXDSD { get; set; }

        public XDSDVEControl(double xdsd)
        {
           
            InitializeComponent();
            XDSD = xdsd * 100;
            sXDSD =XDSD.ToString("0") + "%";
            DataContext = this;
        }
    }
}
