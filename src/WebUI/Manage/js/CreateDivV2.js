
function CreateDiv() {
    this.set = 1;
    this.setcount = 0;
    this.msgstr = "";
    this.Screen = { w: window.screen.availWidth, h: window.screen.availHeight };
    this.Default = { w: 0, h: 0 };
    this.o = null;
    this.bg = null;
    this.runing = false;

    this.Start = function() {
        var val, ac = arguments.length, av = arguments;
        val = ac > 0 ? av[0] : null;
        this.Divinit();
        this.create_num.val(0);
        this.createjdtPlacebg.css({ "width": "0px" });
        this.MsgDiv.html("");
        this.Msg.text("正在" + val + "...");
        this.createtitle.text("操作正在进行中...");
        this.msgstr = val;
    }

    this.End = function () {
        this.createtitle.text("操作已经执行完成！");
        this.Msg.text(this.msgstr + "已完成!");
        this.runing = false;
    }

    this.SetSep = function () {
        this.runing = true;
        var val, ac = arguments.length, av = arguments;
        val = ac > 0 ? av[0] : null;
        this.create_num.text(parseInt(((this.set) / this.setcount) * 100));
        var w = parseInt(((this.set) / this.setcount) * 407);
        if (w > 407) w = 407;
        this.createjdtPlacebg.css({ width: w + "px" });
        this.MsgDiv.html(this.MsgDiv.html() + val);
        this.MsgDiv[0].scrollTop = this.MsgDiv[0].scrollHeight;
        if (this.set == this.setcount) this.End();
        this.set++;
    }

    this.Divinit = function() {
        if (this.Msg == null) this.CreateDivIntoPage();
        this.createtitle = $("#createtitle");
        this.Msg = $("#Msg");
        this.create_num = $("#create_num");
        this.createjdtPlacebg = $("#createjdtPlacebg");
        this.MsgDiv = $("#MsgDiv");
    }

    this.CreateDivIntoPage = function() {
        this.o = $("<div id='CreateMsgPlace' class='MsgDiv'></div>");
        this.o.append("<div class=\"createtitle\"><span id=\"createtitle\">操作正在进行中...</span><div class=\"createclose\"><a onclick=\"javascript:layer.closeLayer();\"  href='javascript:GoTo();'></a></div></div><div class=\"createMsgbg\"><div class=\"createjdt\"><span style=\"margin-left:5px;\" id=\"Msg\"></span><span style=\"margin-left:15px;\">完成度:<span class=\"bold\" id=\"create_num\">0</span>%</span><div class=\"createjdtPlace\"><div class=\"createjdtPlacebg rbfcreate\" id=\"createjdtPlacebg\"></div></div></div><div class=\"createMsg\" id=\"MsgDiv\"></div><div class=\"createbottom\"></div></div>");
        this.bg = $("<div id=\"layerbox\" class=\"layerbox\"></div>");
        this.bg.append(this.o);
        $('body').append(this.bg);
    }
}