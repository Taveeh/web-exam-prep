let savedData = [];
function insertData (newBody, data) {
    let result = JSON.parse(data);
    savedData = result;
    for (let item of result) {
        let newRow = newBody.insertRow();
        // if (result.indexOf(item) >= 4 * currentPage) {
        for (let index of ['Id', 'User', 'Topicid', 'Text', 'Date']) {
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

const getAllTopics = () => {
    let body = $(".table tbody").eq(0);
    let newBody = document.createElement("tbody");
    $.ajax({
        type: 'GET',
        url: "/Main/GetAll",
        success: (data) => {
            insertData(newBody, data);
        }
    })
    body.replaceWith(newBody)
}

const updateTopic = () => {
    let text = $("#textField").val();
    let id = $("#idField").val();
    $.ajax({
        type: 'GET',
        url: "/Main/UpdateTopic",
        data: {
            id,
            text
        },
        success: () => {
            getAllTopics();
        }
    })
}

const addPost = () => {
    let topicname = $('#topicNameField').val();
    let text = $('#postTextName').val();
    $.ajax({
        type: 'GET',
        url: '/Main/AddTopic',
        data: {
            text,
            topicname
        },
        success: () => {
            getAllTopics();
        }
    })
}

const timeout = () => {
    setTimeout(() => {
        $.ajax({
            type: 'GET',
            url: '/Main/GetAll',
            success: function (result) {
                console.log(savedData)
                result = JSON.parse(result);
                console.log(result);
                for (let elem of result) {
                    let ok = false;
                    for (let old of savedData) {
                        if (elem["Id"] === old["Id"]) {
                            ok = true;
                        }
                    }
                    if (!ok) {
                        alert(elem["Text"]);
                        getAllTopics();
                    }
                }
            }
        });
        console.log("Sunt aici yay")
        timeout();
    }, 5000)
}
$(document).ready(() => {
    getAllTopics();
    $("#updateButton").click(() => {
        updateTopic();
    })
    
    $("#addButton").click(() => {
        addPost();
    })
    
    setTimeout(timeout, 5000);
    
    
})