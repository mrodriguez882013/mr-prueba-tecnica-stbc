using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Api.Dtos.Ventas.Requests;
using PruebaTecnica.Api.Dtos.Ventas.Responses;
using PruebaTecnica.Api.Services;
using PruebaTecnica.Api.Services.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PruebaTecnica.Api.Controllers
{
    [ApiController]
    [Route("api/ventas")]
    [Produces("application/json")]
    [Tags("3. Administración de Ventas y Transacciones")]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    public class VentasController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentasController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }


        /// <summary>Registra una nueva venta de forma transaccional usando TVP y actualiza stock.</summary>
        /// <param name="request">Estructura con los datos del vendedor y el detalle de los productos.</param>
        /// <param name="cancellationToken">Token de cancelación de la solicitud.</param>
        [HttpPost]
        [ProducesResponseType(typeof(VentaResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegistrarVenta(
            [FromBody] VentaCreateRequest request,
            CancellationToken cancellationToken)
        {
            var ventaResponse = await _ventaService.RegistrarVentaAsync(request, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, ventaResponse);
        }

        /// <summary>Obtiene un reporte detallado de las ventas realizadas filtradas por rango de fechas.</summary>
        /// <param name="fechaInicio">Fecha de inicio opcional (formato YYYY-MM-DD).</param>
        /// <param name="fechaFin">Fecha de fin opcional (formato YYYY-MM-DD).</param>
        /// <param name="cancellationToken">Token de cancelación de la solicitud.</param>
        [HttpGet("reporte")]
        [ProducesResponseType(typeof(IEnumerable<VentaReporteResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObtenerReporte(
            [FromQuery] DateTime? fechaInicio,
            [FromQuery] DateTime? fechaFin,
            CancellationToken cancellationToken)
        {
            var reporte = await _ventaService.ObtenerReporteVentasAsync(fechaInicio, fechaFin, cancellationToken);
            return Ok(reporte);
        }


        [HttpGet("exportar-excel")]
        public async Task<IActionResult> ExportarExcel(DateTime? fechaInicio, DateTime? fechaFin)
        {
            // 1. Obtener los datos del reporte (consumiendo el servicio o la API)
            var listaReporte = await _ventaService.ObtenerReporteVentasAsync(fechaInicio, fechaFin);

            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add("Reporte de Ventas");

            // Título del reporte
            worksheet.Cell("A1").Value = "Reporte Corporativo de Ventas - BAC Credomatic";
            worksheet.Cell("A1").Style.Font.Bold = true;
            worksheet.Cell("A1").Style.Font.FontSize = 14;

            // Encabezados de la tabla
            worksheet.Cell("A3").Value = "ID Venta";
            worksheet.Cell("B3").Value = "Fecha";
            worksheet.Cell("C3").Value = "Vendedor";
            worksheet.Cell("D3").Value = "Código Producto";
            worksheet.Cell("E3").Value = "Producto";
            worksheet.Cell("F3").Value = "Cantidad";
            worksheet.Cell("G3").Value = "Precio Unitario";
            worksheet.Cell("H3").Value = "IVA";
            worksheet.Cell("I3").Value = "Total Detalle";

            // Estilo corporativo para los encabezados (Gris claro / Borde limpio)
            var headerRow = worksheet.Row(3);
            headerRow.Style.Font.Bold = true;
            headerRow.Style.Fill.BackgroundColor = XLColor.LightGray;

            // Rellenar datos
            int row = 4;
            foreach (var item in listaReporte)
            {
                worksheet.Cell(row, 1).Value = item.IdVenta;
                worksheet.Cell(row, 2).Value = item.Fecha.ToString("yyyy-MM-dd");
                worksheet.Cell(row, 3).Value = item.Vendedor;
                worksheet.Cell(row, 4).Value = item.CodigoProducto;
                worksheet.Cell(row, 5).Value = item.NombreProducto;
                worksheet.Cell(row, 6).Value = item.Cantidad;
                worksheet.Cell(row, 7).Value = item.PrecioUnitario;
                worksheet.Cell(row, 8).Value = item.Iva;
                worksheet.Cell(row, 9).Value = item.TotalDetalle;
                row++;
            }

            // Autoajustar columnas para una vista limpia
            worksheet.Columns().AdjustToContents();

            // Convertir a un flujo de memoria para descargarlo como archivo
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"ReporteVentas_{DateTime.Now:yyyyMMdd}.xlsx");
        }


        [HttpGet("exportar-pdf")]
        public async Task<IActionResult> ExportarPdf(DateTime? fechaInicio, DateTime? fechaFin)
        {
            // Configurar la licencia de QuestPDF (Comunitaria gratuita)
            QuestPDF.Settings.License = LicenseType.Community;

            var listaReporte = await _ventaService.ObtenerReporteVentasAsync(fechaInicio, fechaFin);

            // Definición del documento PDF
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.Letter);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    // Encabezado del PDF
                    page.Header().Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Text("BAC CREDOMATIC").Bold().FontSize(18).FontColor("#b30000");
                            col.Item().Text("Reporte General de Ventas").FontSize(12).FontColor(Colors.Grey.Medium);
                        });
                        row.ConstantItem(100).AlignRight().Text($"Fecha: {DateTime.Now:dd/MM/yyyy}").FontSize(9);
                    });

                    // Contenido / Tabla principal
                    page.Content().PaddingVertical(10).Column(col =>
                    {
                        col.Item().Table(table =>
                        {
                            // Definir columnas de la tabla
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(40);  // ID
                                columns.RelativeColumn(2);   // Vendedor
                                columns.RelativeColumn(3);   // Producto
                                columns.ConstantColumn(50);  // Cantidad
                                columns.ConstantColumn(60);  // Total
                            });

                            // Cabecera de tabla
                            table.Header(header =>
                            {
                                header.Cell().Background("#dcdcdc").Padding(5).Text("ID").Bold();
                                header.Cell().Background("#dcdcdc").Padding(5).Text("Vendedor").Bold();
                                header.Cell().Background("#dcdcdc").Padding(5).Text("Producto").Bold();
                                header.Cell().Background("#dcdcdc").Padding(5).Text("Cant.").Bold();
                                header.Cell().Background("#dcdcdc").Padding(5).Text("Total").Bold();
                            });

                            // Filas de datos
                            foreach (var item in listaReporte)
                            {
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(item.IdVenta.ToString());
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(item.Vendedor);
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(item.NombreProducto);
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(item.Cantidad.ToString());
                                table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5).Text($"${item.TotalDetalle:0.00}");
                            }
                        });
                    });

                    // Pie de página
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Página ");
                        x.CurrentPageNumber();
                        x.Span(" de ");
                        x.TotalPages();
                    });
                });
            });

            // Generar bytes del PDF
            byte[] pdfBytes = document.GeneratePdf();
            return File(pdfBytes, "application/pdf", $"ReporteVentas_{DateTime.Now:yyyyMMdd}.pdf");
        }
    }
}