(function () {

    $(function () {
        debugger;
        var categoryService = abp.services.app.goodscategory;
        var _$modal = $("#CreateModal");
        var _$form = _$modal.find("form");
        _$form.find("button[type='submit']").click(function (e) {
            e.preventDefault();
            if (!_$form.valid()) {
                return;
            }
            var EditDto = _$form.serializeFormToObject();
            abp.ui.setBusy(_$form);
            categoryService.createOrEdit(EditDto).done(function () {
                _$modal.modal('hide');
                Refresh();
            }).always(function () {
                abp.ui.clearBusy(_$form);
            });
        });

        $("#CreateModal").on("hide.bs.modal", function () {
            _$form[0].reset();
        });
        $("#RefreshButton").click(function () {
            Refresh();
        });
        function Refresh() {
            window.location.reload();
        }

        $(".delete").click(function () {
            var id = $(this).attr("data-id");
            var name = $(this).attr("data-name");
            abp.message.confirm("确定要商品分类【" + name + "】吗?", function (isConfirm) {
                if (isConfirm) {
                    categoryService.delete(id).done(function () {
                        Refresh();
                    });
                }
            });
        });

        $(".edit").click(function (e) {
            e.preventDefault();

            var id = $(this).attr("data-id");

            pesonService.getPersonBuyID(id).done(function (data) {
                debugger;
                $("input[name='Id']").val(id);
                $("input[name='Name']").val(data.name).parent().addClass("focused");
                $("input[name='Email']").val(data.email).parent().addClass("focused");
                $("#Address").val(data.address).parent().addClass("focused");
            });

            _$modal.modal('show');
        });
    });
})();