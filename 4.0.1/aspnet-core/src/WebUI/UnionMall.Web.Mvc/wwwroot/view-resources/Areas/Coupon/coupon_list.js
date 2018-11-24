(function () {
    $(function () {
        var couponService = abp.services.app.coupon;
        var _$modal = $("#CreateModal");
        var _$form = _$modal.find("form");
        $("#Save").click(function (e) {
            e.preventDefault();
            if (!_$form.valid()) {
                return;
            }
            var EditDto = _$form.serializeFormToObject();

            couponService.createOrEdit(EditDto).done(function (data) {
                _$modal.modal('hide');
                _$form[0].reset();
                if ($(".pagination .active a").html() != undefined) {
                    $(".pagination .active a").click();
                } else { $("#searchForm").submit(); }
            }).fail(function (data) {
            }).always(function (data) { });
        });
        _$modal.on("hidden.bs.modal", function () {
            $("#formPost").find('input[type=text],textarea,input[type=number]').each(function () {
                $(this).val('');
            });
            $("#formPost").find("select").each(function () {
                $(this).val($(this).find("option").eq(0).attr("value"));
            });
            $("#hiddenImg").val("");
            $("input[name='TenantId']").val(0);
            $("input[name='Id']").val(0);
            $("#Introduce").val('');
            $('#input').fileinput('reset');
        });
        $("#add").click(function () {
            $("#CreateModal").modal("show");
        });
    });
})();
function Delete(btn) {
    var couponService = abp.services.app.coupon;
    var id = $(btn).attr("data-id");
    swal({
        title: $(btn).attr("data-title"),
        text: '',
        type: 'question',
        showCancelButton: true,
        confirmButtonText: $(btn).attr("data-ok"),
        cancelButtonText: $(btn).attr("data-cancle")
    }).then(function (isConfirm) {
        if (isConfirm.value == true) {
            couponService.delete(id).done(function (data) {
                $(".pagination .active a").click();
            });
        }
    });
}