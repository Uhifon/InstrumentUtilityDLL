using System;

namespace InstrumentUtilityDotNet.SpectrumAnalyzerManager
{
    public class Agilent_856x :  ISpectrumAnalyzer
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
        /// 获取中心频率  
        /// </summary>
        /// <returns></returns>
        public override double GetCenterFreq()
        {
            string recvMsg = null;
            try
            {
                recvMsg = base.WriteAndReadString("CF?;");
                if (recvMsg != "")
                    return Convert.ToDouble(recvMsg);
                else
                    return 0;
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
            string recvMsg = null;
            try
            {
                recvMsg = base.WriteAndReadString("MKA?;");
                double x = -999;
                bool res = double.TryParse(recvMsg, out x);
                if (res)
                    return x;
                else
                    return -999;
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
        public override bool SetCenterFreq(double value, FrequencyUnit unit)
        {
            string sendMsg = "CF  " + value;
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
                return base.WriteString(sendMsg);
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
        public override bool SetRBW(bool isAuto, double value = 0)
        {
            string sendMsg = null;
            try
            {
                if (isAuto)
                {
                    sendMsg = "BAND:AUTO ON";
                    return base.WriteString(sendMsg);
                }
                else
                {
                    sendMsg = "BAND:AUTO OFF;RB " + value + "kHz;";
                    return base.WriteString(sendMsg);
                }
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
            string sendMsg = "RL " + value + "dBm;";
            try
            {
                return base.WriteString(sendMsg);
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
        public override bool MarkPeak()
        {
            string sendMsg = "TS;MKPK HI;";
            try
            {
                return base.WriteString(sendMsg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 设置衰减
        /// </summary>
        /// <param name="isAuto">自动/手动衰减</param>
        /// <param name="value">衰减值 单位DB</param>
        /// <returns></returns>
        public override bool SetAttenuation(bool isAuto,double value)
        {
            string sendMsg = null;
            if (isAuto)
            {
                sendMsg = "AT AUTO";
            }
            else
            {
                sendMsg = "AT MANUAL;AT  " + value + "DB";
            }
            try
            {
                return base.WriteString(sendMsg);
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
        public override bool SetSpan(double value,FrequencyUnit unit)
        {
            string sendMsg = "SP " + value + unit.ToString();
            try
            {
                return base.WriteString(sendMsg);
 
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
        public override bool SetRefOffset(double value)
        {
            string sendMsg = "ROFFSET " + value +"dB";
            try
            {
                return base.WriteString(sendMsg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

    }
}
