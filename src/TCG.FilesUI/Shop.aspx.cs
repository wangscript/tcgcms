using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Lifetime;
using System.IO;

using TCG.Utils;
using TCG.Files.Utils;
using TCG.Files.Entity;
using TCG.Files.Kernel;
using TCG.Files.Handlers;

public partial class Shop : FilesMain
{
    string filename = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        string dir = Server.MapPath("filePatch/shop");
        string filename = Fetch.Get("Folder");
        string ext = string.Empty;
        ListFiles(new DirectoryInfo(dir),ref ext);
        this.ltFileNames.Text = ext;

        DirectoryInfo imagesfile = new DirectoryInfo(Server.MapPath("filePatch/shop/" + filename));
        ItemRepeater.DataSource = imagesfile.GetFiles("*.*");
        ItemRepeater.DataBind();

    }

    protected void ItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        FileInfo file = (FileInfo)e.Item.DataItem;
        Image Image1 = (Image)e.Item.FindControl("Image1");
        Literal ltImagePath = (Literal)e.Item.FindControl("ltImagePath");
        Literal Literal1 = (Literal)e.Item.FindControl("Literal1");
        

        string text = base.config["FileSite"] + "filePatch/shop/" + file.DirectoryName.Substring(file.DirectoryName.LastIndexOf("\\")+1,
            file.DirectoryName.Length - file.DirectoryName.LastIndexOf("\\")-1) + "/" + file.Name;

        Image1.ImageUrl = text;
        ltImagePath.Text = text;
        Literal1.Text = text;
    }

    public void ListFiles(FileSystemInfo info,ref string text)
    {
        if (!info.Exists) return;
        if (info.Name.ToLower() != "shop")
        {
            text += "<a href=\"shop.aspx?Folder=" + info.Name + "\">" + info.Name + "</a>";
        }
        if (string.IsNullOrEmpty(filename)) filename = info.Name;

        DirectoryInfo dir = info as DirectoryInfo;
        if (dir == null) return ;
        FileSystemInfo[] files = dir.GetFileSystemInfos();
        for (int i = 0; i < files.Length; i++)
        {
            FileInfo file = files[i] as FileInfo;
            if (file == null)
            {
                ListFiles(files[i],ref text);
            }

        }

    }


}
