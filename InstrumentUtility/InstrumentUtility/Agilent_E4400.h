#pragma once

#include "InstrumentUtility.h"


class Agilent_E4400 : public ISignalSource
{

public:
	Agilent_E4400();
	~Agilent_E4400();
	//获取设备ID号
	virtual char* GetID() ;
	//初始化仪表参数
	virtual bool Reset() ;
	//信号源发射开关
	virtual bool SetSignalSourceState(bool state) ;
	// 设置频率、功率
	virtual bool SetFreqAndLevel(FrequencyUnit unit, double freq, double level);
	//设置频率
	virtual bool SetFreq(FrequencyUnit unit, double freq);
	//设置功率
	virtual bool SetLevel(double level) ;
	//打开脉冲开关
	virtual bool SetPulse(bool onOff);
	//设置脉冲带宽
	virtual bool SetPulseWidth(double width);
	//设置脉冲周期
	virtual bool SetPulsePeriod(double period);



};