using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Net.Http.Json;
using DTOs;

namespace WinForms
{
    public partial class GraficoInscriptosContraCupo : Form
    {
        // Una fila del reporte: sirve tanto para la grilla como para el gráfico.
        private sealed class FilaReporte
        {
            public string Materia { get; init; } = string.Empty;
            public string Comision { get; init; } = string.Empty;
            public int Anio { get; init; }
            public int Cupo { get; init; }
            public int Inscriptos { get; init; }
            public int Disponibles { get; init; }
            public string Ocupacion { get; init; } = string.Empty;
        }
        private List<FilaReporte> _filasGrafico = new();
        private readonly Dictionary<string, string> _nombresPorEtiqueta = new();

        public GraficoInscriptosContraCupo()
        {
            InitializeComponent();
            Text = "Reporte de Cursos";
            ConfigurarGrilla();
            chart.GetToolTipText += chart_GetToolTipText;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {
                await CargarReporteAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el reporte: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrilla()
        {
            DgvReporte.ReadOnly = true;
            DgvReporte.AllowUserToAddRows = false;
            DgvReporte.AllowUserToDeleteRows = false;
            DgvReporte.RowHeadersVisible = false;
            DgvReporte.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DgvReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private static async Task<List<T>?> ObtenerListaAsync<T>(string url)
        {
            try
            {
                return await ApiSession.HttpClient.GetFromJsonAsync<List<T>>(url);
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        private async Task CargarReporteAsync()
        {
            var tCursos = ObtenerListaAsync<CursoDTO>("cursos");
            var tInscripciones = ObtenerListaAsync<AlumnoInscripcionDTO>("inscripciones");
            var tMaterias = ObtenerListaAsync<MateriaDTO>("materias");
            var tComisiones = ObtenerListaAsync<ComisionDTO>("comisiones");

            await Task.WhenAll(tCursos, tInscripciones, tMaterias, tComisiones);

            var cursos = await tCursos;
            var inscripciones = await tInscripciones;

            if (cursos is null || inscripciones is null)
            {
                MessageBox.Show("Para ver este reporte necesita permiso de consulta sobre Cursos e Inscripciones.",
                    "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Materias y comisiones solo ponen nombres; si faltan, se muestra "N° X".
            var materias = (await tMaterias) ?? new List<MateriaDTO>();
            var comisiones = (await tComisiones) ?? new List<ComisionDTO>();

            if (IsDisposed)
                return;

            var inscriptosPorCurso = inscripciones
                .GroupBy(i => i.IdCurso)
                .ToDictionary(g => g.Key, g => g.Count());

            var filas = cursos.Select(c =>
                {
                    int inscriptos = inscriptosPorCurso.TryGetValue(c.Id, out var n) ? n : 0;

                    return new FilaReporte
                    {
                        Materia = materias.FirstOrDefault(m => m.Id == c.IdMateria)?.Descripcion
                                  ?? $"Materia N° {c.IdMateria}",
                        Comision = comisiones.FirstOrDefault(x => x.Id == c.IdComision)?.Descripcion
                                   ?? $"Comisión N° {c.IdComision}",
                        Anio = c.AnioCalendario,
                        Cupo = c.Cupo,
                        Inscriptos = inscriptos,
                        Disponibles = c.Cupo - inscriptos,
                        Ocupacion = c.Cupo > 0
                            ? $"{(int)Math.Round(100.0 * inscriptos / c.Cupo)} %"
                            : "-"
                    };
                })
                .OrderBy(f => f.Materia).ThenBy(f => f.Comision)
                .ToList();

            CargarGrilla(filas);
            ArmarGrafico(filas);
        }

        private void CargarGrilla(List<FilaReporte> filas)
        {
            DgvReporte.DataSource = filas;

            Encabezado(nameof(FilaReporte.Comision), "Comisión");
            Encabezado(nameof(FilaReporte.Anio), "Año");
            Encabezado(nameof(FilaReporte.Ocupacion), "Ocupación");
        }

        private void Encabezado(string columna, string texto)
        {
            if (DgvReporte.Columns[columna] is { } col)
                col.HeaderText = texto;
        }

        private void ArmarGrafico(List<FilaReporte> filas)
        {
            chart.Series.Clear();
            chart.ChartAreas.Clear();
            chart.Legends.Clear();
            chart.Titles.Clear();
            _filasGrafico = filas;
            _nombresPorEtiqueta.Clear();

            chart.BackColor = Color.White;
            chart.AntiAliasing = AntiAliasingStyles.All;
            chart.TextAntiAliasingQuality = TextAntiAliasingQuality.High;

            chart.Titles.Add(new Title(
                $"Cupo vs. inscriptos por curso ({filas.Count} cursos)",
                Docking.Top, new Font("Segoe UI", 12F, FontStyle.Bold), Color.FromArgb(40, 40, 40)));

            if (filas.Count == 0)
                return;

            int max = filas.Max(f => Math.Max(f.Cupo, f.Inscriptos));

            var area = new ChartArea("principal") { BackColor = Color.White };

            area.AxisX.Interval = 1;                       // una etiqueta por curso
            area.AxisX.MajorGrid.Enabled = false;

            area.AxisY.Minimum = 0;
            area.AxisY.Interval = Math.Max(1, (int)Math.Ceiling(max / 8.0));   // marcas enteras
            area.AxisY.LabelStyle.Format = "0";
            area.AxisY.MajorGrid.LineColor = Color.Gainsboro;
            area.AxisY.Title = "Alumnos";
            chart.ChartAreas.Add(area);

            var cupo = new Series("Cupo")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.FromArgb(190, 200, 215),
                IsValueShownAsLabel = true
            };
            cupo["PointWidth"] = "0.7";

            var inscriptos = new Series("Inscriptos")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.SteelBlue,
                IsValueShownAsLabel = true
            };
            inscriptos["PointWidth"] = "0.7";

            for (int i = 0; i < filas.Count; i++)
            {
                var f = filas[i];
                string etiqueta = $"{Truncar(f.Materia, 24)}\n{f.Comision} · {f.Anio}";
                _nombresPorEtiqueta.TryAdd(Clave(etiqueta), f.Materia);

                //así cada curso ocupa su propio lugar en el eje.
                int pc = cupo.Points.AddXY(i + 1, f.Cupo);
                cupo.Points[pc].AxisLabel = etiqueta;

                int pi = inscriptos.Points.AddXY(i + 1, f.Inscriptos);
                inscriptos.Points[pi].AxisLabel = etiqueta;

                // Cupo agotado: la barra de inscriptos se pinta de rojo
                if (f.Cupo > 0 && f.Inscriptos >= f.Cupo)
                    inscriptos.Points[pi].Color = Color.Firebrick;
            }

            chart.Series.Add(cupo);
            chart.Series.Add(inscriptos);

            chart.Legends.Add(new Legend("leyenda")
            {
                Docking = Docking.Bottom,
                Alignment = StringAlignment.Center
            });
        }

        private static string Truncar(string texto, int maximo) =>
            texto.Length <= maximo ? texto : texto[..(maximo - 1)] + "…";
        private static string Clave(string etiqueta) =>
            etiqueta.Replace("\r", "").Replace("\n", " ");

        private void chart_GetToolTipText(object? sender, ToolTipEventArgs e)
        {
            var hit = e.HitTestResult;

            if (hit.ChartElementType == ChartElementType.DataPoint
                && hit.PointIndex >= 0 && hit.PointIndex < _filasGrafico.Count)
            {
                var f = _filasGrafico[hit.PointIndex];
                e.Text = $"{f.Materia}\nComisión {f.Comision} · {f.Anio}\n" +
                         $"Cupo: {f.Cupo}   Inscriptos: {f.Inscriptos}   Ocupación: {f.Ocupacion}";
            }

            else if (hit.ChartElementType == ChartElementType.AxisLabels
                     && hit.Object is CustomLabel etiqueta
                     && _nombresPorEtiqueta.TryGetValue(Clave(etiqueta.Text), out var nombre))
            {
                e.Text = nombre;
            }
        }
    }
}
