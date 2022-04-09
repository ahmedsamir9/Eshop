// example clients
// sandbox account :  sb-cpz4c14706041@personal.example.com, sb-acott15620370@personal.example.com
// sandbox password:  UjrpI5Q^   ,    R$S{8nr1
// sandbox phone :    2053564252, 

// sandbox api credentials Platform partner App
// account personal => business:     sb-bp5ro15341700@business.example.com
// password =>                       jJ!<<j3a
// client id:   AYBBHk4lFAG3hze3QnAZce2efcSXI_9KVtEFdxapeVYhwO6KPqyipFfUpXL8SilUJbamGmiHhwur5LqR
// secret:      EPLto_erwH-0w3d1vhvhaCw2JtPbiA8LgDqF4nml0vgfvA8VHqOeuToDpQ9XIij5Ooes4Hc1YNHJfvpK


// sandbox api credentials DefaultApp
// account business => merchant:     sb-ydust15137375@business.example.com
// client id:   AemG9j6hUoFVdc8LU2W21YZg_dFJK8DzTkjVszq25WxR_RIBpzUQNOOolAjs-FhOS9vCUwHYJu05qRNx
// secret:      EGK-dQH-gBmykFmIqcPD9ql_65dTHTuWaPtA2Enf2VTj12XQzAr-nlmY7e6dTRUAO0sUnRLZ3cm5-z63


// sandbox api credentials EshopApp
// account business => merchant:     sb-ydust15137375@business.example.com
// client id:   AVudMbLx1jCELYj1kvPvxw1M5Nh0aBgsAADCCqMftv-6IllCdvvmjqe1ukeqae85hvj2NceeNDgtENxp
// secret:      EN5RMuQQ3SH8gIsBfYZN7jFIXBRDYOQ2WIDgEBlQK2zDemqMV_RAwQtUGVvk33-fWCUJ9zEMzw-qrGLE


//alert("running paypal.js");

function initPayPalButton() {
    var orderTotal = parseFloat($("#ot").html());
    //orderTotal = 2.5;
    //alert("total pay = " + orderTotal);
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
                //alert("order Completed!");
                // Full available details
                console.log('Capture result', orderData, JSON.stringify(orderData, null, 2));

                // Show a success message within this page, e.g.
                //const element = document.getElementById('paypal-button-container');
                //element.innerHTML = '';
                //element.innerHTML = '<h3>Thank you for your payment!</h3>';
                window.location.href = "https://localhost:44327/cart/PaymentSuccessful";
                clearCart();
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


/*
 * 
 * paypal.Button.render({
            //Configure environment
            env: 'sandbox',
            client: {
                sandbox: 'AcKRtgL4i68EZ2ZYuYUx3ilihAg2YhL5dtF_m19A8MjBWVOn6ewNS4M_uV8gA3xi98zTcUq38gUHbVFx'
            },
            //Customize button
            locale: 'en_US',
            style: {
                size: 'small',
                color: 'gold',
                shape: 'pill'
            },
            commit: true,
            //Set up a payment
            payment: function (data, actions) {
                return actions.payment.create({
                    transactions: [{
                        amount: {
                            total: _total,
                            currency: 'USD'
                        }
                    }]
                });
            },
            //Execute the payment
            onAuthorize: function (data, actions) {
                return actions.payment.execute().then(function () {
                    var url = '@Url.Action("CompleteOrder", "Orders", new { })';
                    window.location.href = url;
                });
            }
        }, '#paypal-button-container')
 * */


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




