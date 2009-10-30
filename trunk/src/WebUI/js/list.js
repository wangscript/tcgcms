$(document).ready(function() {
    var datas = "topics=";
    for (var i = 1; i < topics.length; i++) {
        var txt = (i == topics.length - 1) ? "" : ",";
        datas += "'" + topics[i] + "'" + txt;
    }

    $.ajax(
        {
            type: "GET", url: "/Manage/interface/GetTopicsClicks.aspx", data: datas, error: function() {

            },
            success: function(response) {

                var o = null;
                try {
                    eval("o=[" + response + "];");
                } catch (err) { }
                if (o != null) {
                    for (var i = 0; i < o.length; i++) {
                        try {
                            $("#click_" + o[i].Id).text("(浏览" + o[i].Click + "次)");
                        } catch (err) { }
                    }
                }
            }
        });
});
