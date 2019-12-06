using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstrumentUtilityDotNet.PowerMeterManager
{
    public abstract class IPowerMeter: InstrumentManager
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
        /// 单位切换
        /// </summary>
        /// <param name="unit">DBM,W</param>
        public abstract bool PowerUnitChange(PowerUnit unit);


        /// <summary>
        /// 获取功率计DATA 功率AVG与驻波比SWR 功率计 
        /// </summary>
        /// <param name="sensorNum">传感器编号0-2</param>
        /// <param name="avg">功率</param>
        /// <param name="swr">驻波比</param>
        /// <returns></returns>
        public abstract bool GetPower(int sensorNum, out double avg, out double swr);

    }
}
