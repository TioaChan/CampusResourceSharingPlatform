$(document).ready(function () {

	//=========================================改变导航条按钮链接
	var index = $("#pills-tab .nav-item:has('.active')").parent().index();
	ChangeNavbarBtnLink(index);
	$("#pills-tab a").click(function (e) {
		index = $(this).parent().index();
		ChangeNavbarBtnLink(index);
	});
	//=========================================

	//=========================================
	var X = $('#pill-navbar').offset().top;
	$(document).scroll(function () {
		var Y = $('#pill-navbar').offset().top;
		if (Y != X) {
			$('#pill-navbar').addClass('shadow');
		} else if (Y == X) {
			$('#pill-navbar').removeClass('shadow');
		}
		console.log(X);
	});
	//=========================================
});

function ChangeNavbarBtnLink(e) {
	switch (e) {
	case 0:
		$("#navbar-btn").attr('href', '/Distribute/TakeExpress').text('立即下单');
		break;
	case 1:
		$("#navbar-btn").attr('href', '/Distribute/Purchase').text('立即下单');
		break;
	case 2:
		$("#navbar-btn").attr('href', '/Distribute/Sale').text('立即下单');
		break;
	case 3:
		$("#navbar-btn").attr('href', '/Distribute/Hire').text('立即下单');
		break;
	default:
		$("#navbar-btn").attr('href', '/Distribute/TakeExpress').text('立即下单');
		break;
	}
}