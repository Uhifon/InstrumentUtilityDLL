using Ivi.Visa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentUtilityDotNet.SpectrumAnalyzerManager
{
    public  class RS_FSU :  ISpectrumAnalyzer
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
        /// 获取中心频率 
        /// </summary>
        /// <returns></returns>
        public override double GetCenterFreq()
        {
            string recvMsg = null;
            try
            {
                recvMsg = base.WriteAndReadString("FREQ:CENT?");         
                return Convert.ToDouble(recvMsg);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        /// <summary>
        /// 获取MKA  峰值电平
        /// </summary>
        /// <returns></returns>
        public override double GetMKA()
        {
            string sendMsg = null;
            try
            {
                sendMsg = base.WriteAndReadString("CALC:MARK:Y?");
                return Convert.ToDouble(sendMsg);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        /// <summary>
        /// 设置中心频率  
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="unit">频率单位</param>
        /// <returns></returns>
        public override bool SetCenterFreq( double value, FrequencyUnit unit)
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
                throw (ex);
            }
        }
        /// <summary>
        /// 设置RBW  
        /// </summary>
        /// <param name="isAuto">自动/手动RBW</param>
        /// <param name="value">分辨率带宽，单位KHz</param>
        /// <returns></returns>
        public override bool SetRBW(bool isAuto, double value = 0 )
        {
            string sendMsg = null;
            try
            {
                if (isAuto)
                {
                    sendMsg = "BAND:AUTO ON";
                    base.WriteString(sendMsg);
                }
                else
                {
                    sendMsg = "BAND:AUTO OFF;BAND:RES " + value + "kHz;";
                    base.WriteString(sendMsg);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
 

        /// <summary>
        /// 设置参考电平 
        /// </summary>
        /// <param name="value">参考电平,单位dBm</param>
        /// <returns></returns>
        public override bool SetRefLevel(double value)
        {
            string sendMsg = "DISP:WIND:TRAC:Y:RLEV " + value + "dBm;";
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
        /// 激活标记并搜索峰值  
        /// </summary>
        /// <returns></returns>
        public override bool MarkPeak()
        {
            string sendMsg = "CALC:MARK:MAX";
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
        /// 设置衰减
        /// </summary>
        /// <param name="isAuto">自动/手动衰减</param>
        /// <param name="value">衰减值 单位DB</param>
        /// <returns></returns>
        public override bool SetAttenuation(bool isAuto, double value =0 )
        {
            string sendMsg = null;
            if (isAuto)
            {
                sendMsg = "INP:ATT:AUTO ON";
            }
            else
            {
                sendMsg = "INP:ATT " + value + "DB" ;
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
        /// 设置SPAN
        /// </summary>
        /// <param name="value">带宽</param>
        /// <param name="unit">单位</param>
        /// <returns></returns>
        public override bool SetSpan(double value, FrequencyUnit unit)
        {
            string sendMsg = "FREQ:SPAN " + value+ unit.ToString();
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
        /// 线损设置 REF LVL OFFSET
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool SetRefOffset(double value)
        {
            string sendMsg = " ";
            try
            {
                base.WriteString(sendMsg);
                return false;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
