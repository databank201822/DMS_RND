function convert_data_tabel() {
    var table = $("table").DataTable();
        table.destroy();
        table = $("table").DataTable({
            aLengthMenu: [
                [10, 25, 50, 100, 200, -1],
                [10, 25, 50, 100, 200, "All"]
            ],
            iDisplayLength: 50,
            "bSort": false,
            "bProcessing": true,
            dom:
                "<'row'<'col-md-12'>><'row'><'row'<'col-md-6'l><'col-md-6'f>><'row'<'col-md-12'<'pull-right 'B>>r>t<'row'<'col-md-6'i><'col-md-6 pull-right'p>>",
            buttons: [
                {
                    extend: 'pdfHtml5',
                    customize: function(doc) {
                        doc.content.splice(0,
                            0,
                            {
                                text: ''
                            });
                    }
                },
                'excelHtml5',
                'copyHtml5',
                'csvHtml5'
            ]


        });
    

    table.buttons().container().appendTo('#Exportbtn').addClass('pull-right');
}


function customize_data_Table(reportname,date)
{
    var table = $("table").DataTable();
    table.destroy();

    $('table').append('<caption style="caption-side: top">Transcom Beverages Ltd <br\>' + reportname + '<br/> Date:' + date + '</caption>');
    table = $("table").DataTable({
        aLengthMenu: [
            [10, 25, 50, 100, 200, -1],
            [10, 25, 50, 100, 200, "All"]
        ],
        iDisplayLength: 50,
        "bSort": false,
        "bProcessing": true,
        dom:
            "<'row'<'col-md-12'>><'row'><'row'<'col-md-6'l><'col-md-6'f>><'row'<'col-md-12'<'pull-right 'B>>r>t<'row'<'col-md-6'i><'col-md-6 pull-right'p>>",
        buttons: [
            {
                extend: 'pdfHtml5',
                customize: function (doc) {
                    doc.content.splice(0,
                        0,
                        {
                            text: ''
                        });
                }
            },
            {
                extend: 'excelHtml5',
                messageTop: reportname + '\n Date:' + date
               
            },
            {
                extend: 'csvHtml5',
                messageTop: reportname + '\n Date:' + date
            },
            'copyHtml5'
            
        ]


    });


    table.buttons().container().appendTo('#Exportbtn').addClass('pull-right');
}


