function FileSelected(file, elemInputId, elemImgId) {
    //var reqPath = file.fullPath
    //reqPath = reqPath.replace('wwwroot/', '');
    //console.log('new roxy: ', reqPath, " elem: ", elemInputId + " | ", elemImgId)

    $(window.parent.document).find('#' + elemInputId).attr('value', file.fullPath);
    $(window.parent.document).find('#' + elemImgId).attr('src', file.fullPath);
    console.log('roxy-cb: ', file.fullPath, " elem: ", elemInputId + " | ", elemImgId)

    //window.parent.postMessage({
    //    mceAction: 'FileSelected',
    //    content: file.fullPath
    //}, '*');
}
function GetSelectedValue(){
  /**
  * This function is called to retrieve selected value when custom integration is used.
  * Url parameter selected will override this value.
  */  
  return "";
}
