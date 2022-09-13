

function addRow() {

    var myName = document.getElementById("name");
    var age = document.getElementById("age");
    var table = document.getElementById("myTableData");

    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);

    row.insertCell(0).innerHTML = '<input type="button" value = "Delete" onClick="Javacsript:deleteRow(this)">';
    row.insertCell(1).innerHTML = myName.value;
    row.insertCell(2).innerHTML = age.value;

}

function deleteRow(obj) {

    var index = obj.parentNode.parentNode.rowIndex;
    var table = document.getElementById("myTableData");
    table.deleteRow(index);

}

function addTable() {

    var myTableDiv = document.getElementById("myDynamicTable");

    var table = document.createElement('TABLE');
    table.border = '1';

    var tableBody = document.createElement('TBODY');
    table.appendChild(tableBody);

    for (var i = 0; i < 3; i++) {
        var tr = document.createElement('TR');
        tableBody.appendChild(tr);
        {
            for (var j = 0; j < 3; j++) {
                var td = document.createElement('TD');
                td.width = '75';
                td.appendChild(document.createTextNode("Cell " + i + "," + j));
                tr.appendChild(td);
            }
        }
    }
    myTableDiv.appendChild(table);

}

function GenerateTable() {
    var wardroom = document.getElementById("WardRoom");

    var slot = document.createElement('slot');

    var table = document.createElement('TABLE');

    var NumRoom = document.getElementById("NumRoom");

    for (var i = 0; i < 3; i++) {
        var tr = document.createElement('TR');
        tableBody.appendChild(tr);
        {
            for (var j = 0; j < 3; j++) {
                var td = document.createElement('TD');
                td.appendChild(document.createElement('slot'));
                tr.appendChild(td);
            }
        }
    }
    wardroom.appendChild(table);
    /*wardroom.appendChild(slot);*/
}

//function generateTable() {
//    var wardroom = document.getElementById("WardRoom")
 
//        var wardroomTable = document.createElement('table')
        
//        var wardroomRow = document.createElement('tr')
        
//        var wardroomBody = document.createElement('tbody')
        
//        var slot = document.createElement('slot')

//        wardroomTable.appendChild(slot)

//        wardroom.appendChild(wardroomTable)
    
//}

function generateTable() {
    const wardroom = document.querySelector("div.WardRoom")

    const createWardRoomTable = () => {
        let wardroomTable = document.createElement('table')
        wardroomTable.className = 'wardroomTable'

        let wardroomRow = document.createElement('tr')
        wardroomRow.className = 'wardroomRow'

        let wardroomBody = document.createElement('tbody')
        wardroomBody.className = 'wardroomBody'

        let slot = document.createElement('slot')
        slot.className = 'slot'
        wardroomTable.append(slot)

        wardroom.append(wardroomTable)
    }
}

function load() {

    console.log("Page load finished");

}