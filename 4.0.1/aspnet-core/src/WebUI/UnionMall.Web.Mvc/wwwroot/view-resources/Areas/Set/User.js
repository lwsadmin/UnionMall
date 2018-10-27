(function () {
    var _userService = abp.services.app.user;
    var _$modal = $('#roleCreate');
    var _$form = _$("#userAdd");
    $(function () {
        $("#add").click(function () {
            $("#roleCreate").modal("show");
        });
    });

    _$form.find('button[type="submit"]').click(function () {
        Save();
    });

    function Save() {
        debugger;
        if (!_$form.valid()) {
            return;
        }

        var user = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
        user.roleNames = [];
        var _$roleCheckboxes = $("input[name='role']:checked");
        if (_$roleCheckboxes) {
            for (var roleIndex = 0; roleIndex < _$roleCheckboxes.length; roleIndex++) {
                var _$roleCheckbox = $(_$roleCheckboxes[roleIndex]);
                user.roleNames.push(_$roleCheckbox.val());
            }
        }

        debugger;
        return;
        _userService.update(user).done(function () {
            _$modal.modal('hide');
            location.reload(true); //reload page to see edited user!
        }).always(function () {
            abp.ui.clearBusy(_$modal);
        });
    }
})();