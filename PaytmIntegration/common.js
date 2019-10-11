$(function () {
    console.log("ready!");
    $(".btnCheckout").on('click', function (event) {
        console.log("btn clicked");
        event.stopPropagation();
        event.stopImmediatePropagation();
        //(... rest of your JS code)
        var subscriptionType = $(this).data('subscription-type');
        var amount = 0;
        var mobile = $('#mobile').val(); //Get the values from the page you want to post
        var email = $('#email').val();

        switch (subscriptionType) {
            case 'basic':
                amount = 500;
                break;
            case 'premium':
                amount = 1000;
                break;
            case 'pro':
                amount = 1500;
                break;
            default:
                amount = 500;
        }


        var JSONObject = { // Create JSON object to pass through AJAX
            mobileNumber: mobile, //Make sure these names match the properties in VM
            email: email,
            amount: amount
        };

        $.ajax({ //Do an ajax post to the controller
            type: 'POST',
            url: '/Home/CreatePayment',
            data: JSON.stringify(JSONObject),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                console.log("ajax success");
                window.location.href = result.redirectUrl;
                //$('#lblsucesstxt').text(data.responseText);
            },
            error: function (xhr) {
            }
        });
    });



    //$('#buttonId').click(function () { //On click of your button

    //    //var property1 = $('#property1Id').val(); //Get the values from the page you want to post
    //    //var property2 = $('#property2Id').val();


    //    var JSONObject = { // Create JSON object to pass through AJAX
    //        mobileNumber: "7777777777", //Make sure these names match the properties in VM
    //        email: "test123@gmail.com",
    //        amount: 25
    //    };

    //    $.ajax({ //Do an ajax post to the controller
    //        type: 'POST',
    //        url: '/Home/CreatePayment',
    //        data: JSON.stringify(JSONObject),
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function (result) {
    //            alert("success");
    //            window.location.href = result.redirectUrl;
    //            //$('#lblsucesstxt').text(data.responseText);
    //        },
    //        error: function(xhr) {
    //        }
    //    });
    //});
});











