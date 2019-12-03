using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstrumentUtilityDotNet.PowerMeterManager
{
    public interface IPowerMeter
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
        /// 单位切换
        /// </summary>
        /// <param name="unit">DBM,W</param>
        bool PowerUnitChange(PowerUnit unit);


        /// <summary>
        /// 获取功率计DATA 功率AVG与驻波比SWR 功率计 
        /// </summary>
        /// <param name="sensorNum">传感器编号0-2</param>
        /// <param name="avg">功率</param>
        /// <param name="swr">驻波比</param>
        /// <returns></returns>
        bool GetPower(int sensorNum, out double avg, out double swr);

    }
}
