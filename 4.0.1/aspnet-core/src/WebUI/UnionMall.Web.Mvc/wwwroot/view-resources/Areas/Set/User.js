(function () {

    $(function () {

        var _userService = abp.services.app.user;
        var _$modal = $('#roleCreate');
        var _$form = $("#userAdd");
        $("#add").click(function () {
            $("#roleCreate").modal("show");
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

})();
function Delete(btn) {
    var _userService = abp.services.app.user;
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
            //_userService.delete(id).done(function (data) {
            //    $(".pagination .active a").click();
            //});
            _userService.delete({
                id: id
            }).done(function () {
                $(".pagination .active a").click();
            });
        }
    });
}