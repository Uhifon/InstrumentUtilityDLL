
#include "stdafx.h"
#include "Agilent_856x.h"
#include "stdlib.h"
#include <string>

using namespace std;

Agilent_856x::Agilent_856x()
{

}

Agilent_856x::~Agilent_856x()
{

}

char* Agilent_856x::GetID()
{
	char* sendMsg = "ID?;";
	char* recvMsg="";
	try
	{
		bool res = WriteAndReadString(sendMsg, recvMsg);
 
		return recvMsg;
	}
	catch (char *str)
	{
		throw(str);
	}
	return nullptr;
}

bool Agilent_856x::Reset()
{
	char* sendMsg = "IP;";
	try
	{
		WriteString(sendMsg);
		return true;
	}
	catch (char *str)
	{
		throw(str);
	}
	return false;
}

double Agilent_856x::GetCenterFreq()
{
	char* sendMsg = "CF?;";
	char* recvMsg="";
	try
	{
		bool res = WriteAndReadString(sendMsg, recvMsg);
		return atof(recvMsg);
	}
	catch (char *str)
	{
		throw(str);
	}
	return -1;
}

double Agilent_856x::GetMKA()
{
	char* sendMsg = "MKA?;";
	char* recvMsg = "";
	try
	{
		bool res = WriteAndReadString(sendMsg, recvMsg);
		return atof(recvMsg);
	}
	catch (char *str)
	{
		throw(str);
	}
	return -999;
}

bool Agilent_856x::SetCenterFreq(FrequencyUnit unit, double value)
{
	string sendMsg = "CF  " ;
	sendMsg += value;
	switch (unit)
	{
	case FrequencyUnit::Hz:
		sendMsg += "Hz";
		break;
	case FrequencyUnit::KHz:
		sendMsg += "KHz";
		break;
	case FrequencyUnit::MHz:
		sendMsg += "MHz";
		break;
	case FrequencyUnit::GHz:
		sendMsg += "GHz";
		break;
	}
	try
	{
		WriteString(const_cast<char*>(sendMsg.data()));
		return true;
	}
	catch (char *str)
	{
		throw(str);
	}
	return false;
}

bool Agilent_856x::SetRBW(bool isAuto, double value)
{
	string sendMsg = "BAND:AUTO ";
	bool res = false;
	try
	{
		if (isAuto)
		{
			sendMsg += "ON";
		    res = WriteString(const_cast<char*>(sendMsg.data()));
			return res;
		}
		else
		{
			sendMsg += "OFF";
		    res = WriteString(const_cast<char*>(sendMsg.data()));
 
			char* buf = new char[sizeof(value) + 8];   //设置RBW值
			printf_s(buf, "RB %.2f kHz;", value);
			res = WriteString(buf);
			return res;
		}
	}
	catch (char* str)
	{
		throw(str);
	}
	return false;
}

bool Agilent_856x::SetRefLevel(double value)
{
	char* sendMsg = new char[sizeof(value) + 8];   //设置RBW值
	bool res = false;
	try
	{
		printf_s(sendMsg, "RL %f dBm;", value);
		res = WriteString(sendMsg);
		return res;
	}
	catch (char* str)
	{
		throw(str);
	}
	return false;
}


bool Agilent_856x::MarkPeak()
{
	char* sendMsg = "TS;MKPK HI;";   
	bool res = false;
	try
	{
		res = WriteString(sendMsg);
		return res;
	}
	catch (char* str)
	{
		throw(str);
	}
	return false;
}

bool Agilent_856x::SetAttenuation(bool isAuto, double value)
{
	char* sendMsg = nullptr ;
	bool res = false;
	try
	{
		if (isAuto)
		{
			sendMsg = "AT AUTO";
			res = WriteString(sendMsg);
			return res;
		}
		else
		{
			sendMsg = new char[sizeof(value) + 8];   
			printf_s(sendMsg, "AT MANUAL;AT f% DB", value);
			res = WriteString(sendMsg);
			return res;
		}
	}
	catch (char* str)
	{
		throw(str);
	}
	return false;
}

bool Agilent_856x::SetSpan(double value, FrequencyUnit unit)
{
	string sendMsg = "SP ";
	sendMsg += value;
	switch (unit)
	{
	case FrequencyUnit::Hz:
		sendMsg += "Hz";
		break;
	case FrequencyUnit::KHz:
		sendMsg += "KHz";
		break;
	case FrequencyUnit::MHz:
		sendMsg += "MHz";
		break;
	case FrequencyUnit::GHz:
		sendMsg += "GHz";
		break;
	}
	try
	{
		WriteString(const_cast<char*>(sendMsg.data()));
		return true;
	}
	catch (char *str)
	{
		throw(str);
	}
	return false;
}

 
	
