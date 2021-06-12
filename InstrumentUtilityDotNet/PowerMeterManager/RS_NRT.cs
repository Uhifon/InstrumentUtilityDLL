using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentUtilityDotNet.PowerMeterManager
{
    public class RS_NRT :  IPowerMeter
    {
 
        /// <summary>
        /// 获取设备ID号
        /// </summary>
        public override string GetID()
        {
            string sendMsg = "*IDN?";
            try
            {
                return base.WriteAndReadString(sendMsg);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 初始化仪表参数
        /// </summary>
        /// <returns></returns>
        public override bool Reset()
        {
            string sendMsg = "IP;";
            try
            {
                return base.WriteString(sendMsg);
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 单位切换
        /// </summary>
        /// <param name="unit">DBM,W</param>
        public override bool PowerUnitChange(PowerUnit unit)
        {
            string sendMsg = ":UNIT0:POW " + unit.ToString();
            try
            {
                return base.WriteString(sendMsg);
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 获取功率计DATA 功率AVG与驻波比SWR 功率计 
        /// </summary>
        /// <param name="sensorNum">传感器编号0-2</param>
        /// <param name="avg">功率</param>
        /// <param name="swr">驻波比</param>
        /// <returns></returns>
        public override bool GetPower(int sensorNum, out double avg, out double swr)
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
                throw (ex);
            }

        }
    }
}
