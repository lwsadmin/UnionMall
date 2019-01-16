document.onkeydown = function ()
{
    if (event.keyCode == 13 || event.keyCode == 123 || event.keyCode == 116)
    {
        $("#readBtn").click();
        event.keyCode = 0; event.returnValue = false;
    }
}
$('#myTabs a').click(function (e)
{
    e.preventDefault();
    $("input[name='categoryId']").val($(this).attr("data-cat"));
    $("#searchForm").submit();
    $(this).tab('show');
});
$("#readBtn").click(function ()
{
    if ($("input[name='CardID']").val() == "")
    {
        $("input[name='CardID']").focus();
        return;
    }
    abp.ajax({
        contentType: 'application/x-www-form-urlencoded',
        url: "/member/cardcore/GetCardInfo",
        data: { cardId: $("input[name='CardID']").val() },
        success: function (data)
        {

            if (data.succ)
            {
                $("#HeadImg").attr("src", data.member.headImg);
                $("#CardID").html(data.member.cardID);
                $("#Level").html(data.member.level);
                $("#WeChatName").html(data.member.weChatName);
                $("#Mobile").html(data.member.mobile);
                $("#Sex").html(data.member.sex == "0" ? $("#Male").val() : $("#Female").val());
                $("#BirthDay").html(data.member.birthDay);
                $("#Integral").html(data.member.integral);
                $("#Balance").html(data.member.balance);
                $("#Email").html(data.member.email);
                $("#RegTime").html(data.member.regTime);
                $("#FullName").html(data.member.fullName);

                $("input[name='memberId']").val(data.member.id);
                $("#ConAm").focus();
            } else
            {
                toastr.options = {
                    "closeButton": false,//是否配置关闭按钮
                    "debug": false,//是否开启debug模式
                    "newestOnTop": false,//新消息是否排在最上层
                    "progressBar": true,//是否显示进度条
                    "timeOut": "2000",//1.5s后关闭消息框
                }
                toastr.error($("#noCardId").val(), $("#hint").val());
            }
        }
    })
});

$("#con").click(function (e)
{
    e.preventDefault();
    e.stopPropagation();
    if ($("input[name='CardID']").val() == "" && $("#Integral").html() == "-")
    {
        $("input[name='CardID']").focus();
        return;
    }
    if ($("#ConAm").val() == "")
    {
        $("#ConAm").focus();
        return;
    }
    var l = Ladda.create(this);
    l.start();
    var _Service = abp.services.app.member;
    var note = {};
    note.memberId = $("input[name='memberId']").val();
    note.note = $("#memo").val();
    note.totalPay = $("#ConAm").val();
    note.cashPay = $("#ConAm").val();
    note.value = $("#ConAm").val();
    _Service.memberRecharge(note).done(function (data)
    {
        console.log(data);
        if (data.succ)
        {
            toastr.options = {
                "closeButton": false,//是否配置关闭按钮
                "debug": false,//是否开启debug模式
                "newestOnTop": false,//新消息是否排在最上层
                "progressBar": true,//是否显示进度条
                "timeOut": "2000",//1.5s后关闭消息框
            }
            toastr.success(data.msg, $("#hint").val());
        } else
        {
            toastr.options = {
                "closeButton": false,//是否配置关闭按钮
                "debug": false,//是否开启debug模式
                "newestOnTop": false,//新消息是否排在最上层
                "progressBar": true,//是否显示进度条
                "timeOut": "2000"//1.5s后关闭消息框
            }
            toastr.error(data.msg, $("#hint").val());

        }
        l.stop();
        setTimeout(function ()
        {
            $("#con").removeClass("ladda-button")
        }, 1000)

        //         l.toggle();
        //l.isLoading();
        //l.setProgress(0-1);
    }).fail(function (data)
    {
    }).always(function (data) { });


});