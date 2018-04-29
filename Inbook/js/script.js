function isSearchValid(id) {
    var query = document.getElementById(id).value;
    if (query == "") {
        return false;
    }
}

function CheckLoginForm(emailAddress, password) {

    //CHECK EMAIL

    if (emailAddress.trim() == "") {
        return "* נא הכנס כתובת מייל";
    }

    if (emailAddress.indexOf("@") == -1 || emailAddress.split('@').length > 2 || emailAddress.charAt(0) == '@' || emailAddress.charAt(emailAddress.length - 1) == '@') {
        return "* כתובת אימייל לא תקינה";
    }

    if (emailAddress.indexOf(".") == -1 || emailAddress.split('.').length > 3 || emailAddress.charAt(0) == '.' || emailAddress.charAt(emailAddress.length - 1) == '.' || emailAddress.indexOf("@") - 1 == emailAddress.indexOf('.') || emailAddress.indexOf("@") + 1 == emailAddress.indexOf('.') || emailAddress.indexOf(" ") != -1) {
        return "* כתובת אימייל לא תקינה";
    }

    var bad = "!#$%^&*()_+-=";  
    for (var j = 0; j < bad.length; j++) {
        for (var s = 0; s < emailAddress.length; s++) {
            if (emailAddress.charAt(s) == bad.charAt(j)) {
                return "* כתובת אימייל לא תקינה";
            }
        }

    }

    var password = password.toLowerCase();

    if (password == "") {
        return "* נא הכנס סיסמא";
    }

    if (password.length < 6) {
        return "* סיסמא קצרה מדי";
    }

    return "";
}


function CheckForm(fullName, emailAddress, password) {  
    if (fullName.trim() == "" || password.trim() == "") {
        return "* נא להכניס את כל הפרטים הדרושים";
        }

    if (fullName.length < 4) {
        return "* שם או שם משפחה קצרים מדי";
        }

        for (var i = 0; i < firstName.length; i++) {
            if (!(firstName.charAt(i) >= 'א' && firstName.charAt(i) <= 'ת')) {
                return "* שם מלא לא תקין";
            }
        }

    // CHECK EMAIL

    if (emailAddress == "email address") {
        return "* נא להכניס את כל הפרטים הדרושים";
    }

    if (emailAddress.indexOf("@") == -1 || emailAddress.split('@').length > 2 || emailAddress.charAt(0) == '@' || emailAddress.charAt(emailAddress.length - 1) == '@') {
        return "* כתובת אימייל לא תקינה";
    }

    if (emailAddress.indexOf(".") == -1 || emailAddress.split('.').length > 3 || emailAddress.charAt(0) == '.' || emailAddress.charAt(emailAddress.length - 1) == '.' || emailAddress.indexOf("@") - 1 == emailAddress.indexOf('.') || emailAddress.indexOf("@") + 1 == emailAddress.indexOf('.') || emailAddress.indexOf(" ") != -1) {
        return "* כתובת אימייל לא תקינה";
    }


    for (var i = 0; i < firstName.length; i++) {
        if (!(firstName.charAt(i) >= 'a' && firstName.charAt(i) <= 'z' || emailAddress.charAt(i) == '@' || emailAddress.charAt(i) == '.')) {
            return "* כתובת אימייל לא תקינה";
        }
    }
    
    // CHECK PASSWORD
    if (password.length < 1)
    {
        return "* נא להכניס את כל הפרטים הדרושים";
    }
    if (password.length < 6) {
        return "* סיסמא קצרה מדי";
    }


    return "";
}

    