$(function () {
    var _Service = abp.services.app.commonSpec;
    $("#add").click(function () {
        $(".modal-title").html('');
        $("#Memo").val('');
        $("input[name='Id']").val(0);
        $("input[name='Name']").val("");
        $("input[name='Text']").val("");
        $("#ul_li").html('');
        $("#Modal").modal('show');
    });
});
var Save = function (e) {
    if ($("input[name='Name']").val().trim() == "") {
        $("input[name='Name']").focus();
        e.preventDefault();
        return;
    }
    if ($("#CategoryId").val() == "") {
        $("#CategoryId").trigger("chosen:open");
        e.preventDefault();
        return;
    }
    var _Service = abp.services.app.commonSpec;
    var spec = $("#AddPost").serializeFormToObject();
    var EditDto = {};
    var valueList = [];
    EditDto.spec = spec;
    $("#ul_li").find("li").each(function () {
        valueList.push({ text: $(this).find("a").html() });
    });
    EditDto.valueList = valueList;
    _Service.createOrEdit(EditDto).done(function (data) {
        $("#Modal").modal('hide');
        if ($(".pagination .active a").html() != undefined) {
            $(".pagination .active a").click();
        } else { $("#searchForm").submit(); }

    }).fail(function (data) {
    }).always(function (data) { });
}
function Edit(data) {
    $("#Modal").modal("show");
}
function SetValue() {
    if ($("input[name='Text']").val().trim() == "") {
        $("input[name='Text']").focus();
        return;
    }
    $("#ul_li").append("<li><a href='javascript:void(0);'>" + $("input[name='Text']").val() + "</a><button type='button' class='close'>\
            <span aria-hidden='true' >×</span><span class='sr-only'>Close</span></button></li>");
    $("input[name='Text']").val("").focus();

}
function RemoveItem(btn) {
    $(btn).parent("li").remove();
}
function Delete(btn) {
    var _Service = abp.services.app.commonSpec;
    var id = $(btn).attr("data-id");
    var name = $(btn).attr("data-name");
    swal({
        title: $(btn).attr("data-title"),
        text: '',
        type: 'question',
        showCancelButton: true,
        confirmButtonText: $(btn).attr("data-ok"),
        cancelButtonText: $(btn).attr("data-cancle")
    }).then(function (isConfirm) {
        if (isConfirm.value == true) {
            _Service.delete(id).done(function (data) {
                if ($(".pagination .active a").html() != undefined) {
                    $(".pagination .active a").click();
                    toastr.options = {
                        "closeButton": false,//是否配置关闭按钮
                        "debug": false,//是否开启debug模式
                        "newestOnTop": false,//新消息是否排在最上层
                        "progressBar": true,//是否显示进度条
                        "timeOut": "2000",//1.5s后关闭消息框
                    }
                    toastr.success($(btn).attr("data-msg"), '');
                } else { $("#searchForm").submit(); }
            });
        }
    });
}