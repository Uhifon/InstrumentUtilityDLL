using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentUtilityDotNet.SignalSourceManager
{
    /// <summary>
    /// SpectrumAnalyzer 
    /// </summary>
    public  class SignalSource 
    {
        internal SignalSource()
        {

        }

        /// <summary>
        /// 获取对象实例
        /// </summary>
        /// <param name="signalSourceType">仪表ID 型号</param>
        /// <returns></returns>
        public static ISignalSource GetInstance(SignalSourceType signalSourceType)
        {
            switch(signalSourceType)
            {
                case SignalSourceType.HP_8360:
                    return  new HP_8360();          
                case SignalSourceType.Agilent_E4400:
                    return new Agilent_E4400();
                case SignalSourceType.RS_SMA100A:
                    return new HP_8360();
                case SignalSourceType.RS_SMBV100A:
                    return new HP_8360();
                case SignalSourceType.RS_SMHU:
                    return new HP_8360();
                default:
                    return null;
            }
        }

 


    }
}
