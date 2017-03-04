var rowsToColor = [];
$(function () {
        $('#grid')
            .jqGrid({
                url: "/Home/GetRooms",
                datatype: 'json',
                mtype: 'Get',
                editurl: 'Edit',
                gridComplete: function () {
                    for (var i = 0; i < rowsToColor.length; i++) {

                        //code for introducing a 'Gone to Breakfast' button
                       // var cl = rowsToColor[i];
                       // be = "<input style='height:22px;width:100px;' class='btn-default small' type='button' value='Breakfast' onclick=BreakfastClick(" + rowsToColor[i] + "); ></ids>";
                       //jQuery("#grid").setRowData(rowsToColor[i], { Button: be });

                        var status = $("#" + rowsToColor[i]).find("td").eq(4).html();
                        if (status == "Cleaned" || status == 3) {
                            $("#" + rowsToColor[i]).find("td").css("background-color", "#ccff99");
                            $("#" + rowsToColor[i]).find("td").eq(4).html('Cleaned');
                        }
                        else if (status == "InProgress" || status == 2) {
                            $("#" + rowsToColor[i]).find("td").css("background-color", "#cce6ff");
                            $("#" + rowsToColor[i]).find("td").eq(4).html('InProgress');
                        } else {
                            $("#" + rowsToColor[i]).find("td").eq(4).html('Uncleaned');
                        }
                    }
                },
                ondblClickRow: function(){
                    var row_id = $("#grid").getGridParam('selrow');
                    jQuery('#grid').editRow(row_id, true);
                },
                colNames: ['ID', 'Room No', 'Room Type', 'Occupied Status', 'Cleaning Status', 'Note', 'Event', 'Cleaner'],
                colModel: [
                    
                    { key: true, hidden: true, name: 'ID', index: 'ID' },
                    { key: false, name: 'RoomNo', index: 'RoomNo' },
                    //{ key: false, name: 'Button', index: 'Button' },
                    {
                        key: false,
                        name: 'RoomTypeID',
                        //todo: display description instead of id
                        index: 'RoomTypeID',
                        editable: true,
                        formatter: 'select',
                        edittype: 'select',
                        editoptions: { value: {  1: 'Stay', 2: 'Departure', 3: 'Empty' } }
                    },
                    {
                        key: false,
                        name: 'OccupiedStatusID',
                        index: 'OccupiedStatusID',
                        editable: true,
                        edittype: 'select',
                        formatter: 'select',
                        editoptions: {
                            value: {
                                1: 'Unknown',
                                2: 'Unoccupied',
                                3: 'Occupied',
                                4: 'Checked Out'
                            }
                        }
                    },
                    {
                        key: false,
                        name: 'CleanStatusID',
                        index: 'CleanStatusID',
                        editable: false,
                        edittype: 'select',
                        formatter: rowColorFormatter,
                        editoptions: {
                            value: {
                                1: 'Uncleaned',
                                2: 'InProgress',
                                3: 'Cleaned'
                            }
                        }
                    },
                    {
                        key: false,
                        name: 'Note',
                        index: 'Note',
                        editable: true,
                        edittype: 'text',
                        editoptions: {
                            size: '10'
                        }
                    },
                     {
                         key: false,
                         name: 'CurrentEvent',
                         index: 'CurrentEvent'                         
                     },
                     {
                         key: false,
                         name: 'UserID',
                         index: 'UserID',
                         editable: false,
                         edittype: 'select',
                         formatter: 'select',
                         editoptions: {
                             value: {
                                 6: 'Katie'  
                             }
                         }
                     }

                ],
                pager: jQuery('#pager'),
                rowNum: 10,
                rowList: [10, 20, 30, 40],
                height: '100%',
                viewrecords: true,
                caption: 'Room',
                emptyrecords: 'No Rooms to Display',
                jsonReader: {
                    root: "rows",
                    page: "page",
                    total: "total",
                    records: "records",
                    repeatitems: false,
                    id: "0"
                },
                autowidth: true,
                multiselect: false


            })
            .navGrid('#pager',
            { edit: true, add: true, del: true, search: false, refresh: true },
            {
                //edit options
                zIndex: 100,
                url: 'Home/Edit',
                closeOnEscape: true,
                closeAfterEdit: true,
                recreateForm: true,
                //afterComplete: function(response) {
                //    if (response.responseText) {
                //        alert(response.responseText);
                //    }
                //}
            },
            {
                //add options
                zIndex: 100,
                url: 'Home/Create',
                closeOnEscape: true,
                closeAfterEdit: true,
                recreateForm: true,
                //afterComplete: function(response) {
                //    if (response.responseText) {
                //        alert(response.responseText);
                //    }
                //}
            },
            {
                //delete options
                zIndex: 100,
                url: 'Home/Delete',
                closeOnEscape: true,
                closeAfterEdit: true,
                recreateForm: true,
                msg: "Are you sure you want to delete this room?",
                afterComplete: function(response) {
                    if (response.responseText) {
                        alert(response.responseText);
                    }
                }
            });

        


});

function rowColorFormatter(cellValue, options, rowObject) {
    
        rowsToColor[rowsToColor.length] = options.rowId;
    return cellValue;
}

//function BreakfastClick(row) {
//    alert('hi');
//}