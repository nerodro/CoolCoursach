/* When the user clicks on the button,
       toggle between hiding and showing the dropdown content */
function myFunction() {
    document.getElementById("my Dropdown").classList.toggle("show");
}

function filterFunction() {
    var input, filter, ul, li, a, i;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();
    div = document.getElementById("my Dropdown");
    a = div.getElementsByTagName("a");
    for (i = 0; i < a.length; i++) {
        txtЗначение = a[i].textСодержание || a[i].innerText;
        if (txtЗначение.toUpperCase().indexOf(filter) > -1) {
            a[i].style.display = "";
        } else {
            a[i].style.display = "none";
        }
    }
}

