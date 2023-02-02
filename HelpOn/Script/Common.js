var genErrMsg = 'An error occured while processing your request';
function removeMsg(obj) {
    $(obj).removeClass('succmsg');
    $(obj).html('');
}
function addSuccess(obj, errormsg) {
    $(obj).show();
    $(obj).addClass('col-md-12 alert alert-success background-success');
    $(obj).html(errormsg);
    $(obj).fadeOut(10000);
}
function feedMsg(obj, msg) {
    $(obj).stop(true, true);
    $(obj).show();
    $(obj).addClass('feed-bar');
    $(obj).html(msg);
    $(obj).fadeOut(10000);
}
function tooltip() {
    $('[rel=tooltip]').tooltip();
    $("input:checkbox, input:radio ").uniform();//$("input:checkbox, input:radio, input:file").uniform();
}
function filesUpload(seq, fileCtrl) {
    $("#" + fileCtrl).click();
    $("#" + fileCtrl).change(function () {
        var fileName = $(this).val();
        $("#fileName" + seq).html(fileName);
    });
}
function filesUpload1(obj) {
    $(obj).next("div.uploader").find("input:file").click(); 
}
function filesUpload11(obj) {
    var ss = $(obj).val().split("\\");
    $(obj).parent("div.uploader").siblings('div.customfile').children('div.fileinput').find('span').html(ss[ss.length-1]);
 }
var PatternMatch = function (value, pattern) { // Private Method
    var regExp = new RegExp(pattern, "");
    return regExp.test(value);
}

function setMenu(id) {
    $('#' + id).addClass('active');
    $('#' + id + '>ul').addClass('in');
}

function InitializeNumTxtBox() {
    $('input[type="text"]').keydown(function (e) {
        return numberTxtbox(e);
    });
}

function numberTxtbox(e) {
    e = (e) ? e : window.event
    var charCode = (e.which) ? e.which : event.keyCode;
    if (charCode == 8 || charCode == 9 || (charCode >= 33 && charCode <= 40) || (charCode > 47 && charCode < 58) || (charCode > 91 && charCode < 105) || (charCode > 112 && charCode < 123)) {
        return true;
    }
    return false;
}

function deleteSubQuestion(Id) {
    if (confirm("Are you sure you want to delete this Question")) {
        PostForm('/admin/question/DeleteQuestion/' + Id, null, 'json', 20000, delSubQSuccess);
    }
}

function delSubQSuccess(response) {
    if (response == true) {
        alert('Question deleted successfully');
        window.location.reload();
    }
    else {
        alert(genErrMsg);
    }
}

function deleteAnswer(Id) {
    if (confirm("Are you sure you want to delete this answer/option")) {
        PostForm('/admin/question/DeleteAnswer/' + Id, null, 'json', 20000, delAnsSuccess);
    }
}

function delAnsSuccess(response) {
    if (response == true) {
        alert('Answer/option deleted successfully');
        window.location.reload();
    }
    else {
        alert(genErrMsg);
    }
}

function removefile(fid, ffield, fentityType) {
    if (confirm("Are you sure you want to delete this " + ffield)) {
        var fileModel = new Object();
        fileModel.EntityId = fid;
        fileModel.FileTypes = ffield;
        fileModel.EntityTypes = fentityType;
        PostForm('/admin/question/removefile/', JSON.stringify(fileModel), 'json', 20000, removefileSuccess);
    } else { return false; }
}
function removefileSuccess(response) {
    if (response) {
        feedMsg("#MsgBar1", "File removed successfully.");
        //addMsg("#MsgBar", "File removed successfully."); 
        $('div[data-file-wrapid="' + response.EntityId + '"][data-field-name="' + response.FieldName + '"]').remove();
        $('i[data-id="' + response.FieldName + '_' + response.EntityId + '"]').parent('span').parent('div').remove();
        return true;
    }
}
////School
function removeSchoolImg(fid, ffield, fentityType) {
    if (confirm("Are you sure you want to delete this " + ffield)) {
        var fileModel = new Object();
        fileModel.Id = fid;
        fileModel.FieldName = ffield;
        fileModel.EntityTypes = fentityType;
        PostForm('/admin/SchoolMgmt/removeSchoolImage/', JSON.stringify(fileModel), 'json', 20000, SchoolImageSuccess);
    }
}

function SchoolImageSuccess(response) {
    if (response) {
        alert('File removed successfully.');
        window.location.href = "/admin/schoolmgmt/school?id=" + response.Id;
    }
}
////Teacher
function Teacherremoveimg(fid, ffield, fentityType) {
    if (confirm("Are you sure you want to delete this Teacher Image")) {
        var fileModel = new Object();
        fileModel.Id = fid;
        fileModel.FieldName = ffield;
        fileModel.EntityTypes = fentityType;
        PostForm('/admin/TeacherMgmt/removeTeacherImage/', JSON.stringify(fileModel), 'json', 20000, SchoolTeacherSuccess);
    }
}

function SchoolTeacherSuccess(response) {
    if (response) {
        alert('File removed successfully.');
        window.location.href = "/admin/TeacherMgmt/CreateTeacher/" + response.Id;
    }
}
////Student

function Studentremoveimg(fid, ffield, fentityType) {
    if (confirm("Are you sure you want to delete this Student Image")) {
        var fileModel = new Object();
        fileModel.Id = fid;
        fileModel.FieldName = ffield;
        fileModel.EntityTypes = fentityType;
        PostForm('/admin/StudentMgmt/removeStudentImage/', JSON.stringify(fileModel), 'json', 20000, StudentSuccess);
    }
}

