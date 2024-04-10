function changeLanguage(language) {
    $.ajax({
        url: "/Home/ChangeLanguage",
        type: "POST",
        data: { lang: language },
        dataType: "json",
        async: false,
        success: function (response) {
                location.reload();
        },
        error: function (xhr, status, error) {
        }
    });
}