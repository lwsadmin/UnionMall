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
            ////_$form.find("input[name='Title']").val('');
            ////_$form.find("select[name='ParentId']").val('');
            ////_$form.find("input[name='Sort']").val(0);
            ////_$form.find("textarea[name='Note']").val('');
        });
        $("#add").click(function () {
            $("#CreateModal").modal("show");
        });
    });
})();