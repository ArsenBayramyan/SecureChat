var validObj = {
    FirstName: false,
    LastName: false,
    City: false,
    Email: false,
    Password: false,
    ConfirmPassword: false,
}
function checkField(filedName, filedEmpty, filedIncorrect) {
    let inputType = filedName;
    let emptyClassList = document.getElementById(filedEmpty).classList;
    let incorrectClassList = document.getElementById(filedIncorrect).classList;
    filedElement = document.getElementById(filedName);
    filedName = filedElement.value;
    if (filedName.length > 0) {
        checkCorection(filedName, emptyClassList, incorrectClassList, inputType);
    }
    else {
        emptyClassList.remove('d-none');
        incorrectClassList.add('d-none');
        filedElement.classList.add('is-invalid');
        validObj[inputType] = false;
    }
    checkAllFilds();
}

function checkCorection(filedName, emptyClassList, incorrectClassList, inputType) {
    let reg = new RegExp('^([^0-9]*)$');
    var test = reg.test(filedName);
    console.log(test);
    if (filedName.length < 3 || filedName.length >15 || !test) {
        incorrectClassList.remove('d-none');
        emptyClassList.add('d-none');
        filedElement.classList.add('is-invalid');
        validObj[inputType] = false;
    } else {
        emptyClassList.add('d-none');
        incorrectClassList.add('d-none');
        filedElement.classList.remove('is-invalid');
        validObj[inputType] = true;
    }
}

function validateEmail() {
    let emptyClassList = document.getElementById('emailEmpty').classList;
    let incorrectClassList = document.getElementById('emailIncorrect').classList;
    filedElement = document.getElementById('Email');
    mail = filedElement.value;
    let emailReg = new RegExp(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/);
    var test = emailReg.test(mail);
    if (mail.length > 0) {
        if (test) {
            emptyClassList.add('d-none');
            incorrectClassList.add('d-none');
            filedElement.classList.remove('is-invalid');
            validObj['Email'] = true;
        } else {
            incorrectClassList.remove('d-none');
            emptyClassList.add('d-none');
            filedElement.classList.add('is-invalid');
            validObj['Email'] = false;
        }
    }
    else {
        emptyClassList.remove('d-none');
        incorrectClassList.add('d-none');
        filedElement.classList.add('is-invalid');
        validObj['Email'] = false;
    }
    checkAllFilds();
}

function checkAllFilds() {
    //let arr = [];
    //for (obj in validObj) {
    //    arr.push(validObj[obj])
    //}
    //if (arr.indexOf(false) == -1) {
    //    document.getElementById("signUpBtn").disabled = false;
    //} else {
    //    document.getElementById("signUpBtn").disabled = true;
    //}
}

function validatePassword() {
    let emptyClassList = document.getElementById('passwordEmpty').classList;
    let incorrectClassList = document.getElementById('passwordIncorrect').classList;
    filedElement = document.getElementById('Password');
    password = filedElement.value;
    passwordReg = RegExp('^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*.;,/])(?=.{6,})');
    test = passwordReg.test(password);
    if (password.length > 0) {
        if (test) {
            emptyClassList.add('d-none');
            incorrectClassList.add('d-none');
            filedElement.classList.remove('is-invalid');
            validObj['Password'] = true;
        } else {
            incorrectClassList.remove('d-none');
            emptyClassList.add('d-none');
            filedElement.classList.add('is-invalid');
            validObj['Password'] = false;
            
        }
    } else {
        emptyClassList.remove('d-none');
        incorrectClassList.add('d-none');
        filedElement.classList.add('is-invalid');
        validObj['Password'] = false;
    }
    checkAllFilds();
}
function validateConfirmPassword() {
    let emptyClassList = document.getElementById('confirmPasswordEmpty').classList;
    let incorrectClassList = document.getElementById('confirmPasswordIncorrect').classList;
    filedElement = document.getElementById('ConfirmPassword');
    confirmPassword = filedElement.value;
    password = document.getElementById('Password').value;
    if (confirmPassword.length > 0) {
        if (confirmPassword == password) {
            emptyClassList.add('d-none');
            incorrectClassList.add('d-none');
            filedElement.classList.remove('is-invalid');
            validObj['ConfirmPassword'] = true;
        } else{
            incorrectClassList.remove('d-none');
            emptyClassList.add('d-none');
            filedElement.classList.add('is-invalid');
            validObj['ConfirmPassword'] = false;
        }
    } else {
        emptyClassList.remove('d-none');
        incorrectClassList.add('d-none');
        filedElement.classList.add('is-invalid');
        validObj['ConfirmPassword'] = false;
    }
    checkAllFilds();
}
