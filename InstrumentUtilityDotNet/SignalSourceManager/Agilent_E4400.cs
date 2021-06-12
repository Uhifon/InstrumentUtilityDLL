using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentUtilityDotNet.SignalSourceManager
{
    /// <summary>
    ///  E440系列 Old
    /// </summary>
    public class Agilent_E4400: ISignalSource
    {
        /// <summary>
        /// 获取设备ID号
        /// </summary>
        public override string GetID()
        {
            string sendMsg = "ID?;";
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
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 信号源发射开关
        /// </summary>
        /// <param name="state">On:打开发射 Off：关闭发射</param>
        /// <returns></returns>
        public override bool SetSignalSourceState(bool state)
        {
            string sendMsg = string.Empty;
            if (state)
                sendMsg = "OUTP:ALL ON";
            else
                sendMsg = "OUTP:ALL OFF";
            try
            {
                return base.WriteString(sendMsg);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        /// <summary>
        /// 设置频率、功率
        /// </summary>
        ///  <param name="unit">频率单位</param>
        /// <param name="freq">频率</param>
        /// <param name="level">功率，单位DBM</param>
        /// <returns></returns>
        public override bool SetFreqAndLevel(FrequencyUnit unit, double freq, double level)
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
                return base.WriteString(sendMsg);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 设置频率
        /// </summary>
        /// <param name="unit">频率单位</param>
        /// <param name="freq">频率</param>
        /// <returns></returns>
        public override bool SetFreq(FrequencyUnit unit, double freq)
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
                return base.WriteString(sendMsg);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 设置功率
        /// </summary>
        /// <param name="level">功率</param>
        /// <returns></returns>
        public override bool SetLevel(double level)
        {

            string sendMsg = "POW:LEV " + level + "DBM";
            try
            {
                return base.WriteString(sendMsg);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 打开脉冲
        /// </summary>
        /// <param name="onOff"></param>
        /// <returns></returns>
        public override bool SetPulseSwitch(bool state)
        {
            string sendMsg = string.Empty;
            if (state)
                sendMsg = "PULM:STATe ON";
            else
                sendMsg = "PULM:STATe OFF";
            try
            {
                return base.WriteString(sendMsg);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 设置脉冲信号带宽
        /// </summary>
        /// <param name="width">信号带宽（8us-脉冲周期） 单位:us</param>
        /// <returns></returns>
        public override bool SetPulseWidth(double width)
        {
   
            string sendMsg = "PULM:INTernal:PWIDth " + width + "us";         //INTernal可去掉
            try
            {
                return base.WriteString(sendMsg);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        /// <summary>
        /// 设置脉冲周期（必须大于信号带宽）
        /// </summary>
        /// <param name="period">周期(16us-30s) 单位:us</param>
        /// <returns></returns>
        public override bool SetPulsePeriod(double period)
        {
          
            string sendMsg = "PULM:INTernal:PERiod " + period + "us";         //INTernal可去掉
            try
            {
                return base.WriteString(sendMsg);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 设置调制开关
        /// </summary>
        /// <param name="onOff"></param>
        /// <returns></returns>
        public override bool SetModulationSwitch(bool state)
        {
            string sendMsg = string.Empty;
            if (state)
                sendMsg = "OUTPut:MODulation ON";
            else
                sendMsg = "OUTPut:MODulation OFF";
            try
            {
                return base.WriteString(sendMsg);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
