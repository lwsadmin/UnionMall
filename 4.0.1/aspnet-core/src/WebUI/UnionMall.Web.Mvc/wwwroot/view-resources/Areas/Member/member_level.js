(function () {
    $(function () {
        var _Service = abp.services.app.memberLevel;

        $("#add").click(function () {
            $("#Create").modal("show");
        });
        $('#Create').on('hidden.bs.modal', function (e) {
            // do something...
            $("#Add").find("input").each(function () {
                $(this).val("");
            });
            $("input[name='Id']").val(0); $("input[name='TenantId']").val(0);
        });

        $("#Add").on("submit", function (e) {
            e.preventDefault();
            if (!$(this).valid()) {
                return;
            }
            var EditDto = $(this).serializeFormToObject();
            _Service.createOrEdit(EditDto).done(function (data) {
                $("#Create").modal('hide');
                if ($(".pagination .active a").html() != undefined) {
                    $(".pagination .active a").click();
                } else { $("#searchForm").submit(); }
              
            }).fail(function (data) {
            }).always(function (data) { });
        });
    });
})();


function Edit(Id,Title, TenantId, MinPoint, MaxPoint, SellPrice, InitPoint) {
 
    $("input[name='Id']").val(Id);
    $("input[name='Title']").val(Title);
    $("input[name='TenantId']").val(TenantId);
    $("input[name='MinPoint']").val(MinPoint);
    $("input[name='MaxPoint']").val(MaxPoint);
    $("input[name='SellPrice']").val(SellPrice);
    $("input[name='InitPoint']").val(InitPoint);
    $("#Create").modal('show');
}

function Delete(btn) {
    var _Service = abp.services.app.memberLevel;
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