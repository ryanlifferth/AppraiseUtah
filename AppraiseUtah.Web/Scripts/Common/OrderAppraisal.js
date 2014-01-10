// Add validation check to hidden fields with the .validate class (since jQuery validator does not validate hidden fields)
$.validator.setDefaults({ ignore: ':hidden:not(".validate")' });        // $.validator.setDefaults({ ignore: $(":hidden").not('.validate') }); also works

// Add method to check both first and last name
/*$.validator.addMethod("bothNames", function (value, element, params) {
    var firstName = $('input[id="' + params[0] + '"]').val(),
        lastName  = $('input[id="' + params[1] + '"]').val();
    if (firstName === "" && lastName === "") {
        return false;
    }
    else {
        return true;
    }
}, "Both First and Last Name are required.");*/


$(document).ready(
    function () {

        // Bind to the change event (which is triggered in Appraisers.js) and revalidate the hidden.validate fields since jQuery validator does not automatically validate hidden fields.
        $('.validate').on("change", function () {
            $("#OrderAppraisal").validate().element(".validate");
        });        

        // Add validation rules for the order form
        $("#Appraisal_ClientPerson_FirstName").rules("add", { required: true, messages: { required: "First Name is required." } });
        $("#Appraisal_ClientPerson_LastName").rules("add", { required: true, messages: { required: "Last Name is required." } });
        //$("#ClientPersonFullName").rules("add", { bothNames: ["Appraisal_ClientPerson_FirstName", "Appraisal_ClientPerson_LastName", "ClientPersonFullName"] });
        $("#Appraisal_ClientPerson_Phone").rules("add", { required: true, messages: { required: "Phone Number is required." } });
        $("#Appraisal_ClientPerson_Email").rules("add", { required: true, messages: { required: "Email is required." } });
        $("#Appraisal_ClientPerson_Email").rules("add", { email: true });
        $("#Appraisal_PropertyAddress_Address1").rules("add", { required: true, messages: { required: "Address is required." } });
        $("#Appraisal_PropertyAddress_City").rules("add", { required: true, messages: { required: "City is required." } });

        // Add phone mask
        $(".phone-number").mask("(999) 999-9999");

        
        $("#CancelForm").on("click", function (e) {
            $("span.field-validation-error").html("");
            $("span.field-validation-valid").html("");
            $(".blank-first-child").trigger("change");   // triggers change to update display color
        });

        // Formatting/display to show "placeholder" coloring for DropDownBoxes with an empty first option
        $(".blank-first-child").on("change", function () {
            if ($("option:selected", this).index() === 0) {
                $(this).addClass("select-placeholder");
            }
            else {
                $(this).removeClass("select-placeholder");
            }            
        });

});
