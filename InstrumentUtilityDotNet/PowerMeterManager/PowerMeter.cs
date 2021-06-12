using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentUtilityDotNet.PowerMeterManager
{
    public  class PowerMeter
    {
        internal PowerMeter()
        {   
             
        }

        /// <summary>
        /// 获取对象实例
        /// </summary>
        /// <param name="powerMeterType">仪表型号</param>
        /// <returns></returns>
        public static IPowerMeter GetInstance(PowerMeterType powerMeterType)
        {
        
            switch(powerMeterType)
            {
                case PowerMeterType.RS_NRT:
                   return new RS_NRT();
                default:
                    return null;
            }
        
        }
 
    }
}
