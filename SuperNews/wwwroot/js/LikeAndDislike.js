$("#vbtn-radio1").click(function () {
    $.post("/News/Like",
        {
            Id: $('#NewsId').val(),
        },
        function (data, status) {
            $('#likeCount').text(data);
            $('#label_like').css("background-color", "#29a442");
            $('#label_like').css("color", "white");
            $('#label_dislike').css("background-color", "white");
            $('#label_dislike').css("color", "#ef3445");



        });

});
$("#vbtn-radio2").click(function () {
    $.post("/News/Dislike",
        {
            Id: $('#NewsId').val(),
        },
        function (data, status) {
            $('#dislikeCount').text(data);
            $('#label_dislike').css("background-color", "#ef3445");
            $('#label_dislike').css("color", "white");
            $('#label_like').css("background-color", "white");
            $('#label_like').css("color", "#29a442");


        });
});