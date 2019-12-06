#pragma once
#include "stdafx.h"
#include "visa.h"
#include <stdio.h>


#ifdef InstrumentUtility_EXPORTS
#define InstrumentUtility_API __declspec(dllimport)   //导入导出申明写反会报dll链接错误
#else 
#define InstrumentUtility_API __declspec(dllexport)
#endif // InstrumentManager_EXPORTS


enum SpectrographType
{
	_Agilent_856x,
	_Agilent_ESA_E,
	_Agilent_E4407B,
	_RS_FSU,
	_RS_FSW
};


enum SignalSourceType
{
	_Agilent_E4400,
	_HP_8360,
	_RS_SMHU,
	_RS_SMA100A,
	_RS_SMBV100A
};


enum PowerMeterType
{
	_RS_NRT
};


enum SynthesizeMeterType
{
	_Aglient_8920,
	_Ceyear_AV4957
};

enum FrequencyUnit
{
	Hz,
	KHz,
	MHz,
	GHz
};

enum PowerUnit
{
	DBM,
	W
};

class InstrumentBaseCommand
{
public:
	//连接仪表
	virtual bool OpenInstrument(char* address);
	//断开连接
	virtual bool CloseInstrument();
	//写命令
	virtual bool WriteString(char* command);
	//读命令
	virtual bool ReadString(char* command);
	//写并读命令
	virtual bool WriteAndReadString(char* command, char* retValue);
 
};


//频谱仪接口类
class ISpectropgraph :public InstrumentBaseCommand
{
public:
	//获取设备ID号
	virtual  char* GetID() = 0;
	//初始化仪表参数
	virtual bool Reset() = 0;
	//获取中心频率  
	virtual double GetCenterFreq() = 0;
	//获取MKA  峰值电平
	virtual double GetMKA()=0;
	//设置中心频率  
	virtual bool SetCenterFreq(FrequencyUnit unit, double value) = 0;
	//设置RBW  
	virtual bool SetRBW(bool isAuto, double value = 0) = 0;
	//设置参考电平
	virtual bool SetRefLevel(double value) = 0;
	//激活标记并搜索峰值  
	virtual bool MarkPeak() = 0;
	//设置衰减
	virtual bool SetAttenuation(bool isAuto, double value) = 0;
	//设置SPAN
	virtual bool SetSpan(double value, FrequencyUnit unit) = 0;
};


class ISignalSource :public InstrumentBaseCommand
{
public:
	//获取设备ID号
	virtual char* GetID() = 0;
	//初始化仪表参数
	virtual bool Reset() = 0;
	//信号源发射开关
	virtual bool SetSignalSourceState(bool state) = 0;
	// 设置频率、功率
	virtual bool SetFreqAndLevel(FrequencyUnit unit, double freq, double level) = 0;
	//设置频率
	virtual bool SetFreq(FrequencyUnit unit, double freq) = 0;
	//设置功率
	virtual bool SetLevel(double level) = 0;
	//打开脉冲开关
	virtual bool SetPulse(bool onOff) = 0;
	//设置脉冲带宽
	virtual bool SetPulseWidth(double width) = 0;
	//设置脉冲周期
	virtual bool SetPulsePeriod(double period) = 0;
};

class IPowerMeter :public InstrumentBaseCommand
{
public:
	//获取设备ID号
	virtual char* GetID() = 0;
	//初始化仪表参数
	virtual bool Reset() = 0;
	//单位切换
	virtual bool PowerUnitChange(PowerUnit unit) = 0;
	//获取功率计DATA 功率AVG与驻波比SWR 功率计 
	virtual bool GetPower(int sensorNum, double* avg,   double* swr) = 0;
};

class ISynthesizeMeter:public InstrumentBaseCommand
{
public:
	//获取设备ID号
	virtual char* GetID() = 0;
	//初始化仪表参数
	virtual bool Reset() = 0;
	//设置中心频率  
	virtual bool SetCenterFreq(FrequencyUnit unit, double value) = 0;
	//设置参考电平  幅度标尺  
	virtual bool SetRefLevel(double value) = 0;
	//获取中心频率
	virtual double GetCenterFreq() = 0;
	//设置解调开关
	virtual bool SetAfgState(bool onOff) = 0;

};




class InstrumentUtility_API InstrumentUtility
{
public:
	InstrumentUtility();
	~InstrumentUtility();

	//TODO:在此处填写您的方法

	char* GetCurrentError();
    ViStatus FindRsrc(ViConstString str, ViChar* descriptor);
	ViStatus Open(ViChar*  descriptor);
	ViStatus Close(ViObject  vi);
	ViStatus Clear(ViSession vi);
	ViStatus SetAttribute(ViObject vi, ViAttr attribute, ViAttrState attrState);
	ViStatus Read(ViSession vi, ViPBuf buf, ViUInt32 count, ViPUInt32 retCount);
	ViStatus Write(ViSession vi, ViBuf buf, ViUInt32 count, ViPUInt32 retCount);
	//ViStatus yhReadAsync();
	//ViStatus yhWriteAsync();

	ISpectropgraph*  GetInstance(SpectrographType spectrographType);
	ISignalSource*  GetInstance(SignalSourceType signalSourceType);
	IPowerMeter*  GetInstance(PowerMeterType powerMeterType);
	ISynthesizeMeter*  GetInstance(SynthesizeMeterType synthesizeMeterType);
	
private:
	  char instrDescriptor[VI_FIND_BUFLEN];
	  ViUInt32 numInstrs;
	  ViFindList findList;
	  ViSession defaultRM;
	  char* errorInfo ;
};
 

static	ViSession instr;