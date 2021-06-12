using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstrumentUtilityDotNet.SignalSourceManager
{
    class AV1431:ISignalSource
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
                sendMsg = "OUTPut:STATe ON";
            else
                sendMsg = "OUTPut:STATe OFF";
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

            string sendMsg = "FREQ:CW " + freq;
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

            string sendMsg = "FREQ:CW " + freq;
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
        /// 打开脉冲发生器
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
        /// <param name="width">带宽(范围:10ns – 60s-10ns),单位:us 复位10us</param> 
        /// <returns></returns>
        public override bool SetPulseWidth(double width)
        {
            //PULSe:WIDTh 200us
            string sendMsg = "PULM:INTernal:PWIDth " + width + "us";
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
        /// <param name="period">带宽(范围:20ns – 60s),单位:us 复位20us</param>
        /// <returns></returns>
        public override bool SetPulsePeriod(double period)
        {
            //PULSe:PERiod 100us
            string sendMsg = "PULM:INTernal:PERiod " + period + "us";
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
