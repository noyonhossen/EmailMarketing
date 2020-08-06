$(function() {
    $("#upload_link").on('click', function(e) {
        e.preventDefault();
        $("#upload:hidden").trigger('click');
    });

    $("#upload:hidden").change(function (e) {
        var file = e.target.files[0];
        var isFileSelected = false;
        var fileName = 'Recommended file formats: .csv, .xls, .xlsx';

        if (file !== null && file !== undefined) {
            fileName = 'The file "' + file.name + '" has been selected.';
            isFileSelected = file.size > 0;
            parseExcel(file);
        }

        $("#upload_file_name").text(fileName);
        document.getElementById('nextBtn').disabled = !isFileSelected;
    });

    

    $('#has-column-header').change(function (e) {
        var has = $(this).is(":checked");
        generateColumnHeaderDisable(has);
    });




    //Show Hide
    $('#showHide').click(function () {
        $(this).text(function (i, old) {
            return old == 'Show' ? 'Hide' : 'Show';
        });
    });

    $('#showHideCustom').click(function () {
        $(this).text(function (i, old) {
            return old == 'Show' ? 'Hide' : 'Show';
        });
    });

//Show Hide
});

var mapFieldDropdownData = [];
var mapFieldTableColumnHeaderData = [];
var mapFieldTableRowData = [];
var hasColumnHeader = true;

function renderGetAllFieldMapsForUploadContacts(response) {
    console.log(response);
    mapFieldDropdownData = response;
    console.log(mapFieldDropdownData);
}


//var ExcelToJSON = function () {

    this.parseExcel = function (file) {
        var reader = new FileReader();

        reader.onload = function (e) {
            var data = e.target.result;
            var workbook = XLSX.read(data, {
                type: 'binary'
            });

            workbook.SheetNames.forEach(function (sheetName, index) {
                if (index == 0) {
                    // Here is your object
                    var XL_row_object = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[sheetName]);
                    var json_object = JSON.stringify(XL_row_object);
                    console.log(json_object);
                    //console.log(Object.keys(json_object).length);
                    //Object.keys(json_object).forEach(function (val, index) { console.log(val) });
                    var tableColHdr = [];
                    for (var key in XL_row_object[0]) {
                        console.log(key);
                        console.log(XL_row_object[0][key]);
                        tableColHdr.push(key);
                    }

                    mapFieldTableColumnHeaderData = tableColHdr;
                    mapFieldTableRowData = XL_row_object;

                    generateMapFieldTableData();
                }
            });

        };

        reader.onerror = function (ex) {
            console.log(ex);
        };

        reader.readAsBinaryString(file);
    };
//};



function generateMapFieldTableColumnHeaderData() {
    var rowMarkup = '';

    mapFieldDropdownData.forEach(function (grpValue, grpIndex) {
        rowMarkup += '<optgroup label="' + (grpValue.isStandard ? 'Standard Fields' : 'Custome Fields') + '">';

        grpValue.values.forEach(function (value, index) {
            rowMarkup += '<option value = "' + value.value + '">' + value.text + '</option>';
        });
        rowMarkup += '</optgroup >';
    });

    $('.map-field-dropdown').append(rowMarkup);
}

function generateMapFieldTableData() {
    var rowMarkup = '';

    mapFieldTableColumnHeaderData.forEach(function (value, index) {

        rowMarkup += '<th><select class="form-control map-field-dropdown">' +
            '<option value = "-1">---Ignore---</option>' +
            '</select></th>';
    });

    rowMarkup = '<tr class="text-center">' + rowMarkup + '</tr>';

    $('#map-field-table > thead').append(rowMarkup);

    rowMarkup = '<tr class="text-center has-column-header-row">';

    mapFieldTableColumnHeaderData.forEach(function (colValue, colIndex) {
        rowMarkup += '<td>' + colValue + '</td>';
    });

    rowMarkup += '</tr>'
    $('#map-field-table > tbody').append(rowMarkup);

    generateColumnHeaderDisable(hasColumnHeader);

    mapFieldTableRowData.forEach(function (rowValue, rowIndex) {
        rowMarkup = '<tr class="text-center">';

        mapFieldTableColumnHeaderData.forEach(function (colValue, colIndex) {
            rowMarkup += '<td>' + mapFieldTableRowData[rowIndex][colValue] + '</td>';
        });

        rowMarkup += '</tr>'
        $('#map-field-table > tbody').append(rowMarkup);
    });

    generateMapFieldTableColumnHeaderData();
}

