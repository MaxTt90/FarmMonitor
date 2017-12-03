document.write("<link href=\"/Scripts/artDialog4/skins/default.css\" rel=\"stylesheet\" />");
document.write("<script src=\"/Scripts/artDialog4/jquery.artDialog.js\"></script>");
var alertDefaultTitle = "系统提示";
var loadingDialogId = "artDialog_loading";

//弹出提示框
function MyAlertDialog(t, c) {
    if (t == "") {
        t = alertDefaultTitle;
    }
    art.dialog({
        id: 'myselfdialog',
        lock: true,
        background: '#000', // 背景色
        opacity: 0.2,	// 透明度
        content: c,
        title: t,
        width: 350,
        height: 100,
        ok: true
    });
}

//弹出提示框并链接到指定页面
function MyAlertDialogLink(t, c, link) {

    if (t == "") {
        t = alertDefaultTitle;
    }
    art.dialog({
        id: 'myselfdialog',
        lock: true,
        background: '#000', // 背景色
        opacity: 0.2,	// 透明度
        content: c,
        title: t,
        width: 300,
        ok: function () {
            location = link;
        }
    });
}
//弹出提示框并跳转指定函数
function MyAlertDialogFunc(t, c, func,e) {

    if (t == "") {
        t = alertDefaultTitle;
    }
    art.dialog({
        id: 'myselfdialog',
        lock: true,
        background: '#000', // 背景色
        opacity: 0.2,	// 透明度
        content: c,
        title: t,
        width: 300,
        ok: function () {
            func(e);
        }
    });
}
//弹出数据加载提示框
function MyAlertDialogLoading(t) {
    if (t == "") {
        t = "数据处理中，请耐心等待！";
    }
    art.dialog({
        title: t,
        cancel: false,
        id: loadingDialogId,
        lock: true,
        resize: false,
        drag: false,
        esc: false,
        opacity: 0.5,
        width: "33%",
        height: "33%",
        background: "#ccc"
    });
}

//关闭数据加载提示框
function MyAlertDialogLoadingClose() {
    art.dialog({ id: loadingDialogId }).close();
}


//定时关闭窗体
function showTips(time, content) {
    art.dialog({
        time: time,
        content: content,
        width: 300,
        id: 'dialog_time_close',
        lock: true,
        title: alertDefaultTitle,
        background: '#000', // 背景色
        opacity: 0.2	// 透明度
    });
}