using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTOs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace WinForms;

public partial class FormReportePlanes : Form
{
    // Fila tipada para mostrar en la grilla
    private sealed class FilaMateria
    {
        public string Materia { get; init; } = string.Empty;
        public int HsSemanales { get; init; }
        public int HsTotales { get; init; }
    }

    private List<PlanDTO> _planes = new();
    private List<EspecialidadDTO> _especialidades = new();
    private List<FilaMateria> _materiasActuales = new();

    public FormReportePlanes()
    {
        InitializeComponent();

        // Configuración de licencia gratuita para uso educativo / comunidad
        QuestPDF.Settings.License = LicenseType.Community;
    }

    private async void FormReportePlanes_Load(object sender, EventArgs e)
    {
        if (!ApiSession.TienePermiso("Planes", "Consultar"))
        {
            MessageBox.Show("No tiene permiso para consultar Planes de Estudio.",
                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            this.Close();
            return;
        }

        await CargarDatosAsync();
    }

    private async Task CargarDatosAsync()
    {
        try
        {
            var tPlanes = ApiSession.HttpClient.GetFromJsonAsync<List<PlanDTO>>("planes");
            var tEspecialidades = ApiSession.HttpClient.GetFromJsonAsync<List<EspecialidadDTO>>("especialidades");

            await Task.WhenAll(tPlanes, tEspecialidades);

            _planes = await tPlanes ?? new List<PlanDTO>();
            _especialidades = await tEspecialidades ?? new List<EspecialidadDTO>();

            if (_planes.Count == 0)
            {
                MessageBox.Show("No se encontraron planes de estudio cargados en el sistema.",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Enlazar combo de planes
            cmbPlanes.DisplayMember = nameof(PlanDTO.Descripcion);
            cmbPlanes.ValueMember = nameof(PlanDTO.Id);
            cmbPlanes.DataSource = _planes;

            if (_planes.Count > 0)
            {
                cmbPlanes.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar los datos del reporte: {ex.Message}",
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void cmbPlanes_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbPlanes.SelectedItem is not PlanDTO plan) return;

        _materiasActuales = (plan.Materias ?? new List<MateriaDTO>())
            .Select(m => new FilaMateria
            {
                Materia = m.Descripcion,
                HsSemanales = m.HsSemanales,
                HsTotales = m.HsTotales
            })
            .OrderBy(m => m.Materia)
            .ToList();

        dgvMaterias.DataSource = _materiasActuales;

        if (dgvMaterias.Columns[nameof(FilaMateria.Materia)] is { } colMateria)
            colMateria.HeaderText = "Materia";

        if (dgvMaterias.Columns[nameof(FilaMateria.HsSemanales)] is { } colHsSem)
            colHsSem.HeaderText = "Horas Semanales";

        if (dgvMaterias.Columns[nameof(FilaMateria.HsTotales)] is { } colHsTot)
            colHsTot.HeaderText = "Horas Totales";

        // Totales de control al pie
        int totalMaterias = _materiasActuales.Count;
        int totalHsSemanales = _materiasActuales.Sum(m => m.HsSemanales);
        int totalHsTotales = _materiasActuales.Sum(m => m.HsTotales);

        lblTotalMaterias.Text = $"Total Materias: {totalMaterias}";
        lblTotalHsSemanales.Text = $"Horas Semanales: {totalHsSemanales} hs";
        lblTotalHsTotales.Text = $"Carga Horaria Total: {totalHsTotales} hs";
    }

    private void btnExportarPdf_Click(object sender, EventArgs e)
    {
        if (cmbPlanes.SelectedItem is not PlanDTO plan)
        {
            MessageBox.Show("Debe seleccionar un plan de estudio para exportar.",
                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string nombreEspecialidad = _especialidades.FirstOrDefault(esp => esp.Id == plan.IdEspecialidad)?.Descripcion
                                   ?? "Especialidad General";

        using var saveDialog = new SaveFileDialog
        {
            Filter = "Archivo PDF (*.pdf)|*.pdf",
            FileName = $"{plan.Descripcion.Replace(" ", "_")}_PlanDeEstudios.pdf",
            Title = "Guardar Reporte de Plan de Estudio"
        };

        if (saveDialog.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            GenerarPdf(saveDialog.FileName, plan, nombreEspecialidad, _materiasActuales);

            var respuesta = MessageBox.Show(
                "¡Reporte PDF generado exitosamente!\n\n¿Desea abrir el archivo ahora?",
                "Reporte Listo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (respuesta == DialogResult.Yes)
            {
                Process.Start(new ProcessStartInfo(saveDialog.FileName) { UseShellExecute = true });
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ocurrió un error al generar el PDF: {ex.Message}",
                "Error al generar PDF", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static void GenerarPdf(string rutaArchivo, PlanDTO plan, string nombreEspecialidad, List<FilaMateria> materias)
    {
        int totalHsSemanales = materias.Sum(m => m.HsSemanales);
        int totalHsTotales = materias.Sum(m => m.HsTotales);

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Segoe UI"));

                // Encabezado institucional
                page.Header().Column(col =>
                {
                    col.Item().Text("UNIVERSIDAD TECNOLÓGICA NACIONAL").Bold().FontSize(15).FontColor(Colors.Blue.Darken2);
                    col.Item().Text("Facultad Regional Rosario — Sistema de Gestión Académica (SGA)").FontSize(10).FontColor(Colors.Grey.Darken1);
                    col.Item().PaddingTop(6).LineHorizontal(1).LineColor(Colors.Grey.Lighten1);
                });

                // Cuerpo del reporte
                page.Content().PaddingVertical(15).Column(col =>
                {
                    col.Item().Text($"Ficha Oficial: {plan.Descripcion}").Bold().FontSize(14);
                    col.Item().Text($"Especialidad: {nombreEspecialidad}").FontSize(10).FontColor(Colors.Grey.Darken2);
                    col.Item().Text($"Fecha de Emisión: {DateTime.Now:dd/MM/yyyy HH:mm} hs").FontSize(9).FontColor(Colors.Grey.Darken1);
                    col.Item().PaddingTop(12);

                    // Tabla de materias
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3); // Materia
                            columns.RelativeColumn(1); // Hs Semanales
                            columns.RelativeColumn(1); // Hs Totales
                        });

                        table.Header(header =>
                        {
                            header.Cell().Background(Colors.Blue.Lighten4).Padding(6).Text("Materia").Bold();
                            header.Cell().Background(Colors.Blue.Lighten4).Padding(6).AlignRight().Text("Hs. Semanales").Bold();
                            header.Cell().Background(Colors.Blue.Lighten4).Padding(6).AlignRight().Text("Hs. Totales").Bold();
                        });

                        foreach (var m in materias)
                        {
                            table.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(6).Text(m.Materia);
                            table.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(6).AlignRight().Text($"{m.HsSemanales} hs");
                            table.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(6).AlignRight().Text($"{m.HsTotales} hs");
                        }
                    });

                    // Cuadro resumen de totales al pie
                    col.Item().PaddingTop(15).Background(Colors.Grey.Lighten4).Padding(10).Row(row =>
                    {
                        row.RelativeItem().Text($"Total Materias: {materias.Count}").Bold();
                        row.RelativeItem().Text($"Horas Semanales: {totalHsSemanales} hs").Bold();
                        row.RelativeItem().Text($"Carga Horaria Carrera: {totalHsTotales} hs").Bold();
                    });
                });

                // Pie de página con numeración
                page.Footer().AlignCenter().Text(text =>
                {
                    text.Span("Página ");
                    text.CurrentPageNumber();
                    text.Span(" de ");
                    text.TotalPages();
                });
            });
        })
        .GeneratePdf(rutaArchivo);
    }
}
