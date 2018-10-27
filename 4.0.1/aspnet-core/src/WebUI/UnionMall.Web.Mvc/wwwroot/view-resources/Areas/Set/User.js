(function () {

    $(function () {

        var _userService = abp.services.app.user;
        var _$modal = $('#roleCreate');
        var _$form = $("#userAdd");
        $("#add").click(function () {
            $("#roleCreate").modal("show");
        });
        _$form.find('button[type="submit"]').click(function () {
            if (!_$form.valid()) {
                return;
            }
            Save();
        });

        $('.edit-user').click(function (e) {

            var userId = $(this).attr("data-id");
            e.preventDefault();
            $.ajax({
                url:'/SystemSet/User/Add?userId=' + userId,
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    $("#DivAdd").html(content);
                    $("#roleCreate").modal("show");
  
                },
                error: function (e) { }
            });
        });
    });
    function Save() {
        var _userService = abp.services.app.user;
        var _$modal = $('#roleCreate');
        var _$form = $("#userAdd");
        var user = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
        user.Surname = user.Name;
        user.roleNames = [];

        user.roleNames.push(user.role);
        //var _$roleCheckboxes = $("input[name='role']:checked");
        //if (_$roleCheckboxes) {
        //    for (var roleIndex = 0; roleIndex < _$roleCheckboxes.length; roleIndex++) {
        //        var _$roleCheckbox = $(_$roleCheckboxes[roleIndex]);
        //        user.roleNames.push(_$roleCheckbox.val());
        //    }
        //}
        alert("submit");
        return;
        if (user.Id == "0") {
            _userService.create(user).done(function () {
                _$modal.modal('hide');
                $(".pagination .active a").click();
            }).always(function () {
            });
        } else {
            _userService.update(user).done(function () {
                _$modal.modal('hide');
                $(".pagination .active a").click();
            }).always(function () {

            });
        }
    }
})();