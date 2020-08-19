using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentUtilityDotNet
{
    #region 枚举
    /// <summary>
    /// 仪表连接方式
    /// </summary>
    public enum InstrumentCommunicationMode
    {
        GPIB,
        TCP,
        Serial
    }
    /// <summary>
    /// 频率单位
    /// </summary>
    public enum FrequencyUnit
    {
        Hz,
        KHz,
        MHz,
        GHz
    }

    /// <summary>
    /// 功率单位
    /// </summary>
    public enum PowerUnit
    {
        DBM,
        W
    }

    /// <summary>
    /// 仪表类型
    /// </summary>
    public enum InstrumentType
    {
        /// <summary>
        /// 频谱分析仪
        /// </summary>
        SpectrumAnalyzer,
        /// <summary>
        /// 信号源
        /// </summary>
        SignalSource,
        /// <summary>
        /// 功率计
        /// </summary>
        PowerMeter,
        /// <summary>
        /// 综测仪
        /// </summary>
        SynthesizeMeter
    }

    /// <summary>
    /// 频谱仪型号
    /// </summary>
    public enum SpectrumAnalyzerType
    {
        Agilent_856X,
        Agilent_ESA_E,
        Agilent_E4407B,
        RS_FSU,
        RS_FSW,
        Ceyear_4024D_403X
    }

    /// <summary>
    /// 信号源型号
    /// </summary>
    public enum SignalSourceType
    {
        Agilent_E4400,
        HP_8360,
        RS_SMHU,
        RS_SMA100A,
        RS_SMBV100A,
        Ceyear_AV1431,
        Ceyear_AV146X
    }

    /// <summary>
    /// 功率计型号
    /// </summary>
    public enum PowerMeterType
    {
        RS_NRT
    }

    /// <summary>
    /// 综测仪型号
    /// </summary>
    public enum SynthesizeMeterType
    {
        Aglient_8920,
        Ceyear_AV4957
    }


    /// <summary>
    /// 网络分析仪型号
    /// </summary>
    public enum NetworkAnalyzerType
    {
        RS_ZNB
    }

    #endregion

}
