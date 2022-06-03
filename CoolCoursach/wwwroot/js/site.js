// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var element = document.getElementById('phone');
var maskOptions = {
    mask: '+7(000)000-00-00',
    lazy: false
}

var mask = new IMask(element, maskOptions);

var element2 = document.getElementById('passport');
var maskOptions2 = {
    mask: '0000 000-000',
    lazy: false
}
var mask = new IMask(element2, maskOptions2);

var element3 = document.getElementById('Year');
var maskOptions3 = {
    mask: '00/00/0000',
    lazy: false
}
var mask = new IMask(element3, maskOptions3);

var element4 = document.getElementById('passport2');
var maskOptions4 = {
    mask: '0000 000-000',
    lazy: false
}
var mask = new IMask(element4, maskOptions4);