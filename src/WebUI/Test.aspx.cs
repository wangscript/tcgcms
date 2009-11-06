using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.OleDb;
using System.Web.UI.WebControls;

//using TCG.Pages;
//using TCG.Handlers;

//using TCG.Utils;
//using TCG.Entity;
//using TCG.Release;

using System.Reflection;
using System.ComponentModel;

public partial class Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TestB b = new TestB();
        b.Ba = "sdff";
        TestB a = (TestB)b;
        a.Ba = "sdfsdf";
        TestB c = (TestB)b;
        Response.Write(c.Ba);
    }

    public class TestA
    {
        public string Aa { get { return this._aa; } set { this._aa = value; } }
        private string _aa;
    }

    public class TestB : TestA
    {
        public string Ba { get { return this._aa; } set { this._aa = value; } }
        private string _aa;
    }
    private void ReadExl()
    {
        //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=e:\\发黛西.xls;" + "Extended Properties=Excel 8.0;";
        //OleDbConnection conn = new OleDbConnection(strConn);
        //conn.Open();
        //string strExcel = "";
        //OleDbDataAdapter myCommand = null;
        //DataSet ds = null;
        //strExcel = "select * from [sheet1$]";
        //myCommand = new OleDbDataAdapter(strExcel, strConn);
        //ds = new DataSet();
        //myCommand.Fill(ds, "table1");

        //DataTable dt = ds.Tables[0];
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    for (int n = 0; n < dt.Columns.Count; n++)
        //    {
        //        Response.Write(dt.Rows[i][n].ToString() + "   ");
        //    }
        //    Response.Write("\r\n");
        //}
        //conn.Close();
    }
}