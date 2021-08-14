var register = document.getElementById('register');
var login = document.getElementById('login');
var loginForm = document.getElementById('loginForm');
var registerForm = document.getElementById('registerForm');

window.addEventListener("DOMContentLoaded", function () {
    register.addEventListener("click", showRegisterForm);
    login.addEventListener("click", showLoginForm);
    checkActiveForm();
});

function showRegisterForm() {
    registerForm.style.display = "block";
    loginForm.style.display = "none";
}

function showLoginForm() {
    loginForm.style.display = "block";
    registerForm.style.display = "none";
}

function checkActiveForm() {
    if (activeForm == "RegisterForm")
        showRegisterForm();
    else
        showLoginForm();
}