function generateColumnHeaderDisable(has) {
    if (has) {
        hasColumnHeader = true;
        $('.has-column-header-row').addClass('bg-col-header text-muted');
    } else {
        hasColumnHeader = false;
        $('.has-column-header-row').removeClass('bg-col-header text-muted');
    }
}













//upload contact
const previousBtn = document.getElementById('previousBtn');
const nextBtn = document.getElementById('nextBtn');
const finishBtn = document.getElementById('finishBtn');
const uploadContacts = document.getElementById('uploadContacts');
const mapFields = document.getElementById('mapFields');
const chooseActions = document.getElementById('chooseActions');
const reviewConfirm = document.getElementById('reviewConfirm');
const bullets = [...document.querySelectorAll('.bullet')];

const MAX_STEPS = 4;
let currentStep = 1;


nextBtn.addEventListener('click', () => {

    const currentBullet = bullets[currentStep - 1];
    const currentBullet2 = bullets[currentStep];
    currentBullet.classList.add('completed');
    currentBullet2.classList.add('current');
    currentStep++;
    previousBtn.disabled = false;
    if (currentStep == MAX_STEPS) {
        nextBtn.disabled = true;
        finishBtn.disabled = false;
    }

    if (currentStep == 1) {
        uploadContacts.style.display = 'block';
        mapFields.style.display = 'none';
        chooseActions.style.display = 'none';
        reviewConfirm.style.display = 'none';
    } else if (currentStep == 2) {
        uploadContacts.style.display = 'none';
        mapFields.style.display = 'block';
        chooseActions.style.display = 'none';
        reviewConfirm.style.display = 'none';
    } else if (currentStep == 3) {
        uploadContacts.style.display = 'none';
        mapFields.style.display = 'none';
        chooseActions.style.display = 'block';
        reviewConfirm.style.display = 'none';
    } else if (currentStep == 4) {
        uploadContacts.style.display = 'none';
        mapFields.style.display = 'none';
        chooseActions.style.display = 'none';
        reviewConfirm.style.display = 'block';
    }


});

previousBtn.addEventListener('click', () => {

    const previousBullet2 = bullets[currentStep - 1];
    const previousBullet = bullets[currentStep - 2];
    previousBullet.classList.remove('completed');
    previousBullet2.classList.remove('current');
    currentStep--;
    //nextBtn.disabled = true;
    const file = document.getElementById("upload").files[0];
    const isFileSelected = file !== null && file !== undefined ? file.size > 0 : false;
    nextBtn.disabled = !isFileSelected;
    finishBtn.disabled = true;
    if (currentStep == 1) {
        previousBtn.disabled = true;
    }
    if (currentStep == 1) {
        uploadContacts.style.display = 'block';
        mapFields.style.display = 'none';
        chooseActions.style.display = 'none';
        reviewConfirm.style.display = 'none';
    } else if (currentStep == 2) {
        uploadContacts.style.display = 'none';
        mapFields.style.display = 'block';
        chooseActions.style.display = 'none';
        reviewConfirm.style.display = 'none';
    } else if (currentStep == 3) {
        uploadContacts.style.display = 'none';
        mapFields.style.display = 'none';
        chooseActions.style.display = 'block';
        reviewConfirm.style.display = 'none';
    } else if (currentStep == 3) {
        uploadContacts.style.display = 'none';
        mapFields.style.display = 'none';
        chooseActions.style.display = 'none';
        reviewConfirm.style.display = 'block';
    }

    //content.innerText = `Step Number ${currentStep}`;

});
