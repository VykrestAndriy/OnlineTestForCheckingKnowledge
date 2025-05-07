$(document).ready(function () {
    console.log("site.js готовий");
});

function setLanguage(culture) {
    console.log("Викликано setLanguage з культурою:", culture);
    document.cookie = 'AspNetCore.Culture=' + culture + '|uic=' + culture + ';path=/;';
    location.reload();
}