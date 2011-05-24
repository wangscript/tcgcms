/// <reference path="jquery-1.3.1-vsdoc.js" />
/// <reference path="../Common/AllCategories.aspx" />

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

function GetTemplateById(id) {
    for (var i = 0; i < _Template.length; i++) {
        if (id == _Template[i].Id) return _Template[i];
    }
    return null;
}

function GetAllChildClassIdByClassId(id) {
    var st = "";
    for (var i = 0; i < _Categories.length; i++) {
        if (_Categories[i].ParentId == id) {
            var t = GetAllChildClassIdByClassId(_Categories[i].Id);
            var s = (t == "") ? "" : ",";
            st += (st == "") ? _Categories[i].Id + s + t : "," + _Categories[i].Id + s + t;
        }
    }
    return st;
}

function GetAllChildClassIdByClassIdForCreateHtml(id) {
    var st = "";
    for (var i = 0; i < _Categories.length; i++) {
        if (_Categories[i].ParentId == id ) {
            var t = GetAllChildClassIdByClassId(_Categories[i].Id);

            var s = (t == "") ? "" : ",";
            st += (st == "") ? _Categories[i].Id + s + t : "," + _Categories[i].Id + s + t;
        }
    }
    return st;
}


function GetCategorieById(Id) {
    if (_Categories == null) return null;
    for (var i = 0; i < _Categories.length; i++) {
        if (_Categories[i].Id == Id) return _Categories[i];
    }
}


function GetCagetegoriesEnmu(obj, Skinid, CagetegorId) {
    if (_Categories == null) return null;
    var o = obj;
    if ($(obj).html() != "") {
        o = $("<ul></ul>")
        $(obj).append(o);
    }

    var find = false;
    for (var i = 0; i < _Categories.length; i++) {
        if (_Categories[i].ParentId == CagetegorId && _Categories[i].Skin.Id == Skinid) {
            var li = $("<li><a href=\"javascript:SelectClassValue('" + _Categories[i].Id
                + "','" + _Categories[i].ClassName + "');\" ><span>" + _Categories[i].ClassName + "</span></a></li>");
            $(o).append(li);
            find = true;
            GetCagetegoriesEnmu(li, _Categories[i].Skin.Id, _Categories[i].Id);
        }
    }

    if (!find) {
        $(o).remove();
    }
}

function GetSpecialityEnmu(obj, Skinid, CagetegorId) {
    if (_Speciality == null) return null;
    var o = obj;
    if ($(obj).html() != "") {
        o = $("<ul></ul>")
        $(obj).append(o);
    }

    var find = false;
    for (var i = 0; i < _Speciality.length; i++) {
        if (_Speciality[i].iParent == CagetegorId && _Speciality[i].SkinId == Skinid) {
            var checkh = $("#iSpeciality").val().indexOf(_Speciality[i].Id) > -1 ? "checked" : "";
            var li = $("<li><input type=\"checkbox\" name=\"iiSpeciality\" value=\"" + _Speciality[i].Id + "\" " + checkh + " /><a href=\"javascript:SelectSpecialityValue('" + _Speciality[i].Id
                + "','" + _Speciality[i].vcTitle + "');\" ><span>" + _Speciality[i].vcTitle + "</span></a></li>");
            $(o).append(li);
            find = true;
            GetSpecialityEnmu(li, _Speciality[i].SkinId, _Speciality[i].Id);
        }
    }

    if (!find) {
        $(o).remove();
    }
}


function GetSingTemplate() {
    var newall = new Array();
    for (var i = 0; i < _Template.length; i++) {
        if (_Template[i].Url != "") {
            newall.push(_Template[i]);
        }
    }
    return newall;
}