<%@ Page Title="Manage stored cookies by ASProxy in your PC" Inherits="SalarSoft.ASProxy.PageInMasterLocale" Language="C#" MasterPageFile="~/Theme.master" culture="auto" meta:resourcekey="Page" uiculture="auto" %>
<%@ Import Namespace="SalarSoft.ASProxy" %>
<script runat="server">
	const string authenticationCookie = "ASProxyUser";
	const string sessionStateCookie = "ASProxySession";

	private HttpCookieCollection GetCookiesCollection()
	{
		HttpCookieCollection result = new HttpCookieCollection();
		for (int i = 0; i < Request.Cookies.Count; i++)
		{
			HttpCookie cookie = Request.Cookies[i];
			if (cookie.Name != sessionStateCookie &&
				cookie.Name != authenticationCookie &&
				cookie.Name != Consts.FrontEndPresentation.UserOptionsCookieName &&
				cookie.Name != Consts.FrontEndPresentation.HttpCompressor)
			{
				result.Add(cookie);
			}
		}
		return result;
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		rptCookies.DataSource = GetCookiesCollection();

		rptCookies.DataBind();
	}

	protected void rptCookies_btnDelete(object source, EventArgs e)
	{
		Button btn = (Button)source;
		string cookieName = btn.CommandArgument;

		if (btn.CommandName == "DeleteCookie")
		{
			HttpCookie cookie = Response.Cookies[cookieName];
			cookie.Expires = DateTime.Now.AddYears(-10);

			Response.Cookies.Add(cookie);
			Response.Redirect(Request.Url.ToString(), false);
		}
	}

	protected void rptCookies_btnClearCookies(object source, EventArgs e)
	{
		Button btn = (Button)source;
		string cmdName = btn.CommandName;

		if (cmdName == "ClearCookies")
		{
			HttpCookieCollection reqCookies = GetCookiesCollection();
			for (int i = 0; i < reqCookies.Count; i++)
			{
				HttpCookie cookie = reqCookies[i];
				cookie.Expires = DateTime.Now.AddYears(-10);
				Response.Cookies.Add(cookie);
			}
			Response.Redirect(Request.Url.ToString(), false);
		}
	}
</script>

<asp:Content ContentPlaceHolderID="plhHeadMeta" Runat="Server">
<meta name="Keywords" content="在线代理,网页代理,在线网站代理,在线代理服务,浏览代理,ip代理,网络代理,在线代理地址,代理论坛,在线电视,|IE代理|代理网站|免费代理|代理网址|webproxy|free web proxy|proxy server,free proxy">
<meta name="Description" content="本站提供免费在线代理网站服务(free web proxy )。在上面的输入框中输入想要访问的网站地址(URL),选择列表中的代理服务器,点击“GO”按钮就可以进行使用。使用在线WEB代理可以让您加快网页浏览速度,在线代理并提高上网的安全性。国外代理能够浏览被屏蔽的网站以及封禁中国IP的国外网站,使你上网可以畅游无阻。同时,亦作为学习交流之用,禁止用其在线网页代理浏览任何非法内容,凡因违规浏览而引起的任何法律纠纷,本站概不负责。IE代理(Proxyie.cn)">
<title>管理你的IE Cookie - Web代理 -提供国外免费在线网页代理|在线WEB代理|免费代理服务器|webproxy|free web proxy - (webdl.tcgcms.cn)</title>
	<style type="text/css">
		.desc
		{
			overflow: auto;
		}
		.cookieValue
		{
			min-height: 25px;
			max-height: 90px;
			overflow-y: auto;
			width: 650px;
			word-break: break-all;
			word-wrap: break-word;
		}
		.tblOptions
		{
			margin-top: 10px;
		}
		.option th
		{
			background-color: black;
			color: White;
			margin: 0 auto;
			padding: 10px 0 10px 0;
			text-align: center;
			-moz-border-radius: 10px 10px 0px 0px;
			-webkit-border-radius: 10px 10px 0px 0px;
			border-radius: 10px 10px 0px 0px;
		}
	</style>
</asp:Content>


<asp:Content ContentPlaceHolderID="plhMainBar" Runat="Server">
<script type="text/javascript">	document.getElementById('mnuCookie').className = 'first';</script>
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


<asp:Repeater ID="rptCookies" runat="server">
<HeaderTemplate>
<table cellpadding="0" cellspacing="0" class="tblOptions">
	<tr class="option">
		<th style="width: 100px">
			<asp:Literal ID="lblHeaderOptions" EnableViewState="False" runat="server" meta:resourcekey="lblHeaderOptions"
				Text="Options"></asp:Literal>
		</th>
		<th colspan="4" dir="" style="text-align: center;">
			<asp:Literal ID="lblHeaderDetails" EnableViewState="False" runat="server" meta:resourcekey="lblHeaderDetails"
				Text="Details"></asp:Literal>
		</th>
	</tr>
	<tr class="option">
		<td>
			&nbsp;
		</td>
		<td>
			<asp:Literal ID="tdName" EnableViewState="False" runat="server" meta:resourcekey="tdName"
				Text="Name"></asp:Literal>
		</td>
		<td>
			<asp:Literal ID="tdValue" EnableViewState="False" runat="server" meta:resourcekey="tdValue"
				Text="Value"></asp:Literal>
		</td>
	</tr>
</HeaderTemplate>
<FooterTemplate>
<tr class="option">
	<td colspan="4" align="left">
		<asp:Button CssClass="button" ID="btnClearCookies" runat="server" OnClick="rptCookies_btnClearCookies"
			CommandName="ClearCookies" Text="Delete All" meta:resourcekey="btnClearCookies" />
	</td>
</tr>
</table></FooterTemplate>
<ItemTemplate>
<tr class="option">
	<td>
		<asp:Button CssClass="button" ID="btnDelete" runat="server" OnClick="rptCookies_btnDelete"
			CommandName="DeleteCookie" CommandArgument="<%# Container.DataItem.ToString() %>"
			Text="Delete" meta:resourcekey="btnDelete" />
	</td>
	<td class="name">
		<%#Request.Cookies[Container.DataItem.ToString()].Name%>
	</td>
	<td class="desc">
	</td>
</tr>
<tr class="option">
	<td>
	</td>
	<td class="desc2" colspan="2">
		<div class="cookieValue"><%#HttpUtility.HtmlEncode(HttpUtility.UrlDecode(Request.Cookies[Container.DataItem.ToString()].Value)).Replace("&amp; Name=","&amp;<br /> Name=")%></div>
	</td>
</tr>
</ItemTemplate>
</asp:Repeater>
</asp:Content>


<asp:Content ContentPlaceHolderID="plhOptionsTitle" Runat="Server">
Stored cookies
</asp:Content>


