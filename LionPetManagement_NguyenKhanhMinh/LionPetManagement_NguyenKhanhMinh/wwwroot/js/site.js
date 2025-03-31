let currentPage = 1;
const pageSize = 3;

// Define LoadData globally
function LoadData(page) {
    const lionName = $("#lionName").val();
    const lionTypeName = $("#lionTypeName").val();
    const weight = $("#weight").val();

    $.ajax({
        url: `/LionProfilePage?handler=LoadData&currentPage=${page}&pageSize=${pageSize}&lionName=${lionName}&lionTypeName=${lionTypeName}&weight=${weight}`,
        method: 'GET',
        success: (result) => {
            const lions = result.lions;
            const totalPages = result.totalPages;
            currentPage = result.currentPage;

            // Populate Table
            let tr = "";
            $.each(lions, (index, lion) => {
                tr += `<tr>
                    <td>${lion.lionName}</td>
                    <td>${lion.weight}</td>
                    <td>${lion.characteristics}</td>
                    <td>${lion.warning}</td>
                    <td>${lion.modifiedDate}</td>
                    <td>${lion.lionTypeName}</td>
                    <td>`;

                if (userRole === "2") {
                    tr += `
                        <a href='/LionProfilePage/Edit?id=${lion.lionProfileId}'>Edit</a>
                        <a href='/LionProfilePage/Delete?id=${lion.lionProfileId}'>Delete</a>`;
                }
                tr += `</td>
                </tr>`;
            });
            $("#tableBody").html(tr);

            // Update Pagination Controls
            const paginationHtml = `
                <a class="btn btn-primary mx-2 ${currentPage === 1 ? "disabled" : ""}" onclick="LoadData(${currentPage - 1})">Previous</a>
                <span class="align-self-center mx-2">Page ${currentPage} of ${totalPages}</span>
                <a class="btn btn-primary mx-2 ${currentPage === totalPages ? "disabled" : ""}" onclick="LoadData(${currentPage + 1})">Next</a>
            `;
            $("#paginationControls").html(paginationHtml);
        },
        error: (error) => {
            console.error("Error loading data:", error);
        }
    });
}

$(() => {
    LoadData(currentPage);

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/signalrServer")
        .configureLogging(signalR.LogLevel.Debug)
        .build();

    connection.start()
        .then(() => console.log("SignalR connection established."))
        .catch(err => console.error("SignalR connection failed: ", err));

    connection.on("LoadData", function () {
        LoadData(currentPage);
    });
});
