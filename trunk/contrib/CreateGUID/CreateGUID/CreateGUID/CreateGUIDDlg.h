
// CreateGUIDDlg.h : ͷ�ļ�
//

#pragma once
#include "afxwin.h"


// CCreateGUIDDlg �Ի���
class CCreateGUIDDlg : public CDialog
{
// ����
public:
	CCreateGUIDDlg(CWnd* pParent = NULL);	// ��׼���캯��

// �Ի�������
	enum { IDD = IDD_CREATEGUID_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV ֧��


// ʵ��
protected:
	HICON m_hIcon;

	// ���ɵ���Ϣӳ�亯��
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedOk();
	CEdit m_GUID;
	afx_msg void OnBnClickedCancel();
protected:
	virtual void OnCancel();
public:
	afx_msg void OnBnClickedButton1();
};
