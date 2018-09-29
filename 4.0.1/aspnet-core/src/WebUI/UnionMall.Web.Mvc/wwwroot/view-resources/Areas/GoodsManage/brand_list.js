(function () {

    $(function () {
        var brandService = abp.services.app.brand;
        var _$modal = $("#CreateModal");
        var _$form = _$modal.find("form");
        $("#Save").click(function (e) {
            e.preventDefault();
            if (!_$form.valid()) {
                return;
            }
            var EditDto = _$form.serializeFormToObject();

            brandService.createOrEdit(EditDto).done(function (data) {
                _$modal.modal('hide');
                $(".pagination .active a").click();
            }).fail(function (data) {
            }).always(function (data) { });
        });
        _$modal.on("hidden.bs.modal", function () {
       
            $("#formPost").find('input[type=text],textarea,input[type=hidden],input[type=number]').each(function () {
                $(this).val('');
            });
            $("#formPost input[name='Id']").val(0);
            $("#formPost input[name='TenantId']").val(0);
        });
        $("#add").click(function () {
            $("#CreateModal").modal("show");
        });
    });
})();
function Edit(id) {
    var brandService = abp.services.app.brand;
    brandService.getById(id).done(function (data) {
        $("#formPost input[name='Id']").val(data.id);
        $("#formPost input[name='Title']").val(data.title);
        $("#formPost input[name='TenantId']").val(data.tenantId);
        $("#formPost input[name='Url']").val(data.url);
        $("#formPost input[name='Sort']").val(data.sort);
        $("#formPost textarea[name='Note']").val(data.note);
    });
    $("#CreateModal").modal('show');
}
function Delete(btn) {
    var brandService = abp.services.app.brand;
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
            brandService.delete(id).done(function (data) {
                $(".pagination .active a").click();
            });
        }
    })
}