﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
if (window.location.href.startsWith("http://localhost:5228/Users/Dashboard") || window.location.href.startsWith("http://localhost:5228/Historiques") || window.location.href.startsWith("http://localhost:5228/BlackLists") || window.location.href.startsWith("http://localhost:5228/FavoriteLists")) {
    document.getElementById('test').style.display = 'none';
    document.getElementById('test1').style.display = 'none';
}
window.onload = function() {
    if (window.location.href.startsWith("http://localhost:5228/Users?username=") ||
        window.location.href.startsWith("http://localhost:5228/Users/") ||
        window.location.href.startsWith("http://localhost:5228/Biens/Edit/") ||
        window.location.href === "http://localhost:5228/Biens/Create" ||
        window.location.href === "http://localhost:5228/OffreSpeciales/Create") {
        document.getElementById('becomeSellerBtn').style.display = 'none';
    }
};
if (window.location.href === "http://localhost:5228/Users/Dashboard") {
    document.getElementById('test').style.display = 'none';
    document.getElementById('test1').style.display = 'none';
}
$(document).ready(function () {
    $('.icon-click').on('click', function () {
        $(this).toggleClass('clicked');
    });
});

$("#vehicules").on("click", function () {
    navigateToCategoryView('vehicles');
});

$("#electronics").on("click", function () {
    navigateToCategoryView('household-appliances');
});

$("#clothes").on("click", function () {
    navigateToCategoryView('clothes');
});

//les promotions:

const container = document.getElementById('scrolling-container');
const productWidth = 220; // Width of each product card including margin

function scrollProducts(direction) {
    const scrollAmount = direction === 'right' ? -productWidth : productWidth;
    container.style.transform = `translateX(${scrollAmount}px)`;
}
//list of categories:
// Add an event listener to the document to handle clicks on category links


// Function to navigate to the corresponding view based on the selected category
function navigateToCategoryView(category) {
    switch (category) {
        case 'vehicles':
            window.location.href = '/Biens/ProductsByCategory?categoryId=1'; // Replace with the actual URL
            break;
        case 'clothes':
            window.location.href = '/Biens/ProductsByCategory?categoryId=3'; // Replace with the actual URL
            break;
        case 'household-appliances':
            window.location.href = '/Biens/ProductsByCategory?categoryId=2'; // Replace with the actual URL
            break;
        default:
            // Handle other categories if needed
            break;
    }
}

//-----------------------------------------------CLOTHES,VEHICULES,ECLECTRONICS----------------------------

// OPEN & CLOSE CART
const cartIcon = document.querySelector("#cart-icon");
const cart = document.querySelector(".cart");
const closeCart = document.querySelector("#cart-close");

cartIcon.addEventListener("click", () => {
    cart.classList.add("active");
});

closeCart.addEventListener("click", () => {
    cart.classList.remove("active");
});

// Start when the document is ready
if (document.readyState == "loading") {
    document.addEventListener("DOMContentLoaded", start);
} else {
    start();
}

// =============== START ====================
function start() {
    addEvents();
}

// ============= UPDATE & RERENDER ===========
function update() {
    addEvents();
    updateTotal();
}

// =============== ADD EVENTS ===============
function addEvents() {
    // Remove items from cart
    let cartRemove_btns = document.querySelectorAll(".cart-remove");
    console.log(cartRemove_btns);
    cartRemove_btns.forEach((btn) => {
        btn.addEventListener("click", handle_removeCartItem);
    });

    // Change item quantity
    let cartQuantity_inputs = document.querySelectorAll(".cart-quantity");
    cartQuantity_inputs.forEach((input) => {
        input.addEventListener("change", handle_changeItemQuantity);
    });

    // Add item to cart
    let addCart_btns = document.querySelectorAll(".add-cart");
    addCart_btns.forEach((btn) => {
        btn.addEventListener("click", handle_addCartItem);
    });

    // Buy Order
    const buy_btn = document.querySelector(".btn-buy");
    buy_btn.addEventListener("click", handle_buyOrder);
}

// ============= HANDLE EVENTS FUNCTIONS =============
let itemsAdded = [];

