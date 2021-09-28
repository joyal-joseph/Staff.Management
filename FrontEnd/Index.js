var staffList = [];
var currentList = [...staffList];
var itemsPerPage = 10;
var currentPage = 1;
var deleteList = [];
const Gender = ["Male", "Female", "Others"]

var currentEditingData = {};
async function addCell(cellData) {
    var cell = row.insertCell(-1);
    cell.innerHTML = cellData;
}

async function deleteButton(id) {
    event.stopPropagation();
    await fetch('http://staffmanagement.dev.com/api/Staff/' + id, {
        method: 'DELETE',

    }).then(() => {
        console.log("deleted");
        staffFetch();
    })

}

function update(data) {
    document.querySelector('form').addEventListener('submit', submitHandler);
    if (data != undefined) {
        currentEditingData = {...data };
        document.getElementById("AddOrUpdateHeading").innerHTML = "Update";
        document.getElementById("popUpForm").style.display = "flex";
        document.getElementById("formButton").innerHTML = "Edit";
        document.getElementById("Name").value = data.name;
        document.getElementById("Age").value = data.age;
        document.getElementById("DailyWage").value = data.dailyWage;
        document.getElementById("Gender").value = data.gender;
        document.getElementById("JobType").value = data.jobType;
        document.getElementById("StaffID").value = data.staffID;
        jobField();
    }
}

function add() {
    document.querySelector('form').addEventListener('submit', submitHandler);
    document.getElementById("AddOrUpdateHeading").innerHTML = "Add";
    document.getElementById("popUpForm").style.display = "flex";
    document.getElementById("formButton").innerHTML = "Save";
    document.getElementById("Name").value = null;
    document.getElementById("Age").value = null;
    document.getElementById("DailyWage").value = null;
    document.getElementById("Gender").value = null;
    currentEditingData.jobType = null;
    document.getElementById("JobType").value = "None";
    var FieldDivision = document.getElementById("JobFieldInput");
    FieldDivision.innerHTML = "";
    currentEditingData = null;
    jobField();
}

function submitHandler(e) {
    e.preventDefault();
    const data = Object.fromEntries(new FormData(e.target).entries());
    data.dailyWage = parseInt(data.dailyWage);
    data.staffID = parseInt(data.staffID);
    data.age = parseInt(data.age);
    data.gender = parseInt(data.gender);
    console.log(data);
    var myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");
    var requestOptions = {
        method: 'POST',
        headers: myHeaders,
        body: JSON.stringify(data),
    };
    var url = "http://staffmanagement.dev.com/api/Staff/";
    if (data.staffID != -1) {
        requestOptions.method = "PUT";
        url = url + data.staffID;
    }
    fetch(url, requestOptions)
        .then(response => {
            response.text();
            staffFetch();
        })

    .then(result => console.log(result))
        .catch(error => console.log('error', error));
    popUpClose();
}

function jobField() {
    var selection = document.getElementById("JobType");
    var FieldDivision = document.getElementById("JobFieldInput");
    switch (selection.value) {
        case "Teacher":
            FieldDivision.innerHTML = " <label for='Subject'>Subject:</label" + "\>" +
                "<input type='text' id='Subject' name='subject' value=''><br>" +
                " <label for='ClassTeacher'>Class Teacher:</label" + "\>" +
                "<input type='text' id='ClassTeacher' name='classTeacher' value=''><br>";
            if (currentEditingData != null) {
                document.getElementById("ClassTeacher").value = currentEditingData.classTeacher == null ? "" : currentEditingData.classTeacher;
                document.getElementById("Subject").value = currentEditingData.subject == null ? "" : currentEditingData.subject;
            } else {
                document.getElementById("ClassTeacher").value = "";
                document.getElementById("Subject").value = "";
            }
            break;
        case "Support":
            FieldDivision.innerHTML = " <label for='Lab'>Lab:</label" + "\>" +
                "<input type='text' id='Lab' name='lab' value='' required><br> ";
            if (currentEditingData != null) {
                document.getElementById("Lab").value = currentEditingData.lab == undefined ? "" : currentEditingData.lab;
            } else {
                document.getElementById("Lab").value = "";
            }
            break;
        case "Admin":
            FieldDivision.innerHTML = " <label for='Section'>Section:</label" + "\>" +
                "<input type='text' id='Section' name='section' value='' required><br> ";
            if (currentEditingData != null) {
                document.getElementById("Section").value = currentEditingData.section == null ? "" : currentEditingData.section;
            } else {
                document.getElementById("Section").value = "";
            }
            break;

        default:
            FieldDivision.innerHTML = "";
            break;
    }
}

function popUpClose() {
    document.getElementById("popUpForm").style.display = "none";
    document.querySelector("form").removeEventListener("submit", submitHandler);
}

function toggle() {
    document.getElementById("myDropdown").classList.toggle("show");
}


window.onclick = function(event) {
    if (!event.target.matches('.topbtn')) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}

