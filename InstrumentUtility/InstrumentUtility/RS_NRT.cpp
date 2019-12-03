#include "stdafx.h"
#include "RS_NRT.h"


RS_NRT::RS_NRT()
{
}


RS_NRT::~RS_NRT()
{
}

char * RS_NRT::GetID()
{
	char* sendMsg = "*IDN ?;";
	char* recvMsg = "";
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

bool RS_NRT::Reset()
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

bool RS_NRT::PowerUnitChange(PowerUnit unit)
{
	return false;
}

bool RS_NRT::GetPower(int sensorNum, double* avg, double* swr)
{
	return false;
}


