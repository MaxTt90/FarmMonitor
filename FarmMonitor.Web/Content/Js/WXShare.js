
var shareData = {
    appId: "",
    openId: "",  //访问者的OpenID
    dataId: "", //数据编号---用于区分类型
    dataType: "",//数据类型
    imgUrl: "http://ebook.volvo.wedochina.cn/Content/Images/volvo-10.png", //分享的图片
    url: location.href, //分享的链接
    title: "沃尔沃电子杂志", //分享的标题
    desc: "沃尔沃电子杂志", //分享的内容
    type: "link", //分享类型,music、video或link，不填默认为link
    dataUrl: "", //如果type是music或video，则要提供数据链接，默认为空
    pagePath: "",   //分享链接
    ajaxPath: "",   //Ajax一般处理程序路径
    shareId: "",   //分享内容编号
    shareSignId: "", //分享标记
    fromUrl: "" //分享来源页面路径
};

$(document).ready(function () {
    $.post("/Tracking/SharePara", { "url": location.href.split('#')[0] }, function (data) {
        if (data.code == "1") {
            wx.config({
                debug: false,
                appId: data.appId,
                timestamp: data.timestamp,
                nonceStr: data.nonceStr,
                signature: data.signature,
                jsApiList: [
                  'onMenuShareTimeline',
                  'onMenuShareAppMessage',
                  'onMenuShareQQ',
                  'onMenuShareWeibo'
                ]
            });
        } else {
            alert(data.msg);
        }
    }, "json");
});

wx.ready(function () {

    // 1 判断当前版本是否支持指定 JS 接口，支持批量判断
    //wx.checkJsApi({
    //    jsApiList: [
    //      'getNetworkType',
    //      'previewImage'
    //    ],
    //    success: function (res) {
    //        alert(JSON.stringify(res));
    //    }
    //});

    wx.onMenuShareAppMessage({
        title: shareData.title, // 分享标题
        desc: shareData.desc, // 分享描述
        link: shareData.url, // 分享链接
        imgUrl: shareData.imgUrl, // 分享图标
        type: shareData.type, // 分享类型,music、video或link，不填默认为link
        dataUrl: shareData.dataUrl, // 如果type是music或video，则要提供数据链接，默认为空
        trigger: function (res) {
            //alert('用户点击发送给朋友');
        },
        success: function (res) {
            //shareCallBack("1");
        },
        cancel: function (res) {
            //alert('已取消');
        },
        fail: function (res) {
            alert(JSON.stringify(res));
        }
    });

    wx.onMenuShareTimeline({
        title: shareData.title,
        link: shareData.url,
        imgUrl: shareData.imgUrl,
        trigger: function (res) {
            //alert('用户点击分享到朋友圈');
        },
        success: function (res) {
            //alert('已分享');
            //shareCallBack("2");
        },
        cancel: function (res) {
            //alert('已取消');
        },
        fail: function (res) {
            //alert(JSON.stringify(res));
        }
    });
});

wx.error(function (res) {
    //alert("1");
    //alert(res.errMsg);
});

//分享后回调函数
function shareCallBack(shareType) {
    $.ajax({
        type: "POST",
        cache: false,
        url: shareData.ajaxPath,
        data: { "action": "shareRecord", "DataType": shareData.dataType, "DataId": shareData.dataId, "ShareType": shareType, "ShareContentId": shareData.shareId, "ShareSignId": shareData.shareSignId, "Url": shareData.pagePath, "FromUrl": shareData.fromUrl, "ShareId": shareData.shareId, "Title": shareData.title, "Description": shareData.desc, "ImgUrl": shareData.imgUrl }
    });
}

//把图片路径转化为http开头的url
function GetHttpUrl(objImgSrc) {
    var url = window.location.href;
    var objHead = url.substring(0, url.indexOf("//") + 2);
    var objHost = window.location.host;
    var objPath = url.substring(url.indexOf(window.location.host) + window.location.host.length + 1, url.lastIndexOf("/"));
    var objArray = objHost.split("/");
    var objImgUrl = "";
    if (objImgSrc.indexOf("http") == 0) {
        objImgUrl = objImgSrc;
    } else if (objImgSrc.indexOf("/") == 0) {
        objImgUrl = objHead + objHost + objImgSrc;
    } else {
        var objImagePath1 = "";//虚拟路径部分../../
        if (objImgSrc.indexOf("../") >= 0) {
            objImagePath1 = objImgSrc.substring(objImgSrc.indexOf("../"), objImgSrc.lastIndexOf("../") + 2)
        }

        var objImagePath2 = ""; //实际路径aa/bb/
        if (objImgSrc.lastIndexOf("../") >= 0) {
            objImagePath2 = objImgSrc.substring(objImgSrc.lastIndexOf("../") + 3);

        } else {
            objImagePath2 = objImgSrc
        }
        var count = 0;
        if (objImagePath1 != "") {
            objArray = objImagePath1.split("/");
            count = objArray.length;
        }
        objPathArray = objPath.split("/");
        for (var i = 0; i < objPathArray.length - count; i++) {
            objImgUrl += objPathArray[i] + "/";
        }
        objImgUrl = objHead + objHost + "/" + objImgUrl + objImagePath2;
    }

    return objImgUrl;
}