function filterByType(typeName) {
    var filterBox = document.getElementById("filterButton");
    switch (typeName) {
        case "All staff":
            filterBox.innerHTML = "All staff";
            currentList = [...staffList];
            break;
        case "Teacher":
            filterBox.innerHTML = "Teacher";
            filterByTypeSupport("Teacher");
            break;
        case "Admin":
            filterBox.innerHTML = "Admin";
            filterByTypeSupport("Admin");
            break;
        case "Support":
            filterBox.innerHTML = "Support";
            filterByTypeSupport("Support");
            break;
    }
    // mainTable(currentList);
    paginationButtons(1);
}

function filterByTypeSupport(jobType) {
    currentList = []
    for (var a = 0; a < staffList.length; a++) {
        if (staffList[a].jobType == jobType) {
            currentList.push(staffList[a]);
        }
    }
}

async function staffFetch() {
    let response = await fetch('http://staffmanagement.dev.com/api/Staff');
    let data = await response.json();
    staffList = [...data];
    currentList = staffList;
    currentPage = (Math.ceil(currentList.length / itemsPerPage) < currentPage) && currentPage != 1 ? currentPage - 1 : currentPage;
    paginationButtons(currentPage)
}
async function mainTable(data) {
    deleteList = [];
    console.log(currentPage);
    var table = document.getElementById("StaffTable");
    var tableBody = table.getElementsByTagName("tbody")[0];
    tableBody.innerHTML = "";
    for (let i = 0; i < data.length; i++) {
        var row = tableBody.insertRow(-1);
        row.onclick = () => update(data[i]);

        var cell1 = row.insertCell(-1);
        cell1.innerHTML = data[i].staffID;

        var cell2 = row.insertCell(-1);
        cell2.innerHTML = data[i].name;

        var cell3 = row.insertCell(-1);
        cell3.innerHTML = data[i].age;


        var cell4 = row.insertCell(-1);
        cell4.innerHTML = Gender[data[i].gender];

        var cell5 = row.insertCell(-1);
        cell5.innerHTML = data[i].dailyWage;


        var cell6 = row.insertCell(-1);
        cell6.innerHTML = data[i].jobType;

        var cell7 = row.insertCell(-1)
        if (data[i].subject != null) {

            cell7.innerHTML = data[i].subject;
        } else {
            cell7.innerHTML = "NA";
        }

        var cell8 = row.insertCell(-1)
        if (data[i].classTeacher != null && data[i].classTeacher != "") {

            cell8.innerHTML = data[i].classTeacher;
        } else {
            cell8.innerHTML = "NA";
        }
        var cell9 = row.insertCell(-1)
        if (data[i].jobType == "Admin") {
            cell9.innerHTML = data[i].section;
        } else {
            cell9.innerHTML = "NA";
        }

        var cell10 = row.insertCell(-1)
        if (data[i].lab != null) {
            cell10.innerHTML = data[i].lab;
        } else {
            cell10.innerHTML = "NA";
        }

        var btn = document.createElement("button");
        btn.onclick = () => deleteButton(data[i].staffID);
        btn.id = "DeleteButton";
        btn.textContent = "Delete";

        var buttonCell = row.insertCell(-1);
        buttonCell.appendChild(btn);

        var checkBox = document.createElement("input");
        checkBox.type = "checkbox";
        checkBox.onclick = () => {
            event.stopPropagation();
            deleteList.push(data[i].staffID)
        };
        var checkboxCell = row.insertCell(-1);
        checkboxCell.id = "CheckBoxColumn";
        checkboxCell.appendChild(checkBox);
    }
}

function sortFunction(columnName) {
    function getSortOrder(columnName) {
        return function(a, b) {
            if (a[columnName] > b[columnName]) {
                return 1;
            } else if (a[columnName] < b[columnName]) {
                return -1;
            }
            return 0;
        }
    }

    currentList.sort(getSortOrder(columnName));
    paginationButtons(currentPage)
    console.log(staffList.length);
    console.log(currentList.length);
}

function paginationButtons(currentPage) {
    console.log(currentPage);
    mainTable(currentList.slice((currentPage - 1) * itemsPerPage, currentPage * itemsPerPage));
    var pagerButtons = document.getElementById("pagination");
    pagerButtons.innerHTML = "";

    var button = document.createElement("button");
    button.innerHTML = "&laquo;";
    button.className = "paginationButton";
    button.onclick = "mainTable(currentList)";
    pagerButtons.appendChild(button);
    for (let index = 1; index < Math.ceil(currentList.length / itemsPerPage) + 1; index++) {
        var button = document.createElement("button");
        button.textContent = index;
        button.className = "pagination";
        button.id = index;

        button.onclick = () => {
            this.document.getElementById(globalThis.currentPage).style.background = "";
            globalThis.currentPage = index;
            mainTable(currentList.slice((index - 1) * itemsPerPage, (index) * itemsPerPage));
            document.getElementById(index).style.background = "red";
        }
        pagerButtons.appendChild(button);


    }
    document.getElementById(currentPage).style.background = "red";
    console.log(Math.ceil(138 / 10));
    var button = document.createElement("button");
    button.innerHTML = "&raquo;";
    button.className = "pagination";
    button.onclick = "mainTable(currentList)";
    pagerButtons.appendChild(button);
}

function multipleDelete() {
    deleteList.forEach(id => {
        deleteButton(id);
    });
}


staffFetch();