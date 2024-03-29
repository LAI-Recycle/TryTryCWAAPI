function Choose_Identity_onChange(srcElement) {

    if ($(srcElement).val() === "2") {
        $("#Choose_UID").prop("disabled", false)
        $("#Choose_UID").css("background-color", "");
    }
    else {
        $("#Choose_UID").val("");
        $("#Choose_UID").prop("disabled", true);
        $("#Choose_UID").css("background-color", "#F0F0F0");
    }
}

//-------------´ú¸Õ¥Î
//-------------Cname-------------//
function UidLink_onClick(Cname) {
    var url = "https://usfa.liontravel.com/CSV/MemberQuery" + "?Cname=" + Cname;
    window.open(url, "_blank", "width=1200,height=800");
}
//-------------Cnameµ²§ô-------------//
function OkayButton_onClick() {
    UpdatePictureDetail(Choose_cmm02_comment_id, Choose_cm302_PictureList, ModifyAction)
}

function UpdatePictureDetail(Choose_cmm02_comment_id, Choose_cm302_PictureList, ModifyAction) {
    $.ajax({
        url: "/Comment/CommentEdit?execAction=" + encodeURIComponent(_ActionTypeUpdate),
        type: "POST",
        data: {
            Choose_cmm02_comment_id: Choose_cmm02_comment_id,
            Choose_cm302_PictureList: Choose_cm302_PictureList,
            ModifyAction: ModifyAction
        },
        dataType: "json",
        async: false,
        success: function () {
        },
        error: function (jqXHR, textStatus, errorThrown) {
            _AddJsErrMessage(JsMsg_UpdatePictureDetail);
            _ShowJsErrMessageBox();
        }
    });
}