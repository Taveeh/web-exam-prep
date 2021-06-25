let movies = [];
let documents = [];

const insertData = (newBody, data) => {
    let result = JSON.parse(data);
    for (let item of result) {
        let newRow = newBody.insertRow();
        // if (result.indexOf(item) >= 4 * currentPage) {
        for (let index of ['id', 'ownerid', 'name', 'description', 'subscribers']) {
            let newCol = newRow.insertCell();
            let newText = document.createTextNode(item[index]);
            newCol.appendChild(newText);
        }
        newBody.append(newRow);
        // }
        // if (result.indexOf(item) >= 4 * currentPage + 3) {
        //     break;
        // }
    }
}

const display = () => {
    let i = 0;
    const list = $("#authorData").eq(0);
    while (i < movies.length && i < documents.length){
        let newLi = document.createElement("li");
        let newText = document.createTextNode(movies[i]["Id"] + " - " +
            movies[i]["Title"] + " - "+ movies[i]["Duration"]);
        newLi.appendChild(newText);

        let newLi2 = document.createElement("li");
        let newText2 = document.createTextNode(documents[i]["Id"] +
             " - " +documents[i]["Name"] + " - "+ documents[i]["Contents"]);
        newLi2.appendChild(newText2);
        
        list.append(newLi);
        list.append(newLi2);
        
        i += 1;
    }
    
    while(i < movies.length){
        let newLi = document.createElement("li");
        let newText = document.createTextNode(movies[i]["Id"] + " - " +
            movies[i]["Title"] + " - "+ movies[i]["Duration"]);
        newLi.appendChild(newText);
        list.append(newLi);
        i += 1;
    }
    
    while (i < documents.length){
        let newLi2 = document.createElement("li");
        let newText2 = document.createTextNode(documents[i]["Id"] +
            " - " +documents[i]["Name"] + " - "+ documents[i]["Contents"]);
        newLi2.appendChild(newText2);
        list.append(newLi2);
        i += 1;
    }
};

const getMoviesOfAuthor = () => {
    $.ajax({
        type: 'GET',
        url: "/Main/GetMoviesFromAuthor",
        data: {
        },
        success: (data) => {
            movies = JSON.parse(data);
        }
    })
}

const getDocumentsOfAuthor = () => {
    $.ajax({
        type: 'GET',
        url: "/Main/GetDocumentsFromAuthor",
        data: {
        },
        success: (data) => {
            documents = JSON.parse(data);
        }
    })
}

$(document).ready(() => {
    $("#addDoc").click(() => {
        const name = $("#docName").val();
        const contents = $("#docContent").val();

        $.ajax({
            type: 'GET',
            url: "/Main/AddDocument",
            data: {
                name,
                contents
            },
            success: (data) => {
                console.log(data);
            }
        })
    })
    
    $("#show").click(() => {
        $.ajax({
            type: 'GET',
            url: "/Main/GetDocumentWithMostAuthors",
            data: {
            },
            success: (data) => {
                const doc = JSON.parse(data);
                const p = $("#document").eq(0);
                let text = document.createTextNode(doc["Id"] +
                    " - " +doc["Name"] + " - "+ doc["Contents"]);
                p.append(text);
            }
        })
    })
    
    getDocumentsOfAuthor();
    getMoviesOfAuthor();

    setTimeout(() => {
        console.log(movies);
        console.log(documents);
        display();
    }, 1000);
});

//
// const getChannelsForName = () => {
//     let ownerName = $('#ownerName').val();
//     let body = $(".table tbody").eq(0);
//     let newBody = document.createElement("tbody");
//     $.ajax({
//         type: 'GET',
//         url: "http://localhost/Ex6_2020/Controller.php",
//         data: {
//             action: 'getForOwner',
//             ownerName: ownerName
//         },
//         success: (data) => {
//             insertData(newBody, data);
//         }
//     })
//     body.replaceWith(newBody)
// }
