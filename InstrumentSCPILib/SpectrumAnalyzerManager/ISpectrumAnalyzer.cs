using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstrumentSCPILib.SpectrumAnalyzerManager
{
    public interface ISpectrumAnalyzer
    {
        /// <summary>
        /// 连接仪表
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        bool Connect(string address);

        /// <summary>
        /// 断开连接
        /// </summary>
        void DisConnect();
        /// <summary>
        ///  Write
        /// </summary>
        /// <param name="command"></param>
        void WriteCommand(string command);

        /// <summary>
        ///  WriteAndRead
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        string WriteAndReadCommand(string command);
        /// <summary>
        /// 获取设备ID号
        /// </summary>
        string GetID();

        /// <summary>
        /// 初始化仪表参数
        /// </summary>
        /// <returns></returns>
        bool Reset();

        /// <summary>
        /// 获取中心频率  
        /// </summary>
        /// <returns></returns>
        double GetCenterFreq();

        /// <summary>
        /// 获取MKA  峰值电平
        /// </summary>
        /// <returns></returns>
        double GetMKA();

        /// 设置中心频率  
        /// </summary>
        /// <param name="unit">频率单位</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        bool SetCenterFreq(FrequencyUnit unit, double value);

        /// <summary>
        /// 设置RBW  
        /// </summary>
        /// <param name="value">分辨率带宽，单位KHz</param>
        /// <returns></returns>
        bool SetRBW(double value);

        /// <summary>
        /// 设置自动/手动 RBW  
        /// </summary>
        /// <param name="OnOff">自动、手动RBW</param>
        /// <returns></returns>
        bool SetAutoRBW(bool OnOff);
        /// <summary>
        /// 设置参考电平
        /// </summary>
        /// <param name="value">参考电平,单位dBm</param>
        /// <returns></returns>
        bool SetRefLevel(double value);

        /// <summary>
        /// 激活标记并搜索峰值  
        /// </summary>
        /// <returns></returns>
        bool MarkPeak();

        /// <summary>
        /// 设置自动衰减
        /// </summary>
        /// <returns></returns>
        bool SetAutoAttenuation();

        /// <summary>
        ///  设置手动衰减值
        /// </summary>
        /// <param name="value">衰减值 单位DB</param>
        /// <returns></returns>
        bool SetManulAttenuation(double value);

        /// <summary>
        /// 设置SPAN
        /// </summary>
        /// <param name="value">带宽</param>
        /// <param name="unit">单位</param>
        /// <returns></returns>
        bool SetSpan(double value, FrequencyUnit unit);


        /// <summary>
        /// 线损 参考电平 补偿值 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool SetRefOffset(double value);

    }
}
