// example clients
// sandbox account :  sb-cpz4c14706041@personal.example.com, sb-acott15620370@personal.example.com
// sandbox password:  UjrpI5Q^   ,    R$S{8nr1
// sandbox phone :    2053564252



// sandbox api credentials
// account:     sb-o47xbv14705967@personal.example.com
// client id:   AWUV0G5WP3ZrEkUpxIiU0ZDKXq8THQTd6bA0_JKJXCDE0CVfIlrsvDnTtiK54QFPhQFuX83RvXTXCjVo
// secret:      EKOnQr0tA3hciL5Dxk74RU_2kBnD1lRt7f1dGsOUWP8y62ZDel5xhaFyUrEfwZVScKGj8FTVn7HgBCyi


alert("running paypal.js");


/*
paypal_sdk.Buttons({
    style: {
        color: 'gold',
        shape: 'pill',
        label: 'paypal',
        size: 'responsive',
        branding: true
    },
    createOrder: function (data, actions) {
        // Set up the transaction
        return actions.order.create({
            purchase_units: [{
                amount: {
                    value: '150'
                }
            }]
        });
    },
    onApprove: function (data, actions) {
        //alert("on approve....");

        // This function captures the funds from the transaction.
        return actions.order.capture().then(function (details) {
            alert("inside .then");
            // This function shows a transaction success message to your buyer.
            alert('Transaction completed by ' + details.payer.name.given_name);
        });
    },
    
    onApprove: function (data, actions) {
        alert("on approve");
        // Capture the funds from the transaction
        return actions.order.capture().then(function (details) {
            alert("payment captured!...");
            alert("payment done...  \n orderID: " + data.orderID);
            // capture funds login
            
          return fetch('/pay-with-pp', {
            method: 'post',
            headers: {
              'content-type': 'application/json'
            },
            body: JSON.stringify({
              orderID: data.orderID,
              product_id : product_id,
              _token : token
            })
          }).then(function(res){
            alert('Payment has been made! Please see the delivery status on orders page!');
            window.location.href = redirect_url
          });
            
        });
    },
    
}).render('#paypal-btn');

*/




function initPayPalButton() {
    //await setTimeout(() => {}, 2000);
    //var orderTotal = 555;
    var orderTotal = parseFloat($("#ot").html());
    alert("total pay = " + orderTotal);
    paypal.Buttons({
        style: {
            shape: 'rect',
            color: 'gold',
            layout: 'horizontal',
            label: 'buynow',

        },

        createOrder: function (data, actions) {
            return actions.order.create({
                purchase_units: [{
                    "amount": {
                        "currency_code": "USD",
                        "value": orderTotal,
                    }
                }]
            });
        },

        onApprove: function (data, actions) {
            return actions.order.capture().then(function (orderData) {

                // Full available details
                console.log('Capture result', orderData, JSON.stringify(orderData, null, 2));

                // Show a success message within this page, e.g.
                const element = document.getElementById('paypal-button-container');
                element.innerHTML = '';
                element.innerHTML = '<h3>Thank you for your payment!</h3>';

                // Or go to another URL:  actions.redirect('thank_you.html');

            });
        },

        onError: function (err) {
            console.log(err);
        }
    }).render('#paypal-button-container');
}


$(document).ready(function () {
    initPayPalButton();
});


/*

    paypal.Buttons({
        style: {
            layout: 'vertical',
            color: 'gold',
            shape: 'pill',
            label: 'paypal'
        },
        createOrder: function (data, actions) {
            // Set up the transaction
            return actions.order.create({
                purchase_units: [{
                    amount: {
                        value: '0.01'
                    }
                }]
            });
        },

    }).render('#paypal-btn');

*/


 