/**
* Basic sample plugin inserting abbreviation elements into CKEditor editing area.
*/
// Written by minh.duong(23/03/2020)
// Plugin upload image using Jquery,Ajax,Html5
// Register the plugin with the editor.
// http://docs.cksource.com/ckeditor_api/symbols/CKEDITOR.plugins.html

CKEDITOR.plugins.add('uploadimage',
    {
        init: function (editor) {
            console.log(this.path);
            editor.addCommand('abbrDialog', new CKEDITOR.dialogCommand('abbrDialog'));
            editor.ui.addButton('Abbr',
                {
                    label: 'Chèn ảnh',
                    command: 'abbrDialog',
                    icon: this.path + 'images/icon.png'
                });


            CKEDITOR.dialog.add('abbrDialog', function (editor) {
                return {
                    title: 'Upload ảnh',
                    minWidth: 400,
                    minHeight: 200,
                    contents:
                        [
                            {
                                id: 'tab1',
                                label: 'Basic Settings',
                                elements:
                                    [
                                        {
                                            type: 'html',
                                            html: "<div><form id='form1' enctype='multipart/form-data' method='post' action='/CMS/Handler/UploadFile'><div class='row'><label for='file'>Select a File to Upload</label><br /><input type='file' name='file' id='file' onchange='fileSelected();'/></div><div id='fileName'></div><div id='fileSize'></div><div id='fileType'></div><div class='row'></div><div id='progressNumber'></div></form></div>"
                                        }
                                    ]
                            }
                            //{
                            //    id: 'tab2',
                            //    label: 'Basic Settings',
                            //    elements:
                            //        [
                            //            {
                            //                type: 'html',
                            //                html: "<div><form id='form1' enctype='multipart/form-data' method='post' action='/Handler/UploadFile'><div class='row'><label for='fileToUpload'>Select a File to Upload</label><br /><input type='file' name='fileToUpload' id='fileToUpload' onchange='fileSelected();'/></div><div id='fileName'></div><div id='fileSize'></div><div id='fileType'></div><div class='row'></div><div id='progressNumber'></div></form></div>"
                            //            }
                            //        ]
                            //}
                        ],
                    onOk: function () {
                        var retVal = JSON.parse(uploadFile());
                        editor.insertHtml("<img src='" + retVal.fileName + "' width='" + retVal.width + "' height='" + retVal.height + "' />");
                    }
                };
            });
        }
    });


/**************************************Upload file **********************************/
function fileSelected() {
    var file = document.getElementById('file').files[0];
    if (file) {
        var fileSize = 0;
        if (file.size > 1024 * 1024)
            fileSize = (Math.round(file.size * 100 / (1024 * 1024)) / 100).toString() + 'MB';
        else
            fileSize = (Math.round(file.size * 100 / 1024) / 100).toString() + 'KB';

        document.getElementById('fileName').innerHTML = 'Name: ' + file.name;
        document.getElementById('fileSize').innerHTML = 'Size: ' + fileSize;
        document.getElementById('fileType').innerHTML = 'Type: ' + file.type;
    }
}

function uploadFile() {
    var fd = new FormData();
    fd.append("file", document.getElementById('file').files[0]);
    var xhr = new XMLHttpRequest();
    xhr.upload.addEventListener("progress", uploadProgress, false);
    xhr.addEventListener("load", uploadComplete, false);
    xhr.addEventListener("error", uploadFailed, false);
    xhr.addEventListener("abort", uploadCanceled, false);
    xhr.open("POST", "/CMS/Handler/UploadFile/", false);
    xhr.send(fd);
    console.log(xhr);
    return (xhr.response);
}

function uploadProgress(evt) {
    if (evt.lengthComputable) {
        var percentComplete = Math.round(evt.loaded * 100 / evt.total);
        document.getElementById('progressNumber').innerHTML = percentComplete.toString() + '%';
    }
    else {
        document.getElementById('progressNumber').innerHTML = 'unable to compute';
    }
}

function uploadComplete(evt) {
    return evt.target.responseText;
}

function uploadFailed(evt) {
    alert("There was an error attempting to upload the file.");
}

function uploadCanceled(evt) {
    alert("The upload has been canceled by the user or the browser dropped the connection.");
}