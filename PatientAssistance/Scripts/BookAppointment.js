jQuery("#LevelOfPain").hide();
jQuery("#HospitalName").hide();
jQuery("#AveProcessTime").hide();
jQuery("#PatientCount").hide();
jQuery("#WaitingTime").hide();
jQuery("#painlevel").hide();
jQuery("#painLevelTitle").hide();

jQuery("#hospital").hide();
jQuery("#hospitalTitle").hide();

jQuery("#illness").click(function (){
    jQuery("#painlevel").show();
    jQuery("#painLevelTitle").show();

});

jQuery("#painlevel").click(function () {
    jQuery("#hospital").show();
    jQuery("#hospitalTitle").show();
    //TODO: Dynamic Setting
    jQuery("#LevelOfPain").val(4);
});
jQuery("#painlevel").click(function () {
    jQuery("#hospital").show();
    jQuery("#hospitalTitle").show();
    //TODO: Dynamic Setting
    jQuery("#HospitalName").val("Hospital 1");
    jQuery("#AveProcessTime").val(2);
    jQuery("#PatientCount").val(5);
    jQuery("#WaitingTime").val(10);

});
