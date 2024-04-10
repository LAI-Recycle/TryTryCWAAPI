function changeLanguage(language) {
    $.ajax({
        url: "/Home/ChangeLanguage",
        type: "GET",
        data: { lang: language },
        dataType: "json",
        async: false,
        success: function (response) {
            debugger;
            if (response.success) {
                location.reload();
            }
        },
        error: function (xhr, status, error) {
        }
    });
}