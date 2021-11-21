(function ($) {
    const $body = $("body");

    // Lording
    $body.append("<div id=\"lording\" class=\"lording\"></div>");
    var $lording = $("#lording");

    // Error indicator
    $body.append("<div id=\"ajax-error\" class=\"ajax-error\"></div>");
    var $error = $("#ajax-error");
    $error.css("display", "none").css("position", "absolute");

    $(document).bind("ajaxStart", function () {
        $lording.show();
        $body.css("cursor", "wait");
    });
    $(document).bind("ajaxStop", function () {
        $lording.hide();
        $body.css("cursor", "auto");
    });
    $(document).bind("ajaxError", function (e, xhr) {
        if (xhr.status !== 0) {
            $error.text("Error: " + xhr.status);
            const $w = $(window);
            $error.css("top", $w.scrollTop() + $w.height() - $error.height()).css("width", "100%").fadeIn(0).fadeOut(3000);
        }
    });

    const input = $(".input-validation-error").first();
    if (input.length > 0) {
        input.delay(0).queue(function () {
            $(this).focus();
        });
    } else {
        $(":input:enabled:visible:not([readonly])").first().focus();
    }
}(jQuery));
