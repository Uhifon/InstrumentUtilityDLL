using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentUtilityDotNet.SynthesizeMeterManager
{
    public class Ceyear_AV4957 :  ISynthesizeMeter
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
            string sendMsg = "*RST";
            try
            {
                base.WriteString(sendMsg);
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 设置中心频率  
        /// </summary>
        /// <param name="value">频率</param>
        /// <param name="unit">频率单位</param>
        /// <returns></returns>
        public override bool SetCenterFreq(double value, FrequencyUnit unit)
        {
            string sendMsg = "FREQ: CENT ";
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
                throw (ex);
            }

        }

        /// <summary>
        /// 设置参考电平  幅度标尺  
        /// </summary>
        /// <param name="value">参考电平 单位dbm</param>
        /// <returns></returns>
        public override bool SetRefLevel(double value)
        {
            string sendMsg = "DISP:WIND:TRAC:Y:RLEV " + value + "DBM"; ;
            try
            {
                base.WriteString(sendMsg);
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 获取中心频率
        /// </summary>
        /// <returns></returns>
        public override double GetCenterFreq()
        {
            string sendMsg = "FREQ:CENT?";
            try
            {
                string recvMsg = base.WriteAndReadString(sendMsg);
                return Convert.ToDouble(recvMsg);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 设置解调开关。 
        /// </summary>
        /// <param name="onOff">音频开关</param>
        /// <returns></returns>
        public override bool SetAfgState(bool onOff)
        {
            string sendMsg = null;
            if (!onOff)
                sendMsg = "DMOD:STAT OFF ";
            else
                sendMsg = "DMOD:STAT ON ";
            try
            {
                base.WriteString(sendMsg);
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

    }
}
