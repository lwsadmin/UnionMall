var lang = $("#CurrentLanguage").attr("data-lan");
if (lang == "zh-Hans") {
    $.getScript('http://static.runoob.com/assets/jquery-validation-1.14.0/dist/localization/messages_zh.js', function () {
    });
    $.getScript('/lib/fileinput/js/locales/zh.js', function () {
    });
}
$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
    // e.target // newly activated tab
    if (String(e.target).indexOf("GoodsAttr") != -1) {
        if ($("#Goods_CategoryId").val() != "" && $("#NowCatId").val() != $("#Goods_CategoryId").val()) {
            //$("#searchForm").attr("data-ajax-update", "#tableChat");
            $("#attrForm").attr("action", "/goodsmanage/goods/GetAttr?goodsId=" + $("input[name='Id']").val() + "&catid=" + $("#Goods_CategoryId").val());
            $("#attrForm").submit();
        }
        $("#attrTable").find("input").each(function () {
            if ($(this).val() == "") {
                $(this).focus();
                return false;
            }
        });
    }
    if (String(e.target).indexOf("GoodsSpec") != -1) {

        $("#specTable").html('');
        if ($("#Goods_CategoryId").val() != "") {
            $("#specForm").attr("action", "/goodsmanage/goods/GetSpec?goodsId=" + $("input[name='Id']").val() + "&catid=" + $("#Goods_CategoryId").val());
            $("#specForm").submit();
        }
        $(".goodsLists").find("input").each(function () {
            if ($(this).val() == "") {
                $(this).focus();
                return false;
            }
        });
    }
})
$(function () {
    if ($("input[name='Id']").val() != "0") {
        $("#attrForm").attr("action", "/goodsmanage/goods/GetAttr?goodsId=" + $("input[name='Id']").val() + "&catid=" + $("#Goods_CategoryId").val());
        $("#attrForm").submit();

        $("#specForm").attr("action", "/goodsmanage/goods/GetSpec?goodsId=" + $("input[name='Id']").val() + "&catid=" + $("#Goods_CategoryId").val());
        $("#specForm").submit();
    }

    var imgArray = [];

    if (img != '') {
        imgArray.push(img);
    }
    $('.chosen-select').chosen({ width: "100%" });
    $("#formPost").validate({ ignore: "" });
    $("#input").fileinput({
        language: $("#CurrentLanguage").attr("data-lan") == 'zh-Hans' ? 'zh' : 'es',
        showRemove: false,
        showUpload: false,
        allowedFileExtensions: ['jpg', 'png', 'jpeg'],//接收的文件后缀
        showUploadedThumbs: false,
        uploadUrl: "/Common/Upload",
        initialPreview: imgArray,
        showClose: false,
        showCancel: false,
        slugCallback: function (filename) {
            return filename.replace('(', '_').replace(']', '_');
        }
    });
    //选择文件后处理事件
    $("#input").on("filebatchselected", function (event, files) {
        //执行文件上传方法
        $("#input").fileinput("upload");
    });
    //上传成功后处理方法
    $("#input").on("fileuploaded", function (event, data, previewId, index) {
        var url = data.response.result.url;
        $("input[name='Image']").val(url);
        //$(".hid" + index).val(url).addClass(previewId);
    });

    var _Service = abp.services.app.goods;
    $("#formPost").on("submit", function (e) {

        e.preventDefault();
        if (!$(this).valid()) {
            if (String($("#myTabs .active a").attr("href")).indexOf("basic") == -1) {
                $("a[href='#basic']").tab('show');
            }
            return;
        }
        if ($("#Goods_CategoryId").val() == "") {
            if (String($("#myTabs .active a").attr("href")).indexOf("basic") == -1) {
                $("a[href='#basic']").tab('show');
            }
            $("#Goods_CategoryId").trigger("chosen:open");
            return;
        }
        //if ($("#Goods_BrandId").val() == "") {
        //    if (String($("#myTabs .active a").attr("href")).indexOf("basic") == -1) {
        //        $("a[href='#basic']").tab('show');
        //    }
        //    $("#Goods_BrandId").trigger("chosen:open");
        //    return;
        //}
        if ($("#Goods_ChainStoreId").val() == "") {
            if (String($("#myTabs .active a").attr("href")).indexOf("basic") == -1) {
                $("a[href='#basic']").tab('show');
            }
            $("#Goods_ChainStoreId").trigger("chosen:open");
            return;
        }
        if ($("#Goods_Type").val() == "") {
            if (String($("#myTabs .active a").attr("href")).indexOf("basic") == -1) {
                $("a[href='#basic']").tab('show');
            }
            $("#Goods_Type").trigger("chosen:open");
            return;
        }
        var arrValid = true, specValid = true;
        $("#attrTable").find("input").each(function () {
            if ($(this).val() == "") {
                arrValid = false;
                if (String($("#myTabs .active a").attr("href")).indexOf("GoodsAttr") == -1) {
                    $("a[href='#GoodsAttr']").tab('show');
                    return false;
                }
                arrValid = false;
                $(this).focus();
                return false;
            }
        });
        if (arrValid == false) {
            return;
        }
        $(".goodsLists").find("input").each(function () {
            if ($(this).val() == "") {
                specValid = false;
                if (String($("#myTabs .active a").attr("href")).indexOf("GoodsSpec") == -1) {
                    $("a[href='#GoodsSpec']").tab('show');
                    return false;
                }
                arrValid = false;
                $(this).focus();
                return false;
            }
        });
        if (specValid == false) {
            return;
        }
        var goods = $(this).serializeFormToObject();
        goods.categoryId = $("#Goods_CategoryId").val();
        goods.brandId = ($("#Goods_BrandId").val() == "" ? "0" : $("#Goods_BrandId").val());
        goods.chainStoreId = $("#Goods_ChainStoreId").val();
        goods.type = $("#Goods_Type").val();
        var EditDto = {};
        EditDto.goods = goods;
        var imgArray = [];
        for (var i = 0; i < 7; i++) {
            if ($("#Url" + i).val() == "") {
                continue;
            }
            imgArray.push({
                tenantId: $("input[name='TenantId']").val(),
                objectId: $("input[name='Id']").val(),
                type: 0,
                id: $("#id" + i).val(),
                size: $("#Size" + i).val(),
                url: $("#Url" + i).val(),
                name: $("#Name" + i).val()
            });
        }
        EditDto.imageList = imgArray;
        var result = [];
        $(".goodsLists").find("tr").each(function () {
            var tr = $(this);
            if (tr.find("td").html() != undefined) {
                var objValue = {
                    objectId: $("input[name='Id']").val(),
                    type: 0,
                    image: '',
                    price: tr.find("input[name='price']").val(),
                    retailPrice: tr.find("input[name='RetailPrice']").val(),
                    stock: tr.find("input[name='Stock']").val(),
                    sKU: tr.find("input[name='SKU']").val(),
                };
                var valueText = "";
                $(this).find("td").each(function () {
                    if ($(this).attr("data-value") != undefined) {
                        valueText += (($(this).attr("data-spec") + ":" + $(this).attr("data-value") + ','));
                    }
                });
                objValue.valueText = valueText;
                result.push(objValue);
            }
        });
        EditDto.valueList = result;
        _Service.createOrEdit(EditDto).done(function (data) {
            var url = sessionStorage.getItem("page");
            if (url != "" && url != undefined) {
                window.location.href = url;
            } else {
                window.location.href = '/goodsmanage/goods/list';
            }

        }).fail(function (data) {
        }).always(function (data) { });
    });
});
var language = $("#CurrentLanguage").attr("data-lan") == 'zh-Hans' ? 'zh-CN' : 'en';
var editor
KindEditor.ready(function (K) {
    editor = K.create('#Detail',
        {
            langType: language,
            height: '300px',
            uploadJson: "/Common/EditorUpload",
            fileManagerJson: '/Common/Upload',
            allowFileManager: false,
            formatUploadUrl: false,
            items: [
                'justifyleft', 'justifycenter', 'justifyright',
                'justifyfull', '|', 'fontname', 'fontsize', 'forecolor', 'hilitecolor', 'bold',
                'italic', 'underline', '|', 'image', 'media', 'table', 'hr', 'emoticons', 'baidumap', 'link', '|',
                'quickformat', 'preview', 'fullscreen'
            ],
            afterBlur: function () { this.sync(); },
            afterUpload: function (url) { },
            afterError: function (data) { }
        });
});