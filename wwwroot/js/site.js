// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var seconds = 00;
var tens = 00;
var min = 00;
var OutputMin = document.getElementById('min');
var OutputSeconds = document.getElementById('second');
var OutputTens = document.getElementById('tens');
var buttonReset = document.getElementById('ms-reset');
var interval;


window.onload = function () {
    clearInterval(interval);
    interval = setInterval(startTime, 10);
}

//Needs stop function of clearInterval(interval); tied to win/lose condition

buttonReset.addEventListener('click', () => {
    clearInterval(interval);
    tens = "00";
    seconds = "00";
    OutputSeconds.innerHTML = seconds;
    OutputTens.innerHTML = tens;
    console.log("reset button clicked");
    interval = setInterval(startTime, 10);
})

function startTime() {
    tens++;
    if (tens <= 9) {
        OutputTens.innerHTML = "0" + tens;
    } else {
        OutputTens.innerHTML = tens;
    }

    if (tens > 99) {
        seconds++;
        OutputSeconds.innerHTML = "0" + seconds;
        tens = 0;
        OutputTens.innerHTML = "0" + tens;
    }

    if (seconds > 9) {
        OutputSeconds.innerHTML = seconds;
    }

    if (seconds > 59) {
        min++;
        OutputMin.innerHTML = "0" + min;
        seconds = 0;
        OutputSeconds.innerHTML = "0" + seconds;
        tens = 0;
        OutputTens.innerHTML = "0" + tens;
    }
}