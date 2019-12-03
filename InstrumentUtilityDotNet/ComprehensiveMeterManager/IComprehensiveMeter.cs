using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstrumentUtilityDotNet.ComprehensiveMeterManager
{
    public  interface IComprehensiveMeter
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
        /// 设置中心频率  
        /// </summary>
        /// <param name="unit">频率单位</param>
        /// <param name="value">频率</param>
        /// <returns></returns>
        bool SetCenterFreq(FrequencyUnit unit, double value);

        /// <summary>
        /// 设置参考电平  幅度标尺  
        /// </summary>
        /// <param name="value">参考电平 单位dbm</param>
        /// <returns></returns>
        bool SetRefLevel(double value);


        /// <summary>
        /// 获取中心频率
        /// </summary>
        /// <returns></returns>
        double GetCenterFreq();

        /// <summary>
        /// 设置解调开关。
        /// </summary>
        /// <param name="onOff"> 解调开关</param>
        /// <returns></returns>
        bool SetAfgState(bool onOff);

     

    }
}
