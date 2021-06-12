#pragma once

#include "InstrumentUtility.h"


class Agilent_856x : public ISpectropgraph
{

public:
	 Agilent_856x();
	~Agilent_856x();
	//获取设备ID号
	virtual  char* GetID();
	//初始化仪表参数
	virtual bool Reset() ;
	//获取中心频率  
	virtual double GetCenterFreq() ;
	//获取MKA  峰值电平
	virtual double GetMKA();
	//设置中心频率  
	virtual bool SetCenterFreq(FrequencyUnit unit, double value) ;
	//设置RBW  
	virtual bool SetRBW(bool isAuto,double value=0);
	//设置参考电平
	virtual bool SetRefLevel(double value) ;
	//激活标记并搜索峰值  
	virtual bool MarkPeak() ;
	//设置衰减
	virtual bool SetAttenuation(bool isAuto, double value) ;
	//设置SPAN
	virtual bool SetSpan(double value, FrequencyUnit unit) ;



};