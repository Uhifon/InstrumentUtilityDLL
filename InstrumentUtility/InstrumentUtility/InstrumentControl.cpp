// InstrmentControl.cpp : 定义 DLL 应用程序的导出函数。
//
#include "stdafx.h"
#include  <iostream>
#include "InstrumentControl.h"
#include <stdlib.h>
#include "Agilent_856x.h"
#include "Agilent_E4400.h"
#include "RS_NRT.h"
#include "Aglient_8920.h"

using namespace std;

InstrumentControl instrumentControl;
 

InstrumentControl::InstrumentControl()
{
 
}

InstrumentControl::~InstrumentControl()
{
	 
}

/*************************************************
Function: yhFindRsrc
Description: 寻找Visa资源
Input: str 匹配正则表达式
Output: descriptor 输出标识设备位置的字符串。
Return: 
*************************************************/
ViStatus InstrumentControl::FindRsrc(ViConstString str, ViChar* descriptor)
{
	ViStatus status = viOpenDefaultRM(&defaultRM);//ViSession,OUT 默认资源管理器会话的唯一逻辑标识符。
	if (status < VI_SUCCESS)
	{
		errorInfo =  "无法向VISA资源管理器打开会话！\n";
		return status;
	}
	  /*Interface         Expression
		--------------------------------------
		GPIB              "GPIB[0-9]*::?*INSTR"
		VXI               "VXI?*INSTR"
		GPIB - VXI        "GPIB-VXI?*INSTR"
		Any VXI           "?*VXI[0-9]*::?*INSTR"
		Serial            "ASRL[0-9]*::?*INSTR"
		PXI               "PXI?*INSTR"
		All instruments   "?*INSTR"
		All resources     "?*"
		* /*/
	//(ViSession sesn, ViConstString expr, ViPFindList vi,ViPUInt32 retCnt, ViChar _VI_FAR desc[]);
	 status = viFindRsrc(defaultRM,  //ViSession,IN 资源管理器会话(应该总是从viOpenDefaultRM()返回的会话
						 str,        //ViConstString,IN 这是一个正则表达式，后面跟着一个可选的逻辑表达式，查看上面注释。
						 &findList,  //ViPFindList,OUT 返回标识此搜索会话的句柄。这个句柄将用作viFindNext()中的输入  
						 &numInstrs, //ViPUInt32,OUT 匹配的数量
						 descriptor  //ViChar,OUT 返回标识设备位置的字符串。然后可以将字符串传递给viOpen()，以建立到给定设备的会话
						 );
	 if (status < VI_SUCCESS)
		 errorInfo = "寻找资源发生错误！\n";
	 
	 status = viFindNext(findList, descriptor);


	 return status;
}

//打开指向指定资源的会话
ViStatus InstrumentControl::Open(ViChar*  descriptor)
{
	ViStatus status = viOpen(defaultRM,  //ViSession,IN 资源管理器会话(应该总是从viOpenDefaultRM()返回的会话
					descriptor, //ViConstRsrc,IN 资源的唯一符号名。例：TCPIP0::ftp.ni.com::21::SOCKET，VXI0::0::INSTR，GPIB0::14::INSTR
					VI_NULL,    //ViAccessMode,IN 指定访问资源的模式。有关有效值，请参阅说明部分。如果参数值是VI_NULL，则会话使用visa提供的默认值。
					VI_NULL,    //ViUInt32,IN 指定此操作在返回错误之前等待的最大时间段(以毫秒为单位)。这并没有设置I/O超时—为此，您必须使用属性VI_ATTR_TMO_VALUE调用viSetAttribute()。
		     		 &instr     //ViPSession,OUT 引用会话的唯一逻辑标识符
	                 );
	if (status < VI_SUCCESS)
	{
		errorInfo = "打开资源发生错误！\n" ;
		Close(defaultRM);
	}
	return status;
}

//清理设备
ViStatus InstrumentControl::Clear(ViSession vi)
{
	ViStatus status = viClear(vi);  //ViSession 会话的唯一逻辑标识符
	if (status < VI_SUCCESS)
	{
		errorInfo = "清理设备资源错误！\n";
	}
	return status;
}


//关闭资源
ViStatus InstrumentControl::Close(ViObject  vi)
{
	ViStatus status = viClose(vi); //ViSession 会话的唯一逻辑标识符
	if (status < VI_SUCCESS)
	{
		errorInfo = "关闭资源错误！\n";
	}
	return status;
}

