//Admin Courses Data Table

$('#adminCourses').DataTable({
    layout: {
        topStart: $('<div><h4>Courses List</h4></div>'),
        topEnd: {
            search: {
                placeholder: 'Search'
            }
        },
        bottomStart: {
            paging: {
                numbers: 3
            }
        },
        bottomEnd: null

    },
    ordering: false,
    responsive: true,
    pagingType: 'simple_numbers',
    pageLength: 5,
    scrollX: true,
    scrollY: true,
    columns: [{ width: '20%' }, { width: '20%' }, { width: '20%' }, { width: '20%' }, { width: '20%' }]
});

let adminCoursesTable = $('#adminCourses').DataTable();

$('#main-search-box').keyup(function () {
    adminCoursesTable.search($(this).val()).draw();
})

$("#dt-search-0").after(`<i class="bi bi-search text-secondary fs-5 position-relative admin " id="data-table-search-icon-sm"></i>`);

//$("#dt-search-0").after(`<i class="bi bi-search text-secondary fs-5 position-relative admin d-flex d-md-none " id="data-table-search-icon"></i>`);

$("#dt-search-0").before(`<a class="btn btn-primary position-relative d-sm-none " id="create-new-course-btn-sm" href="/Admin/CreateCourse">Create New Course</a>`);

$("#dt-search-0").after(`<a class="btn btn-primary position-relative d-none d-sm-inline" id="create-new-course-btn" href="/Admin/CreateCourse">Create New Course</a>`);

// For Ajax Call

/*

 $(document).ready(function () {
            $.ajax({
                url: "/Admin/CoursesList",
                method: "GET",

                success: function (data) {
                    var courseList = JSON.parse(data);
                    var tableRows = "";
                    $.each(courseList, function (index, course) {
                        courseDeleteModalId = course.CourseId;
                        courseDeleteModalIdWithHashSymbol = "#" + courseDeleteModalId;
                        courseDate = new Date(course.Date).toLocaleDateString();
                        courseTime = new Date(course.Time).toLocaleTimeString([], {
                            timeStyle: 'short'
                        });

                        tableRows +=
                            `<tr>
                                <td>${course.Name}</td>
                                <td>${course.Description}</td>
                                <td class="text-center">${course.NoOfDays}</td>
                                <td class="text-center">${courseDate} <br /> ${courseTime}</td>
                                <td class="text-center">

                                    <a href="EditCourse?id=${course.CourseId}" class="btn"><i class="bi bi-pencil-fill"></i></a>

                                    <button type="button" class="btn" data-bs-toggle="modal"
                                            data-bs-target="${courseDeleteModalIdWithHashSymbol}">
                                        <i class="bi bi-trash-fill"></i>
                                    </button>

                                    <!-- Modal -->
                                    <div class="modal fade" id="${courseDeleteModalId}" tabindex="-1"
                                            aria-labelledby="course-delete-modal-Label" aria-hidden="true">
                                        <div class="modal-dialog modal-dialog-centered">
                                            <div class="modal-content p-5 ">
                                                <div class="modal-header">
                                                    <button type="button" class="btn-close" data-bs-dismiss="modal"
                                                            aria-label="Close"></button>
                                                </div>
                                                <div class="modal-body">
                                                    <p class="fw-bold fs-5  ">Are you sure to Delete this Record?</p>
                                                    <p>Do you really want to delete this record? This process cannot be undone.</p>

                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" class="btn" data-bs-dismiss="modal">Cancel</button>
                                                    <a href="/Admin/DeleteCourse?id=${course.CourseId}" class="btn btn-danger">Delete</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </td>
                        </tr>`;
                    });

                    $("#adminCourses tbody").append(tableRows);

                    //Admin Courses Table

                    $('#adminCourses').DataTable({
                        layout: {
                            topStart: $('<div><h4>Courses List</h4></div>'),
                            topEnd: {
                                search: {
                                    placeholder: 'Search..'
                                }
                            },
                            bottomStart: {
                                paging: {
                                    numbers: 3
                                }
                            },
                            bottomEnd: null

                        },
                        ordering: false,
                        responsive: true,
                        pagingType: 'simple_numbers',
                        pageLength: 5,
                        scrollX: true,
                        scrollY: true,
                        columns: [{ width: '20%' }, { width: '20%' }, { width: '20%' }, { width: '20%' }, { width: '20%' }]
                    });

                    let adminCoursesTable = $('#adminCourses').DataTable();

                    $('#main-search-box').keyup(function () {
                        adminCoursesTable.search($(this).val()).draw();
                    })

                    $("#dt-search-0").after(`<i class="bi bi-search text-secondary fs-5 position-relative admin d-sm-none" id="data-table-search-icon-sm"></i>`);

                    $("#dt-search-0").after(`<i class="bi bi-search text-secondary fs-5 position-relative admin d-none d-sm-inline" id="data-table-search-icon"></i>`);

                    $("#dt-search-0").before(`<a class="btn btn-primary position-relative d-sm-none " id="create-new-course-btn-sm" href="/Admin/CreateCourse">Create New Course</a>`);

                    $("#dt-search-0").after(`<a class="btn btn-primary position-relative d-none d-sm-inline" id="create-new-course-btn" href="/Admin/CreateCourse">Create New Course</a>`);
                },
                error: function (error) {
                    console.log(error);
                }
            });

        });

*/