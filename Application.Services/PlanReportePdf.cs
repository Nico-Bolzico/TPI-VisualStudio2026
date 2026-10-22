using Domain.Model;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Application.Services
{
    public static class PlanReportePdf
    {
        static PlanReportePdf()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public static byte[] Generar(Plan plan, string nombreEspecialidad)
        {
            var materias = plan.Materias
                .OrderBy(m => m.Descripcion)
                .ToList();

            int totalHsSemanales = materias.Sum(m => m.HsSemanales);
            int totalHsTotales = materias.Sum(m => m.HsTotales);

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Segoe UI"));

                    page.Header().Column(col =>
                    {
                        col.Item().Text("UNIVERSIDAD TECNOLÓGICA NACIONAL").Bold().FontSize(15).FontColor(Colors.Blue.Darken2);
                        col.Item().Text("Facultad Regional Rosario — Sistema de Gestión Académica (SGA)").FontSize(10).FontColor(Colors.Grey.Darken1);
                        col.Item().PaddingTop(6).LineHorizontal(1).LineColor(Colors.Grey.Lighten1);
                    });

                    page.Content().PaddingVertical(15).Column(col =>
                    {
                        col.Item().Text($"Ficha Oficial: {plan.Descripcion}").Bold().FontSize(14);
                        col.Item().Text($"Especialidad: {nombreEspecialidad}").FontSize(10).FontColor(Colors.Grey.Darken2);
                        col.Item().Text($"Fecha de Emisión: {DateTime.Now:dd/MM/yyyy HH:mm} hs").FontSize(9).FontColor(Colors.Grey.Darken1);
                        col.Item().PaddingTop(12);

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Background(Colors.Blue.Lighten4).Padding(6).Text("Materia").Bold();
                                header.Cell().Background(Colors.Blue.Lighten4).Padding(6).AlignRight().Text("Hs. Semanales").Bold();
                                header.Cell().Background(Colors.Blue.Lighten4).Padding(6).AlignRight().Text("Hs. Totales").Bold();
                            });

                            foreach (var m in materias)
                            {
                                table.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(6).Text(m.Descripcion);
                                table.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(6).AlignRight().Text($"{m.HsSemanales} hs");
                                table.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(6).AlignRight().Text($"{m.HsTotales} hs");
                            }
                        });

                        col.Item().PaddingTop(15).Background(Colors.Grey.Lighten4).Padding(10).Row(row =>
                        {
                            row.RelativeItem().Text($"Total Materias: {materias.Count}").Bold();
                            row.RelativeItem().Text($"Horas Semanales: {totalHsSemanales} hs").Bold();
                            row.RelativeItem().Text($"Carga Horaria Carrera: {totalHsTotales} hs").Bold();
                        });
                    });

                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.Span("Página ");
                        text.CurrentPageNumber();
                        text.Span(" de ");
                        text.TotalPages();
                    });
                });
            }).GeneratePdf();
        }
    }
}
