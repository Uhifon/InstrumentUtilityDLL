using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstrumentUtilityDotNet.SignalSourceManager
{
    public interface ISignalSource
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
        /// 信号源发射开关
        /// </summary>
        /// <param name="state">On:打开发射 Off：关闭发射</param>
        /// <returns></returns>
        bool SetSignalSourceState(bool state);

        /// <summary>
        /// 设置频率、功率
        /// </summary>
        ///  <param name="unit">频率单位</param>
        /// <param name="freq">频率</param>
        /// <param name="level">功率，单位DBM</param>
        /// <returns></returns>
        bool SetFreqAndLevel(FrequencyUnit unit, double freq, double level);
        /// <summary>
        /// 设置频率
        /// </summary>
        /// <param name="unit">频率单位</param>
        /// <param name="freq">频率</param>
        /// <returns></returns>
        bool SetFreq(FrequencyUnit unit, double freq);
        /// <summary>
        /// 设置功率
        /// </summary>
        /// <param name="level">功率</param>
        /// <returns></returns>
        bool SetLevel(double level);

        /// <summary>
        /// 打开脉冲开关
        /// </summary>
        /// <param name="onOff"> </param>
        /// <returns></returns>
        bool SetPulse(bool onOff);

        /// <summary>
        /// 设置脉冲带宽
        /// </summary>
        /// <param name="width">带宽 us</param>
        /// <returns></returns>
        bool SetPulseWidth(double width);


        /// <summary>
        /// 设置脉冲周期
        /// </summary>
        /// <param name="period">周期 </param>
        /// <returns></returns>
        bool SetPulsePeriod(double period);

    }
}
