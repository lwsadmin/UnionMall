
$(function () {
    $('#ReturnUrlHash').val(location.hash);

    var $loginForm = $('#LoginForm');
    if (localStorage.getItem("remmber")) {
        $("#rememberme").prop("checked",true);
        $("input[name='TenancyName']").val(localStorage.getItem("TenancyName"));
        $("input[name='usernameOrEmailAddress']").val(localStorage.getItem("usernameOrEmailAddress"));
        $("input[name='Password']").val(localStorage.getItem("Password"));
    }

    $loginForm.submit(function (e) {
        e.preventDefault();
        if ($("input[name='TenancyName']").val() == "") {
            $("input[name='TenancyName']").focus();
            return false;
        }
        if ($("input[name='usernameOrEmailAddress']").val() == "") {
            $("input[name='usernameOrEmailAddress']").focus();
            return false;
        }
        if ($("input[name='Password']").val() == "") {
            $("input[name='Password']").focus();
            return false;
        }
        if ($("#rememberme").is(":checked")) {
            localStorage.setItem("remmber", $("#rememberme").val());
            localStorage.setItem("TenancyName", $("input[name='TenancyName']").val());
            localStorage.setItem("usernameOrEmailAddress", $("input[name='usernameOrEmailAddress']").val());
            localStorage.setItem("Password", $("input[name='Password']").val());
        } else {
            localStorage.clear();
        }
        $("#LoginButton").attr("disabled", "disabled");
        abp.ui.setBusy(
            $('#LoginArea'),

            abp.ajax({
                contentType: 'application/x-www-form-urlencoded',
                url: $loginForm.attr('action'),
                data: $loginForm.serialize(),
                success: function (data) { 
                    if (data != null) {
                        $("#LoginButton").removeAttr("disabled");
                        alert(data.msg);
                        //swal({
                        //    title: '',
                        //    text: data.msg,
                        //    type: 'error',
                        //    confirmButtonText: 'OK'
                        //})
                    } else {
            
                       // window.location.href = '/';
                    }
                },
                fail: function (data) {
                 
                }
            })
        );
    });

    $('a.social-login-link').click(function () {
        var $a = $(this);
        var $form = $a.closest('form');
        $form.find('input[name=provider]').val($a.attr('data-provider'));
        $form.submit();
    });
    $loginForm.find('input[type=text]:first-child').focus();
});
