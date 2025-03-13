const { signalR } = require("../microsoft/signalr/dist/browser/signalr");

$(() => {
    loadStudentData();

    var connection = new signalR.HubConnectionBuilder().withUrl("signalrServer").buid();
    connection.start();

    connection.on("LoadStudents", function () {
        loadStudentData();
    })

    loadStudentData();

    function loadStudentData() {
        var tr = '';
        $.ajax({
            url: '/StudentView/Index',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {
                    tr += '<tr><td>${v.Id}</td><td>${v.Name}</td></tr>'
                })

                $("#tableBody").html(tr);
            },
            error: (error) => {
                console.log(error)
            }
        })
    }
})