using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTOs;

namespace WinForms;

public partial class FormPersonaEdit : Form
{
    public PersonaDTO Persona { get; private set; }

    // Constructor para ALTA
    public FormPersonaEdit()
    {
        InitializeComponent();
        Persona = new PersonaDTO();
        this.Text = "Nueva persona";
    }

    // Constructor para MODIFICAR
    public FormPersonaEdit(PersonaDTO personaExistente)
    {
        InitializeComponent();
        Persona = personaExistente;
        this.Text = "Modificar persona";

        txtLegajo.Text = personaExistente.Legajo.ToString();
        txtNombre.Text = personaExistente.Nombre;
        txtApellido.Text = personaExistente.Apellido;
        txtDireccion.Text = personaExistente.Direccion;
        txtEmail.Text = personaExistente.Email;
        txtTelefono.Text = personaExistente.Telefono;
        dtpFechaNacimiento.Value = personaExistente.FechaNacimiento;
        cmbTipoPersona.SelectedItem = personaExistente.TipoPersona;

        if (personaExistente.IdPlan.HasValue)
            nudIdPlan.Value = personaExistente.IdPlan.Value;
    }

    private void FormPersonaEdit_Load(object sender, EventArgs e)
    {
        if (cmbTipoPersona.SelectedItem == null)
            cmbTipoPersona.SelectedIndex = 0;

        ActualizarEstadoIdPlan();
    }

    private void cmbTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
    {
        ActualizarEstadoIdPlan();
    }

    // Habilita el campo IdPlan solo si el tipo elegido es "Alumno",
    // ya que Profesor no requiere plan asignado (ver Persona.SetIdPlan
    // en el dominio: exige IdPlan solo cuando TipoPersona == Alumno).
    private void ActualizarEstadoIdPlan()
    {
        bool esAlumno = cmbTipoPersona.SelectedItem?.ToString() == "Alumno";
        nudIdPlan.Enabled = esAlumno;

        if (!esAlumno)
            nudIdPlan.Value = 0;
    }

    private void FormPersonaEdit_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (this.DialogResult != DialogResult.OK)
            return;

        if (!int.TryParse(txtLegajo.Text, out int legajo) || legajo <= 0)
        {
            MessageBox.Show("El legajo debe ser un número entero mayor que 0.",
                "Dato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.Cancel = true;
            return;
        }

        if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text))
        {
            MessageBox.Show("Nombre y Apellido son obligatorios.",
                "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.Cancel = true;
            return;
        }

        bool esAlumno = cmbTipoPersona.SelectedItem?.ToString() == "Alumno";

        Persona.Legajo = legajo;
        Persona.Nombre = txtNombre.Text.Trim();
        Persona.Apellido = txtApellido.Text.Trim();
        Persona.Direccion = txtDireccion.Text.Trim();
        Persona.Email = txtEmail.Text.Trim();
        Persona.Telefono = txtTelefono.Text.Trim();
        Persona.FechaNacimiento = dtpFechaNacimiento.Value;
        Persona.TipoPersona = cmbTipoPersona.SelectedItem?.ToString() ?? "Alumno";
        Persona.IdPlan = esAlumno ? (int)nudIdPlan.Value : null;
    }
}
