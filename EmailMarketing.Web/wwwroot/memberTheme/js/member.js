$(function() {
    $("#upload_link").on('click', function(e) {
        e.preventDefault();
        $("#upload:hidden").trigger('click');
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
    nextBtn.disabled = false;
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
