(function () {
    $(function () {
        $("#AddPost").validate({});
        var _Service = abp.services.app.commonCategory;

        $("#add").click(function () {
            $("#Modal").modal('show');
        });
        $('#Modal').on('hidden.bs.modal', function (e) {
            // do something...
            $(".modal-title").html('');
            $("#Memo").val('');
            $("input[name='Id']").val(0);
            $("input[name='Sort']").val("");
            $("input[name='Title']").val("");
        });

        $("#AddPost").on("submit", function (e) {
            e.preventDefault();
            if (!$(this).valid()) {
                return;
            }
            var EditDto = $(this).serializeFormToObject();
            _Service.createOrEdit(EditDto).done(function (data) {
                $("#Modal").modal('hide');
                if ($(".pagination .active a").html() != undefined) {
                    $(".pagination .active a").click();
                } else { $("#searchForm").submit(); }

            }).fail(function (data) {
            }).always(function (data) { });
        });
    });
})();


function Edit(Id, Title, Sort,Memo) {

    $(".modal-title").html(Title);
    $("input[name='Id']").val(Id);
    $("input[name='Title']").val(Title);

    $("input[name='Sort']").val(Sort);

    $("#Memo").val(Memo);
    $("#Modal").modal('show');
}

function Delete(btn) {
    var _Service = abp.services.app.commonCategory;
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