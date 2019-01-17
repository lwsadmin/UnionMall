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
function AddItem(td)
{

    var p = parseFloat($("#total").html());
    var sp = $(td).siblings(td).eq(2).html();
    $("#total").html(parseFloat(p) + parseFloat(sp));
    var id = $(td).attr("data-id");
    if ($("#tr" + id).html() != undefined)
    {
        var n = $("#tr" + id).find("td").eq(2).html();
        $("#tr" + id).find("td").eq(2).html(parseInt(n) + 1);
        return;
    }
    var s = "<tr data-id='"+id+"' id='tr" + id + "'>\
            <td>"+ $(td).siblings(td).eq(0).html() + "</td >\
            <td>"+ $(td).siblings(td).eq(2).html() + "</td>\
            <td>1</td>\
            <td onclick='RemoveItem(this);'>\
                <a href = 'javascript:void(0);' title = '' >\
                    <i class='fa fa-close text-navy'></i>\
                    </a>\
                </td>\
            </tr>";
    $("#selectedTable").append(s);
}
function RemoveItem(td)
{
    var p = parseFloat($("#total").html());
    var sp = $(td).siblings(td).eq(1).html();
    var n = $(td).siblings(td).eq(2).html();
    $("#total").html(parseFloat(p) - parseFloat(sp) * parseFloat(n));

    $(td).parent("tr").remove();
}