//设置属性
ViStatus InstrumentControl::SetAttribute(ViObject vi, ViAttr attribute, ViAttrState attrState)
{
	ViStatus status = viSetAttribute( vi,        //IN 会话的唯一逻辑标识符。
							attribute,  //IN 要修改其状态的属性。
							attrState   //IN 为指定对象设置的属性的状态。单个属性值的解释由对象定义。
						   );
	if (status < VI_SUCCESS)
	{
		errorInfo = "设置属性命令错误！\n";
	}
	return status;
}


//读取参数
ViStatus InstrumentControl::Read(ViSession vi, ViPBuf buf, ViUInt32 count, ViPUInt32 retCount)
{
	ViStatus status = viRead(vi,      //ViSession IN 会话的唯一逻辑标识符
					buf,     //ViBuf OUT 要发送到设备的数据块的位置
					count,   //ViUInt32 IN 要读取的字节数
					retCount //ViPUInt32 OUT 实际传输的字节数
					);
	if (status < VI_SUCCESS)
	{
		errorInfo = "读取命令错误！\n";
	}
	return status;
}

//设置参数
ViStatus InstrumentControl::Write(ViSession vi, ViBuf buf, ViUInt32 count, ViPUInt32 retCount)
{
	ViStatus	status = viWrite(vi,      //ViSession IN 会话的唯一逻辑标识符
					 buf,     //ViBuf IN 要发送到设备的数据块的位置
					 count,   // ViUInt32 IN 要写入的字节数
					 retCount //ViPUInt32 OUT 实际传输的字节数
				   );
	if (status < VI_SUCCESS)
	{
		errorInfo = "写入命令错误！\n";
	}
	return status;
}

 


//获取最近一条错误消息
char* InstrumentControl::GetCurrentError()
{
	if (errorInfo != NULL)
		return errorInfo;
	else
		return "没有错误消息\n";

} 


ISpectropgraph* InstrumentControl::GetInstance(SpectrographType spectrographType)
{
	switch (spectrographType)
	{
	case _Agilent_856x:
		return  new Agilent_856x();
		break;
	case _Agilent_ESA_E:
		break;
	case _Agilent_E4407B:
		break;
	case _RS_FSU:
		break;
	case _RS_FSW:
		break;
	default:
		break;
	}
	return new Agilent_856x();
}

ISignalSource*  InstrumentControl::GetInstance(SignalSourceType signalSourceType)
{
	switch (signalSourceType)
	{
	case _Agilent_E4400:
		return new Agilent_E4400();
	case _HP_8360:
		break;
	case _RS_SMHU:
		break;
	case _RS_SMA100A:
		break;
	case _RS_SMBV100A:
		break;
	default:
		break;
	}
	return new Agilent_E4400();
}

 

IPowerMeter* InstrumentControl::GetInstance( PowerMeterType powerMeterType)
{
	switch (powerMeterType)
	{
	case _RS_NRT:
		return new RS_NRT();
	default:
		break;
	}
	return new RS_NRT();
}

ISynthesizeMeter* InstrumentControl::GetInstance(SynthesizeMeterType synthesizeMeterType)
{
	switch (synthesizeMeterType)
	{
	case _Aglient_8920:
		return new Aglient_8920();
	case _Ceyear_AV4957:
		break;
	default:
		break;
	}
	return new Aglient_8920();
}

bool InstrumentBaseCommand::OpenInstrument(char * address)
{
	ViStatus  status = instrumentControl.Open(address);
	if(status<VI_SUCCESS)
	    return false;
	else
		return true;
}

bool InstrumentBaseCommand::CloseInstrument()
{
	ViStatus  status = instrumentControl.Close(instr);
	if (status<VI_SUCCESS)
		return false;
	else
		return true;
}


bool InstrumentBaseCommand::WriteString(char * command)
{ 
	static ViUInt32 retCount;
	ViStatus status = instrumentControl.Write(instr,(ViBuf)command, (ViUInt32)strlen(command),&retCount);
	if (status<VI_SUCCESS)
		return false;
	else
		return true;
}
bool InstrumentBaseCommand::ReadString(char * outData)
{
	static ViUInt32 retCount;
	 ViStatus status = instrumentControl.Read(instr, (ViBuf)outData, 100, &retCount);
	if (status<VI_SUCCESS)
		return false;
	else
		return true;
}
bool InstrumentBaseCommand::WriteAndReadString(char* command, char* retValue)
{
	bool res = WriteString(command);
	res = ReadString(retValue);
	return res;
}

