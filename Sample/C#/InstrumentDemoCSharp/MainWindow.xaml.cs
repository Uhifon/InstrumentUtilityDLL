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
           


            IComprehensiveMeter meter =   ComprehensiveMeter.GetInstance(InstrumentUtilityDotNet.ComprehensiveMeterType.Aglient_8920);
           bool res = meter.Connect("TCPIP0::192.168.0.10::5000::SOCKET");


            meter.GetID();

            ISignalSource signalSource = SignalSource.GetInstance(InstrumentUtilityDotNet.SignalSourceType.RS_SMBV100A);
            signalSource.GetID();


            //instrument = new InstrumentUtilityDotNet.InstrumentManager();
            //string addr = this.cmbGPIBBoard.Text + "::"+this.tbGPIBID.Text + "::INSTR";    //GPIB0::18::INSTR
            //bool res = instrument.InitiateIO488(addr);  
            //if (res)
            //{
            //    this.btnConnect.Background = new SolidColorBrush(Colors.YellowGreen);
            //    this.btnConnect.Content = "连接成功";
            //    isConnected = true;
            //}
            //else
            //{
            //    this.btnConnect.Background = new SolidColorBrush(Colors.Transparent);
            //    this.btnConnect.Content = "连接失败";
            //    isConnected = false;
            //}
        }


        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            //if (!isConnected)
            //    MessageBox.Show("通讯失败！");
            //else
            //{
            //    instrument.WriteString(this.tbSend.Text);

            //}
        }

        private void btnSendAndRecv_Click(object sender, RoutedEventArgs e)
        {
            //if (!isConnected)
            //    MessageBox.Show("通讯失败！");
            //else
            //{
            //   this.tbRecv.Text= instrument.WriteAndReadString(this.tbSend.Text);

            //}

        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }

        private void btnFindResouce_Click(object sender, RoutedEventArgs e)
        {
            InstrumentUtilityDotNet.InstrumentManager instrumentManager = new InstrumentUtilityDotNet.InstrumentManager();
            string[] str = instrumentManager.FindAllResource();
        }
    }
}
