var trackIdName = "aowen_track_id"; //id隐藏域编号
var trackGuid = "aowen_guid";   //GUID隐藏域编号
var sleepTime = 10000;

//初始化统计信息
$(document).ready(function () {
    var _body = $("body");
    var _objId = $("<input type=\"hidden\" value=\"0\" id=\"" + trackIdName + "\" />");
    var _objGuid = $("<input type=\"hidden\" value=\"\" id=\"" + trackGuid + "\" />");
    _body.append(_objId);
    _body.append(_objGuid);
});

//添加页面访问记录
function AddTrack(modulName) {
    var _url = location.href;
    var _sourceId = getUrlParam("s");
    var _mobile = getUrlParam("m");
    $.post("/Tracking/Add", { "url": _url, "sourceId": _sourceId, "mobile": _mobile, "modulName": modulName }, function (data) {
        if (data.code == "1") {
            $("#" + trackGuid).val(data.guid);
            $("#" + trackIdName).val(data.id);
            setTimeout("RefreshTrack()", sleepTime);
        }
    }, "json");
}

//刷新页面停留时间
function RefreshTrack(modulName) {
    var _id = $("#" + trackIdName).val();
    var _guid = $("#" + trackGuid).val();

    $.post("/Tracking/Refresh", { "id": _id, "guid": _guid }, function (data) {
        if (data.code == "1") {
            $("#" + trackIdName).val(data.id);
            setTimeout("RefreshTrack()", sleepTime);
        }
    }, "json");
}

//统计点击操作
function AddClick(modulName) {
    var _url = location.href;
    $.post("/Tracking/Click", { "url": _url, "modulName": modulName }, function (data) {
    }, "json");
}


//获取url中的参数
function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
    var r = window.location.search.substr(1).match(reg);  //匹配目标参数
    if (r != null) return unescape(r[2]); return ""; //返回参数值
}