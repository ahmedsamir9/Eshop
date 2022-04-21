// example clients
// sandbox account :  sb-cpz4c14706041@personal.example.com, sb-acott15620370@personal.example.com
// sandbox password:  UjrpI5Q^   ,    R$S{8nr1
// sandbox phone :    2053564252, 

// sandbox api credentials Platform partner App
// account personal => business:     sb-bp5ro15341700@business.example.com
// password =>                       jJ!<<j3a
// client id:   AYBBHk4lFAG3hze3QnAZce2efcSXI_9KVtEFdxapeVYhwO6KPqyipFfUpXL8SilUJbamGmiHhwur5LqR
// secret:      EPLto_erwH-0w3d1vhvhaCw2JtPbiA8LgDqF4nml0vgfvA8VHqOeuToDpQ9XIij5Ooes4Hc1YNHJfvpK



//alert("running paypal.js");

function initPayPalButton() {
    //alert("inittializing paypal button");
    var orderTotal = parseFloat($("#ot").html());
    
    paypal.Buttons({
        style: {
            shape: 'rect',
            color: 'gold',
            layout: 'horizontal',
            label: 'buynow',
        },
        createOrder: function (data, actions) {
            return actions.order.create({
                intent: "CAPTURE",
                purchase_units: [{
                    "amount": {
                        "currency_code": "USD",
                        "value": orderTotal,
                    }
                }]
            });
        },
        onShippingChange: function (data, actions) {
            return actions.resolve();
        },
        onApprove: function (data, actions) {
            //alert("on aprove running....");
            return actions.order.capture().then(function (orderData) {
                //console.log('Capture result', orderData, JSON.stringify(orderData, null, 2));
                
                window.location.href = "https://localhost:44327/cart/PaymentSuccessful";
                toOrder();
            });
        },
        onCancel: function (data) {
            alert("payment cancelled");
        },
        onError: function (err) {
            window.location.href = "https://localhost:44327/cart/PaymentFailed";
            console.log(err);
        }
    }).render('#paypal-button-container');
}


$(document).ready(function () {
    initPayPalButton();
});


// another to call the api manually from our backend.
/*

    paypal.Buttons({
        // Sets up the transaction when a payment button is clicked
        createOrder: function (data, actions) {
            return fetch("/paypal/createorder", {
                method: "get",
              // use the "body" param to optionally pass additional order information
              // like product ids or amount
            })
            .then((response) => {
                //alert("response = " + response);
                return response.json();
            })
            .then((order) => {
                //alert("order = " + order);
                return order.id;
            });
        },

    // Finalize the transaction after payer approval
        // /paypal/captureorder/{orderId:int}
        onApprove: function (data, actions) {
            return fetch(`/paypal/captureorder/${data.orderID}/capture`, {
                method: "get",
            })
            .then((response) => response.json())
            .then((orderData) => {
                // Successful capture! For dev/demo purposes:
                console.log(
                    "Capture result",
                    orderData,
                    JSON.stringify(orderData, null, 2)
                );
                var transaction = orderData.purchase_units[0].payments.captures[0];
                alert(
                    "Transaction " +
                    transaction.status +
                    ": " +
                    transaction.id +
                    "\n\nSee console for all available details"
                );
                // When ready to go live, remove the alert and show a success message within this page. For example:
                // var element = document.getElementById('paypal-button-container');
                // element.innerHTML = '<h3>Thank you for your payment!</h3>';
                // Or go to another URL:  actions.redirect('thank_you.html');
            });
        },
}).render("#paypal-button-container");

*/




