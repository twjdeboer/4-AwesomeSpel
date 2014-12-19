$(document).ready(function(){
	$(document).on('click', '#adduser', function (){
		$.get('adduser',{username: 'fred4', password: 'aweseome', name: 'Frederik Boutersma', email: 'fredjeboutje@gmail.com'});
	});
});