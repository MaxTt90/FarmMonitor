//document.write("<script src=\"/Scripts/layer/layer.js\"></script>");

//function AlertDialog(content) {
//    layer.open({
//        title: [
//            '提示',
//            'background-color: #696969;color: #fff; font-size: 28px;'
//        ],
//        content: content,
//        btn: ['确定']

//    });
//}

document.write("<link href=\"/Scripts/artDialog4/skins/default.css\" rel=\"stylesheet\" />");
document.write("<script src=\"/Scripts/artDialog4/jquery.artDialog.js\"></script>");



//弹出提示框
function AlertDialog(c) {
    art.dialog({
        id: 'myselfdialog',
        lock: true,
        background: '#000', // 背景色
        opacity: 0.2,	// 透明度
        content: c,
        title: "提示",
        width: 550,
        height:130,
        left:100,
        ok: true
    });
}

//返回首页
function AlertDialogLink(c,link) {
    art.dialog({
        id: 'myselfdialog',
        lock: true,
        background: '#000', // 背景色
        opacity: 0.2,	// 透明度
        content: c,
        title: "提示",
        width: 550,
        height: 130,
        left: 100,
        button: [{
            name: '返回首页',
            callback: function () {
                location = link;
            },
            focus: true
        }]
    });
}

//提示弹出层即将关闭
function AlertDialogCloseHint(c, obj) {
    art.dialog({
        id: 'myselfdialog',
        lock: true,
        background: '#000', // 背景色
        opacity: 0.2,	// 透明度
        content: c,
        title: "提示",
        width: 550,
        height: 130,
        left: 100,
        button: [{
            name: '确定',
            callback: function () {
                $(obj).parents(".hint").addClass("hide");
            },
            focus: true
        },
        {
            name: '取消',
            focus: true
        }]
    });
}