$.ajaxSetup({
    statusCode: {
        200: function (response) {
            if (response.error == false && response.message.length > 0) {
                toastr.success(response.message, 'Uğurludur');
                setTimeout(function () {
                    window.location.reload();

                }, 1000);
            }
        },
        500: function () {
            toastr.error('Server xətası', 'Xəta!');
        }
    },
    beforeSend: function (xhr) {
        $('.theme-loader').fadeIn('slow', function () {
            $(this).show();
        });
    },
    complate: function () {
        $('.theme-loader').fadeOut('slow', function () {
            $(this).hide();
        });
    }
});