
$(function () {
    window.Dashboard = {
        url_get_data_dashboard: "/home/getDashboard",
        color_1: [
            '#4988C2',
            '#EC252A',
            '#B2EBF2',
            '#80DEEA',
            '#4DD0E1',
            '#26C6DA',
            '#00BCD4',
            '#00ACC1',
            '#0097A7',
            '#00838F',
            '#006064'
        ],
        color_2: [
            '#B2EBF2',
            '#80DEEA',
            '#4DD0E1',
            '#26C6DA',
            '#00BCD4',
            '#00ACC1',
            '#0097A7',
            '#00838F',
            '#006064'
        ],
        init: function () {
            //Dashboard.ActivityChart();
            Dashboard.BuildDataDashBoard();
        },
        BuildDataDashBoard: function () {
            $.ajax({
                type: 'get',
                url: Dashboard.url_get_data_dashboard,
                success: function (rp) {
                    if (rp.status) {
                        // Build data
                        Dashboard.BuildDataStats(rp.data.stats);
                        Dashboard.BuildPieChar('device_pie_donut', rp.data.device);
                        Dashboard.BuildPieChar('package_pie_donut', rp.data.package, Dashboard.color_2);
                        Dashboard.BuildPieChar('contract_pie_donut', rp.data.contract);
                    } else {

                    }
                },

            });
        },
        BuildDataStats: function (data) {
            var html = '';
            if (data[0].Total_Contract > 0) 
            {
                $('#h3_contract').html(data[0].Total_Contract);
            }

            if (data[0].Total_Device > 0) 
            {
                $('#h3_Device').html(data[0].Total_Device);
            }

            if (data[0].Total_Package > 0) 
            {
                $('#h3_package').html(data[0].Total_Package);
            }
        },
        BuildPieChar: function (element, data, colors) {
            if (colors === undefined) {
                colors = Dashboard.color_1;
            }

            var _data_seria = [];
            for (var i = 0; i < data.length; i++) {
                _data_seria.push({ label: data[i].name, value: data[i].value});
            }

            Morris.Donut({
                element: element,
                data: _data_seria,
                colors: colors,
            });

        },
        ActivityChart: function () {
            Morris.Line({
                element: "weekly-activity-chart",
                data: [{
                    day: Date.parse("2022-12-05"),
                    Running: 100,
                    Walking: 40,
                    Cycling: 62,
                    anhph: 280,
                },
                {
                    day: Date.parse("2022-12-06"),
                    Running: 150,
                    Walking: 200,
                    Cycling: 120,
                    anhph: 210,
                },
                {
                    day: Date.parse("2022-12-07"),
                    Running: 200,
                    Walking: 105,
                    Cycling: 70,
                    anhph: 290,
                },
                {
                    day: Date.parse("2022-12-08"),
                    Running: 125,
                    Walking: 150,
                    Cycling: 75,
                    anhph: 270,
                },
                {
                    day: Date.parse("2022-12-09"),
                    Running: 150,
                    Walking: 275,
                    Cycling: 100,
                    anhph: 280,
                },
                {
                    day: Date.parse("2022-12-10"),
                    Running: 200,
                    Walking: 325,
                    Cycling: 80,
                    anhph: 210,
                },
                {
                    day: Date.parse("2022-12-11"),
                    Running: 260,
                    Walking: 130,
                    Cycling: 90,
                    anhph: 200,
                }
                ],
                xkey: "day",
                xLabels: ["day"],
                ykeys: ["Running", "Walking", "Cycling", "anhph"],
                labels: ["Running Label", "Walking Label", "Cycling Label", "anhph"],
                resize: true,
                smooth: true,
                pointSize: 3,
                pointStrokeColors: ["#FF7588", "#16D39A", "#FFA87D", "#0FA87B"],
                gridLineColor: "#e3e3e3",
                behaveLikeLine: true,
                numLines: 6,
                gridtextSize: 14,
                lineWidth: 3,
                hideHover: "auto",
                lineColors: ["#FF7588", "#16D39A", "#FFA87D", "#0FA87B"],
                xLabelFormat: function (x) {
                    var day = x.getDay();
                    var days = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
                    return days[day];
                }
            });

            //Morris.Area({
            //    element: "weekly-activity-chart",
            //    data: [
            //        {
            //            year: "2016-12-01",
            //            AvgSessionDuration: 0,
            //            PagesSession: 0
            //        },
            //        {
            //            year: "2016-12-02",
            //            AvgSessionDuration: 150,
            //            PagesSession: 90
            //        },
            //        {
            //            year: "2016-12-03",
            //            AvgSessionDuration: 140,
            //            PagesSession: 120
            //        },
            //        {
            //            year: "2016-12-04",
            //            AvgSessionDuration: 105,
            //            PagesSession: 240
            //        },
            //        {
            //            year: "2016-12-05",
            //            AvgSessionDuration: 190,
            //            PagesSession: 140
            //        },
            //        {
            //            year: "2016-12-06",
            //            AvgSessionDuration: 230,
            //            PagesSession: 250
            //        },
            //        {
            //            year: "2016-12-07",
            //            AvgSessionDuration: 270,
            //            PagesSession: 190
            //        }
            //    ],
            //    xkey: "year",
            //    ykeys: ["AvgSessionDuration", "PagesSession"],
            //    labels: ["Avg. Session Duration", "Pages/Session"],
            //    behaveLikeLine: true,
            //    ymax: 300,
            //    resize: true,
            //    pointSize: 0,
            //    pointStrokeColors: ["#BABFC7", "#16D39A"],
            //    smooth: false,
            //    gridLineColor: "#e3e3e3",
            //    numLines: 6,
            //    gridtextSize: 14,
            //    lineWidth: 0,
            //    fillOpacity: 0.8,
            //    hideHover: "auto",
            //    lineColors: ["#BABFC7", "#16D39A"]
            //});

        }
    }
});
$(document).ready(function () {
    Dashboard.init();
});
