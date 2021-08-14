var pswContent = document.getElementById('passwordContent');
var psw = document.getElementById('changePsw');

window.addEventListener("DOMContentLoaded", function () {
    showChangePassword();
    checkLogs();
});

function showChangePassword() {
    pswContent.style.display = "block";
    psw.style.color = "white";
}

function checkLogs() {
    debugger;
    if (errorMessage !== null) {
        error.innerHTML = errorMessage;
        error.style.color = "red";
    }
    else if (feedback !== null) {
        error.innerHTML = feedback;
        error.style.color = "green";
    }

}