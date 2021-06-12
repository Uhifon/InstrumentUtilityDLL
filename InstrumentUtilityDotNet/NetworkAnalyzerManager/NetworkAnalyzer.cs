using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstrumentUtilityDotNet.NetworkAnalyzerManager
{
    public  class NetworkAnalyzer
    {

        internal NetworkAnalyzer()
        {

        }
        /// <summary>
        ///获取对象实例
        /// </summary>
        /// <param name="networkAnalyzerType">仪表型号</param>
        /// <returns></returns>
        public static INetworkAnalyzer GetInstance(NetworkAnalyzerType  networkAnalyzerType)
        {
            switch (networkAnalyzerType)
            {
                case NetworkAnalyzerType.RS_ZNB:
                   return new RS_ZNB();
                case NetworkAnalyzerType.Aglient_E5071C:
                    return new RS_ZNB();
                case NetworkAnalyzerType.Aglient_E506X:
                    return new RS_ZNB();
                default:
                    return null;
               
            }
        }
    }
}
