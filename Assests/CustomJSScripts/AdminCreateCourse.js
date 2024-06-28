
let n = 2;
$("#add-date-time-controls").on('click', function () {

    let dateId = "dt-" + n;
    n++;

    $("#date-time-container").append(`
            <div class="d-flex align-items-center col-12 row mt-2" id = "` + dateId + `">
                <div class="mb-3 col-5">
                    <label class="form-label" for="Date">Date</label>
                    <input class="form-control w-100 shadow-none" data-val="true" data-val-required="Start date of the course is missing" id="Date" name="Date" required="true" type="date" value="">
            <span class="field-validation-valid" data-valmsg-for="Date" data-valmsg-replace="true"></span>
                    </div>
                    <div class="mb-3 col-5">

                    <label class="form-label" for="Time">Time</label>
        <input class="form-control w-100 shadow-none" data-val="true" data-val-required="Start time of the course is missing" id="Time" name="Time" required="true" type="time" value="">
            <span class="field-validation-valid" data-valmsg-for="Time" data-valmsg-replace="true"></span>
                    </div>
                <button type="button" class="btn col-2 mt-1 "  onclick="removeDateTime('` + dateId + `')"><i class="bi bi-trash-fill fs-5 "></i></button>
            </div>`);
});

function removeDateTime(dateTimeId) {
    $("#" + dateTimeId).remove();
}

//Ajax Call

/*$("#create-course").on("submit", function (e) {
    e.preventDefault();

    var form = $('#create-course');

    var token = $('input[name="__RequestVerificationToken"]', form).val();
    {
        Name: $("#Name").val(),
            NoOfDays: $("#NoOfDays").val(),
                Description: $("#Description").val(),
                    Date: [],
                        Time: [],
            };

    $("[name=Date]").each(function (i, item) {
        formData.Date.push($(item).val());
    });

    $("[name=Time]").each(function (i, item) {
        formData.Time.push($(item).val());
    });

    $.ajax({
        method: "POST",
        url: "/Admin/SaveCourse",
        data: {
            __RequestVerificationToken: token,
            courseDetails: formData,
        },
        success: function (data) {

            window.location.href = "/Admin/Courses";

        },
    });
});*/
