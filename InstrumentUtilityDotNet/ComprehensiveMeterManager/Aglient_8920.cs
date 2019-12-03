using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentUtilityDotNet.ComprehensiveMeterManager
{
    public class Aglient_8920 :InstrumentManager,IComprehensiveMeter
    {
        /// <summary>
        /// 连接设备
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool  Connect(string address)
        {
            return  base.InitiateIO488(address);
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void  DisConnect()
        {
            base.Close();
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
        /// 初始化仪表参数
        /// </summary>
        /// <returns></returns>
        public bool Reset()
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
        /// 设置中心频率  
        /// </summary>
        /// <param name="unit">频率单位</param>
        /// <param name="value">频率</param>
        /// <returns></returns>
        public   bool SetCenterFreq(FrequencyUnit unit, double value)
        {

            string sendMsg = "RFG: FREQ ";
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
        /// 设置参考电平  幅度标尺  
        /// </summary>
        /// <param name="value">参考电平 单位dbm</param>
        /// <returns></returns>
        public   bool SetRefLevel(double value)
        {
            string sendMsg = "RFG:AMPL " + value + "DBM;";
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
        public   double GetCenterFreq()
        {
            string sendMsg = "MEAS: AFR: FREQ ?;";
            try
            {
                string recvMsg = base.WriteAndReadString(sendMsg);
                return Convert.ToDouble(recvMsg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }


        /// <summary>
        /// 设置解调开关
        /// </summary>
        /// <param name="onOff"> 设置解调开关</param>
        /// <returns></returns>
        public   bool SetAfgState(bool onOff)
        {
            string sendMsg = null;
            if (!onOff)
                sendMsg = "AFG1:FM:STAT OFF";
            else
                sendMsg = "AFG1:FM:STAT ON";
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
