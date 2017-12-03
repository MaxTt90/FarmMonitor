
document.write("<script src='../../../Scripts/loading/jquery.imgpreload.min.js'></script>");

var imgNum = 0;
var images = [];

function LoadingBar(id) {
    this.loadbar = $("#" + id);
    this.percentEle = $(".percent", this.loadbar);
    this.percentNumEle = $(".percentNum", this.loadbar);
    this.max = 100;
    this.currentProgress = 0;
}
LoadingBar.prototype = {
    constructor: LoadingBar,
    setMax: function (maxVal) {
        this.max = maxVal;
    },
    setProgress: function (val) {
        if (val >= this.max) {
            val = this.max;
        }
        this.currentProgress = parseInt((val / this.max) * 100) + "%";
        this.percentEle.width(this.currentProgress);
        this.percentNumEle.text(this.currentProgress);
    }
};

//get all images in style
function getallBgimages() {
    var url, B = [], A = document.getElementsByTagName('*');
    A = B.slice.call(A, 0, A.length);
    while (A.length) {
        url = document.deepCss(A.shift(), 'background-image');
        if (url) url = /url\(['"]?([^")]+)/.exec(url) || [];
        url = url[1];
        if (url && B.indexOf(url) == -1) B[B.length] = url;
    }
    return B;
}

document.deepCss = function (who, css) {
    if (!who || !who.style) return '';
    var sty = css.replace(/\-([a-z])/g, function (a, b) {
        return b.toUpperCase();
    });
    if (who.currentStyle) {
        return who.style[sty] || who.currentStyle[sty] || '';
    }
    var dv = document.defaultView || window;
    return who.style[sty] ||
    dv.getComputedStyle(who, "").getPropertyValue(css) || '';
}

Array.prototype.indexOf = Array.prototype.indexOf ||
 function (what, index) {
     index = index || 0;
     var L = this.length;
     while (index < L) {
         if (this[index] === what) return index;
         ++index;
     }
     return -1;
 }
//里面有两种方式
//id为进度条id，loaddiv为遮盖层id
function preLoadImg(id,loaddiv) {
    //第一种方式：通过dom方法获取页面中的所有img，包括<img>标签和css中的background-image
    var imgs = document.images;
    for (var i = 0; i < imgs.length; i++) {
        images.push(imgs[i].src);
    }

    var cssImages = getallBgimages();
    for (var j = 0; j < cssImages.length; j++) {
        images.push(cssImages[j]);
    }

    //第二种方式：把所有该网页上用到的图片文件都预先放入一个数组里    
    //$.imgpreload(['images/bg1.jpg', 'images/bg2.jpg'], function () {
    //    //此处是显示进度百分比时需要用到的背景图，这个可以先加载进去
    //});

    ////then push all other images in array to load    
    //images.push("images/test_1.png");
    //images.push("images/test_2.png");
    //images.push("images/test_3.png");
    ////。。。
    //images.push("images/test_n.png");

    /*这里是真正的图片预加载 preload*/
    var loadbar = new LoadingBar(id);
    var max = 100;
    loadbar.setMax(max);
    $.imgpreload(images,
    {
        each: function () {
            /*this will be called after each image loaded*/
            var status = $(this).data('loaded') ? 'success' : 'error';
            if (status == "success") {
                ++imgNum;
                //if (imgNum % 5 == 0) {
                    var v = (parseFloat(imgNum) / images.length).toFixed(2);
                    loadbar.setProgress(Math.round(v * 100));
                //}
                
            }
        },
        all: function () {
            /*this will be called after all images loaded*/
            loadbar.setProgress(100);
            setTimeout("$('.load-wrap').hide()", 500);
           // $('.' + loaddiv).hide();
        }
    });
}