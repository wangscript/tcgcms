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
using TCG.Release;

using System.Reflection;
using System.ComponentModel;

public partial class Test :  Origin
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //DataTable dt = base.conn.GetDataTable("SELECT * FROM ResourcesInfo order by dAddDate");
        //if (dt != null && dt.Rows.Count > 0)
        //{
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        base.conn.Execute("UPDATE ResourcesInfo SET iid ='" + Guid.NewGuid().ToString() + "' where iid='" + dt.Rows[i]["iid"].ToString() + "'");
        //    }
        //}

        DataTable dt = base.conn.GetDataTable("SELECT * FROM TemplateInfo order by id");
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                base.conn.Execute("UPDATE TemplateInfo SET id ='" + Guid.NewGuid().ToString() + "' where id='" + dt.Rows[i]["id"].ToString() + "'");
            }
        }
    }

    private void ReadExl()
    {
        string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=e:\\发黛西.xls;" + "Extended Properties=Excel 8.0;";
        OleDbConnection conn = new OleDbConnection(strConn);
        conn.Open();
        string strExcel = "";
        OleDbDataAdapter myCommand = null;
        DataSet ds = null;
        strExcel = "select * from [sheet1$]";
        myCommand = new OleDbDataAdapter(strExcel, strConn);
        ds = new DataSet();
        myCommand.Fill(ds, "table1");

        DataTable dt = ds.Tables[0];
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            for (int n = 0; n < dt.Columns.Count; n++)
            {
                Response.Write(dt.Rows[i][n].ToString() + "   ");
            }
            Response.Write("\r\n");
        }
        conn.Close();
    }
}