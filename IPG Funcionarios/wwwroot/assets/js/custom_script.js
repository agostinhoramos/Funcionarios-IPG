/* ALL FUNCTION HERE */
function _form(path, params, method) {
    method = method || "post";
    var form = document.createElement("form");
    form.setAttribute("method", method);
    form.setAttribute("action", path);

    for (var key in params) {
        if (params.hasOwnProperty(key)) {
            var hiddenField = document.createElement("input");
            hiddenField.setAttribute("type", "hidden");
            hiddenField.setAttribute("name", key);
            hiddenField.setAttribute("value", params[key]);
            form.appendChild(hiddenField);
        }
    }

    document.body.appendChild(form);
    form.submit();
}

function _auto_item_per_page(val) {
    var el = $("#entries_per_page");
    el.find('option').attr("selected", null);
    el.find('option[value="' + val + '"]').attr("selected", true);
}

/* ----------------------------------------------------- */
$(document).ready(function () {
    $('.view-html [role="Edit"] [role="no-form"] input[type="submit"]').click(function () {
        var obj = {};
        var aData = $('[role="no-form"] div input.form-control');
        var len = aData.length;
        obj["__RequestVerificationToken"] = $('input[name="__RequestVerificationToken"]').val();
        obj["id"] = $("input[type='hidden']").eq(1).val();
        for (i = 0; i < len; i++) { obj[aData.eq(i).attr("name")] = aData.eq(i).val(); }
        _form(
            $('.view-html [role="no-form"] form').attr("action"),
            obj,
            $('.view-html [role="no-form"] form').attr("method")
        );
    });

    $('.view-html [role="Create"] [role="no-form"] input[type="submit"]').click(function () {
        var obj = {};
        var aData = $('[role="no-form"] div input.form-control');
        var len = aData.length;
        obj["__RequestVerificationToken"] = $('input[name="__RequestVerificationToken"]').val();
        for (i = 0; i < len; i++) { obj[aData.eq(i).attr("name")] = aData.eq(i).val(); }
        _form(
            $('.view-html [role="no-form"] form').attr("action"),
            obj,
            $('.view-html [role="no-form"] form').attr("method")
        );
    });

    $('.view-html [role="Delete"] [role="no-form"] input[type="submit"]').click(function () {
        _form(
            $('.view-html [role="no-form"] form').attr("action"),
            { __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val() },
            $('.view-html [role="no-form"] form').attr("method")
        );
    });
});