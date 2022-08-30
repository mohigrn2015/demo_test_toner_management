
//function GetColorMachinebyproject(event) {
//    var projectid = event.target.value;
//    var date = $('#submitDate').val();
//    $.ajax({
//        url: '/MachineData/LoadCLMachine',
//        type: 'GET',
//        dataType: 'json',
//        data: {
//            id: projectid,
//            date: date
//        },
//        success: function (machines) {
//            console.log(machines);
//            $('#colormachinedata').empty();
//            $('#colortonerData').empty();
//            var data = '';
//            var tonerIdList = [];
         
//            var tonerData = '';
//            var currentStock = 0;
//            var currentPercentage = 0;
//            var totalToner = 0;

//            machines.forEach(function (item) {
//   var tonerdetails = '';
//                if (item.colorType) {
//                    data =
//                        `<tr class="flex align-items-center">
//	                                    <td><input name='MachineIDs' type="hidden" value="${item.machineId}" /> ${item.machineId}</td>
//                                        <td>${item.machineSN}</td>             
//                                        <td>${item.machineModel}</td>
//                                        <td>${item.tonerConfigs[0]?.toner?.tonarModel}</td>
//	                                    <td><input name="PreviousUsges" readonly="" class="form-control" value="${item.currentUses}" id="previousUseages${item.machineId}"></td>
//	                                    <td><input name="CurrentUses" onclick="changeCurrentuseage(${item.machineId})" id="currentUseges${item.machineId}" class="form-control currentUse" value=""></td>
//	                                    <td><input name="TotalUsges" readonly="" class="form-control totalUseges" id="totalUseges${item.machineId}" value=""></td>
//	                                    <td class="d-flex justify-content-center align-items-center" id="tonerdetails${item.machineId}">
//                                        ${tonerdetails}
//	                                    </td>
//                                    </tr>`
//                    $("#colormachinedata").append(data);
//                    item.tonerConfigs.forEach(function (i) {
//                        tonerdetails += `<div class="text-center">
//			                                    <label style="background:#ababab; color: black; padding: 8px;">${i.toner.tonarModel}</label><br>
//			                                    <input name="TonerUsges" class="form-control px-0 total${i.tonarID}" onclick="changetoneruseage(${i.tonarID})">
//                                            </div>`
//                        tonerIdList.push(i);
                      
//                    })
//   $(`#tonerdetails${item.machineId}`).empty();
//  $(`#tonerdetails${item.machineId}`).append(tonerdetails);
               
              

//                    /*$(`#tonerdetails${item.machineId}`).append(tonerdetails);*/


//                }

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
//            console.log(err)
//        }

//    })
//}



function changetoneruseage(id) {
    var pre = $('#current' + id).val();
    if (pre == undefined || pre == '' || pre == 'NaN') {
        pre = 0;
    }
    $('.total' + id).keyup(function (event) {
        $('#current' + id).val(parseInt(pre) + parseInt(event.target.value))
    })

}


$('#colormachinedata').on('input', '.currentUse', function (event) {
    var total = 0;
    $('.currentUse').each(function () {
        total += parseInt(this.value, 10) || 0;
    });
    $('#totalCounter').val(total);
})

function hittotalfun() {
    var total = 0;
    $('.totalUseges').each(function () {
        total += parseInt(this.value, 10) || 0;
    });
    $('.totaluse').val(total);

}

