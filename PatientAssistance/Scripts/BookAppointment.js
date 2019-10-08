
function populateLevelOfPain() {
    var root = jQuery("#painlevel")
    var lev1 = jQuery("<div id='" + 1 + "'></div> ");
    lev1.data("severity", 1);
    lev1.addClass("pain_lev1");
    lev1.addClass("pain_lev");
     root.append(lev1);
    var lev2 = jQuery("<div id='" + 2 + "'></div> ");
    lev2.data("severity", 2);
    lev2.addClass("pain_lev2");
    lev2.addClass("pain_lev");
    root.append(lev2);
    var lev3 = jQuery("<div id='" + 3 + "'></div> ");
    lev3.data("severity", 3);
    lev3.addClass("pain_lev3");
    lev3.addClass("pain_lev");
    root.append(lev3);
    var lev4 = jQuery("<div id='" + 4 + "'></div> ");
    lev4.data("severity", 4);
    lev4.addClass("pain_lev4");
    lev4.addClass("pain_lev");
    root.append(lev4);
    var lev5 = jQuery("<div id='" + 5 + "'></div> ");
    lev5.data("severity", 5);
    lev5.addClass("pain_lev5");
    lev5.addClass("pain_lev");
    root.append(lev5);

}

function ComputeTimeInHours(minutes) {

    var time = "";
    if (minutes < 60) {
        time = minutes + " min(s)";
    }
    else {
        var hrs = Math.floor(minutes / 60);
        var min = "";
        if (minutes % 60 >= 30) {
            min = ".5"
 
        }
        // TODO: calculate decimal hours
        time = hrs + min + " hr(s)";
    }

    return time;
}

function GetHospitalList() {
    var hospitals = jQuery("#hospitalData").text();
    var hospitalList = JSON.parse(hospitals);

    return hospitalList;
}

function GetHospitalWithTotalTime() {
    var hospitalList = GetHospitalList();
    var levelOfPain = jQuery("#LevelOfPain").val();

    for (var item in hospitalList)
    {
        var hospital = hospitalList[item];
        for (var waitlist in hospital.waitingList)
        {
            var waitingListDetails = hospital.waitingList[waitlist];
            if (waitingListDetails.levelOfPain == levelOfPain -1) {
                //Compute Waiting Time according to levelOfPain
                hospitalList[item].totalWaitingTime = waitingListDetails.patientCount * waitingListDetails.averageProcessTime;
                hospitalList[item].totalWaitingTimeInHrs = ComputeTimeInHours(hospitalList[item].totalWaitingTime);
                break;
            }
        }
    }

    return hospitalList;

}

function GetSortedHospitals() {
    var hospitalList = GetHospitalWithTotalTime();
    var sortedList = hospitalList.sort((a, b) => (a.totalWaitingTime > b.totalWaitingTime) ? 1 : -1);
    return sortedList;

}

function populateHospital() {

    var root = jQuery("#hospital")
    var hospitals = GetSortedHospitals();
    var levelOfPain = jQuery("#LevelOfPain").val();
    for (var item in hospitals) {
        var hospitalDetails = hospitals[item];
        //var hosp = jQuery("<div id='" + 1 + "'>" + hospitalDetails.name + "    " + hospitalDetails.totalWaitingTimeInHrs + "</div> ");
        var hosp = jQuery("<div> <table> <tr> <th style='width:70%'>" + hospitalDetails.name + "</th> <th class='waiting-time'> Waiting Time: " + hospitalDetails.totalWaitingTimeInHrs + "</th> </tr> </table> </div>");
        hosp.data("name", hospitalDetails.name);

        for (var waitlist in hospitalDetails.waitingList) {
            var waitingListDetails = hospitalDetails.waitingList[waitlist];
            if (waitingListDetails.levelOfPain == levelOfPain - 1) {
                //Compute Waiting Time according to levelOfPain
                hosp.data("aveProcessTime", waitingListDetails.averageProcessTime);
                hosp.data("patientCount", waitingListDetails.patientCount);
                hosp.data("totalWaitingTime", waitingListDetails.patientCount * waitingListDetails.averageProcessTime);
                break;
            }
        }

        hosp.addClass("hospitals");

        root.append(hosp);
    }

  
}

function setFieldsFromHospitals() {

    jQuery("#hospital").find(".hospitals").click(function () {
        var hospitalName = jQuery(this).data().name;
        jQuery("#HospitalName").val(hospitalName);
        var aveProcessTime = jQuery(this).data().aveProcessTime;
        jQuery("#AveProcessTime").val(aveProcessTime);
        var patientCount = jQuery(this).data().patientCount;
        jQuery("#PatientCount").val(patientCount);
        var totalWaitingTime = jQuery(this).data().totalWaitingTime;
        jQuery("#WaitingTime").val(totalWaitingTime);
        jQuery("#hospital").find(".selected").removeClass("selected");
        jQuery(this).addClass("selected");
        jQuery("#btnSubmit").attr("disabled", false);
        jQuery("#btnSubmit").addClass("btn-enabled");

 
    });
}

jQuery("#painlevel").hide();
jQuery("#painLevelTitle").hide();

jQuery("#hospital").hide();
jQuery("#hospitalTitle").hide();
jQuery("#btnSubmit").attr("disabled", true);

jQuery("#illness").find(".illnessclass").click(function () {
    var illness = jQuery(this).text();
    illness = illness.trim();
    jQuery("#painLevelTitle").text("Select severity level for " + illness + ":");
    jQuery("#illness").find(".selected").removeClass("selected");
    jQuery(this).addClass("selected");
});

jQuery("#illness").click(function (){
    jQuery("#painlevel").show();
    jQuery("#painLevelTitle").show();

    if (jQuery("#painlevel").children().length < 1) {
        populateLevelOfPain();
        jQuery("#painlevel").find(".pain_lev").click(function () {
            var severity = jQuery(this).data().severity;
            jQuery("#LevelOfPain").val(severity);
            jQuery("#painlevel").find(".selected").removeClass("selected");

            jQuery(this).addClass("selected");

            jQuery("#hospital").show();
            jQuery("#hospitalTitle").show();

            jQuery("#hospital").find(".hospitals").remove();
            populateHospital();
            setFieldsFromHospitals();

            

 
        });

    }
    
 
});
