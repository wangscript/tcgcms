
/**
* µ¯³ö¿ò¼Ü
* @author ÕÅ°ïÆ½ zhangbp@5173.com
* @copyright 5173.com
* @version 1.0
**/


var layer = {
    id: "",
    callBack: "",
    init: function() {
        if (!document.getElementById("bestrow")) {
            var _bg = document.createElement("div");
            var _if = document.createElement("iframe");
            _bg.id = "bestrow";
            _bg.appendChild(_if);
            document.body.appendChild(_bg);
        }
    },
    openLayer: function() {
        this.init();
        var _json = arguments[0];
        this.id = _json.id;
        this.callBack = _json.callBack;
        var _LC = document.getElementById(this.id);
        _LC.style.display = "block";
        document.getElementById("bestrow").style.display = "block";
        _LC.style.marginLeft = -1 * (_json.width / 2) - 2 + "px";
        //document.documentElement.style.overflow="hidden";

        if (window.XMLHttpRequest) {

            _LC.style.marginTop = -1 * (_json.height / 2) - 2 + "px";
        }


    },
    closeLayer: function() {
        document.getElementById(this.id).style.display = "none";
        document.getElementById("bestrow").style.display = "none";
        //document.documentElement.style.overflow="auto";
        this.callBack.call();

    }


};

   
     