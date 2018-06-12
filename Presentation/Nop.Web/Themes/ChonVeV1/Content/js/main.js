// JavaScript Document
$(function() {
	$('.popupDatepicker').datepick();
});

function showDate(date) {
	alert('The date chosen is ' + date);
}
$(document).ready(function(e) {	
	$('.sub-menu-mobile span').click(function(){
		if($('.menu').hasClass('active-display')){
				$('.menu').stop().slideUp();
				$('.menu').removeClass('active-display');
			}else{
				$('.menu').stop().slideDown();
				$('.menu').addClass('active-display');
			}
	});	
			$('.nav-resul ul li').unbind('click');
			$('.nav-resul ul li').click(function(event) {
				$('.nav-resul ul li').removeClass('active');
				$(this).addClass('active');
				$('.tab').removeClass('active');
				var tab_active = $(this).data('active');
				$(tab_active).addClass('active');
			});
			$('.col-md-4 .bus-start').unbind('click');
			$('.col-md-4 .bus-start').click(function(event) {
				$('.col-md-4 .bus-start').removeClass('active');
				$(this).addClass('active');
				$('.tab').removeClass('active');
				var tab_active = $(this).data('active');
				$(tab_active).addClass('active');
			});	
			


					
		$('#my-slide').DrSlider({
			width: undefined,
			height: undefined,
			userCSS: false,
			transitionSpeed: 1000,
			duration: 8000,
			showNavigation: true,
			classNavigation: undefined,
			navigationColor: '#0A8043',
			navigationHoverColor: '#0A8043',
			navigationHighlightColor: '#DFDFDF',
			navigationNumberColor: '#000000',
			positionNavigation: 'out-center-bottom',
			navigationType: 'circle',
			showControl: true,
			classButtonNext: undefined,
			classButtonPrevious: undefined,
			controlColor: '#FFFFFF',
			controlBackgroundColor: '#000000',
			positionControl: 'left-right',
			transition: 'slide-left',
			showProgress: true,
			progressColor: '#797979',
			pauseOnHover: false,
		});		
		$(".menu ul li").click(function(){
			$(".menu ul li").removeClass("active");
			$(this).addClass("active");			
			});	
});