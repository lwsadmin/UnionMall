
var fisrtMenuCount = 3, SubMenuCount = 5;
//var defaulName = "菜单名";

function Init()//页面加载完成初始化,加载一级二级菜单和右侧数据
{
    var firstSize = $("#menuList").find(".FirstMenu").length;

    for (var i = 0; i < fisrtMenuCount - firstSize; i++) {
        var firstMenu = "<li class='FirstMenu' data-name='' data-pagepath='' data-url=''  data-appid=''  data-key='' data-type='view' onclick='FisrtMenuClick(this);'>\
       <a href='javascript:void(0);'><span class='glyphicon glyphicon-plus' aria-hidden='true'></span></a>\
                                        </li>";
        $("#menuList").append(firstMenu);
    }
    $(".FirstMenu").each(function () {

        if ($(this).find(".subMenu").length < SubMenuCount) {

            var secondMenuHtml = "<li class='subMenu' onclick='AddSubMenu(this);'>\
<a style='color: #B9B9B9;' href='javascript:void(0);'><span class='glyphicon glyphicon-plus' aria-hidden='true'></span></a>\
                                                    </li>";
            if ($(this).find(".sub_subBox").length == 0) {

                secondMenuHtml = "  <ul class='sub_subBox' style='display: none;'> <li class='subMenu' onclick='AddSubMenu(this);'>\
<a style='color: #B9B9B9;' href='javascript:void(0);'><span class='glyphicon glyphicon-plus' aria-hidden='true'></span></a>\
                                                    </li><i class='arrow arrow_out'></i>\
                                                <i class='arrow arrow_in'></i>\
                                            </ul>";

                $(this).append(secondMenuHtml);
            } else {
                if ($(this).find(".subMenu").length!= 0) {
                    //+号按钮放在最上面
                    $(this).children(".sub_subBox").children(".subMenu").first().before(secondMenuHtml);
                } else {
                    $(this).children(".sub_subBox").append(secondMenuHtml);
                }
            }


        }
    });
    $(".FirstMenu").eq(0).addClass("current MenuSelected");
    $(".FirstMenu").eq(0).children(".sub_subBox").css("display", "block");
}

function FisrtMenuClick(obj)//点击一级菜单
{

    $(".MenuSelected").removeClass("MenuSelected");
    $(obj).addClass("current").addClass("MenuSelected").siblings(".FirstMenu").removeClass("current");
    if ($(obj).children("a").children(".firstTitle").html() == undefined)//添加
    {
        $(obj).children("a").html("").append("<span class='firstTitle'>" + defaulName + "</span>");
    }
    $(".sub_subBox").css("display", "none");
    $(obj).find(".sub_subBox").css("display", "block");

    GetMenuInfo();
}

function AddSubMenu(obj)//添加二级菜单
{
    $(".MenuSelected").removeClass("MenuSelected");
    var count = $(obj).siblings(".subMenu").length;
    $(".subMenu").removeClass("current");
    var s = "<li  data-name='菜单名' data-content='' data-key=''  data-pagepath='' data-appid=''  data-type='view' class='subMenu current MenuSelected' onclick='SubMenuClick(this);'><a style=color: #B9B9B9;'  href='javascript:void(0);'><span>菜单名</span></a></li>";

    $(obj).after(s);
    if (count == (SubMenuCount - 1)) {
        $(obj).remove();
    }
    GetMenuInfo();
    event.stopPropagation();
}

function SubMenuClick(obj)//点击二级菜单
{

    $(".MenuSelected").removeClass("MenuSelected");
    $(".subMenu").removeClass("current");
    $(obj).addClass("current").addClass("MenuSelected");
    GetMenuInfo();
    event.stopPropagation();
}

$(function () {
    $("input[name='MenuType']").on("change", function () {
       
        $(".frm_radio_label ").removeClass("selected");
        $(this).parent(".frm_radio_label").addClass("selected");
        $("input[name='MenuType']").each(function () {
            var div = $(this).data("div");
            $("#" + div).css("display", "none");
        });

        $("#" + $(this).data("div")).css("display", "block");

        SetHtmlData();
    });
});



function GetMenuInfo() {

    var selectedMenu = $(".MenuSelected");
    $("#txtMenuName").val($(selectedMenu).attr("data-name"));
    $(".global_info").html($(selectedMenu).attr("data-name"));

    $("#urlText").val($(selectedMenu).attr("data-url"));
    $("#appid").val($(selectedMenu).attr("data-appid"));
    $("#wxaPath").val($(selectedMenu).attr("data-pagepath"));


    $("input[name='MenuType'][value='" + $(selectedMenu).attr("data-type") + "']").click();
    $("#SecPath").val($(selectedMenu).attr("data-url"));
    
    $("#appid").val($(selectedMenu).attr("data-appid"));
    //if ($("#SecPath").val().length == 0) {
    //    $("#SecPath").val("");
    //}

    //if ($("#wxaPath").val().length == 0) {
    //    $("#wxaPath").val("pages/index/index");
    //}
}



///为HTML元素赋值 .MenuSelected 当前选中的html
function SetHtmlData() {

    var txtName = $("#txtMenuName").val();
    if (txtName.length == 0) {
        txtName = defaulName;
    }

    $(".MenuSelected").attr("data-name", txtName);
    $(".MenuSelected").attr("data-type", $("input[name='MenuType']:checked").val());
    $(".MenuSelected").attr("data-url", $("#urlText").val());
    $(".MenuSelected").attr("data-appid", $("input[name='appid']").val());
    $(".MenuSelected").attr("data-pagepath", $("#wxaPath").val());

    $(".MenuSelected").children("a").children("span").html(txtName);

}



//菜单删除
function Delete() {
    if ($(".MenuSelected").hasClass("FirstMenu"))//一级菜单删除
    {
        var firstMenu = "<li class='FirstMenu current MenuSelected' data-name='' data-pagepath='' data-url=''  data-appid=''  data-key='' data-type='view' onclick='FisrtMenuClick(this);'>\
       <a href='javascript:void(0);'><span class='glyphicon glyphicon-plus' aria-hidden='true'></span></a>\
<ul class='sub_subBox' style='display: none;'> <li class='subMenu' onclick='AddSubMenu(this);'>\
<a style='color: #B9B9B9;' href='javascript:void(0);'><span class='glyphicon glyphicon-plus' aria-hidden='true'></span></a>\
                                                    </li><i class='arrow arrow_out'></i>\
                                                <i class='arrow arrow_in'></i>\
                                            </ul>\
                                        </li>";
        $(".MenuSelected").after(firstMenu).eq(0).remove();
    } else {
        if ($(".MenuSelected").parent(".sub_subBox").find(".glyphicon").length == 0) {
            $(".MenuSelected").after("<li class='subMenu' onclick='AddSubMenu(this);'><a style='color: #B9B9B9;'\
            href='javascript:void(0);'><span class='glyphicon glyphicon-plus' aria-hidden='true'></span></a></li>");
        }

        $(".MenuSelected").remove();

    }
}