using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentUtilityDotNet.SynthesizeMeterManager
{
    public  class SynthesizeMeter 
    {

        internal SynthesizeMeter()
        {

        }

        /// <summary>
        /// 获取对象实例
        /// </summary>
        /// <param name="synthesizeMeterType">仪表型号</param>
        /// <returns></returns>
        public static ISynthesizeMeter GetInstance(SynthesizeMeterType synthesizeMeterType)
        {
            switch (synthesizeMeterType)
            {
                case SynthesizeMeterType.Aglient_8920:
                    return new Aglient_8920();
                case SynthesizeMeterType.Ceyear_AV4957:
                    return new Ceyear_AV4957();
                case SynthesizeMeterType.RS_CMA180:
                    return new RS_CMA180();
                default:
                    return null;
            }
        }

 
  
    }
}
