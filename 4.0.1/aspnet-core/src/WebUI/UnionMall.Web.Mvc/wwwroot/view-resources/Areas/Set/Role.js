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
   
            if (!_$form.valid()) {
                return;
            }
            var role = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
            role.permissions = [];
            var _$permissionCheckboxes = $("input[name='permission']:checked:visible");
            if (_$permissionCheckboxes) {
                for (var permissionIndex = 0; permissionIndex < _$permissionCheckboxes.length; permissionIndex++) {
                    var _$permissionCheckbox = $(_$permissionCheckboxes[permissionIndex]);
                    role.permissions.push(_$permissionCheckbox.val());
                }
            }
            console.log(role);
            debugger;
            if (role.Id=="0") {
                _roleService.create(role).done(function (data) {
                    if (data.message != null && data.message != "") {
                        sweetAlert(
                            data.message,
                            '',
                            'error'
                        )
                    }
                }).fail(function (data) {
                   // alert(data.result.error.message);
                }).always(function () {

                });
            } else {
                _roleService.update(role).done(function () {

                }).always(function () {

                });
            }


        });


    });

})();
