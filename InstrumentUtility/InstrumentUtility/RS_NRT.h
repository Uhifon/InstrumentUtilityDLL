#pragma once
#include "InstrumentControl.h"

class RS_NRT : public IPowerMeter
{
public:
	RS_NRT();
	~RS_NRT();
	//获取设备ID号
	virtual char* GetID() ;
	//初始化仪表参数
	virtual bool Reset() ;
	//单位切换
	virtual bool PowerUnitChange(PowerUnit unit) ;
	//获取功率计DATA 功率AVG与驻波比SWR 功率计 
	virtual bool GetPower(int sensorNum, double* avg, double* swr) ;

};

