$(document).ready(
    function () {

        
        // TODO:  Use knockoutJS
        $("#appraiserList > .dropdown-menu > a").on('click', function () {
            var appraiserName = $(this).children(".appraiser-name").html();
            var appraiserId = $(this).children(".appraiser-id").html();

            $("#appraiserList").children(".dropdown-menu").children("a").removeClass("active");
            $(this).parents("#appraiserList").children(".dropdown-selected").children(".selected-appraiser").html(appraiserName);
            $(this).addClass("active");
            $(this).parents("#appraiserList").children("input[name='appraiserId']").val(appraiserId).trigger('change');
            $("input[data-item='AppraiserId']").val(appraiserId).trigger('change');

            //return false;
        });

});
