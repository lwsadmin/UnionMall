(function () {
    $(function () {
        var _Service = abp.services.app.business;
        var _$modal = $("#CreateModal");
        var _$form = _$modal.find("form");
        $("#Save").click(function (e) {
            e.preventDefault();
            if (!_$form.valid()) {
                return;
            }
            var EditDto = _$form.serializeFormToObject();
            debugger;
            _Service.createOrEdit(EditDto).done(function (data) {
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
        });
        $("#add").click(function () {
            $("#CreateModal").modal("show");
        });
    });
})();

function EditShow(data) {
    $("#CreateModal").modal("show");
}
