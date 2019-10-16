using InstrumentSCPILib.ComprehensiveMeterManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentSCPILib
{
    /// <summary>
    /// 地址转换
    /// </summary>
    public class AddressConversion
    {
 
        /// <summary>
        /// VISA地址转GPIB
        /// </summary>
        /// <param name="addr">GPIB0::18::INSTR</param>
        /// <returns></returns>
        public static string[] VisaAddrToGPIB(string addr)
        {
            string[] Res = new string[2];
            string[] arr = addr.Split(':');
            if (arr != null && arr.Length == 5)
            {
                Res[0] = arr[0];
                Res[1] = arr[2];
            }
            return Res;
        }

        /// <summary>
        /// VISA地址转IP
        /// </summary>
        /// <param name="addr">TCPIP0::192.168.0.10::8000::SOKECT</param>
        /// <returns></returns>
        public static string[] VisaAddrToIP(string addr)
        {
            string[] Res = new string[2];
            string[] arr = addr.Split(':');
            if (arr != null && arr.Length == 7)
            {
                Res[0] = arr[2];
                Res[1] = arr[4];
            }
            return Res;
        }

        /// <summary>
        /// GPIB地址转VISA格式
        /// </summary>、
        /// <param name="id">0</param>
        /// <param name="addr">18</param>
        /// <returns></returns>
        public static string GPIBToVisaAddr(string id, string addr)
        {
            return id + "::" + addr + "::INSTR";
        }

        /// <summary>
        /// TCPIP地址转VISA格式
        /// </summary>、
        /// <param name="id">0</param>
        /// <param name="addr">18</param>
        /// <returns></returns>
        public static string TCPToVisaAddr(string ip, string port)
        {
            return "TCPIP0::" + ip + "::" + port + "::SOKECT";
        }
    }
}
