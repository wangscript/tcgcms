using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TCG.Pages;
using TCG.Files.Handlers;

using TCG.Utils;
using TCG.Entity;
using TCG.Release;

public partial class Test :  Origin
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string fullName = @"E:\TCG.V9\src\WebUI\index.zip";//C:\test\a.zip 
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
}