function handle_addCartItem() {
    let product = this.parentElement;
    let title = product.querySelector(".product-title").innerHTML;
    let price = product.querySelector(".product-price").innerHTML;
    let imgSrc = product.querySelector(".product-img").src;
    console.log(title, price, imgSrc);

    let newToAdd = {
        title,
        price,
        imgSrc,
    };

    // handle item is already exist
    if (itemsAdded.find((el) => el.title == newToAdd.title)) {
        alert("This Item Is Already Exist!");
        return;
    } else {
        itemsAdded.push(newToAdd);
    }

    // Add product to cart
    let cartBoxElement = CartBoxComponent(title, price, imgSrc);
    let newNode = document.createElement("div");
    newNode.innerHTML = cartBoxElement;
    const cartContent = cart.querySelector(".cart-content");
    cartContent.appendChild(newNode);

    update();
}

function handle_removeCartItem() {
    this.parentElement.remove();
    itemsAdded = itemsAdded.filter(
        (el) =>
            el.title !=
            this.parentElement.querySelector(".cart-product-title").innerHTML
    );

    update();
}

function handle_changeItemQuantity() {
    if (isNaN(this.value) || this.value < 1) {
        this.value = 1;
    }
    this.value = Math.floor(this.value); // to keep it integer

    update();
}
/*
function handle_buyOrder() {
    if (itemsAdded.length <= 0) {
        alert("There is No Order to Place Yet! \nPlease Make an Order first.");
        return;
    }
    $(document).ready(function () {
        // Open modal when the button is clicked
        $("#openModalBtn").click(function () {
            $("#blurredBackground").fadeIn();
            $("#myModal").fadeIn();
        });

        // Close modal when the close button is clicked
        $("#closeModalBtn").click(function () {
            $("#blurredBackground").fadeOut();
            $("#myModal").fadeOut();
        });
    });
}*/
/*
function handle_buyOrder() {
    if (itemsAdded.length <= 0) {
        alert("There is No Order to Place Yet! \nPlease Make an Order first.");
        return;
    }
    $.get("/Biens/IsUserLoggedIn", function (data) {
        if (data === true) {
            window.location.href = "/VotreContrôleur/PageDePaiement";
        } else {
            window.location.href = "/Users/Login";
        }
    });
}
*/
// =========== UPDATE & RERENDER FUNCTIONS =========
function updateTotal() {
    let cartBoxes = document.querySelectorAll(".cart-box");
    const totalElement = cart.querySelector(".total-price");
    let total = 0;
    cartBoxes.forEach((cartBox) => {
        let priceElement = cartBox.querySelector(".cart-price");
        let price = parseFloat(priceElement.innerHTML.replace("$", ""));
        let quantity = cartBox.querySelector(".cart-quantity").value;
        total += price * quantity;
    });

    // keep 2 digits after the decimal point
    total = total.toFixed(2);
    // or you can use also
    // total = Math.round(total * 100) / 100;

    totalElement.innerHTML = "$" + total;
}

// ============= HTML COMPONENTS =============
function CartBoxComponent(title, price, imgSrc) {
    return `
    <div class="cart-box">
        <img src=${imgSrc} alt="" class="cart-img">
        <div class="detail-box">
            <div class="cart-product-title">${title}</div>
            <div class="cart-price">${price}</div>
            <input type="number" value="1" class="cart-quantity">
        </div>
        <!-- REMOVE CART  -->
        <i class='bx bxs-trash-alt cart-remove'></i>
    </div>`;
}

///////////////////////////////////////////////////////////////////////////////////////////////////
// LES INFOS D CARTE BANCAIRE
$(document).ready(function () {
    // Open modal when the button is clicked
    $("#openModalBtn").click(function () {
        $("#blurredBackground").fadeIn();
        $("#myModal").fadeIn();
    });

    // Close modal when the close button is clicked
    $("#closeModalBtn").click(function () {
        $("#blurredBackground").fadeOut();
        $("#myModal").fadeOut();
    });
});


function updateTotal() {
    let cartBoxes = document.querySelectorAll(".cart-box");
    const totalElement = cart.querySelector(".total-price");
    let total = 0;
    cartBoxes.forEach((cartBox) => {
        let priceElement = cartBox.querySelector(".cart-price");
        let price = parseFloat(priceElement.innerHTML.replace("$", ""));
        let quantity = cartBox.querySelector(".cart-quantity").value;
        total += price * quantity;
    });

    // keep 2 digits after the decimal point
    total = total.toFixed(2);
    // or you can use also
    // total = Math.round(total * 100) / 100;

    totalElement.innerHTML = "$" + total;
}