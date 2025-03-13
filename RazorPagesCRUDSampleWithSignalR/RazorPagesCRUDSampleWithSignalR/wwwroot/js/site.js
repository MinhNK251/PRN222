$(() => {
    loadStudentData();

    var connection = new signalR.HubConnectionBuilder().withUrl("signalrServer").build();
    connection.start();

    connection.on("LoadStudents", function () {
        loadStudentData();
    })

    //loadStudentData();// need review

    function loadStudentData() {
        var tr = '';
        $.ajax({
            url: '/StudentView/Index?handler=Students',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    tr += `<tr><td>${v.id}</td><td>${v.name}</td></tr>`
                })

                $("#tableBody").html(tr);
            },
            error: (error) => {
                console.log(error)
            }
        })
    }
})