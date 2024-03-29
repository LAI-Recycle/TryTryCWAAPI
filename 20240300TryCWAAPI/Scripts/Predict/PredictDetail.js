function Choose_Cityname_onChange(selectElement) {
    var selectedValue = selectElement.value;
    window.location.href = "/Predict/PredictDetail?choose_cityname=" + selectedValue
    
}

//-------------´ú¸Õ¥Î
function UpdateDetail(choose_cityname) {
    $.ajax({
        url: "/Predict/PredictDetail",
        type: "POST",
        data: {
            choose_cityname: choose_cityname
        },
        dataType: "json",
        async: false,
        success: function () {
            debugger;
            window.location.href = "/Predict/PredictDetail?choose_cityname=" + choose_cityname
        },
        error: function (jqXHR, textStatus, errorThrown) {
            debugger;
            _AddJsErrMessage("¿ù»~");
            _ShowJsErrMessageBox();
        }
    });
}


//-------------Cname-------------//
function UidLink_onClick(Cname) {
    var url = "https://usfa.liontravel.com/CSV/MemberQuery" + "?Cname=" + Cname;
    window.open(url, "_blank", "width=1200,height=800");
}
//-------------Cnameµ²§ô-------------//
