using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstrumentUtilityDotNet.NetworkAnalyzerManager
{
     public abstract class INetworkAnalyzer : InstrumentManager
    {
        /// <summary>
        /// 连接仪表
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        bool Connect(string address)
        {
            return base.InitiateIO488(address);
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        void DisConnect()
        {
            base.Close();
        }
        /// <summary>
        ///  Write
        /// </summary>
        /// <param name="command"></param>
        void WriteCommand(string command)
        {
            base.WriteString(command);
        }

        /// <summary>
        ///  WriteAndRead
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        string WriteAndReadCommand(string command)
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
        /// 设置开始频率
        /// </summary>
        /// <param name="freq"></param>
        /// <param name="unit"></param>
        public abstract bool SetStartFreq(int freq, FrequencyUnit unit);

        /// <summary>
        /// 设置终止频率
        /// </summary>
        /// <param name="freq"></param>
        /// <param name="unit"></param>
        public abstract bool SetStopFreq(int freq, FrequencyUnit unit);

        /// <summary>
        /// 设置中心频率
        /// </summary>
        /// <param name="freq"></param>
        /// <param name="unit"></param>
        public abstract bool SetCenterFreq(int freq, FrequencyUnit unit);

        /// <summary>
        /// 设置带宽
        /// </summary>
        /// <param name="span"></param>
        /// <param name="unit"></param>
        public abstract bool SetSpan(int span, FrequencyUnit unit);

    }
}
