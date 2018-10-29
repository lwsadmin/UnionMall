
(function () {
    $(function () {
        $(".ck-first").on("change", function () {
            var ck = $(this);
            $("." + $(this).val()).find("input[type='checkbox']").each(function () {
                $(this).prop("checked", ck.is(":checked"));
            });
        });
        $("input[name='selectAll']").on("change", function () {
            var ck = $(this);
            $(this).parents("td").siblings("td").find("input[type='checkbox']").each(function () {
                $(this).prop("checked", ck.is(":checked"));
            });
        });
        var _roleService = abp.services.app.role;
        var _$form = $('form[name=Role]');
        var _roleService = abp.services.app.role;
        _$form.on('submit', function save() {

            var t = abp.localization.localize('Save', 'UnionMall');
            if (!_$form.valid()) {
                return;
            }
            var role = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
            role.permissions = [];
            role.manageRole = '';
            $("input[name='ManageRole']:checked").each(function () {
                role.manageRole += $(this).val() + ',';
            });
            role.manageRole = String(role.manageRole).substring(0, role.manageRole.lastIndexOf(','));

            var _$permissionCheckboxes = $("input[name='permission']:checked:visible");
            if (_$permissionCheckboxes) {
                for (var permissionIndex = 0; permissionIndex < _$permissionCheckboxes.length; permissionIndex++) {
                    var _$permissionCheckbox = $(_$permissionCheckboxes[permissionIndex]);
                    role.permissions.push(_$permissionCheckbox.val());
                }
            }
            if (role.Id == "0") {
                _roleService.create(role).done(function (data) {
                    //if (data.message != null && data.message != "") {
                    //    sweetAlert(
                    //        data.message,
                    //        '',
                    //        'error'
                    //    )
                    //}
                    SaveSuccess();

                }).fail(function (data) {
                    // alert(data.result.error.message);
                }).always(function () {

                });
            } else {
                _roleService.update(role).done(function (data) {
                    SaveSuccess();

                }).always(function () {

                });
            }
        });


    });
})();
function Delete(btn) {
    var _roleService = abp.services.app.role;
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
            _roleService.deleteServices(id).done(function (data) {
                $(".pagination .active a").click();
            });
        }
    });
}