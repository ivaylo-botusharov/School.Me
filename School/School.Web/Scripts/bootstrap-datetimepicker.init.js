$(document).ready(
    function () {
        $("#datetimepicker1").datetimepicker({
            format: 'YYYY-MM-DD'
        });

        $("#datetimepicker2").datetimepicker({
            useCurrent: false,
            format: 'YYYY-MM-DD'
        });

        $("#datetimepicker1").on("dp.change", function (e) {
            $('#datetimepicker2').data("DateTimePicker").minDate(e.date);
        });

        $("#datetimepicker2").on("dp.change", function (e) {
            $('#datetimepicker1').data("DateTimePicker").maxDate(e.date);
        });

    });