using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstrumentUtilityDotNet.NetworkAnalyzerManager
{
    public abstract class INetworkAnalyzer : InstrumentManager
    {
        /// <summary>
        /// 数据格式
        /// </summary>
        public enum Format
        {
            //Log Mag|Phase|Group Delay|Lin Mag|SWR|Real|Imaginary|Expand Phase| Positive Phase
            /// <summary>
            /// Log Mag 对数幅度格式
            /// </summary>
            MLOGarithmic,
            /// <summary>
            /// 相位格式
            /// </summary>
            PHASe,
            /// <summary>
            /// 群延迟格式
            /// </summary>
            GDELay,
            /// <summary>
            /// Smith图表格式（线性/相位）
            /// </summary>
            SLINear,
            /// <summary>
            /// Smith图表格式（对数/相位）
            /// </summary>
            SLOGarithmic,
            /// <summary>
            /// Smith图表格式（实部/虚部）
            /// </summary>
            SCOMplex,
            /// <summary>
            /// Smith图表格式（R+jX）
            /// </summary>
            SMITh,
            /// <summary>
            ///  Smith图表格式（G+jB）
            /// </summary>
            SADMittance,
            /// <summary>
            /// 极性格式（Lin/相位）
            /// </summary>
            PLINear,
            /// <summary>
            /// 极性格式（对数/相位）
            /// </summary>
            PLOGarithmic,
            /// <summary>
            /// 极性格式（实部/虚部）
            /// </summary>
            POLar,
            /// <summary>
            /// 线性幅度格式
            /// </summary>
            MLINear,
            /// <summary>
            /// SWR格式
            /// </summary>
            SWR,
             /// <summary>
             /// 实部格式
             /// </summary>
            REAL,
            /// <summary>
            /// 虚部格式
            /// </summary>
            IMAGinary,
            /// <summary>
            /// 扩展相位格式
            /// </summary>
            UPHase,
            /// <summary>
            /// 正相位格式
            /// </summary>
            PPHase
        }


        /// <summary>
        /// 标记类型设置
        /// </summary>
        public enum MARKType
        {
            MAXimum,
            MINimum,
            PEAK
        }


        /// <summary>
        /// 触发模式
        /// </summary>
        public enum TriggerMode
        {
            Hold,
            Single,
            Continuous
        }

        /// <summary>
        /// 连接仪表
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool Connect(string address)
        {
            return base.Open(address);
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
        /// <returns></returns>
        public abstract string GetID();

        /// <summary>
        /// 初始化仪表参数
        /// </summary>
        /// <returns></returns>
        public abstract bool Reset();


        /// <summary>
        /// 设置开始频率
        /// </summary>
        /// <param name="freq">Hz</param>
        /// <returns></returns>
        public abstract bool SetStartFreq(ulong freq);

        /// <summary>
        /// 设置终止频率
        /// </summary>
        /// <param name="freq">Hz</param>
        /// <returns></returns>
        public abstract bool SetStopFreq(ulong freq);
        /// <summary>
        /// 设置测量点数
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public abstract bool SetSweepPoints(int num);
        /// <summary>
        /// 设置中心频率
        /// </summary>
        /// <param name="freq">Hz</param>
        /// <returns></returns>
        public abstract bool SetCenterFreq(ulong freq);

        /// <summary>
        /// 设置带宽
        /// </summary>
        /// <param name="span">Hz</param>
        /// <returns></returns>
        public abstract bool SetSpan(ulong span);

        /// <summary>
        //  (显示参数)对选择的通道(Ch)设置选择迹线(Tr)的测量参数。
        /// <param name="para">
        //从下列两项中选择：
        //“S<XY>”
        //这里：
        //x=1到4
        //Y=1到4
        //A|B|C|D
        //R<X>(X= 1 - 4)
        //AUX1或AUX2</param>
        /// <returns></returns>
        public abstract bool SetDEFine(string para);

        /// <summary>
        /// 设置数据格式
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public abstract bool SetDataFormat(Format format);


        /// <summary>
        /// 标记类型设置
        /// </summary>
        /// <param name="markID">标记编号</param>
        /// <param name="type">标记类型</param>
        /// <returns></returns>
        public abstract bool SetMarkType(int markID,MARKType type);

        /// <summary>
        /// 获取Mark点Y值
        /// </summary>
        /// <param name="markID">标记编号</param>
        /// <param name="values">返回值</param>
        /// <returns></returns>
        public abstract bool ReadMarkY(int markID, out double[] values);

        /// <summary>
        /// 设置外部信号源功率设置的功率电平值
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public abstract bool SetLevel(double level);

        /// <summary>
        /// 开启/关闭激励信号输出，激励信号输出开启时才能进行测量
        /// </summary>
        /// <param name="onoff"></param>
        /// <returns></returns>
        public abstract bool SetRFOut(bool onoff);



        /// <summary>
        /// 设置触发模式
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public abstract bool SetTriggerMode(TriggerMode mode);

        /// <summary>
        /// 设置中频带宽
        /// </summary>
        /// <param name="bandwidth"></param>
        /// <returns></returns>
        public abstract bool SetBandwidth(double bandwidth );
        

    }

 
}
