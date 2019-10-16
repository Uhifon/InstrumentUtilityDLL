using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentSCPILib.SignalSourceManager
{
    /// <summary>
    ///  E440系列 Old
    /// </summary>
    public class Agilent_E4400:InstrumentManager , ISignalSource
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
            string sendMsg = "ID?;";
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
            string sendMsg = "IP;";
            try
            {
                base.WriteString(sendMsg);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;            }
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
                sendMsg = "OUTP:ALL ON";
            else
                sendMsg = "OUTP:ALL OFF";
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
            string sendMsg = "*RST;FREQ:CW "+freq;
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
            sendMsg += "POW:LEV " + level + "DBM";
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

            string sendMsg = "*RST;FREQ:CW "+freq;
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
        public  bool SetLevel(double level)
        {

            string sendMsg = "POW:LEV " + level + "DBM";
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
        /// 打开脉冲
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
        /// <param name="width">信号带宽（8us-脉冲周期） 单位:us</param>
        /// <returns></returns>
        public  bool SetPulseWidth(double width)
        {
   
            string sendMsg = "PULM:INTernal:PWIDth " + width + "us";         //INTernal可去掉
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
        /// <param name="period">周期(16us-30s) 单位:us</param>
        /// <returns></returns>
        public  bool SetPulsePeriod(double period)
        {
          
            string sendMsg = "PULM:INTernal:PERiod " + period + "us";         //INTernal可去掉
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
