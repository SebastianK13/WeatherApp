var key = document.getElementById('changeKey');
var psw = document.getElementById('changePsw');
var permission = document.getElementById('changePermission');
var pswContent = document.getElementById('passwordContent');
var apiContent = document.getElementById('apiKeyContent');
var permissionContent = document.getElementById('permissionContent');
var apiKeyReadonly = document.getElementById('apiKeyReadonly');
var apiKeySubmit = document.getElementById('chaneApiKeySubmit');
var api_key;
var wasApiKeyChanged = false;
var error = document.getElementById('error');
var searchB = document.getElementById('searchBox');
var currentRole = document.getElementById('currentRole');
var foundUsers;
var results = document.getElementById('results');
var newRole = document.getElementById('newRole');
var aTags;
var changePermissionBtn = document.getElementById('changeUserPermission');
var pickedUser;
var permissionLog = document.getElementById('permissionLog');

window.addEventListener("DOMContentLoaded", function () {
    key.addEventListener("click", showChangeApiKey);
    psw.addEventListener("click", showChangePassword);
    permission.addEventListener("click", showChangePermission);
    apiKeySubmit.addEventListener("click", apiKeyUpdate);
    searchB.addEventListener("keyup", findUsers);
    aTags = results.getElementsByTagName('a');
    results.addEventListener("click", pickResult);
    window.addEventListener("click", closeResultList);
    changePermissionBtn.addEventListener("click", changePermission);
    setProperSubViewAsActive();
    FetchCurrentApiKey();
    checkLogs();
});

function changePermission() {
    if (searchB.value == pickedUser.username)
        postChangePermission();
    else
        permissionLog.innerText = "Find user field is incorrect";
}

function postChangePermission() {
    const params = {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(pickedUser)
    };
    var postMethod = "/Account/ChangePermission/";
    fetch(postMethod, params)
        .then(response => response.json())
        .then(function (data) {
            debugger;
            if (data.error)
                permissionLog.style.color = "red";
            else
                permissionLog.style.color = "green";
            permissionLog.innerText = data.message;
        }).catch(function () {
            console.log("data-error")
        });
}

function findUsers() {
    phrase = searchB.value;
    const params = {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(phrase)
    };
    var postMethod = "/Account/GetUserList/";
    fetch(postMethod, params)
        .then(response => response.json())
        .then(function(data){
            foundUsers = data;
            PrepareResultList();
        }).catch(function () {
            console.log("data-error")
        });
}

function PrepareResultList() {
    for (i = 0; i < aTags.length; i++) {
        if (i < foundUsers.length) {
            aTags[i].style.display = "block";
            aTags[i].innerText = foundUsers[i].username;
            aTags[i].style.borderBottom = "none";
            debugger;
            aTags[i].data = foundUsers[i].role+';'+foundUsers[i].toRole;
            if (i == foundUsers.length - 1) {
                aTags[i].style.borderBottom = "1px solid rgba(0,0,0, .8)";
            }
        }
        else {
            aTags[i].style.display = "none";
        }
    }
}

function pickResult (e) {

    if (e.target.tagName == "A") {
        var a = e.target.innerText;
        searchB.value = a;
        var roles = (e.target.data).split(';');
        currentRole.value = roles[0];
        newRole.value = roles[1];
        pickedUser = {
            "username": a,
            "role": roles[0],
            "toRole": roles[1]
        };
        for (i = 0; i < aTags.length; i++) {
            aTags[i].style.display = "none";
        }
        debugger;
    }
};

function closeResultList () {
    for (i = 0; i < aTags.length; i++) {
        aTags[i].style.display = "none";
    }
};

function apiKeyUpdate() {
    FetchCurrentApiKey();
}

function checkLogs() {
    if (errorMessage !== null) {
        error.innerHTML = errorMessage;
        error.style.color = "red";
    }
    else if (feedback !== null) {
        error.innerHTML = feedback;
        error.style.color = "green";
    }

}

function setProperSubViewAsActive() {
    switch (currentSubView) {
        case 1:
            showChangePassword();
            break;
        case 2:
            showChangeApiKey();
            break;
        case 3:
            showChangePermission()
            break;
        default:
            showChangePassword();
            break;
    }
}

function showChangeApiKey() {
    hideChangePermissionContent();
    hideChangePswContent();
    apiContent.style.display = "block";
    key.style.color = "white";
    currentSubView = 2;
    debugger;
}

function showChangePassword() {
    hideChangePermissionContent();
    hideApiKeyContent();
    pswContent.style.display = "block";
    psw.style.color = "white";
    currentSubView = 1;
}

function showChangePermission() {
    hideChangePswContent();
    hideApiKeyContent();
    permissionContent.style.display = "block";
    permission.style.color = "white";
    currentSubView = 3;
}

function hideApiKeyContent() {
    apiContent.style.display = "none";
    key.style.color = "rgba(255, 255, 255, 0.8)";
}

function hideChangePswContent(){
    pswContent.style.display = "none";
    psw.style.color = "rgba(255, 255, 255, 0.8)";
}

function hideChangePermissionContent() {
    permissionContent.style.display = "none";
    permission.style.color = "rgba(255, 255, 255, 0.8)";
}

function FetchCurrentApiKey() {
    const params = { method: "POST" };
    var postMethod = "/Account/GetApiKey/";
    fetch(postMethod, params)
        .then(response => response.json())
        .then(data => {
            apiKeyReadonly.value = data;
        }).catch(function () {
            console.log("data-error")
        });
}