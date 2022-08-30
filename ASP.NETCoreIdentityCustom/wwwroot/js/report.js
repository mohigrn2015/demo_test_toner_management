﻿$('#btnPrint').click(function () {

    makePdf();


});

window.html2canvas = html2canvas;
window.jsPDF = window.jspdf.jsPDF;
function makePdf() {
    $('#footer').css("display", "block");
    var form = $('#PrintTable');


    html2canvas(form[0], {
        allowTent: true,
        useCORS: true,
        scale: 0.5
    }).then((canvas) => {
        var img = canvas.toDataURL('image/PNG');
        var doc = new jsPDF({
            orientation: 'p',
            unit: 'in',
            format: [6.50, 17.2],
            putOnlyUsedFonts: true
        });
        doc.setFont('Arial');
        doc.getFontSize(13);
        doc.addImage(img, 'PNG', 0, 0);
        doc.save('toner.pdf');
        margin: { horizontal: 50 };


    });
}


