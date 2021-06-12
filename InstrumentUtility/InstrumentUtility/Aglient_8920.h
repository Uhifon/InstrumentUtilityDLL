#pragma once
#include "InstrumentUtility.h"

class Aglient_8920 : public ISynthesizeMeter
{
public:
	Aglient_8920();
	~Aglient_8920();
	//获取设备ID号
	virtual char* GetID() ;
	//初始化仪表参数
	virtual bool Reset() ;
	//设置中心频率  
	virtual bool SetCenterFreq(FrequencyUnit unit, double value) ;
	//设置参考电平  幅度标尺  
	virtual bool SetRefLevel(double value);
	//获取中心频率
	virtual double GetCenterFreq();
	//设置解调开关
	virtual bool SetAfgState(bool onOff) ;
};

