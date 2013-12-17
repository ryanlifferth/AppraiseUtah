$(document).ready(
    function () {

        
        // TODO:  Use knockoutJS
        $("#appraiserList > .dropdown-menu > a").on('click', function () {
            var content = $(this).html() + '&nbsp;<span class="caret"></span>';

            $(this).parents("#appraiserList").children(".dropdown-selected").html(content);
        });




});
