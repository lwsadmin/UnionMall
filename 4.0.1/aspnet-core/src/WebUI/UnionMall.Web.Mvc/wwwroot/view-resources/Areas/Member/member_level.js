(function () {
    $(function () {
        var _Service = abp.services.app.memberLevel;

        $("#add").click(function () {
            $("#Create").modal("show");
        });
        $('#Create').on('hidden.bs.modal', function (e) {
            // do something...
            $("#Add").find("input").each(function () {
                $(this).val("");
            });
        })

    });
})();