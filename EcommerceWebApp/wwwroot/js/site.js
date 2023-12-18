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

//les promotions:

const container = document.getElementById('scrolling-container');
const productWidth = 220; // Width of each product card including margin

function scrollProducts(direction) {
    const scrollAmount = direction === 'right' ? -productWidth : productWidth;
    container.style.transform = `translateX(${scrollAmount}px)`;
}


