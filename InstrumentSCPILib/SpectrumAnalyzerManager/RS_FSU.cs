using Ivi.Visa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentSCPILib.SpectrumAnalyzerManager
{
    public  class RS_FSU : InstrumentManager, ISpectrumAnalyzer
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
        /// 获取中心频率  
        /// </summary>
        /// <returns></returns>
        public  double GetCenterFreq()
        {
            string recvMsg = null;
            try
            {
                recvMsg = base.WriteAndReadString("FREQ:CENT?");         
                return Convert.ToDouble(recvMsg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }


        /// <summary>
        /// 获取MKA  峰值电平
        /// </summary>
        /// <returns></returns>
        public  double GetMKA()
        {
            string sendMsg = null;
            try
            {
                sendMsg = base.WriteAndReadString("CALC:MARK:Y?");
                return Convert.ToDouble(sendMsg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }

        }

        /// <summary>
        /// 设置中心频率  
        /// </summary>
        /// <param name="unit">频率单位</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public  bool SetCenterFreq(FrequencyUnit unit, double value)
        {
            string sendMsg = "FREQ:CENT " + value;
            switch (unit)
            {
                case FrequencyUnit.Hz:
                    sendMsg += "Hz";
                    break;
                case FrequencyUnit.KHz:
                    sendMsg += "KHz";
                    break;
                case FrequencyUnit.MHz:
                    sendMsg += "MHz";
                    break;
                case FrequencyUnit.GHz:
                    sendMsg += "GHz";
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
        /// 设置RBW  
        /// </summary>
        /// <param name="value">分辨率带宽，单位KHz</param>
        /// <returns></returns>
        public  bool SetRBW(double value)
        {
            string sendMsg = "BAND:RES " + value + "kHz;";
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
        /// 设置自动/手动 RBW  
        /// </summary>
        /// <param name="OnOff">自动、手动RBW</param>
        /// <returns></returns>
        public  bool SetAutoRBW(bool OnOff)
        {
            string sendMsg = null;
            if (OnOff)
                sendMsg = "BAND:AUTO ON";
            else
                sendMsg = "BAND:AUTO OFF";
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
        /// 设置参考电平 
        /// </summary>
        /// <param name="value">参考电平,单位dBm</param>
        /// <returns></returns>
        public  bool SetRefLevel(double value)
        {
            string sendMsg = "DISP:WIND:TRAC:Y:RLEV " + value + "dBm;";
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
        /// 激活标记并搜索峰值  
        /// </summary>
        /// <returns></returns>
        public  bool MarkPeak()
        {
            string sendMsg = "CALC:MARK:MAX";
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
        /// 设置自动衰减
        /// </summary>
        /// <returns></returns>
        public  bool SetAutoAttenuation()
        {
            string sendMsg = "INP:ATT:AUTO ON";
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
        ///  设置手动衰减值
        /// </summary>
        /// <param name="value">衰减值 单位DB</param>
        /// <returns></returns>
        public  bool SetManulAttenuation(double value)
        {
            string sendMsg = "INP:ATT " + value + "DB";
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
        /// 设置SPAN
        /// </summary>
        /// <param name="value">带宽</param>
        /// <param name="unit">单位</param>
        /// <returns></returns>
        public  bool SetSpan(double value, FrequencyUnit unit)
        {
            string sendMsg = "FREQ:SPAN " + value+ unit.ToString();
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
        /// 线损设置 REF LVL OFFSET
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public  bool SetRefOffset(double value)
        {
            string sendMsg = " ";
            try
            {
                base.WriteString(sendMsg);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