function StudentSuccess(response) {
    if (response) {
        alert('File removed successfully.');
        window.location.href = "/admin/StudentMgmt/CreateStudent?id=" + response.Id;
    }
}
function goBack() {
    history.go(-1);
    return false;
}

function cancelBack() {
    window.history.back();
}

function editQuestionBack() {
    var id = $('#ParentQId').val();
    if (id == 0) {
        id = $('#QuestionId').val();
    }
    window.location = '/admin/question/completequestion/' + id;
}

function UserBasicDetails(Id) {
    PostForm('/admin/usermgmt/BasicUserDetails/' + Id, null, 'json', 20000, renderUserDetails);
}

function renderUserDetails(data) {
    if (data) {
        var str = '<div style="margin:20px;"><ul>';
        str += '<li><b>' + data.UserType.Title + ' Name - </b>' + data.Name + '</li>';
        str += '<li><b>Username - </b>' + data.UserName + '</li>';
        str += '<li><b>Email - </b>' + data.Email + '</li>';
        str += '</ul></div>';
        $('#userDetails').html(str);
    }
}

function StudentDetails(Id) {
    PostForm('/admin/usermgmt/BasicStudentDetails/' + Id, null, 'json', 20000, renderStudentDetails);
}

function renderStudentDetails(data) {
    if (data) {
        var str = '<div style="margin:20px;"><ul>';
        str += '<li><b>Student Name - </b>' + data.Name + '</li>';
        str += '<li><b>Username - </b>' + data.UserName + '</li>';
        str += '<li><b>Email - </b>' + data.Email + '</li>';
        str += '<li><b>Grade - </b>' + data.Grade + '</li>';
        str += '<li><b>Class - </b>' + data.Class + '</li>';
        str += '</ul></div>';
        $('#userDetails').html(str);
    }
}

function UIBlock(time) {
    $.blockUI();
    setTimeout(function () { $.unblockUI(); }, time);
}

function UIunBlock() {
    $.unblockUI();
}
function successs(response) {
    $('#ansIndicate').show();
    UIunBlock();
    var uId = $('#UnitId').val();
    var hId = $('#HistoryId').val();
    nextPreQuestion(crntSequence + 1);
}

function printReport() {
    w = window.open();
    w.document.write($('#printArea').html());
    w.document.close();
    w.focus();
    w.print();
    w.close();
}
function removeTinymceInstant(id) {
    if (typeof tinyMCE != 'undefined') {
        tinyMCE.execCommand('mceFocus', false, "OptionText" + id);
        tinyMCE.execCommand('mceRemoveControl', false, "OptionText" + id);
        $('#OptionText' + id).remove();
        //var container = document.getElementById("OptionText" + id);
        //container.removeChild(document.getElementById("OptionText" + id));
        //i normally just do $("#main-text").remove(); but you specified not using jquery, so this should, in theory, remove it, if main-text-parent is replaced with the parent container of your main-text.
    }
}
//function processAjaxData(response, urlPath) {
function appendUrlQueryStr(urlPath) {
    //document.getElementById("content").innerHTML = response.html;
    //document.title = response.pageTitle;
    //window.history.pushState({ "html": response.html, "pageTitle": response.pageTitle }, "", urlPath);
    window.history.pushState({ "pageTitle": "" }, "", (window.location.pathname) + urlPath);
}

////DatePicker Remove Checkbox check


function disableInput(ids) {
    var ss = ids.split(',');

    $.each(ss, function (i, item) {
        $('#' + item).attr('disabled', 'disabled');
    });
}
function enableInput(ids) {
    var ss = ids.split(',');
    $.each(ss, function (i, item) {
        $('#' + item).removeAttr('disabled');
    });
}
//audio player scripts startregion
function playEvent(obj) {
    //var ss = document.getElementById("audio_1");
    //ss.pause();
    //json call
    //if(response){
    //}
    //else{
    //stop
    //}
    //debugger
    // var ss = $(obj).parent('div').siblings('audio') ;
    // ss[0].pause();

    //$(obj).parent('div').siblings('audio').remove();
    //$(obj).closest('div').attr("style", "display:none");
}
var playCount = 0;
//play event
function playEvent1(obj) {
    var ss = $(obj).attr("wrapper");
    if ($(ss).children('audio').attr("data-entityid")) {
        $(ss).children('div.play-pause').attr("style", "display:none");
        if ($('#hra').val() != "0" && playCount == 0) {
            obj.pause(); //pause the file 
            var data1 = new Object();
            data1.EntityId = $(ss).children('audio').attr("data-entityid");
            data1.QuestionId = $('#ParentQuestionId').val();
            data1.HistoryId = $('#HistoryId').val();
            data1.rvac = $('#hra').val();
            data1.type = "a";
            $.ajax({
                url: '/exam/canplayfile',
                type: 'POST',
                data: data1,
                error: function () {
                },
                dataType: 'json',
                success: function (data) {
                    if (data && playCount == 0) {
                        playCount = 1;
                        obj.play();
                    } else {
                    }
                    return false;
                }
            });
        }
    }
}
//track end event 
function trackEnded1(obj) {
    playCount = 0;
    var ss = $(obj).attr("wrapper");
    if ($(ss).children('audio').attr("data-entityid")) {
        $(ss).children('div.play-pause').attr("style", "display:block");

        //alert($(obj).attr('mp3'));
        $(ss).children('audio').load();
        //$(ss).children('audio').prop("currentTime", 0);
        $(ss).children('div.scrubber').children('div.progress').attr("style", "width:0px");
        $(ss).children('div.time').children('em.played').html("00:00");
    }
}
//audio player scripts endregion

