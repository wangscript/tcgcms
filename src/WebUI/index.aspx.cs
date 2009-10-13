using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.OleDb;
using System.Web.UI.WebControls;

using TCG.Pages;
using TCG.Files.Handlers;

using TCG.Utils;
using TCG.Entity;
using TCG.Release;

public partial class index :  Origin
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //string fullName = @"E:\TCG.V9\src\WebUI\index.zip";//C:\test\a.zip 
        //解压文件 
        //AttachmentUnZip.UpZip(fullName); 
        // string[] FileProperties = new string[2]; 
        // FileProperties[0] = fullName;//待解压的文件 
        // FileProperties[1] = System.IO.Path.GetDirectoryName(fullName);//解压后放置的目标目录 
        // UnZipClass UnZc=new UnZipClass(); 
        // UnZc.UnZip(FileProperties); 
        //string dir = System.IO.Path.GetDirectoryName(fullName);
        //string fileName = System.IO.Path.GetFileName(fullName);
        //fileZip.UnZipFile(fileName, dir);

        //List<VersionItem> vers = Versions.WebVersionHistory;

       // Response.Write(Versions.GetVerStr( Versions.HigherVersion.Ver));
        

    }

    private void Zip()
    {
        string [] FileProperties = new string[2];
        string fullName = @"E:\TCG.V9\src\WebUI\index.htm";
        string destPath=System.IO.Path.GetDirectoryName(fullName);//C:\test 
        //待压缩文件 
        FileProperties[0]=fullName; 

        //压缩后的目标文件 
        FileProperties[1]= destPath +"\\"+ System.IO.Path.GetFileNameWithoutExtension(fullName) + ".zip";
        fileZip Zc = new fileZip(); 
        Zc.ZipFileMain(FileProperties); 
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