alert("running cart.js");


getAllItems();

//addItem(2);

function addItem(productID) {
    $.ajax({
        url: `https://localhost:44327/cart/add/${productID}`,
        method: 'POST',
        contentType: 'application/json',
        success: function (data) {
            getAllItems();
        },
        error: function (error) {
            alert("error: " + error.statusText);
        }
    });
}

function clearCart() {
    $.ajax({
        url: 'https://localhost:44327/cart/clear',
        method: 'GET',
        success: function (data) {
            getAllItems();
        },
        error: function (error) {
            alert("error: " + error.statusText);
        }
    });
}

function getAllItems() {

    //$.ajax({
    //    url: "https://localhost:44327/cart/getAllItems",
    //    type: "GET",
    //    //data: { year: ((val * 1) + 1) }
    //})
    //.done(function(partialViewResult) {
    //    $("#cartPV").html(partialViewResult);
    //})
    //.fail(()=>{
    //    alert("error loading partial view");
    //});

    $.ajax({
        url: `https://localhost:44327/cart/getAllItems`,
        method: 'GET',
        success: function (data) {
            $("#cartPV").html(data);
            var orderTotal = "$ " + $("#ot").html();
            alert("total = " + orderTotal);
            $("#orderTotal").html(orderTotal);
        },
        error: function (error) {
            alert("error loading partial view");
        }
    });
}

function removeItem(productID) {
    $.ajax({
        url: `https://localhost:44327/cart/remove/${productID}`,
        method: 'GET',
        success: function (data) {
            getAllItems();
        },
        error: function (error) {
            alert("error: " + error.statusText);
        }
    });
}

function increase(productId) {
    //alert(productId);
    var product = { productId }

    $.ajax({
        url: 'https://localhost:44327/cart/increase',
        method: 'POST',
        data: JSON.stringify(product),
        contentType: 'application/json',
        success: function (data) {
            getAllItems();
        },
        error: function (error) {
            alert("error: " + error.statusText);
        }
    });
}


function decrease(productId) {

    var product = { productId }
    $.ajax({
        url: 'https://localhost:44327/cart/decrease',
        method: 'POST',
        data: JSON.stringify(product),
        contentType: 'application/json',
        success: function (data) {
            getAllItems();
        },
        error: function (error) {
            alert("error: " + error.statusText);
        }
    });
}