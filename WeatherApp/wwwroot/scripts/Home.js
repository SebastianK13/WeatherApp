var map = document.getElementById("mapContainer");
var clickedElement;
var lastClicked;
var model;
var speed = document.getElementById("speed");
var deg = document.getElementById("deg");
var clouds = document.getElementById("clouds");
var humidity = document.getElementById("humidity");
var city = document.getElementById("city");
var voivodeship = document.getElementById("voivodeship");
var date = document.getElementById("date");
var temp = document.getElementById("temp");
var feels = document.getElementById("feels");
var minTemp = document.getElementById("minTemp");
var maxTemp = document.getElementById("maxTemp");
var pressure = document.getElementById("pressure");
var weatherContainer = document.getElementById("weatherReadings");
var nextCityBtn = document.getElementById("nextCity");
var activeCity;

map.addEventListener("click", function (e) {
    var t = e.target.href;
    if (t != null) {
        var id = t.split('#');
        if (id.length == 2) {
            clickedElement = document.getElementById(id[1]);
            if (clickedElement == lastClicked) {
                clickedElement.style.display = "none";
                lastClicked = null;
                weatherContainer.style.display = "none";
                resetButton();
            }
            else {
                clickedElement.style.display = "block";
                if (lastClicked != null) {
                    lastClicked.style.display = "none";
                }

                resetButton();
                weatherContainer.style.display = "block";
                lastClicked = clickedElement;
                GetWeather(id[1]);
            }
        }
    }
});

function GetWeather(voivodeship) {
    const params = {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(voivodeship)
    };
    var getMethod = "/Home/GetWeather/";
    fetch(getMethod, params)
        .then(response => response.json())
        .then(function (data) {
            model = data;
            debugger;
            if (model.length == 2) {
                nextCityBtn.style.display = "block";
                nextCityBtn.innerHTML = model[1].voivodeship.cityName;
                activeCity = 1;
            }

            completeDataForCity(0);
        }).catch(function () {
            console.log("data-error")
        });
}

function completeDataForCity(i) {

        speed.innerHTML = "Wind speed: "+model[i].wind.speed+"km/h";
        deg.innerHTML = "Wind direction: "+model[i].wind.deg+" degree";
        clouds.innerHTML = "Clouds: "+model[i].weathers[0].description;
        humidity.innerHTML = "Humidity: "+model[i].main.humidity+"%";
        city.innerHTML = "City: "+model[i].voivodeship.cityName;
        voivodeship.innerHTML = "Voivodeship: "+model[i].voivodeship.voivodeshipName;
        date.innerHTML = "Update date: "+(model[i].date).replace("T"," ");
        temp.innerHTML = "Temperature: " + model[i].main.temp + "°C";
        feels.innerHTML = "Perceptible Temperature: " + model[i].main.feels_like + "°C";
        minTemp.innerHTML = "Min. Temperature: " + model[i].main.temp_min + "°C";
        maxTemp.innerHTML = "Max. Temperature: " + model[i].main.temp_max +"°C";
        pressure.innerHTML = "Pressure: "+model[i].main.pressure+" hpa";
}

nextCityBtn.addEventListener("click", function () {
    debugger;
    if (activeCity == 1) {
        completeDataForCity(1);
        activeCity = 2;
        nextCityBtn.innerHTML = model[0].voivodeship.cityName;
    }
    else if (activeCity == 2) {
        completeDataForCity(0);
        activeCity = 1;
        nextCityBtn.innerHTML = model[1].voivodeship.cityName;
    }
});

function resetButton() {
    activeCity = 0;
    nextCityBtn.innerHTML = "";
    nextCityBtn.style.display = "none";
}