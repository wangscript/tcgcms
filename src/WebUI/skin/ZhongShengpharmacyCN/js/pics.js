


var sh;
var templayer;
preLeft = 0; currentLeft = 0; stopscroll = false; getlimit = 0; preTop = 0; currentTop = 0;
function scrollLeft() {
    var marquees = document.getElementById("marquees")
    if (stopscroll == true) return;
    preLeft = marquees.scrollLeft;
    marquees.scrollLeft += 2;
    if (preLeft == marquees.scrollLeft) {
        //marquees.scrollLeft=templayer.offsetWidth-marqueesWidth+1;
    }
}

function scrollRight() {
    var marquees = document.getElementById("marquees")
    if (stopscroll == true) return;

    preLeft = marquees.scrollLeft;
    marquees.scrollLeft -= 2;

}

function Left() {
    stopscroll = false;
    sh = setInterval("scrollLeft()", 20);
}

function Right() {
    stopscroll = false;
    sh = setInterval("scrollRight()", 20);
}

function StopScroll() {
    stopscroll = true;
    clearInterval(sh);
}