using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DTOs;

namespace WinForms;

public partial class FormComisionEdit : Form
{
    public ComisionDTO Comision { get; private set; }

    public FormComisionEdit(List<PlanDTO> planes)
    {
        InitializeComponent();
        Comision = new ComisionDTO();
        this.Text = "Nueva Comisión";
        CargarComboPlanes(planes, null);
    }

    public FormComisionEdit(ComisionDTO comisionExistente, List<PlanDTO> planes)
    {
        InitializeComponent();
        Comision = comisionExistente;
        this.Text = "Modificar Comisión";

        txtDescripcion.Text = comisionExistente.Descripcion;
        nudAnioEspecialidad.Value = comisionExistente.AnioEspecialidad >= 1 && comisionExistente.AnioEspecialidad <= 6
            ? comisionExistente.AnioEspecialidad
            : 1;

        CargarComboPlanes(planes, comisionExistente.IdPlan);
    }

    private void CargarComboPlanes(List<PlanDTO> planes, int? idPlanSeleccionado)
    {
        cmbPlanes.DataSource = planes;
        cmbPlanes.DisplayMember = nameof(PlanDTO.Descripcion);
        cmbPlanes.ValueMember = nameof(PlanDTO.Id);

        if (idPlanSeleccionado.HasValue)
        {
            cmbPlanes.SelectedValue = idPlanSeleccionado.Value;
        }
    }

    private void btnGuardar_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
        {
            MessageBox.Show("La descripción / código de la comisión es obligatoria.",
                "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtDescripcion.Focus();
            return;
        }

        if (cmbPlanes.SelectedValue is not int idPlan || idPlan <= 0)
        {
            MessageBox.Show("Debe seleccionar un plan de estudio.",
                "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            cmbPlanes.Focus();
            return;
        }

        Comision.Descripcion = txtDescripcion.Text.Trim();
        Comision.AnioEspecialidad = (int)nudAnioEspecialidad.Value;
        Comision.IdPlan = idPlan;

        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private void btnCancelar_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }
}
