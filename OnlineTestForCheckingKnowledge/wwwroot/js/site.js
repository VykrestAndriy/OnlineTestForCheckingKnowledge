$(document).ready(function () {
});

function setLanguage(culture) {
    document.cookie = 'AspNetCore.Culture=' + culture + '|uic=' + culture + ';path=/;';
    location.reload();
}