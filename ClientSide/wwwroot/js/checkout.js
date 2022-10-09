redirectToCheckout = function (sessionId) {
    var stripe = Stripe("pk_test_51LY8JDLlAlcuXPXEvVRmJmV1uqfqHTUg8fKOj9FQ3outqE2QtnxZLIHuMJgRLuWFIbwhj1fC2NdBjVzPF6fI9UNS00dsJ2RJGQ");
    stripe.redirectToCheckout({ sessionId: sessionId });
}