using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentUtilityDotNet.SignalSourceManager
{

    /// <summary>
    ///HP8360系列
    /// </summary>
    public class HP_8360 : InstrumentManager, ISignalSource
    {
        /// <summary>
        /// 连接设备
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool Connect(string address)
        {
            return base.InitiateIO488(address);
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void DisConnect()
        {
            base.Close();
        }

        /// <summary>
        ///  Write
        /// </summary>
        /// <param name="command"></param>
        public void WriteCommand(string command)
        {
            base.WriteString(command);
        }

        /// <summary>
        ///  WriteAndRead
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public string WriteAndReadCommand(string command)
        {
            return base.WriteAndReadString(command);
        }
        /// <summary>
        /// 获取设备ID号
        /// </summary>
        public string GetID()
        {
            string sendMsg = "*IDN?";
            try
            {
                return base.WriteAndReadString(sendMsg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 初始化仪表参数
        /// </summary>
        /// <returns></returns>
        public  bool Reset()
        {
            string sendMsg = "*RST";
            try
            {
                base.WriteString(sendMsg);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 信号源发射开关
        /// </summary>
        /// <param name="state">On:打开发射 Off：关闭发射</param>
        /// <returns></returns>
        public  bool SetSignalSourceState(bool state)
        {
            string sendMsg = string.Empty;
            if (state)
                sendMsg = "OUTPut:STATe ON";
            else
                sendMsg = "OUTPut:STATe OFF";
            try
            {
                base.WriteString(sendMsg);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }


        /// <summary>
        /// 设置频率、功率
        /// </summary>
        ///  <param name="unit">频率单位</param>
        /// <param name="freq">频率</param>
        /// <param name="level">功率，单位DBM</param>
        /// <returns></returns>
        public  bool SetFreqAndLevel(FrequencyUnit unit, double freq, double level)
        {
            
            string sendMsg = "FREQ:CW "+ freq;
            switch (unit)
            {
                case FrequencyUnit.Hz:
                    sendMsg += "Hz;";
                    break;
                case FrequencyUnit.KHz:
                    sendMsg += "KHz;";
                    break;
                case FrequencyUnit.MHz:
                    sendMsg += "MHz;";
                    break;
                case FrequencyUnit.GHz:
                    sendMsg += "GHz;";
                    break;
            }
            sendMsg += ":POW:LEV " + level + "DBM";
            try
            {
                base.WriteString(sendMsg);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 设置频率
        /// </summary>
        /// <param name="unit">频率单位</param>
        /// <param name="freq">频率</param>
        /// <returns></returns>
        public  bool SetFreq(FrequencyUnit unit, double freq)
        {

            string sendMsg = "FREQ:CW "+ freq;
            switch (unit)
            {
                case FrequencyUnit.Hz:
                    sendMsg += "Hz;";
                    break;
                case FrequencyUnit.KHz:
                    sendMsg += "KHz;";
                    break;
                case FrequencyUnit.MHz:
                    sendMsg += "MHz;";
                    break;
                case FrequencyUnit.GHz:
                    sendMsg += "GHz;";
                    break;
            }
            try
            {
                base.WriteString(sendMsg);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 设置功率
        /// </summary>
        /// <param name="level">功率</param>
        /// <returns></returns>
        public  bool SetLevel (double level)
        {

            string sendMsg = ":POW:LEV " + level + "DBM";
            try
            {
                base.WriteString(sendMsg);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

 

        /// <summary>
        /// 打开脉冲发生器
        /// </summary>
        /// <param name="onOff"></param>
        /// <returns></returns>
        public  bool SetPulse(bool state)
        {
            string sendMsg = string.Empty;
            if (state)
                sendMsg = "PULM:STATe ON";
            else
                sendMsg = "PULM:STATe OFF";
            try
            {
                base.WriteString(sendMsg);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 设置脉冲信号带宽
        /// </summary>
        /// <param name="width">带宽(范围:1us to 65.5 ms),单位:us</param>
        /// <returns></returns>
        public  bool SetPulseWidth(double width)
        {
            //PULSe:WIDTh 200us
            string sendMsg = "PULM:INTernal:WIDth " + width + "us";
            try
            {
                base.WriteString(sendMsg);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        /// <summary>
        /// 设置脉冲周期（必须大于信号带宽）
        /// </summary>
        /// <param name="period">带宽(范围:2us - 信号带宽，max:65.5 ms),单位:us</param>
        /// <returns></returns>
        public  bool SetPulsePeriod(double period)
        {
            //PULSe:PERiod 100us
            string sendMsg = "PULM:INTernal:PERiod " + period + "us";
            try
            {
                base.WriteString(sendMsg);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }

}
