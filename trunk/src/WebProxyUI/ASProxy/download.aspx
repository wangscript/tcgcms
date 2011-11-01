<%@ Page Title="Download your file with ASProxy" Inherits="SalarSoft.ASProxy.PageInMasterLocale" Language="C#" MasterPageFile="~/Theme.master" culture="auto" meta:resourcekey="Page" uiculture="auto" %>
<%@ Import Namespace="SalarSoft.ASProxy" %>
<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="SalarSoft.ResumableDownload" %>
<script runat="server">
	protected void btnDisplay_Click(object sender, EventArgs e)
	{
		try
		{
			txtUrl.Text = UrlProvider.CorrectInputUrl(txtUrl.Text);

			string downurl = UrlProvider.GetASProxyPageUrl(Consts.FilesConsts.PageDownload, txtUrl.Text, true);
			lnkDownload.NavigateUrl = downurl;
			lnkDownload.Visible = true;
			divDownloadLink.Visible = true;

			lblAutoPrompt.Text = string.Format(lblAutoPrompt.Text, downurl);
			lblAutoPrompt.Visible = true;

		}
		catch (ThreadAbortException)
		{
		}
		catch (Exception ex)
		{
			if (Systems.LogSystem.ErrorLogEnabled)
				Systems.LogSystem.LogError(ex, ex.Message, Request.Url.ToString());

			lblErrorMsg.Text = ex.Message;
			lblErrorMsg.Visible = true;
		}
	}
</script>

<asp:Content ContentPlaceHolderID="plhHeadMeta" Runat="Server">
<meta name="Keywords" content="在线代理,网页代理,在线网站代理,在线代理服务,浏览代理,ip代理,网络代理,在线代理地址,代理论坛,在线电视,|IE代理|代理网站|免费代理|代理网址|webproxy|free web proxy|proxy server,free proxy">
<meta name="Description" content="本站提供免费在线代理网站服务(free web proxy )。在上面的输入框中输入想要访问的网站地址(URL),选择列表中的代理服务器,点击“GO”按钮就可以进行使用。使用在线WEB代理可以让您加快网页浏览速度,在线代理并提高上网的安全性。国外代理能够浏览被屏蔽的网站以及封禁中国IP的国外网站,使你上网可以畅游无阻。同时,亦作为学习交流之用,禁止用其在线网页代理浏览任何非法内容,凡因违规浏览而引起的任何法律纠纷,本站概不负责。IE代理(Proxyie.cn)">
<title>通过代理下载文件 - Web代理 -提供国外免费在线网页代理|在线WEB代理|免费代理服务器|webproxy|free web proxy - (webdl.tcgcms.cn)</title>
</asp:Content>


<asp:Content ContentPlaceHolderID="plhMainBar" Runat="Server">
<script type="text/javascript">	document.getElementById('mnuDownload').className = 'first';</script>
<div class="urlBar"><asp:Literal ID="lblUrl" EnableViewState="False" runat="server" 
meta:resourcekey="lblUrl" Text="Enter URL:"></asp:Literal><br />
<asp:TextBox ID="txtUrl" CssClass="urlText" runat="server" Columns="70" dir="ltr"
Width="550px" meta:resourcekey="txtUrl"></asp:TextBox>
<asp:Button CssClass="button" ID="btnDisplay" runat="server"
OnClick="btnDisplay_Click" Text="Download it" meta:resourcekey="btnDisplay" />
<div class="urlBarLinkBar" id="divDownloadLink" enableviewstate="false" visible="false"
runat="server">
<asp:Literal ID="lblPrompt" runat="server" enableviewstate="False" 
meta:resourcekey="lblPrompt" 
Text="If your borwser does not prompt download dialog click the link below."></asp:Literal>
<asp:HyperLink ID="lnkDownload" runat="server" EnableViewState="False" 
NavigateUrl="download.aspx" meta:resourcekey="lnkDownload">Download link</asp:HyperLink>
</div>
<asp:Label Style="display: block" class="urlBarDesc" ID="lblErrorMsg" runat="server"
EnableViewState="False" Font-Bold="True" ForeColor="Red" Text="Error message"
ToolTip="Error message" Visible="False" meta:resourcekey="lblErrorMsg"></asp:Label>
</div>
</asp:Content>





<asp:Content ContentPlaceHolderID="plhContent" Runat="Server">
<div id="ads">
    <div class="ad_item">
<script type="text/javascript"><!--
    google_ad_client = "ca-pub-0958480561228656";
    /* web代理首页1 */
    google_ad_slot = "0794761653";
    google_ad_width = 336;
    google_ad_height = 280;
//-->
</script>
<script type="text/javascript"
src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
</script>
    </div>
  
<div class="ad_item">
<script type="text/javascript"><!--
    google_ad_client = "ca-pub-0958480561228656";
    /* web代理2 */
    google_ad_slot = "5948042232";
    google_ad_width = 336;
    google_ad_height = 280;
//-->
</script>
<script type="text/javascript"
src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
</script>
</div>
</div>

<asp:Label ID="lblAutoPrompt" runat="server" Text='<script type="text/javascript">window.onload=function(){{window.location="{0}";}}</script>'
EnableViewState="False" Visible="False" meta:resourcekey="lblAutoPrompt"></asp:Label>
</asp:Content>


<asp:Content ContentPlaceHolderID="plhOptionsTitle" Runat="Server">
</asp:Content>

