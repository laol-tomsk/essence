const wpSelector = $('#work_product_criterion_definition_level_of_details_definition_id');
wpSelector.parent().hide();
let levelOfDetails = wpSelector.html();

function workProductSelected() {
   const workProduct = $('#work_product_definition_id :selected').text();
   const options = $(levelOfDetails).filter("optgroup[label='" + workProduct +"']").html();
   const levelOfDetailsSelect = $('#work_product_criterion_definition_level_of_details_definition_id');
   if (options) {
      levelOfDetailsSelect.html(options);
      levelOfDetailsSelect.parent().show();
   } else {
      levelOfDetailsSelect.empty();
      levelOfDetailsSelect.parent().hide();
   }
}

window.addEventListener('DOMContentLoaded', () => {
   $('#work_product_definition_id').change(workProductSelected);
});