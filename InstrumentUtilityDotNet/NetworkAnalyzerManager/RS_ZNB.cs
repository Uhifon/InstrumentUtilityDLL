using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstrumentUtilityDotNet.NetworkAnalyzerManager
{
    public class RS_ZNB : INetworkAnalyzer 
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
        /// 设置开始频率
        /// </summary>
        /// <param name="freq">Hz</param>
        public override bool SetStartFreq(ulong freq)
        {
            string sendMsg = ":SENS1:FREQuency:STARt " + freq.ToString();
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
        /// 设置终止频率
        /// </summary>
        /// <param name="freq">Hz</param>
        public override bool SetStopFreq(ulong freq)
        {
            string sendMsg = ":SENS1:FREQuency:STOP " + freq.ToString();
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
        /// 设置测量点数
        /// </summary>
        /// <param name="num">2到20001</param>
        public override bool SetSweepPoints(int num)
        {
            string sendMsg = ":SENS1:SWE:POIN " + num.ToString();
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
        /// 设置中心频率
        /// </summary>
        /// <param name="freq">Hz</param>
        public override bool SetCenterFreq(ulong freq)
        {
            string sendMsg = ":SENS1:FREQ:CENT " + freq.ToString();
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
        /// 设置带宽
        /// </summary>
        /// <param name="span">Hz</param>
        public override bool SetSpan(ulong span)
        {
            string sendMsg = ":SENS1:FREQuency:SPAN " + span.ToString();         
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
        /// (显示参数)对选择的通道(Ch)设置选择迹线(Tr)的测量参数。
        /// </summary>
        /// <param name="para">"S21":测试频段,"S11":测试频段</param>
        /// <returns></returns>
        public override bool SetDEFine(string para="S21")
        {
            string sendMsg = ":CALC1:PAR1:DEF " + para;         
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
        /// 设置数据格式
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public override bool SetDataFormat(Format format)
        {
            string sendMsg = "CALC1:FORM " + format.ToString();
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
        /// 标记类型设置
        /// </summary>
        /// <returns></returns>
        public override bool SetMarkType(int markID, MARKType type)
        {
            string sendMsg1 = string.Format(":CALC1:MARK{0}:FUNC:TYPE {1}", markID, type);
            string sendMsg2 = string.Format(":CALC1:MARK{0} ON", markID);
            string sendMsg3 = string.Format(":CALC1:MARK{0}:FUNC:TRAC ON", markID);
           
            try
            {
                bool ret1 =  base.WriteString(sendMsg1);
                bool ret2 = base.WriteString(sendMsg2);
                bool ret3 = base.WriteString(sendMsg3);
                return ret1 && ret2 && ret3;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 获取Mark点Y值
        /// </summary>
        /// <returns></returns>
        public override bool ReadMarkY(int markID, out double[] values)
        {
            values = new double[2];
            string sendMsg = string.Format(":CALC1:MARK{0}:Y?", markID);
            try
            {
                string data = base.WriteAndReadString(sendMsg);
                //返回示例： +3.94924525217E-003,+0.00000000000E+000 数据（0）：标记位置的响应值（主值）。 数据（1）：标记位置的响应值（次值）。数据格式不是Smith图表格式或极性格式时数据值始终为0。
                if (data != "")
                {
                    string[] arr = data.Split(',');
                    values[0]= double.Parse(arr[0]);
                    values[1]= double.Parse(arr[1]);
                    return true;
                }  
                else
                    return false;
            }
            catch (Exception ex)
            {
              
                return false;
            }
        }

     


        /// <summary>
        /// 设置选择通道(Ch)的功率电平
        /// </summary>
        /// <param name="level">功率dBm</param>
        /// <returns></returns>
        public override bool SetLevel(double level)
        {
            string sendMsg = "SOUR1:POW:LEV " + level.ToString();
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
        /// 射频开关
        /// </summary>
        /// <param name="onoff"></param>
        /// <returns></returns>
        public override bool SetRFOut(bool onoff)
        {
            string sendMsg = "";
            if (onoff)
                sendMsg = ":OUTP ON";
            else
                sendMsg = ":OUTP OFF";
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
        /// 设置触发模式
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public override bool SetTriggerMode(TriggerMode mode)
        {
            string sendMsg = "";
            switch (mode)
            {
                case TriggerMode.Hold:
                    sendMsg = ":INIT1:CONT OFF";
                    break;
                case TriggerMode.Single:
                    sendMsg = ":INIT1";
                    break;
                case TriggerMode.Continuous:
                    sendMsg = ":INIT1:CONT ON";
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
        /// 设置中频带宽 10到500000Hz 预设值70000Hz
        /// </summary>
        /// <param name="bandwidth"></param>
        /// <returns></returns>
        public override bool SetBandwidth(double bandwidth)
        {
            string sendMsg = ":SENSe1:BANDwidth " + bandwidth.ToString();
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