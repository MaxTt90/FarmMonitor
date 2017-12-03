
/*
表单验证常用JavaScript方法
*/

//验证字符串是否为空
function ValidateEmpty(input) {
    if ($.trim(input) == "") {
        return true;
    }
    return false;
}