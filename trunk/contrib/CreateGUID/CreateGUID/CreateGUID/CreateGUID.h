
// CreateGUID.h : PROJECT_NAME Ӧ�ó������ͷ�ļ�
//

#pragma once

#ifndef __AFXWIN_H__
	#error "�ڰ������ļ�֮ǰ������stdafx.h�������� PCH �ļ�"
#endif

#include "resource.h"		// ������


// CCreateGUIDApp:
// �йش����ʵ�֣������ CreateGUID.cpp
//

class CCreateGUIDApp : public CWinAppEx
{
public:
	CCreateGUIDApp();

// ��д
	public:
	virtual BOOL InitInstance();

// ʵ��

	DECLARE_MESSAGE_MAP()
};

extern CCreateGUIDApp theApp;