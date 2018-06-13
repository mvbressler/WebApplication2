$(document).ready(function () {

    //$("#checkAll").click(function () {
    //    $('table input:checkbox.checkBox').each(function () {
    //        $(".checkBox").prop('checked', $(this).prop('checked'));
    //    });
        
    //});

    $("#checkAll").click(function () {
        $('table tbody input:checkbox').each(function (i, item) {
            $(item).prop('checked', $("#checkAll").prop('checked'));
        });

    });

    $("#delete_all").click(function () {
        var selectedIDs = new Array();
        //$('table input:checkbox.checkBox').each(function () {
        //    if ($(this).prop('checked')) {
        //        selectedIDs.push($(this).val());
        //    }
        //});
        $('table tbody input:checked').each(function () {            
              selectedIDs.push($(this).val());           
        });

        var options = {};
        options.url = "/Blogs/MultiDelete";
        options.type = "POST";
        options.data = { selectedIDs: JSON.stringify(selectedIDs) };
        //options.data = { selectedIDs: selectedIDs };
        //options.contentType = "application/json";
        //options.dataType = "json";
        options.success = function (msg) {
            alert(msg);
        };
        options.error = function () {
            alert("Error while deleting the records!");
        };
        $.ajax(options);

    });
});