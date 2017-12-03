document.write("<link href=\"/Scripts/artDialog4/skins/default.css\" rel=\"stylesheet\" />");
document.write("<script src=\"/Scripts/artDialog4/jquery.artDialog.js\"></script>");

function MyAlertDialog(t, c) {
    if (t == "") {
        t = "提示";
    }
    art.dialog({
        lock: true,
        background: '#000', // 背景色
        opacity: 0.2,	// 透明度
        content: c,
        title: t,
        width:300,
        ok: true
    });    
}