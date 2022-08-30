
document.addEventListener("DOMContentLoaded", function (event) {
    $('#colorTonar').hide();
    $('#bcTonar').hide();
    $('#cons').select2({
        placeholder: 'Select'
    });
});


function GetProjectbycustomer(event) {
    var customerid = event.target.value;
    $.ajax({
        type: 'GET',
        url: '/Machines/LoadProject',
        dataType: 'json',
        data: { id: customerid },
        success: function (projects) {
            $("#projectId").empty();
            $("#projectId").append(`<option value="0">Select Project</option>`);
            var data = "";
            projects.forEach(function (item) {
                data = `<option value="${item.projectId}">${item.projectName}</option>`
                $("#projectId").append(data);
            })
        }
    })
}

function GetProjectbyproject(event) {
    // alert(event.target.value)
    $('.color-list').css("display", "none");
    $('.white_list').css("display", "none");
    var projectid = event.target.value;
    var date = $('#submitDate').val();
    $.ajax({
        url: '/MachineData/LoadMachine',
        type: 'GET',
        dataType: 'json',
        data: {
            id: projectid,
            date: date
        },
        success: function (machines) {
            console.log(machines);
            //$('#machinedata').empty();
            //$('#tonerData').empty();
            //var data = '';
            //var tonerIdList = [];
            //var tonerData = '';
            //var currentUse = 0;
            //var currentStock = 0;
            //var currentPercentage = 0;
            //var totalUse = 0;
            //var totalToner = 0;
            //machines.forEach(function (item) {
            //    if (item.colorType == false) {
            //        data = `<tr>
            //                <td><input name='MachineId' type="hidden" value="${item.machineId}" /> ${item.machineId}</td>
            //                <td>${item.machineSN}</td>             
            //                <td>${item.machineModel}</td>
            //                <td>${item.tonerConfigs[0]?.toner?.tonarModel}</td>
            //                <td><input name='PreviousUses' readonly class="form-control" value="${item.currentUses}" id="previousUseages${item.machineId}" /></td>
            //                <td><input name='CurrentUses' onclick="changeCurrentuseage(${item.machineId})" id="currentUseges${item.machineId}" class="form-control currentUse" value="${currentUse}" /></td>
            //                <td><input name='TotalCounter' readonly class="form-control totalUsegess" id="totalUseges${item.machineId}" value="${totalUse}"  /></td>
            //                <td><input name='TonerUsges' onclick="changetoneruseage(${item.tonerConfigs[0]?.tonarID})" id="total${item.tonerConfigs[0]?.tonarID}" class="form-control total${item.tonerConfigs[0]?.tonarID}" /></td>
            //            </tr>`
            //        $("#machinedata").append(data);
            //        item.tonerConfigs.forEach(function (i) {
            //            tonerIdList.push(i);
            //        })
            //    }
            //});

            //var result = tonerIdList.reduce(function (memo, e1) {
            //    var matches = memo.filter(function (e2) {
            //        return e1.tonarID == e2.tonarID
            //    })
            //    if (matches.length == 0)
            //        memo.push(e1)
            //    return memo;
            //}, []);
            //result.forEach(function (r) {
            //    tonerData = `<tr>
            //                <td>${r.toner.tonarModel}</td>
            //                <td><input name='TotalUsges' onclick=changetotaltoner(${r.tonarID}) class="form-control"" id="currentStocks${r.tonarID}" value="${currentStock}" /></td>
            //                <td><input name='TotalUsges' readonly class="form-control"" id="current${r.tonarID}" value="${currentPercentage}" /></td>
            //                <td><input name='TotalUsges' class="form-control"" id="totalTonners${r.tonarID}" value="${totalToner}" /></td>
            //                </tr>`
            //    $("#tonerData").append(tonerData);
            //    console.log(r);
            //})
            if (machines[0].length > 0) {
                $('.white_list').css("display", "block");
                $('#machinedata').empty();
                $('#tonerData').empty();
                var data = '';
                var tonerIdList = [];
                var tonerData = '';
                var currentUse = 0;
                var currentStock = 0;
                var currentPercentage = 0;
                var totalUse = 0;
                var totalToner = 0;
                machines[0].forEach(function (item) {
                    if (item.colorType == false) {
                        data = `<tr>
                            <td><input name='MachineId' type="hidden" value="${item.machineId}" /> ${item.machineId}</td>
                            <td>${item.machineSN}</td>             
                            <td>${item.machineModel}</td>
                            <td>${item.tonerConfigs[0]?.toner?.tonarModel}</td>
                            <td><input name='PreviousUses' readonly class="form-control" value="${item.currentUses}" id="previousUseages${item.machineId}" /></td>
                            <td><input name='CurrentUses' onclick="changeCurrentuseage(${item.machineId})" id="currentUseges${item.machineId}" class="form-control currentUse" value="${currentUse}" /></td>
                            <td><input name='TotalCounter' readonly class="form-control totalUsegess" id="totalUseges${item.machineId}" value="${totalUse}"  /></td>
                            <td><input name='TotalPercentage' onclick="changetoneruseage(${item.tonerConfigs[0]?.tonarID})" id="total${item.tonerConfigs[0]?.tonarID}" class="form-control total${item.tonerConfigs[0]?.tonarID}" /></td>
                        </tr>`
                        $("#machinedata").append(data);
                        item.tonerConfigs.forEach(function (i) {
                            tonerIdList.push(i);
                        })
                    }
                });

                var result = tonerIdList.reduce(function (memo, e1) {
                    var matches = memo.filter(function (e2) {
                        return e1.tonarID == e2.tonarID
                    })
                    if (matches.length == 0)
                        memo.push(e1)
                    return memo;
                }, []);
                result.forEach(function (r) {
                    tonerData = `<tr>
                            <td>${r.toner.tonarModel}</td>
                            <td><input name='CurrentStock' onclick=changetotaltoner(${r.tonarID}) class="form-control"" id="currentStocks${r.tonarID}" value="${currentStock}" /></td>
                            <td><input name='CurrentPercentage' readonly class="form-control"" id="current${r.tonarID}" value="${currentPercentage}" /></td>
                            <td><input name='TotalToner' class="form-control"" id="totalTonners${r.tonarID}" value="${totalToner}" /></td>
                            </tr>`
                    $("#tonerData").append(tonerData);
                    console.log(r);
                })

            }
            /*  Direction by Rakib*/
            if (machines[1].length > 0) {
                $('.color-list').css("display", "block");

                $('#colormachinedata').empty();
                $('#colortonerData').empty();
                var data = '';
                var tonerIdList = [];

                var tonerData = '';
                var currentStock = 0;
                var currentPercentage = 0;
                var totalToner = 0;

                machines[1].forEach(function (item) {
                    var tonerdetails = '';
                    if (item.colorType) {
                        data =
                            `<tr class="flex align-items-center">
	                                    <td><input name='MachineIDs' type="hidden" value="${item.machineId}" /> ${item.machineId}</td>
                                        <td>${item.machineSN}</td>             
                                        <td>${item.machineModel}</td>
                                        <td>${item.tonerConfigs[0]?.toner?.tonarModel}</td>
	                                    <td><input name="PreviousesUses" readonly="" class="form-control" value="${item.currentUses}" id="PreviousesUses${item.machineId}"></td>
	                                    <td><input name="CurrentsUses" onclick="changecolorCurrentuseage(${item.machineId})" id="CurrentsUses${item.machineId}" class="form-control currentUse"></td>
	                                    <td><input name="TotalsCounter" readonly="" class="form-control Totalsuses" id="Totalsuses${item.machineId}" value=""></td>
	                                    <td class="d-flex justify-content-center align-items-center" id="tonerdetails${item.machineId}">
                                        ${tonerdetails}
	                                    </td>
                                    </tr>`
                        $("#colormachinedata").append(data);
                        item.tonerConfigs.forEach(function (i) {
                            tonerdetails += `<div class="text-center">
			                                    <label style="background:#ababab; color: black; padding: 8px;">${i.toner.tonarModel}</label><br>
			                                    <input name="TotalsPercentage" class="form-control px-0 total${i.tonarID}" onclick="changecolortoneruseage(${i.tonarID})">
                                            </div>`
                            tonerIdList.push(i);

                        })
                        $(`#tonerdetails${item.machineId}`).empty();
                        $(`#tonerdetails${item.machineId}`).append(tonerdetails);



                        /*$(`#tonerdetails${item.machineId}`).append(tonerdetails);*/



                    }

                });

                var result = tonerIdList.reduce(function (memo, e1) {
                    var matches = memo.filter(function (e2) {
                        return e1.tonarID == e2.tonarID
                    })
                    if (matches.length == 0)
                        memo.push(e1)
                    return memo;
                }, []);
                result.forEach(function (r) {
                    tonerData = `<tr>
                            <td>${r.toner.tonarModel}</td>                           
                            <td><input name='CurrentsStock' onclick=changecolortotaltoner(${r.tonarID}) class="form-control"" id="currentsStocks${r.tonarID}" value="${currentStock}" /></td>
                            <td><input name='CurrentsPercentage' readonly class="form-control"" id="currents${r.tonarID}" value="${currentPercentage}" /></td>
                            <td><input name='TotalsToner' class="form-control"" id="totalsTonners${r.tonarID}" value="${totalToner}" /></td>
                            </tr>`
                    $("#colortonerData").append(tonerData);
                    console.log(r);
                })





            }
            /*  End direction*/
        }, error: function (err) {
            console.log(err)
        }

    })
}
function Counttotal() {
    var countercount;
    var totalCouterUse = 0;
    $('td:nth-child(3)').each(function () {
        countercount = $(this).html();
        totalCouterUse += parseInt(countercount);
        $('#totalCouterUse').text(totalCouterUse);

    });
};
function changeCurrentuseage(id) {
    var previousvalue = $('#previousUseages' + id).val();

    $('#currentUseges' + id).keyup(function (event) {
        if (event.target.value < previousvalue) {
            $('#totalUseges' + id).val(event.target.value - previousvalue)
        } else {
            $('#totalUseges' + id).val(event.target.value - previousvalue)
        }
    })

}


