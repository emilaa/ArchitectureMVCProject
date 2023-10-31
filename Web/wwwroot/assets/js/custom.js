jQuery(document).ready(function ($) {
    $(document).on("click", '.category', function () {
        var id = $(this).data('id')
        $.ajax({
            method: "GET",
            url: "/faq/filterbycategory",
            data: {
                id: id
            },
            success: function (result) {
                $('#questions').html("");
                $('#questions').append(result);
            }
        })
    })

    //$("#doctors_form").submit(function (e) {
    //    e.preventDefault();
    //});


    //$(document).on("click", '#search-doctor', function () {
    //    var name = $('#FullName').val()
    //    $.ajax({
    //        method: "GET",
    //        url: "/doctor/filterbyname",
    //        data: {
    //            name: name
    //        },
    //        success: function (result) {
    //            if (name != "") {
    //                $('#doctor-items').html("");
    //                $('#doctor-items').append(result);
    //            }
    //            else {
    //                $('#FullName').attr("placeholder", "Type Something")
    //            }

    //        }
    //    })
    //})


    $(document).on("click", '.category', function () {
        var id = $(this).data('id')
        $.ajax({
            method: "GET",
            url: "/shop/index",
            data: {
                id: id
            },
            success: function (result) {
                console.log(result)
                if (result != "") {
                    $('#products').html("");
                    $('#products').append(result);
                }

            }
        })
    })

    //$(document).on("click", '#product-search-btn', function () {
    //    var name = $('#search-product').val()
    //    $.ajax({
    //        method: "GET",
    //        url: "/shop/index",
    //        data: {
    //            name: name
    //        },
    //        success: function (result) {
    //            console.log(result)
    //            if (result != "") {
    //                $('#products').html("");
    //                $('#products').append(result);
    //            }

    //        }
    //    })
    //})


    $(document).on("click", '.addToCardBtn', function () {
        var id = $(this).data('id')
        var basketCount = $('#basketCount')
        var basketCurrentCount = $('#basketCount').html()
        $.ajax({
            method: "POST",
            url: "/basket/add",
            data: {
                id: id
            },
            success: function (result) {
                console.log(basketCount)
                basketCurrentCount++;
                basketCount.html("")
                basketCount.append(basketCurrentCount)
            }
        })

    })

    $(document).on("click", "#openBasket", function () {
        $.ajax({
            method: "GET",
            url: "/basket/minibasket",
            success: function (result) {
                console.log(result)
                $('#miniBasket').html("");
                $('#miniBasket').append(result);
            }
        })
    })

    $(document).on("click", '.deleteButton', function () {
        var id = $(this).data('id')
        var basketCount = $('#basketCount')
        var basketCurrentCount = $('#basketCount').html()
        var quantity = $(this).data('quantity')
        var sum = basketCurrentCount - quantity
        $.ajax({
            method: "POST",
            url: "/basket/delete",
            data: {
                id: id
            },
            success: function (result) {
                console.log(sum)

                $(`.basket-product[id=${id}]`).remove();
                basketCount.html("")
                basketCount.append(sum)
            }
        })
    })


    $(document).on("click", '.decrease', function () {
        var id = $(this).data('id')
        console.log(id)
        $.ajax({
            method: "POST",
            url: "/basket/decreasecount",
            data: {
                id: id
            },
            success: function (result) {

            }
        })
    })


    $(document).on("click", '.increase', function () {
        var id = $(this).data('id')
        console.log(id)
        $.ajax({
            method: "POST",
            url: "/basket/increasecount",
            data: {
                id: id
            },
            success: function (result) {

            }
        })
    })





});