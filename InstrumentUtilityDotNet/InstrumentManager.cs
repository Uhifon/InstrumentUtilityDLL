using Ivi.Visa.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentUtilityDotNet
{
 
    /// <summary>
    /// IO488协议对象通信管理类
    /// </summary>
    public class InstrumentManager
    {
        private ResourceManagerClass m_IOResourceManager = new ResourceManagerClass();
        private FormattedIO488Class m_IO488 = null;
        private string receive = string.Empty;
     
        /// <summary>
        /// 寻找所有仪表资源
        /// </summary>
        /// <returns></returns>
        public string[] FindAllResource()
        {
            IEnumerable<string> resources = m_IOResourceManager.FindRsrc("?*");//寻找仪表  
            return resources.ToArray();
        }

        /// <summary>
        /// 寻找所有GPIB资源
        /// </summary>
        /// <returns></returns>
        public string[] FindGPIBResource()
        {
            IEnumerable<string> resources = m_IOResourceManager.FindRsrc("GPIB?*");
            return resources.ToArray();
        }
        /// <summary>
        /// 寻找所有TCPIP资源
        /// </summary>
        /// <returns></returns>
        public string[] FindTcpIpResource()
        {
            IEnumerable<string> resources = m_IOResourceManager.FindRsrc("TCPIP?*");   
            return resources.ToArray();
        }
        /// <summary>
        /// 寻找所有USB资源
        /// </summary>
        /// <returns></returns>
        public string[] FindUSBResource()
        {
            IEnumerable<string> resources = m_IOResourceManager.FindRsrc("USB?*");
            return resources.ToArray();
        }
        /// <summary>
        /// 连接IO488对象，GPIB,TCP协议
        /// </summary>
        /// <param name="m_IOName">GPIB0::" + addr + "::INSTR,TCPIP0::192.168.0.10::5000::SOCKET</param>
        /// <returns></returns>
        public bool Open(string m_IOName)
        {
            try
            {
                string m_Description = string.Empty;
                m_IO488 = new FormattedIO488Class();
                // 初始化m_IOName指定的GPIB接口
                m_IO488.IO = (IMessage)m_IOResourceManager.Open(m_IOName, AccessMode.NO_LOCK, 2000);
              
                return true;
            }
            catch { return false; }
        }
     
        /// <summary>
        /// 关闭IO488 IO接口
        /// </summary>
        public void Close()
        {
            try
            {
                if (m_IO488 != null)
                    m_IO488.IO.Close();
            }
            catch { }
        }

        /// <summary>
        /// 清理IO接口缓存区函数、关闭接口函数、设置超时时间函数
        /// </summary>
        public void Clear()
        {
            try
            {

                if (m_IO488 != null)
                    m_IO488.IO.Clear();
            }
            catch { }
        }

        /// <summary>
        /// 发送命令 字符串型
        /// </summary>
        /// <param name="m_Command"></param>
        public void WriteString(string m_Command)
        {
            try
            {
 
                if (m_IO488 != null)
                    m_IO488.IO.WriteString(m_Command);
                
             
            }
            catch (System.IO.IOException error)  //掉线
            {
               throw error;
            }
            catch (Exception ex)
            {
                if (ex.Message == "VI_ERROR_NCIC: Not the controller-in-charge")  //GPIB掉线
                    throw ex;
            }
        }

        /// <summary>
        /// 发送命令 字节型
        /// </summary>
        /// <param name="m_Command"></param>
        public void WriteBytes(byte[] m_Command)
        {
            try
            {
            
                if (m_IO488 != null)
                    m_IO488.WriteNumber(m_Command, IEEEASCIIType.ASCIIType_Any, true);

            }
            catch (System.IO.IOException error)
            { 
                throw error;
            }

        }


        /// <summary>
        /// 发送命令，在返回1之前停止命令处理
        /// </summary>
        /// <param name="m_Command"></param>
        /// <param name="m_Timeout"></param>
        public  void WriteOpc(string m_Command, int m_Timeout)
        {
            try
            {               
                if (m_IO488 != null)
                {
                    m_IO488.WriteString(m_Command, false);
                    m_IO488.WriteString("*OPC?", false); // 在返回1之前停止命令处理
                }

                string m_Temp = string.Empty;
                for (int i = 0; i < m_Timeout * 10; i++)
                    if (ReadString().Length > 0)
                        return;
                    else
                        System.Threading.Thread.Sleep(100);

            }
            catch { }
        }

        /// <summary>
        /// 读取（字符串）
        /// </summary>
        /// <returns></returns>
        public string ReadString()
        {
            string m_Temp = string.Empty;
            try
            {
              
                if (m_IO488 != null)
                    m_Temp = m_IO488.ReadString();
            }
            catch (System.IO.IOException error)  //控制箱掉线
            {
                throw error;
            }
            catch (Exception ex)
            {
               if (ex.Message == "VI_ERROR_NCIC: Not the controller-in-charge")  //掉线
                   throw ex;
            }
            return m_Temp;
        }

        /// <summary>
        /// 读取（字节）
        /// </summary>
        /// <returns></returns>
        public byte[] ReadByte()
        {
            byte[] m_Temp = null;
            try
            {
                if (m_IO488 != null)
                    m_Temp = m_IO488.IO.Read(0);
            }
            catch (System.IO.IOException error)  //控制箱掉线
            {
                throw error;
            }
            catch (Exception ex)
            {
                if (ex.Message == "VI_ERROR_NCIC: Not the controller-in-charge")  //GPIB掉线
                    throw ex;
            }
            return m_Temp;
        }


        /// <summary>
        /// 发送并读取（字符串）
        /// </summary>
        /// <param name="m_Command"></param>
        /// <returns></returns>
        public string WriteAndReadString(string m_Command)
        {
            string m_Temp = string.Empty;
            try
            {
                WriteString(m_Command);
                if (m_IO488 != null)
                    m_Temp = m_IO488.ReadString();
            }
            catch (System.IO.IOException error) 
            {
                throw error;
            }
            catch (Exception ex)
            {
                if (ex.Message == "VI_ERROR_NCIC: Not the controller-in-charge")  //掉线
                    throw ex;
            }

            return m_Temp;
        }

        /// <summary>
        /// 发送并读取（字节）
        /// </summary>
        /// <param name="m_Command"></param>
        /// <returns></returns>
        public byte[] WriteAndReadBytes(byte[] m_Command)
        {
            byte[] m_Temp = null;
            try
            {
                WriteBytes(m_Command);
                if (m_IO488 != null)
                    m_Temp = m_IO488.IO.Read(0);

            }
            catch (System.IO.IOException error)
            {
                throw error;
            }
            catch (Exception ex)
            {
                if (ex.Message == "VI_ERROR_NCIC: Not the controller-in-charge")  //掉线
                    throw ex;
            }

            return m_Temp;
        }

        
    }
}
