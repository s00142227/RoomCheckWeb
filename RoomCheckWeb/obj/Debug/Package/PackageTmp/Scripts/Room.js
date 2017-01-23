﻿//todo: before all of the jqgrid perform function to get all status ids and descriptions in a json string
$(function () {
        $('#grid')
            .jqGrid({
                url: "/Home/GetRooms",
                datatype: 'json',
                mtype: 'Get',
                colNames: ['ID', 'RoomNo', 'RoomType', 'OccupiedStatus'],
                colModel: [
                    { key: true, hidden: true, name: 'ID', index: 'ID' },
                    { key: false, name: 'RoomNo', index: 'RoomNo' },
                    {
                        key: false,
                        name: 'RoomTypeID',
                        //todo: display description instead of id
                        index: 'RoomTypeID',
                        editable: true,
                        edittype: 'select',
                        editoptions: { value: { '1': 'Stay', '2': 'Departure', '3': 'Empty' } }
                    },
                    {
                        key: false,
                        name: 'OccupiedStatusID',
                        index: 'OccupiedStatusID',
                        editable: true,
                        edittype: 'select',
                        editoptions: {
                            value: {
                                '1': 'Unknown',
                                '2': 'Unoccupied',
                                '3': 'Occupied',
                                '3': 'Checked Out'
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
                afterComplete: function(response) {
                    if (response.responseText) {
                        alert(response.responseText);
                    }
                }
            },
            {
                //add options
                zIndex: 100,
                url: 'Home/Create',
                closeOnEscape: true,
                closeAfterEdit: true,
                recreateForm: true,
                afterComplete: function(response) {
                    if (response.responseText) {
                        alert(response.responseText);
                    }
                }
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

    //function getOccupiedStatus() {
    //    $.getJSON("yourUrl", null, function (data) {
    //        if (data != null) {
    //            //construct string.  
    //            //(or the server could return a string directly)
    //        }
    //    });
    //}


});