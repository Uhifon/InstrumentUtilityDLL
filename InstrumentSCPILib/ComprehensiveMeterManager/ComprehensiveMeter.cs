using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentSCPILib.ComprehensiveMeterManager
{
    public  class ComprehensiveMeter 
    {

        internal ComprehensiveMeter()
        {

        }

        /// <summary>
        /// 获取对象实例
        /// </summary>
        /// <param name="comprehensiveMeterType">仪表型号</param>
        /// <returns></returns>
        public static IComprehensiveMeter GetInstance(ComprehensiveMeterType comprehensiveMeterType)
        {
            switch (comprehensiveMeterType)
            {
                case ComprehensiveMeterType.Aglient_8920:
                    return new Aglient_8920();
                case ComprehensiveMeterType.Ceyear_AV4957:
                    return new Ceyear_AV4957();
                default:
                    return null;
            }
        }

 
  
    }
}
