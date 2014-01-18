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




        // autofill form
        $(document).keydown(function (e) {
            if (e.keyCode == 65 && e.ctrlKey) {
                alert("ctrl A");
                // TODO:  autofill somme order information
                /*
                appraiserId:4
                Appraisal.AppraiserId:4
                Appraisal.ClientPerson.CompanyName:Ryan's Loan service
                Appraisal.ClientPerson.FirstName:Ryan
                Appraisal.ClientPerson.LastName:Loaner
                Appraisal.ClientPerson.Phone:(801) 555-1234
                Appraisal.ClientPerson.Email:ryanlifferth@gmail.com
                Appraisal.ClientAddress.Address1:123 Company Street
                Appraisal.ClientAddress.Address2:
                Appraisal.ClientAddress.City:SLC
                Appraisal.ClientAddress.StateCode:UT
                Appraisal.ClientAddress.PostalCode:84121
                Appraisal.OccupantPerson.FirstName:Aimee
                Appraisal.OccupantPerson.LastName:Occupant
                Appraisal.OccupantPerson.Phone:(801) 555-4545
                Appraisal.OccupantPerson.Email:aimee@occupant.com
                Appraisal.PropertyTypeCode:SFR
                Appraisal.PropertyAddress.Address1:123 Property Place
                Appraisal.PropertyAddress.Address2:
                Appraisal.PropertyAddress.City:Layton
                Appraisal.PropertyAddress.StateCode:UT
                Appraisal.PropertyAddress.PostalCode:84111
                Appraisal.ContactForAccess:false
                Appraisal.SalesContractPrice:355000
                Appraisal.LegalDescription:Legal description sample text.
                Appraisal.AppraisalPurposeCode:SC
                Appraisal.Comments:Please contact us at your earliest convenience.
                */
            }
        });
});
