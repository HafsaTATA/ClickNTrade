// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('.icon-click').on('click', function () {
        $(this).toggleClass('clicked');
    });
});

$("#vehicules").on("click", function () {
    window.location.href = "/vehicule";
});

$("#electronics").on("click", function () {
    window.location.href = "/electronics";
});

$("#clothes").on("click", function () {
    window.location.href = "/clothes";
});


