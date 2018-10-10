(function () {
    $(function () {
        var storeService = abp.services.app.chainStore;
        var _$modal = $("#CreateModal");
        var _$form = _$modal.find("form");
        $("#Save").click(function (e) {
            e.preventDefault();
            if (!_$form.valid()) {
                return;
            }
            var EditDto = _$form.serializeFormToObject();
            storeService.createOrEdit(EditDto).done(function (data) {
                _$modal.modal('hide');
                _$form[0].reset();
                $(".pagination .active a").click();
            }).fail(function (data) {
            }).always(function (data) { });
        });
        _$modal.on("hidden.bs.modal", function () {
            $("#formPost").find('input[type=text],textarea,input[type=hidden],input[type=number]').each(function () {
                $(this).val('');
            });
            $("#formPost").find("select").each(function () {
                $(this).val($(this).find("option").eq(0).attr("value"));
            });
            ProChange($("select[name='ProviceID']"));
        });
        $("#add").click(function () {
            $("#CreateModal").modal("show");
        });
    });
})();
function Delete(btn) {
    var Service = abp.services.app.chainStore;
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

            $.post("/business/chainstore/delete", { id: id }, function (data) {
                if (data.result.succ) {
                    $(".pagination .active a").click();
                } else {
                    swal(
                        data.result.msg,
                        '',
                        'error'
                    )
                }
            });
        }
    })
}