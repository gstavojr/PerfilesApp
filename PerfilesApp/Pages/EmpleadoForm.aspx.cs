using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Perfiles.AccesoDatos.Repository;
using PerfilesApp.Models;
using PerfilesApp.Service;

namespace PerfilesApp.Pages
{
  public partial class EmpleadoForm : System.Web.UI.Page
  {

    private DepartamentoRespository departamentoRespository = new DepartamentoRespository();
    private EmpleadoService empleadoService = new EmpleadoService();
    protected async void Page_Load(object sender, EventArgs e)
    {

      string empleadoIdStr = Request.QueryString["id"];
      string titlePage = empleadoIdStr != null && empleadoIdStr != "0" ? "Editar" : "Agregar";



      if (!IsPostBack)
      {

        this.GetDepartamentos();
        lblTitle.Text = $"{titlePage} Empleado";
        var empleadoId = empleadoIdStr != null && empleadoIdStr != "0" ? Convert.ToInt32(empleadoIdStr) : 0;
        lblEmpleadoId.Text = empleadoId.ToString();
        if (empleadoId != 0)
        {
          await this.GetEmpleado(empleadoId);
        }
      }
    }

    protected async Task GetEmpleado(int empleadoId)
    {
      var empleadoSearched = await this.empleadoService.GetObject(empleadoId);
      if (empleadoSearched == null) return;
      lblEmpleadoId.Text = empleadoId.ToString();
      txtNombre.Text = empleadoSearched.Nombre;
      txtApellido.Text = empleadoSearched.Apellido;
      txtDpi.Text = empleadoSearched.Dpi;
      txtFechaNacimiento.Text = empleadoSearched.FechaNacimiento.ToString("yyyy-MM-dd");
      ddlSexo.SelectedValue = empleadoSearched.Sexo;
      txtFechaIngreso.Text = empleadoSearched.FechaIngreso.ToString("yyyy-MM-dd");
      txtDireccion.Text = empleadoSearched.Direccion;
      txtNit.Text = empleadoSearched.NIT;
      ddlDepartamento.SelectedValue = empleadoSearched.DepartamentoId.ToString();
    }

    protected void GetDepartamentos()
    {
      List<Departamento> departamentos = (List<Departamento>)this.departamentoRespository
        .GetDepartamentos();

      if (departamentos.Count == 0) return;

      ddlDepartamento.DataSource = departamentos;
      ddlDepartamento.DataTextField = "Nombre";
      ddlDepartamento.DataValueField = "DepartamentoId";
      ddlDepartamento.DataBind();
    }

    protected async void BtnSave_Click(object sender, EventArgs e)
    {
      var isValidForm = this.IsValidateForm();
      
      if (!isValidForm)
      {
        this.SetMessageAlert("Por favor, validar los campos", "alert alert-danger mt-3");
        return;
      }

      Empleado empleado = new Empleado
      {
        EmpleadoId = lblEmpleadoId.Text != "0" ? Convert.ToInt32(lblEmpleadoId.Text) : 0,
        Nombre = txtNombre.Text,
        Apellido = txtApellido.Text,
        Dpi = txtDpi.Text,
        FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text),
        Sexo = ddlSexo.SelectedValue,
        FechaIngreso = Convert.ToDateTime(txtFechaIngreso.Text),
        Direccion = txtDireccion.Text,
        NIT = txtNit.Text != "" ? txtNit.Text : "",
        DepartamentoId = Convert.ToInt32(ddlDepartamento.SelectedValue),

      };

      var resp = await this.empleadoService.Save(empleado);

      string message = 1 == 1 ? "Se guardo el departamento" : "Error al guardar el departamento";
      string cssClass = 1 == 1 ? "alert alert-success mt-3" : "alert alert-danger mt-3";

      this.SetMessageAlert(message, cssClass);

      string url = ResolveUrl("~/About.aspx");

      Response.AppendHeader("Refresh", $"2; url={url}");

    }

    private void SetMessageAlert(string message, string cssClass)
    {
      lblMensajeAlerta.Text = message;
      alerta.CssClass = cssClass;
      alerta.Visible = true;
    }

    protected bool IsValidateForm()
    {
      bool isValidForm = true;

      var campos = new Dictionary<TextBox, Label>
      {
        { txtNombre, lblNombreError },
        { txtApellido, lblApellidoError },
        { txtDpi, lblDpiError },
        { txtFechaNacimiento, lblFechaNacimientoError },
        { txtFechaIngreso, lblFechaIngresoError },
        { txtDireccion, lblDireccion },
      };

      // Limpiar mensajes previos 
      foreach (var campo in campos)
      {
        campo.Value.Text = "";
        campo.Value.Visible = false;
      }

      // Validar campos vacios
      foreach (var campo in campos)
      {
        if (string.IsNullOrEmpty(campo.Key.Text))
        {
          campo.Value.Text = $"{campo.Key.ID.Substring(3)} es requerido";
          campo.Value.Visible = true;
          isValidForm = false;
        }
      }

      // Validar el dropdownlist Sexo
      if (string.IsNullOrWhiteSpace(ddlSexo.SelectedValue) || ddlSexo.SelectedValue == "0" )
      {
        lblSexoError.Text = "El Sexo es un campo obligatorio";
        lblSexoError.Visible = true;
        isValidForm = false;
      }

      if (string.IsNullOrWhiteSpace(ddlDepartamento.SelectedValue))
      {
        lblDepartamentoError.Text = "El departamento es obligatorio";
        lblDepartamentoError.Visible = true;
        isValidForm = false;
      }


      

      return isValidForm;

    }
  }
}