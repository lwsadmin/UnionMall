
$(function () {


    var _$modal = $('#role');
    var _$form = _$modal.find('form');
   
    $('#RefreshButton').click(function () {
        refreshRoleList();
    });
    $('.btn-danger').click(function () {
        var roleId = $(this).attr("data-role-id");
        var roleName = $(this).attr('data-role-name');

        deleteRole(roleId, roleName);
    });
    $("#add").click(function () {
        debugger;
        _$modal.modal("show");
    });
    $('.btn-primary').click(function (e) {
        var roleId = $(this).attr("data-role-id");

        e.preventDefault();
        $.ajax({
            url: abp.appPath + '/SystemSet/Role/Edit?roleId=' + roleId,
            type: 'POST',
            contentType: 'application/html',
            success: function (content) {
                alert(content);
                //$('#RoleEditModal div.modal-content').html(content);
            },
            error: function (e) { }
        });
    });

    _$form.find('button[type="submit"]').click(function (e) {
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }

        var role = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
        role.permissions = [];
        var _$permissionCheckboxes = $("input[name='permission']:checked");
        if (_$permissionCheckboxes) {
            for (var permissionIndex = 0; permissionIndex < _$permissionCheckboxes.length; permissionIndex++) {
                var _$permissionCheckbox = $(_$permissionCheckboxes[permissionIndex]);
                role.permissions.push(_$permissionCheckbox.val());
            }
        }

        abp.ui.setBusy(_$modal);
        _roleService.create(role).done(function () {
            _$modal.modal('hide');
            location.reload(true); //reload page to see new role!
        }).always(function () {
            abp.ui.clearBusy(_$modal);
        });
    });

    _$modal.on('shown.bs.modal', function () {
        _$modal.find('input:not([type=hidden]):first').focus();
    });

    function refreshRoleList() {
        location.reload(true); //reload page to see new role!
    }

    function deleteRole(roleId, roleName) {
        var s = abp.localization.localize('AreYouSureWantToDelete', 'UnionMall');
        debugger;
        abp.message.confirm(
            abp.utils.formatString(abp.localization.localize('AreYouSureWantToDelete', 'UnionMall'), roleName),
            function (isConfirmed) {
                if (isConfirmed) {
                    _roleService.delete({
                        id: roleId
                    }).done(function () {
                        refreshRoleList();
                    });
                }
            }
        );
    }
});
