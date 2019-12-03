using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentUtilityDotNet.PowerMeterManager
{
    public class RS_NRT : InstrumentManager, IPowerMeter
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
        public bool Reset()
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
                return false;
            }
        }

        /// <summary>
        /// 单位切换
        /// </summary>
        /// <param name="unit">DBM,W</param>
        public bool PowerUnitChange(PowerUnit unit)
        {
            string sendMsg = ":UNIT0:POW " + unit.ToString();
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
        /// 获取功率计DATA 功率AVG与驻波比SWR 功率计 
        /// </summary>
        /// <param name="sensorNum">传感器编号0-2</param>
        /// <param name="avg">功率</param>
        /// <param name="swr">驻波比</param>
        /// <returns></returns>
        public bool GetPower(int sensorNum, out double avg, out double swr)
        {
            avg = 0;
            swr = 0;
            string sendMsg = String.Format(":TRIG;*WAI;:SENS{0}:DATA?", sensorNum);
            try
            {
                string recvStr = base.WriteAndReadString(sendMsg);
                string[] data = recvStr.Split(',');
                avg = Math.Round(Convert.ToDouble(data[0]), 2);
                swr = Math.Round(Convert.ToDouble(data[1]), 2);
                return true;
            }
            catch (Exception ex)
            {
                avg = 0;
                swr = 0;
                Console.WriteLine(ex.Message);
                return false;
            }

        }
    }
}
