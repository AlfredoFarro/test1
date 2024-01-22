function validateLength_8(oSrc, args) {
    args.IsValid = (args.Value.length >= 8);
}
//function validateLength_10(oSrc, args) {
//    args.IsValid = (args.Value.length >= 8);
//}
function validarVacio(oSrc, args) {
    args.IsValid = (args.Value.length >= 2);
}
//function validateLength_7_9(oSrc, args) {
//    var len = args.Value.length;
//    if (len >= 6 && len <= 9)
//        args.IsValid = true;
//    else
//        args.IsValid = false;
//}

function validateLength_7_9(oSrc, args) {
    var len = args.Value.length;
    if (len >= 7 && len <= 9)
        args.IsValid = true;
    else
        args.IsValid = false;
}

function validateLength_7(oSrc, args) {
    args.IsValid = (args.Value.length >= 7);
}


function checkBeforeConfirm()//put this javascript function
{
    if (Page_ClientValidate() == true)//method to check validations
    {
        if (confirm('¿Guardar Registro?') == true)//confirm after validations check
        {
            return true;
        }
        else {
            return false;
        }
    }
    else {
        return false;
    }
}




function isFloatNumber(e, t) {
    var n;
    var r;
    if (navigator.appName == "Microsoft Internet Explorer" || navigator.appName == "Netscape") {
        n = t.keyCode;
        r = 1;
        if (navigator.appName == "Netscape") {
            n = t.charCode;
            r = 0
        }
    } else {
        n = t.charCode;
        r = 0
    }
    if (r == 1) {
        if (!(n >= 48 && n <= 57 || n == 44)) {
            t.returnValue = false
        }
    } else {
        if (!(n >= 48 && n <= 57 || n == 0 || n == 44)) {
            t.preventDefault()
        }
    }
}