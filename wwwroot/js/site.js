// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



$(document).ready(() => {
    var mes = "";
    var year = "";
    var comercio = "";
    var suma = 0.00;

    $('#Mes').change(function ()
    {
        mes = $(this).val();
        console.log(mes);
    });

    $('#sumatoria').text("0.00");

    $('#Año').change(function ()
    {
        year = $(this).val();
        console.log(year);
    });
    $('#Comercio').change(function ()
    {
        comercio = $(this).val();
        console.log(comercio);
    });




    $('#ConsultaVentas').click(function ()
    {
        $('#TablaVentas tbody').empty();
        mes = $('#Mes').val();
        year = $('#Año').val();
        comercio = $('#Comercio').val();
        $('#sumatoria').text("0.00");
        console.log(mes);
        console.log(year);
        console.log(comercio);
        suma = 0;
        $.ajax({
            type: "POST",
            url: "/Facturas/Ventas",
            data: { Mes: mes, Año: year, Comercio: comercio },
            //contentType: "application/json charset=utf-8",
            success: function (data) {

                for (let i = 0; i < data.length; i++)
                {

                    $('#TablaDatosVentas').append(GeneraRowVentas(data[i].idFactura, data[i].nombreFactura, data[i].tipoVenta, data[i].monto, data[i].fecha));
                    suma = suma + parseFloat(data[i].monto);
                    console.log(suma); 
                    $('#sumatoria').text("Monto Total : ¢ " + suma.toString());
                }
               
                console.log(data);

            },
            error: function (req, status, error) {
                console.log(error);
                $('#sumatoria').text("0.00");
            }
        });

    });

    function GeneraRowVentas(idfactura, nombre, tipoventa, monto, fecha)
    {
        var data = "";
        data = data + "<tr>"
        data = data + "<td  class='text-center'>" + idfactura + "</td>";
        data = data + "<td>" + nombre + "</td>";
        data = data + "<td  class='text-center'>" + tipoventa + "</td>";
        data = data + "<td  class='text-center'>" + "¢ " + monto + "</td>";
        data = data + "<td  class='text-center'>" + fecha + "</td>";
        data = data + "</tr>";
        return data;
    }
});