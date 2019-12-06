using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstrumentUtilityDotNet.SignalSourceManager
{
    public abstract class ISignalSource: InstrumentManager
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
        public string WriteAndReadCommand(string command)
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
        /// 信号源发射开关
        /// </summary>
        /// <param name="state">On:打开发射 Off：关闭发射</param>
        /// <returns></returns>
        public abstract bool SetSignalSourceState(bool state);

        /// <summary>
        /// 设置频率、功率
        /// </summary>
        ///  <param name="unit">频率单位</param>
        /// <param name="freq">频率</param>
        /// <param name="level">功率，单位DBM</param>
        /// <returns></returns>
        public abstract bool SetFreqAndLevel(FrequencyUnit unit, double freq, double level);
        /// <summary>
        /// 设置频率
        /// </summary>
        /// <param name="unit">频率单位</param>
        /// <param name="freq">频率</param>
        /// <returns></returns>
        public abstract bool SetFreq(FrequencyUnit unit, double freq);
        /// <summary>
        /// 设置功率
        /// </summary>
        /// <param name="level">功率</param>
        /// <returns></returns>
        public abstract bool SetLevel(double level);

        /// <summary>
        /// 打开脉冲开关
        /// </summary>
        /// <param name="onOff"> </param>
        /// <returns></returns>
        public abstract bool SetPulse(bool onOff);

        /// <summary>
        /// 设置脉冲带宽
        /// </summary>
        /// <param name="width">带宽 us</param>
        /// <returns></returns>
        public abstract bool SetPulseWidth(double width);


        /// <summary>
        /// 设置脉冲周期
        /// </summary>
        /// <param name="period">周期 us</param>
        /// <returns></returns>
        public abstract bool SetPulsePeriod(double period);

    }
}
