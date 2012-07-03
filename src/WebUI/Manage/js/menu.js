//--------------

function show() {
    var small = $(document).find(".sbg2");
    small.each(function(n) {
        var tsmall = $(small[n]);
        tsmall.css({ "display": "block" });
    });
}

function hidden() {
    var small = $(document).find(".sbg2");
    small.each(function(n) {
        var tsmall = $(small[n]);
        tsmall.css({ "display": "none" });
    });
}

function ChangeIcon(num) {
    for (var i = 0; i < stNums[num]; i++) {
        var st = $("#menu_" + num + "_" + i);
        if (st ) {
            st.toggle();
        }
    }

    var small = $(document).find(".sbg2");
    small.each(function (n) {
        var tsmall = $(small[n]);
        if (tsmall.attr("id").indexOf("menu_" + num) == -1) {
            tsmall.css({ "display": "none" })
        }
    });
}