function changecolorCurrentuseage(id) {
    var previousvalue = $('#PreviousesUses' + id).val();

    $('#CurrentsUses' + id).keyup(function (event) {
        if (event.target.value < previousvalue) {
            $('#Totalsuses' + id).val(event.target.value - previousvalue)
        } else {
            $('#Totalsuses' + id).val(event.target.value - previousvalue)
        }
    })

}

function changetotaltoner(id) {
    var currentPercentage = $('#current' + id).val();
    $('#currentStocks' + id).keyup(function (event) {
        $('#currentStocks' + id).css("background-color", "white");
        $('#totalTonners' + id).val(+event.target.value + +currentPercentage)
    })

}
function changecolortotaltoner(id) {
    var currentPercentage = $('#currents' + id).val();
    $('#currentsStocks' + id).keyup(function (event) {
        $('#currentsStocks' + id).css("background-color", "white");
        $('#totalsTonners' + id).val(+event.target.value + +currentPercentage)
    })

}


function GetMechineDetails(event) {
    $.ajax({
        type: 'GET',
        url: '/Machines/GetMachineById',
        dataType: 'json',
        data: { id: event.target.value },
        success: function (machine) {
            if (machine.colorType == false) {
                $('#colorTonar').hide();
                $('#bcTonar').show();
                $('#colorType').val('Black & White Printer');
                $('#colorType').css("background-color", "#c1bcbc");
                $('#colorType').css("color", "black");
            } else {
                $('#colorTonar').show();
                $('#bcTonar').hide();
                $('#colorType').val('Color Printer');
                $('#colorType').css("background-color", "#ef4646");
                $('#colorType').css("color", "white");

            }
        }
    })

}
//function GetCLProjectbyproject(event) {

