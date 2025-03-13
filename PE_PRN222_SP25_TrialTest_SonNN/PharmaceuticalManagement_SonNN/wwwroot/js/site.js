let currentPage = 1;
const pageSize = 3;

// Define LoadData globally
function LoadData(page) {
    const searchActiveIngredients = $("#searchActiveIngredients").val();
    const searchExpirationDate = $("#searchExpirationDate").val();
    const searchWarnings = $("#searchWarnings").val();

    $.ajax({
        url: `/MedicineInformationPage?handler=LoadData&currentPage=${page}&pageSize=${pageSize}&searchActiveIngredients=${searchActiveIngredients}&searchExpirationDate=${searchExpirationDate}&searchWarnings=${searchWarnings}`,
        method: 'GET',
        success: (result) => {
            const medicines = result.medicines;
            const totalPages = result.totalPages;
            currentPage = result.currentPage;

            // Populate Table
            let tr = "";
            $.each(medicines, (index, medicine) => {
                tr += `<tr>
                    <td>${medicine.medicineName}</td>
                    <td>${medicine.activeIngredients}</td>
                    <td>${medicine.expirationDate}</td>
                    <td>${medicine.dosageForm}</td>
                    <td>${medicine.warningsAndPrecautions}</td>
                    <td>${medicine.manufacturerName}</td>
                    <td>`;

                if (userRole === "2") {
                    tr += `
                        <a href='/MedicineInformationPage/Edit?id=${medicine.medicineId}'>Edit</a>
                        <a href='/MedicineInformationPage/Delete?id=${medicine.medicineId}'>Delete</a>`;
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
