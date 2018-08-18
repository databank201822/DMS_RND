﻿function Get_Zonefilter() {

        $.ajax({
            url: "/DynamicFilter/Zone_filter",
            dataType: "HTML",
            async: "true",
            cache: "false",
            success: function (data) {
                $("#zone_filter").html(data);
                $("select").multiselect().multiselectfilter();
               
            }
        });
   
}

function Get_PSRfilter() {

    $.ajax({
        url: "/DynamicFilter/PSR_filter",
        dataType: "HTML",
        async: "true",
        cache: "false",
        success: function (data) {
            $("#zone_filter").html(data);
            $("select").multiselect().multiselectfilter();

        }
    });

}

function Get_ASM_byid() {
    
    var rsmIds = $('#RSM_ids').val();
   
    $('#ASM_ids').html("");
    $('#ASM_ids').multiselect("refresh");
   
    //alert(RSM_ids);
    if (rsmIds.length!==0) {
        $.ajax({
            type: "POST",
            url: "/DynamicFilter/Getzoneidsbyparentid",
            data: { ids: rsmIds },
            dataType: "JSON",
            async: "true",
            cache: "false",
            success: function (data) {
                var options = '';
              
                for (var i = 0; i < data.length; i++) {
                    options += '<option value="' + data[i].id + '">' + data[i].biz_zone_name + '</option>';
                   
                }
                $('#ASM_ids').html(options);
                $('#ASM_ids').multiselect("refresh");
              
            }
        });

    }
    Get_CE_byid();
}
function Get_CE_byid() {

    var asmIds = $('#ASM_ids').val();
   
    $('#CE_ids').html("");
    $('#CE_ids').multiselect("refresh");
  

    //alert(ASM_ids);
    if (asmIds.length !== 0) {
        $.ajax({
            type: "POST",
            url: "/DynamicFilter/Getzoneidsbyparentid",
            data: { ids: asmIds },
            dataType: "JSON",
            async: "true",
            cache: "false",
            success: function (data) {
                var options = '';

                for (var i = 0; i < data.length; i++) {
                    options += '<option value="' + data[i].id + '">' + data[i].biz_zone_name + '</option>';
                }
                $('#CE_ids').html(options);
                $('#CE_ids').multiselect("refresh");
                
            }
        });

    }
    Get_db_byid();
}
function Get_db_byid() {
    
    var ceIds = $('#CE_ids').val();
  
    $("#DB_ids").html("");
    $('#DB_ids').multiselect("refresh");
    //alert(ASM_ids);
    if (ceIds.length !== 0) {
        $.ajax({
            type: "POST",
            url: "/DynamicFilter/Getdbidsbyzoneid",
            data: { ids: ceIds },
            dataType: "JSON",
            async: "true",
            cache: "false",
            success: function (data) {
                var options = '';
                for (var i = 0; i < data.length; i++) {
                    options += '<option value="' + data[i].DB_Id + '">' + data[i].DB_name + '</option>';

                }
                $("#DB_ids").html(options);
                $("#DB_ids").multiselect("refresh");
            }
        });

    }
    Get_psr_byid();
}
function Get_psr_byid() {

    var dbIds = $('#DB_ids').val();

    $("#PSR_ids").html("");
    $('#PSR_ids').multiselect("refresh");
    //alert(ASM_ids);
    if (dbIds.length !== 0) {
        $.ajax({
            type: "POST",
            url: "/DynamicFilter/Getpsridsbydbidid",
            data: { ids: dbIds },
            dataType: "JSON",
            async: "true",
            cache: "false",
            success: function (data) {
                var options = '';
                for (var i = 0; i < data.length; i++) {
                    options += '<option value="' + data[i].psr_id + '">' + data[i].psr_name + '</option>';

                }
                $("#PSR_ids").html(options);
                $("#PSR_ids").multiselect("refresh");
            }
        });

    }
}


function Get_Skufilter() {

    $.ajax({
        url: "/DynamicFilter/Sku_filter",
        dataType: "HTML",
        async: "true",
        cache: "false",
        success: function (data) {
            $('#sku_filter').html(data);
            $('select').multiselect().multiselectfilter();
        }
    });
}

function Get_Daterange() {

    $.ajax({
        url: "/DynamicFilter/DateRange_filter",
        dataType: "HTML",
        async: "true",
        cache: "false",
        success: function (data) {
            $('#daterange_filter').html(data);
           
        }
    });
}