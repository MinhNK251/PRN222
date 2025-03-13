function LoadData() {
    const searchTitle = $("#searchTitle").val();
    const userRole = $("#userRole").val();

    $.ajax({
        url: `/NewsArticlePages/Index?handler=LoadData&searchTitle=${searchTitle}`,
        method: 'GET',
        success: (result) => {
            const articles = result.articles.$values; // Access the array of articles
            let tr = "";

            $.each(articles, (index, article) => {
                // Ensure category is available
                const categoryName = article.Category ? article.Category.CategoryName : "No Category";

                // Ensure tags are available
                let tagsHtml = "<p>No Tag available</p>";
                if (article.Tags && article.Tags.$values && article.Tags.$values.length > 0) {
                    tagsHtml = "<ul>";
                    article.Tags.$values.forEach(tag => {
                        if (tag && tag.TagName) {
                            tagsHtml += `<li>${tag.TagName}</li>`;
                        }
                    });
                    tagsHtml += "</ul>";
                }

                // Ensure createdBy and updatedBy accounts exist
                const createdBy = article.CreatedBy ? article.CreatedBy.AccountName : "Unknown";
                const updatedBy = article.UpdatedBy ? article.UpdatedBy.AccountName : "Unknown";

                // Build the table row
                tr += `<tr>
                    <td>${article.NewsTitle || "undefined"}</td>
                    <td>${article.Headline || "undefined"}</td>
                    <td>${categoryName}</td>
                    <td>${tagsHtml}</td>
                    <td>${article.NewsSource || "undefined"}</td>
                    <td>${article.NewsStatus || "false"}</td>
                    <td>${article.CreatedDate || "undefined"}</td>
                    <td>${article.ModifiedDate || "undefined"}</td>
                    <td>${createdBy}</td>
                    <td>${updatedBy}</td>
                    <td>
                        ${userRole === "1"
                        ? `<a href="./NewsArticlePages/Edit?id=${article.NewsArticleId}">Edit</a> | 
                               <a href="./NewsArticlePages/Details?id=${article.NewsArticleId}">Details</a> | 
                               <a href="./NewsArticlePages/Delete?id=${article.NewsArticleId}">Delete</a>`
                        : `<a href="./NewsArticlePages/Details?id=${article.NewsArticleId}">Details</a>`
                        }
                    </td>
                </tr>`;
            });

            // Update the table body
            $("#tableBody").html(tr);
        },
        error: (error) => {
            console.error("Error loading data:", error);
        }
    });
}

$(() => {
    LoadData();

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/signalrServer")
        .build();

    connection.start()
        .then(() => console.log("SignalR connection established."))
        .catch(err => console.error("SignalR connection failed:", err));

    connection.on("LoadData", function () {
        LoadData();
    });
});