//    var projectid = event.target.value;
//    $.ajax({
//        url: '/ColorToner/LoadMachine',
//        type: 'GET',
//        dataType: 'json',
//        data: { id: projectid },
//        success: function (machines) {
//            console.log(machines);
//            $('#colormachinedata').empty();
//            $('#colortonerData').empty();
//            var data = '';
//            var tonerIdList = [];
//            var tonerData = '';
//            var previousUse = 11;
//            var currentUse = 30;
//            var currentStock = 0;
//            var currentPercentage = 0;
//            var totalUse = 0;
//            var totalToner = 0;
//            machines.forEach(function (item) {
//                data = `<tr>
//                            <td>${item.machineId}</td>
//                            <td>${item.machineSN}</td>             
//                            <td>${item.machineModel}</td>
//                            <td>${item.tonerConfigs[0]?.toner?.tonarModel}</td>
//                            <td><input name='PreviousUsges' readonly class="form-control" value="${previousUse}" id="previousUseages${item.machineId}" /></td>
//                            <td><input name='CurrentUses' onclick="changeCurrentuseage(${item.machineId})" id="currentUseges${item.machineId}" class="form-control" value="${currentUse}" /></td>
//                            <td><input name='TotalUsges' readonly class="form-control"" id="totalUseges${item.machineId}" value="${totalUse}" /></td> 
//                        </tr>`

