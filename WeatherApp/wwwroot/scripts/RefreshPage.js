var errorSection = document.getElementById("error");

window.addEventListener("DOMContentLoaded", function () {
    checkMessages();
});

function checkMessages() {
    if (error != null) {
        errorSection.style.color = "red";
        errorSection.innerHTML = error;
    }
    else if (feedback != null) {
        errorSection.innerHTML = feedback;
        errorSection.style.color = "green";
    }
}
