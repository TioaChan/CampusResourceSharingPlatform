$(document).ready(function () {
	var ua = navigator.userAgent.toLocaleLowerCase();
	var screenWidth = window.screen.width;
	var bodyHeight = $("body").height();
	console.log(ua);
	console.log("屏幕宽度 " + screenWidth);
	console.log("内容高度 " + bodyHeight);
});