//                $("#colormachinedata").append(data);
//                item.tonerConfigs.forEach(function (i) {
//                    tonerIdList.push(i);
//                })
//            });



//            var result = tonerIdList.reduce(function (memo, e1) {
//                var matches = memo.filter(function (e2) {
//                    return e1.tonarID == e2.tonarID
//                })
//                if (matches.length == 0)
//                    memo.push(e1)
//                return memo;
//            }, []);
//            result.forEach(function (r) {
//                tonerData = `<tr>
//                            <td>${r.toner.tonarModel}</td>                           
//                            <td><input name='TotalUsges' onclick=changetotaltoner(${r.tonarID}) class="form-control"" id="currentStocks${r.tonarID}" value="${currentStock}" /></td>
//                            <td><input name='TotalUsges' readonly class="form-control"" id="current${r.tonarID}" value="${currentPercentage}" /></td>
//                            <td><input name='TotalUsges' class="form-control"" id="totalTonners${r.tonarID}" value="${totalToner}" /></td>
//                            </tr>`
//                $("#colortonerData").append(tonerData);
//                console.log(r);
//            })

//        }, error: function (err) {
//            /*console.log(err)*/
//        }

//    })
//}
function GetSelectedLimit(event) {
    var limit = 4;
    if ($("input[name='TonarIDs']:checked").length >= limit) {
        $("input[name='TonarIDs']").not(":checked").attr("disabled", true);
    } else {
        $("input[name='TonarIDs']").not(":checked").removeAttr('disabled');
    }
}

function changetoneruseage(id) {
    var pre = $('#current' + id).val();
    if (pre == undefined || pre == '' || pre == 'NaN') {
        pre = 0;
    }
    $('.total' + id).keyup(function (event) {
        $('#current' + id).val(parseFloat(pre) + parseFloat(event.target.value))
    })

}
function changecolortoneruseage(id) {
    var pre = $('#currents' + id).val();
    if (pre == undefined || pre == '' || pre == 'NaN') {
        pre = 0;
    }
    $('.total' + id).keyup(function (event) {
        $('#currents' + id).val(parseFloat(pre) + parseFloat(event.target.value))
    })

}



$('#machinedata').on('input', '.currentUse', function (event) {
    var total = 0;
    $('.currentUse').each(function () {
        total += parseInt(this.value, 10) || 0;
    });
    $('.totalAmount').val(total);
    $('#totalUsegess')
})

function hitfun() {
    var total = 0;
    $('.totalUsegess').each(function () {
        total += parseInt(this.value, 10) || 0;
    });
    $('.totaluse').val(total);

}
function hittotalfun() {
    var total = 0;
    $('.Totalsuses').each(function () {
        total += parseInt(this.value, 10) || 0;
    });
    $('.totalcoloruse').val(total);

}
//for color model
