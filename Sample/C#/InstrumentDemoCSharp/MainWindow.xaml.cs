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
using InstrumentUtilityDotNet.ComprehensiveMeterManager;
using InstrumentUtilityDotNet.PowerMeterManager;
using InstrumentUtilityDotNet.SpectrumAnalyzerManager;
using InstrumentUtilityDotNet.SignalSourceManager;


namespace InstrumentDemoCsharp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isConnected = false;
        InstrumentUtilityDotNet.InstrumentManager instrument;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            lbInfo.Content = null;
            IComprehensiveMeter meter =   ComprehensiveMeter.GetInstance(InstrumentUtilityDotNet.ComprehensiveMeterType.Aglient_8920);
            try
            {
                isConnected = meter.Connect(tbAddr.Text);
                if (isConnected)
                    lbInfo.Content = "连接成功！仪表ID：" + meter.GetID();
                else
                    lbInfo.Content = "连接失败！";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                try
                {
                    instrument.WriteString(this.tbSend.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnSendAndRecv_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                try
                {
                    this.tbRecv.Text = instrument.WriteAndReadString(this.tbSend.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
 
        private void btnFindResouce_Click(object sender, RoutedEventArgs e)
        {
            cmbAddress.Items.Clear();
            InstrumentUtilityDotNet.InstrumentManager instrumentManager = new InstrumentUtilityDotNet.InstrumentManager();
            string[] arr = instrumentManager.FindAllResource();
            foreach (string addr in arr)         
                cmbAddress.Items.Add(addr);
        }
    }
}
