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

using InstrumentUtilityDotNet;
using InstrumentUtilityDotNet.PowerMeterManager;
using InstrumentUtilityDotNet.SpectrumAnalyzerManager;
using InstrumentUtilityDotNet.SignalSourceManager;
using InstrumentUtilityDotNet.SynthesizeMeterManager;
using System.Threading;

namespace InstrumentDemoCsharp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isConnected = false;
        InstrumentUtilityDotNet.InstrumentManager instrument;

        bool isSpectrumMeterConnected = false;
        bool isSignalSourceConnected = false;
        bool isPowerMeterConnected = false;
        bool isSynthesizeMeterConnected = false;

        ISpectrumAnalyzer spectrumAnalyzer;
        ISignalSource signalSource;
        IPowerMeter powerMeter;
        ISynthesizeMeter synthesizeMeter;

        SolidColorBrush sucessColor = new SolidColorBrush(Colors.LawnGreen);
        SolidColorBrush normalColor = new SolidColorBrush(Colors.AliceBlue);


        public MainWindow()
        {
            InitializeComponent();
            instrument = new InstrumentManager();
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!isConnected)
                {
                    isConnected = instrument.Open(tbAddr.Text);
                    if (isConnected)
                        this.btnConnect.Background = sucessColor;
                    else
                        this.btnConnect.Background = normalColor;
                }
                else
                    this.btnConnect.Background = sucessColor;
            }
            catch (Exception ex)
            {
                isConnected = false;
                this.btnConnect.Background = normalColor;
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

        #region 频谱仪
        private void btnSpeOpen_Click(object sender, RoutedEventArgs e)
        {
            SpectrumAnalyzerType type = (SpectrumAnalyzerType)(this.cmbSpeType.SelectedIndex);
            string addr = this.tbSpeAddr.Text;
            if (!isSpectrumMeterConnected)
            {
                spectrumAnalyzer = SpectrumAnalyzer.GetInstance(type);
                isSpectrumMeterConnected = spectrumAnalyzer.Connect(addr);
                if (isSpectrumMeterConnected)
                    this.btnSpeOpen.Background = sucessColor;
                else
                    this.btnSpeOpen.Background = normalColor;
            }
            else
                this.btnSpeOpen.Background = sucessColor;
        }

        private void btnSpeClose_Click(object sender, RoutedEventArgs e)
        {
            if (isSpectrumMeterConnected)
            {
                spectrumAnalyzer.DisConnect();
            }
            this.btnSpeOpen.Background = normalColor;
        }

        private void btnSpeReset_Click(object sender, RoutedEventArgs e)
        {
            if (!isSpectrumMeterConnected)
                return;
            spectrumAnalyzer.Reset();
        }

        private void btnSpeIDQuery_Click(object sender, RoutedEventArgs e)
        {
            if (!isSpectrumMeterConnected)
                return;
            tbSpeID.Text = spectrumAnalyzer.GetID();
        }

        private void btnSpeCentFreqSet_Click(object sender, RoutedEventArgs e)
        {
            if (!isSpectrumMeterConnected)
                return;
            double freq = Convert.ToDouble(this.tbSpeCentFreq.Text);
            FrequencyUnit unit = (FrequencyUnit)this.cmbSpeCentFreqUnit.SelectedIndex;
            spectrumAnalyzer.SetCenterFreq(freq, unit);
        }

        private void btnSpeSpanSet_Click(object sender, RoutedEventArgs e)
        {
            if (!isSpectrumMeterConnected)
                return;
            double span = Convert.ToDouble(this.tbSpeSpan.Text);
            FrequencyUnit unit = (FrequencyUnit)this.cmbSpeSpanUnit.SelectedIndex;
            spectrumAnalyzer.SetSpan(span, unit);
        }

        private void btnSpeRefLevelSet_Click(object sender, RoutedEventArgs e)
        {
            if (!isSpectrumMeterConnected)
                return;
            double refLevel = Convert.ToDouble(this.tbSpeRefLevel.Text);
            spectrumAnalyzer.SetRefLevel(refLevel);
        }

        private void btnSpeRBWSet_Click(object sender, RoutedEventArgs e)
        {
            if (!isSpectrumMeterConnected)
                return;
            bool b = (bool)rbSpeRBWAuto.IsChecked;
            double rbw = Convert.ToDouble(this.tbSpeRBW.Text);
            spectrumAnalyzer.SetRBW(b, rbw);
        }

        private void btnSpeMKA_Click(object sender, RoutedEventArgs e)
        {
            if (!isSpectrumMeterConnected)
                return;
            spectrumAnalyzer.MarkPeak();
            Thread.Sleep(1000);
            this.tbSpeMKA.Text = spectrumAnalyzer.GetMKA().ToString();
        }
        #endregion

        #region 信号源
        private void btnSigOpen_Click(object sender, RoutedEventArgs e)
        {
            SignalSourceType type = (SignalSourceType)(this.cmbSigType.SelectedIndex);
            string addr = this.tbSigAddr.Text;
            if (!isSignalSourceConnected)
            {
                signalSource = SignalSource.GetInstance(type);
                isSignalSourceConnected = signalSource.Connect(addr);
                if (isSignalSourceConnected)
                    this.btnSigOpen.Background = sucessColor;
                else
                    this.btnSigOpen.Background = normalColor;
            }
            else
                this.btnSigOpen.Background = sucessColor;
        }

        private void btnSigClose_Click(object sender, RoutedEventArgs e)
        {
            if (isSignalSourceConnected)
            {
                signalSource.DisConnect();
            }
            this.btnSigOpen.Background = normalColor;
        }

        private void btnSigReset_Click(object sender, RoutedEventArgs e)
        {
            if (!isSignalSourceConnected)
                return;
            signalSource.Reset();
        }

        private void btnSigIDQuery_Click(object sender, RoutedEventArgs e)
        {
            if (!isSignalSourceConnected)
                return;
            tbSigID.Text = signalSource.GetID();
        }

        private void btnSigFreqPowerSet_Click(object sender, RoutedEventArgs e)
        {
            if (!isSignalSourceConnected)
                return;
            double freq = Convert.ToDouble(this.tbSigFreq.Text);
            double power = Convert.ToDouble(this.tbSigPower.Text);
            FrequencyUnit unit = (FrequencyUnit)this.cmbSigFreqUnit.SelectedIndex;
            signalSource.SetFreqAndLevel(unit, freq, power);
        }
        private void btnSigTriggerModeSet_Click(object sender, RoutedEventArgs e)
        {
            if (this.cmbSigTriggerMode.SelectedIndex == 1)
            {
                signalSource.SetPulseSwitch(true);
                double width = Convert.ToDouble(this.tbSigPulseWidth.Text);
                double period = Convert.ToDouble(this.tbSigPulsePeriod.Text);
                signalSource.SetPulseWidth(width);
                signalSource.SetPulsePeriod(period);
            }
            else
            {
                signalSource.SetPulseSwitch(false);
            }   
        }
        private void cmbSigTriggerMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!this.IsLoaded)
                return;
            if (cmbSigTriggerMode.SelectedIndex == 0)
            {
                tbSigPulseWidth.IsEnabled = false;
                tbSigPulsePeriod.IsEnabled = false;
            }
            else
            {
                tbSigPulseWidth.IsEnabled = true;
                tbSigPulsePeriod.IsEnabled = true;
            }

        }

        private void cmbSigPluseSwitch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isSignalSourceConnected)
                return;
             if(this.cmbSigPluseSwitch.SelectedIndex==0)
                 signalSource.SetPulseSwitch(false);
             else
                signalSource.SetPulseSwitch(true);
        }

        private void cmbSigModulationSwitch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isSignalSourceConnected)
                return;
            if (this.cmbSigModulationSwitch.SelectedIndex == 0)
                signalSource.SetModulationSwitch(false);
            else
                signalSource.SetModulationSwitch(true);
        }

        #endregion

        #region 功率计
        private void btnPowerOpen_Click(object sender, RoutedEventArgs e)
        {
            PowerMeterType type = (PowerMeterType)(this.cmbPowerType.SelectedIndex);
            string addr = this.tbPowerAddr.Text;
            if (!isPowerMeterConnected)
            {
                powerMeter = PowerMeter.GetInstance(type);
                isPowerMeterConnected = powerMeter.Connect(addr);
                if (isPowerMeterConnected)
                    this.btnPowerOpen.Background = sucessColor;
                else
                    this.btnPowerOpen.Background = normalColor;
            }
            else
                this.btnPowerOpen.Background = sucessColor;
        }

        private void btnPowerClose_Click(object sender, RoutedEventArgs e)
        {
            if (isPowerMeterConnected)
            {
                powerMeter.DisConnect();
            }
            this.btnPowerOpen.Background = normalColor;
        }

        private void btnPowerReset_Click(object sender, RoutedEventArgs e)
        {
            if (!isPowerMeterConnected)
                return;
            powerMeter.Reset();
        }

        private void btnPowerIDQuery_Click(object sender, RoutedEventArgs e)
        {
            if (!isPowerMeterConnected)
                return;
            tbPowerID.Text = powerMeter.GetID();
        }
        private void btnPowerValueQuery_Click(object sender, RoutedEventArgs e)
        {
            if (!isPowerMeterConnected)
                return;
            int sensorNum = this.cmbPowerSensorNum.SelectedIndex;
            double avg;
            double swr;
            powerMeter.GetPower(sensorNum,out avg,out swr);
            this.tbPowerAvg.Text = avg.ToString();
            this.tbPowerSwr.Text = swr.ToString();
        }
        private void cmbPowerMeterUnit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isPowerMeterConnected)
                return;
            PowerUnit unit = (PowerUnit)this.cmbPowerMeterUnit.SelectedIndex;
            powerMeter.PowerUnitChange(unit);
        }


        #endregion

        #region 综测仪
        private void btnSynthesizeOpen_Click(object sender, RoutedEventArgs e)
        {
            SynthesizeMeterType type = (SynthesizeMeterType)(this.cmbSynthesizeType.SelectedIndex);
            string addr = this.tbSynthesizeAddr.Text;
            if (!isSynthesizeMeterConnected)
            {
                synthesizeMeter = SynthesizeMeter.GetInstance(type);
                isSynthesizeMeterConnected = synthesizeMeter.Connect(addr);
                if (isSynthesizeMeterConnected)
                    this.btnSynthesizeOpen.Background = sucessColor;
                else
                    this.btnSynthesizeOpen.Background = normalColor;
            }
            else
                this.btnSynthesizeOpen.Background = sucessColor;
        }

        private void btnSynthesizeClose_Click(object sender, RoutedEventArgs e)
        {
            if (isSynthesizeMeterConnected)
            {
                synthesizeMeter.DisConnect();
            }
            this.btnSynthesizeOpen.Background = normalColor;
        }

        private void btnSynthesizeReset_Click(object sender, RoutedEventArgs e)
        {
            if (!isSynthesizeMeterConnected)
                return;
            synthesizeMeter.Reset();
        }

        private void btnSynthesizeIDQuery_Click(object sender, RoutedEventArgs e)
        {
            if (!isSynthesizeMeterConnected)
                return;
            tbSynthesizeID.Text = synthesizeMeter.GetID();
        }

        private void btnSynthesizeRefLevelSet_Click(object sender, RoutedEventArgs e)
        {
            if (!isSynthesizeMeterConnected)
                return;
            double level = Convert.ToDouble(this.tbSynthesizeRefLevel.Text);
            synthesizeMeter.SetRefLevel(level);
        }

        private void btnSynthesizeCentFreqSet_Click(object sender, RoutedEventArgs e)
        {
            if (!isSynthesizeMeterConnected)
                return;
            double freq = Convert.ToDouble(this.tbSynthesizeCentFreq.Text);
            FrequencyUnit unit = (FrequencyUnit)this.cmbSynthesizeCentFreqUnit.SelectedIndex;
            synthesizeMeter.SetCenterFreq(freq,unit);
        }

        private void btnSynthesizeAfgStateSet_Click(object sender, RoutedEventArgs e)
        {
            if (!isSynthesizeMeterConnected)
                return;
            if (this.cmbSynthesizeAfgState.SelectedIndex == 0)
                synthesizeMeter.SetAfgState(false);
            else
                synthesizeMeter.SetAfgState(true);
        }

        private void btnSynthesizeCentFreqGet_Click(object sender, RoutedEventArgs e)
        {
            if (!isSynthesizeMeterConnected)
                return;
   
           this.tbSynthesizeCentFreqGet.Text =  synthesizeMeter.GetCenterFreq().ToString();
        }




        #endregion
 

      
    }
}
