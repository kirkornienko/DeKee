$(function() {
	// card image switch
	$('.choose-card-select').change(function (e) {
		$('.card-preview__placeholder').hide();
		$('.card-preview__placeholder.card-' + e.target.value).show();
	});
});