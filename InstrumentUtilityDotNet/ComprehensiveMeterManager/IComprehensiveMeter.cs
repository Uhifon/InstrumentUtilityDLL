using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstrumentUtilityDotNet.ComprehensiveMeterManager
{
    public abstract class IComprehensiveMeter: InstrumentManager
    {
        /// <summary>
        /// 连接仪表
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool Connect(string address)
        {
            return base.InitiateIO488(address);
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public  void DisConnect()
        {
            base.Close();
        }

        /// <summary>
        ///  Write
        /// </summary>
        /// <param name="command"></param>
        public  void WriteCommand(string command)
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
        /// 设置中心频率  
        /// </summary>
        /// <param name="unit">频率单位</param>
        /// <param name="value">频率</param>
        /// <returns></returns>
        public abstract bool SetCenterFreq(FrequencyUnit unit, double value);

        /// <summary>
        /// 设置参考电平  幅度标尺  
        /// </summary>
        /// <param name="value">参考电平 单位dbm</param>
        /// <returns></returns>
        public abstract bool SetRefLevel(double value);


        /// <summary>
        /// 获取中心频率
        /// </summary>
        /// <returns></returns>
        public abstract double GetCenterFreq();

        /// <summary>
        /// 设置解调开关。
        /// </summary>
        /// <param name="onOff"> 解调开关</param>
        /// <returns></returns>
        public abstract bool SetAfgState(bool onOff);

     

    }
}
