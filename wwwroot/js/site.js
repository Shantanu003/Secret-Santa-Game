// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function showFileName(inputId, dropboxId) {
    var inputElement = document.getElementById(inputId);
    var dropboxElement = document.getElementById(dropboxId);
    var fileName = inputElement.files.length > 0 ? inputElement.files[0].name : "No file selected";
    dropboxElement.querySelector(".file-name").textContent = fileName;
}