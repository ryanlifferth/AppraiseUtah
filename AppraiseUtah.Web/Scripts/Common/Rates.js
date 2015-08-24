
$(document).ready(
    function () {

        //http://www.zillow.com/webservice/GetRateSummary.htm?zws-id=X1-ZWz1bazgl9xxjf_1acr8&state=UT&output=json

        // Get the rates from Zillow.com service
        /*$.getJSON( "http://www.zillow.com/webservice/GetRateSummary.htm?jsoncallback=?",
                   { "state": "UT", "output": "json", "zws-id": "X1-ZWz1bazgl9xxjf_1acr8" },
                   function( data ) {
                       alert(json.response.today);
                   });
        */

        $.getJSON("api/rates/UT")
            .done(function (data) {
                $("#RatePanel #Loader").hide();
                $("#RatePanel .rate-container").show();
                $("#RatePanel .powered-by-zillow").show();

                //alert(data.Today.ThirtyYearFixed);
                $("#Thirty .rate-title").html("30 year");
                $("#Thirty .rate").html(parseFloat(Math.round(data.Today.ThirtyYearFixed * 100) / 100).toFixed(2) + '%');
                $("#Fifteen .rate-title").html("15 year");
                $("#Fifteen .rate").html(parseFloat(Math.round(data.Today.FifteenYearFixed * 100) / 100).toFixed(2) + '%');
                $("#FiveOne .rate-title").html("5/1 Arm");
                $("#FiveOne .rate").html(parseFloat(Math.round(data.Today.FiveOneArm * 100) / 100).toFixed(2) + '%');
            });

});