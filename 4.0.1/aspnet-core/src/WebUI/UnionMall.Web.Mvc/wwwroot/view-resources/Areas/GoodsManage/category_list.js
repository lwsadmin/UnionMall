(function () {

    $(function () {

        var categoryService = abp.services.app.goodsCategory;
        var _$modal = $("#CreateModal");
        var _$form = _$modal.find("form");
        $("#Save").click(function (e) {

            e.preventDefault();
            if (!_$form.valid()) {
                return;
            }
            var EditDto = _$form.serializeFormToObject();
            categoryService.createOrEdit(EditDto).done(function (data) {
                _$modal.modal('hide');
                $(".pagination .active a").click();
            }).fail(function (data) {
              //  debugger;
             //   abp.message.error(data.message, 'Error');
            }).always(function (data) {
               
                
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