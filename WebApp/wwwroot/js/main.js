jQuery(document).ready(function($) {
	$('.bar-menu').click(function(event) {
		$(this).next('.list-child').slideToggle(300);
	});
});