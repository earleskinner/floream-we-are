$(function () {
    //$("img").lazyload({
    //	effect : "fadeIn"
    //});
    $('#container').mixItUp();
});


//function (){	     

var $nav = $("nav"),
	orgNavpos = 70, //offset
	scrollHeight,
	scrollTop,
	$window = $(window),
	$document = $(document);

function scrollNav() {
    scrollTop = $document.scrollTop();
    scrollHeight = scrollTop + $window.height();

    if (scrollTop > orgNavpos) {
        $nav.addClass("small-nav");
    } else {
        $nav.removeClass("small-nav");
    }
}
$window.resize(function () {
    //init();
    scrollNav();
});
$window.scroll(function () {
    scrollNav();
});
$window.trigger("resize");
//}