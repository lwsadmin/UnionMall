﻿(function () {

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
            if (EditDto.ParentId=="") {
                EditDto.ParentId = "0";
            }
            categoryService.createOrEdit(EditDto).done(function (data) {
                _$modal.modal('hide');
                _$form[0].reset();
                $(".pagination .active a").click();
            }).fail(function (data) {
              //  debugger;
             //   abp.message.error(data.message, 'Error');
            }).always(function (data) {
               
                
            });
        });

    });
})();