///高拍仪
var eloam = (function () {
    var dev;
    var video;
    ///获得插件名称
    function plugin() {
        return document.getElementById('view1');
    }
    ///获得窗口名称
    function view() {
        return document.getElementById('view1');
    }
    //左转
    function Left() {
        if (video) {
            plugin().Video_RotateLeft(video);
        }
    }
    //右转 
    function Right() {
        if (video) {
            plugin().Video_RotateRight(video);
        }
    }

    function addEvent(obj, name, func) {
        if (obj.attachEvent) {
            obj.attachEvent("on" + name, func);
        } else {
            obj.addEventListener(name, func, false);
        }
    }

    function OpenVideo() {
        CloseVideo();

        dev = plugin().Global_CreateDevice(1, 0);
        video = plugin().Device_CreateVideo(dev, 0, 1);
        if (video) {

            view().View_SelectVideo(video);
            view().View_SetText("打开视频中，请等待...", 0);
            //Right();
        }
        else {
            view().View_SetText("未找到设备，接入后重试！");
        }
    }

    function CloseVideo() {
        if (video) {
            view().View_SetText("", 0);
            plugin().Video_Release(video);
            video = null;
        }
    }

    function Load() {
        //设备接入和丢失
        //type设备类型， 1 表示视频设备， 2 表示音频设备
        //idx设备索引
        //dbt 1 表示设备到达， 2 表示设备丢失		
        addEvent(plugin(), 'DevChange', function(type, idx, dbt) 
        {
            if (1 !== type) {
                return;
            }
        	OpenVideo();
        });

        view().Global_SetWindowName("view");

        plugin().Global_InitDevs();
    }

    function Unload() {
        if (video) {
            view().View_SetText("", 0);
            plugin().Video_Release(video);
            video = null;
        }
        if (dev) {
            plugin().Device_Release(dev);
            dev = null;
        }
        plugin().Global_DeinitDevs();
    }

    ///保存为图片
    function SaveToImg(fileName) {
        var Name = fileName;
        var img = plugin().Video_CreateImage(video, 0, view().View_GetObject());
        var bSave = plugin().Image_Save(img, Name, 0);
        if (bSave) {
            view().View_PlayCaptureEffect();
        }
        plugin().Image_Release(img);
        return bSave;
    }
    ///保存为Pdf
    function saveToPdf(fileName) {
        if (video) {
            var imgList = plugin().Video_CreateImageList(video, 0, 0);
            if (imgList) {
                var len = plugin().ImageList_GetCount(imgList);
                for (var i = 0; i < len; i++) {
                    var img = plugin().ImageList_GetImage(imgList, i);

                    //var date = new Date();
                    //var yy = date.getFullYear().toString();
                    //var mm = (date.getMonth() + 1).toString();
                    //var dd = date.getDate().toString();
                    //var hh = date.getHours().toString();
                    //var nn = date.getMinutes().toString();
                    //var ss = date.getSeconds().toString();
                    //var mi = date.getMilliseconds().toString();
                    //var namepdf = "\\\\192.168.0.65\\svn\\文档扫描\\" + yy + mm + dd + hh + nn + ss + mi + ".pdf";

                    var b = plugin().Image_SaveToPDF(img, 0, fileName, 0);
                    if (b) {
                        view().View_PlayCaptureEffect();
                    }
                    plugin().Image_Release(img);
                }
                plugin().ImageList_Release(imgList);
            }
        }

    }
    return {
        ///初始化载入
        Load: Load,
        ///退出卸载
        UnLoad: Unload,
        ///打开视频
        OpenVideo: OpenVideo,
        ///关闭视频
        CloseVideo: CloseVideo,
        ///保存为图片
        SaveAsImg: SaveToImg,
        ///保存为Pdf
        SaveAsPdf: saveToPdf,
        ///向左旋转
        RotateLeft: Left,
        ///向右旋转
        RotateRight:Right
    };

})();