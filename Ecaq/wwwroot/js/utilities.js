function BlzRoxy() {

	TinyBlzRoxy()		
	
}


//window.BlzRoxy = () => {

//	height: 550,
//	plugins: 'code image link lists table wordcount',
//	toolbar: 'undo redo | styles | bold italic underline forecolor backcolor | image link removeformat code | align | bullist numlist | table',
//	menubar: false,
//	images_upload_base_path: '/some/basepath',
//	file_picker_callback: RoxyFileBrowser


//	//height: 550,
//	//plugins: 'advlist anchor autolink charmap code codesample directionality help image editimage insertdatetime link lists media nonbreaking pagebreak preview searchreplace table template tableofcontents visualblocks visualchars wordcount',
//	//toolbar: 'undo redo | blocks | bold italic strikethrough forecolor backcolor blockquote | link image media | alignleft aligncenter alignright alignjustify | numlist bullist outdent indent | removeformat',
//	//content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:16px }',
//	//images_upload_base_path: '/some/basepath',
//	//file_picker_callback: RoxyFileBrowser


//	//height: 500,
//	//plugins: [
//	//    'advlist', 'autolink', 'lists', 'link', 'image', 'charmap', 'preview',
//	//    'anchor', 'searchreplace', 'visualblocks', 'code', 'fullscreen',
//	//    'insertdatetime', 'media', 'table', 'help', 'wordcount'
//	//],
//	//toolbar: 'undo redo | blocks | ' +
//	//    'bold italic forecolor | alignleft aligncenter ' +
//	//    'alignright alignjustify | bullist numlist outdent indent removeformat code',
//	//content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:16px }',
//	//images_upload_base_path: '/some/basepath',
//	//file_picker_callback: RoxyFileBrowser

//}


window.TinyBlzRoxy = {

	height: 550,
	plugins: 'code image link lists table wordcount',
	toolbar: 'undo redo | styles | bold italic underline forecolor backcolor | image link removeformat code | align | bullist numlist | table',
	menubar: false,
	images_upload_base_path: '/some/basepath',
	file_picker_callback: RoxyFileBrowser


	//height: 550,
	//plugins: 'advlist anchor autolink charmap code codesample directionality help image editimage insertdatetime link lists media nonbreaking pagebreak preview searchreplace table template tableofcontents visualblocks visualchars wordcount',
	//toolbar: 'undo redo | blocks | bold italic strikethrough forecolor backcolor blockquote | link image media | alignleft aligncenter alignright alignjustify | numlist bullist outdent indent | removeformat',
	//content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:16px }',
	//images_upload_base_path: '/some/basepath',
	//file_picker_callback: RoxyFileBrowser


	//height: 500,
	//plugins: [
	//    'advlist', 'autolink', 'lists', 'link', 'image', 'charmap', 'preview',
	//    'anchor', 'searchreplace', 'visualblocks', 'code', 'fullscreen',
	//    'insertdatetime', 'media', 'table', 'help', 'wordcount'
	//],
	//toolbar: 'undo redo | blocks | ' +
	//    'bold italic forecolor | alignleft aligncenter ' +
	//    'alignright alignjustify | bullist numlist outdent indent removeformat code',
	//content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:16px }',
	//images_upload_base_path: '/some/basepath',
	//file_picker_callback: RoxyFileBrowser

}

function RoxyFileBrowser(callback, value, meta) {
	//console.log('callback: ', callback + " value: ", value + " type: ", type)

	var roxyFileman = 'lib/fileman/index.html';

	const instanceApi = tinyMCE.activeEditor.windowManager.openUrl({
		title: 'Fileman',
		url: roxyFileman,
		width: 850,
		height: 550,
		onMessage: function (dialogApi, details) {


			console.log("details: ", details);
			console.log("valu: ", value + " | meta: ", meta);
			console.log("dialoagapi stringify: ", dialogApi);

			callback(details.content);

			instanceApi.close();
		}
	});
	console.log("dialoagapi: ", instanceApi, instanceApi[0]);
	return false;
}

// Used for RoxyModal
window.RoxyReturnImage = (elemId) => {

	const element = document.getElementById(elemId);
	var retVal = element.getAttribute("src");

	return retVal;
};



function SwiperBlz() {
	console.log("Swiper Blz");
	const swiper = new Swiper('.swiper', {
		// Optional parameters
		//direction: 'horizontal',

		effect: 'fade',
		fadeEffect: {
			crossFade: true
		},
		loop: true,

		//autoplay: {
		//	delay: 4000
		//},

		// If we need pagination
		pagination: {
			el: '.swiper-pagination',
		},

		// Navigation arrows
		navigation: {
			nextEl: '.swiper-button-next',
			prevEl: '.swiper-button-prev',
		},

		// And if we need scrollbar
		scrollbar: {
			el: '.swiper-scrollbar',
		},
	});
	
}
