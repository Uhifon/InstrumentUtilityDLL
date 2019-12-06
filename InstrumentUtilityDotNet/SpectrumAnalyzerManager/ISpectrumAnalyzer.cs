using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstrumentUtilityDotNet.SpectrumAnalyzerManager
{
    public abstract class ISpectrumAnalyzer: InstrumentManager
    {
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
        public  string WriteAndReadCommand(string command)
        {
            return base.WriteAndReadString(command);
        }
        /// <summary>
        /// 获取设备ID号
        /// </summary>
        public abstract string GetID();

        /// <summary>
        /// 初始化仪表参数
        /// </summary>
        /// <returns></returns>
        public abstract bool Reset();

        /// <summary>
        /// 获取中心频率  
        /// </summary>
        /// <returns></returns>
        public abstract double GetCenterFreq();

        /// <summary>
        /// 获取MKA  峰值电平
        /// </summary>
        /// <returns></returns>
        public abstract double GetMKA();

        /// 设置中心频率  
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="unit">频率单位</param>
        /// <returns></returns>
        public abstract bool SetCenterFreq(double value, FrequencyUnit unit);


        /// <summary>
        /// 设置自动/手动 RBW  
        /// </summary>
        /// <param name="isAuto">自动/手动RBW</param>
        /// <param name="value">分辨率带宽，单位KHz</param>
        /// <returns></returns>
        public abstract bool SetRBW(bool isAuto, double value);
        /// <summary>
        /// 设置参考电平
        /// </summary>
        /// <param name="value">参考电平,单位dBm</param>
        /// <returns></returns>
        public abstract bool SetRefLevel(double value);

        /// <summary>
        /// 激活标记并搜索峰值  
        /// </summary>
        /// <returns></returns>
        public abstract bool MarkPeak();

        /// <summary>
        /// 设置衰减值
        /// </summary>
        /// <param name="isAuto">自动/手动衰减</param>
        /// <param name="value">衰减值 单位DB</param>
        /// <returns></returns>
        public abstract bool SetAttenuation(bool isAuto,double value);
 

        /// <summary>
        /// 设置SPAN
        /// </summary>
        /// <param name="value">带宽</param>
        /// <param name="unit">单位</param>
        /// <returns></returns>
        public abstract bool SetSpan(double value, FrequencyUnit unit);


        /// <summary>
        /// 线损 参考电平 补偿值 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract bool SetRefOffset(double value);

    }
}
