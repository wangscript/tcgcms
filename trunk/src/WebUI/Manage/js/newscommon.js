function GetNewsItemById(id) {
    for (var i = 0; i < NewsLis.length; i++) {
        if (id == NewsLis[i][0]) {
            return NewsLis[i];
        }
    }
    return null;
}

function GetNewsSiteByClassId(id) {
    for (var i = 0; i < NewsLis.length; i++) {
        if (id == NewsLis[i][0]) {
            if (NewsLis[i][1] == 0) {
                return NewsLis[i];
            } else {
                return GetNewsSiteByClassId(NewsLis[i][1]);
            }
        }
    }
}

function GetTemplateInfoById(id) {
    for (var i = 0; i < AllTemplates.length; i++) {
        if (id == AllTemplates[i][0]) return AllTemplates[i];
    }
    return null;
}

function GetAllChildClassIdByClassId(id) {
    var st = "";
    for (var i = 0; i < NewsLis.length; i++) {
        if (NewsLis[i][1] == id) {
            var t = GetAllChildClassIdByClassId(NewsLis[i][0]);
            var s = (t == "") ? "" : ",";
            st += (st == "") ? NewsLis[i][0] + s + t : "," + NewsLis[i][0] + s + t;
        }
    }
    return st;
}