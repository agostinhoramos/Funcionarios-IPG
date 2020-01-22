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

function LoadSearchIcon(boo){
    let ico = $("form[action] .mdi"),
    txt = $("#search");
    if(boo){ // yes
        ico.removeClass("mdi-magnify")
        .addClass("mdi-img-loading");
        txt.css("color", "#adb0b5");
    }else{ // no
        ico.removeClass("mdi-img-loading")
        .addClass("mdi-magnify");
        txt.css("color", "#495057");
    }
}

const capitalize = (s) => {
    if (typeof s !== 'string') return '';
    return s.charAt(0).toUpperCase() + s.slice(1);
};

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

    _auto_item_per_page(aspNet.EPerPage);

    $("li a#prev").click(function () {
        if (aspNet.Page > 1) {
            var url = aspNet.URL + '?page='
                + (aspNet.Page - 1) + (aspNet.Sort ? '&sort=' + aspNet.Sort : '') + (aspNet.Query ? '&q=' + aspNet.Query : '')
                + '&o=' + _asp.ordr + '&ipp=' + _asp.ippg;
            url ? window.location.assign(url) : null;
        }
    });
    $("li a#next").click(function () {
        if (aspNet.Page < aspNet.nPage) {
            var url = aspNet.URL + '?page='
                + (aspNet.Page + 1) + (aspNet.Sort ? '&sort=' + aspNet.Sort : '') + (aspNet.Query ? '&q=' + aspNet.Query : '')
                + '&o=' + _asp.ordr + '&ipp=' + _asp.ippg;
            url ? window.location.assign(url) : null;
        }
    });

    if (aspNet.Page !== 1) { $("a#prev").parent().removeClass("disabled"); } else { $("a#prev").parent().addClass("disabled"); }
    if (aspNet.Page !== aspNet.nPage) { $("a#next").parent().removeClass("disabled"); } else { $("a#next").parent().addClass("disabled"); }

    let id_wait = null;

    if (aspNet.Query) { $('html, body').animate({ scrollTop : 90 }, 'slow'); }

    try {
        aspNet.Query = decodeURIComponent(_asp.qery);
    } catch (e) { aspNet.Query = ""; }

    $("#search").keyup(function () {
        var query = $(this).val();
        clearTimeout(id_wait);
        LoadSearchIcon(!0);
        if (!query || query.length < 1) {
            aspNet.URL = aspNet.URL + '?page=1&sort='+_asp.sort+'&q=&o='+_asp.ordr+'&ipp='+_asp.ippg;
            aspNet.URL ? window.location.assign(aspNet.URL) : null;
            LoadSearchIcon(!1);
        } else {
            id_wait = setTimeout(function () {
                aspNet.URL = aspNet.URL + '?page=1&sort='+_asp.sort+'&q='+query+'&o='+_asp.ordr+'&ipp='+_asp.ippg;
                aspNet.URL ? window.location.assign(aspNet.URL) : null;
                LoadSearchIcon(!1);
            },1100);
        }
    }).val(aspNet.Query).attr("autocomplete","off")
      .focus().attr("placeholder", aspNet.pHoldTxt);

    $("#entries_per_page").change(function () {
        let ipp = $(this).val();
        aspNet.URL = aspNet.URL + '?page=' + (_asp.page ? _asp.page : '1')
            + (_asp.sort ? '&sort=' + _asp.sort : '')
            + (_asp.qery ? '&q=' + _asp.qery : '')
            + (_asp.ordr ? '&o=' + _asp.ordr : '')
            + (ipp ? '&ipp=' + ipp : '');

        aspNet.URL ? window.location.assign(aspNet.URL) : null;
    });

    $("tr.sort th").click(function () {
        let s, o, url='', type = $(this).text().trim().toLowerCase();
        try { s = location.href.split("sort=")[1].split("&")[0];
        } catch (e) { s = ""; }
        try { o = location.href.split("o=")[1].split("&")[0];
        } catch (e) { o = ""; }

        if (s!=='0') {
            url = '&sort=' + (s === '0' ? '1'
            : o !== type ? '1' : '0') + '&o=' + type;
        }

        url = aspNet.URL + "?page=" + (_asp.page ? _asp.page : '1')
            + (_asp.ippg ? "&ipp="+_asp.ippg : "") + url
            + (aspNet.Query ? "&q=" + aspNet.Query : "");
        url ? window.location.assign(url) : null;
    });
});