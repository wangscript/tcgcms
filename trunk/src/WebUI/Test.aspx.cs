using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.OleDb;
using System.Web.UI.WebControls;

using TCG.Pages;
using TCG.Handlers;

using TCG.Utils;
using TCG.Entity;
//using TCG.Release;

using TCG.Data;

using System.Reflection;
using System.ComponentModel;

public partial class Test : adminMain
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write(Guid.NewGuid().ToString());
        //Resources res = (Resources)CachingService.Get("0555408c-bb8e-429a-ab01-5232c5b30e43");
        //Response.Write(Guid.NewGuid().ToString());
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