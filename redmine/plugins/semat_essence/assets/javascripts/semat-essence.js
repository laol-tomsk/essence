$(document).ready(() => {
   $('[se-link]').click(function(e) {
      window.location.href = $(this).attr('se-link');
   });
});