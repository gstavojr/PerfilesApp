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

      this.GetDepartamentos();

      if (!IsPostBack)
      {
        lblTitle.Text = $"{titlePage} Empleado";
        var empleadoId = empleadoIdStr != null && empleadoIdStr != "0" ? Convert.ToInt32(empleadoIdStr) : 0;

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

      ddlDepartamento.DataSource = departamentos;
      ddlDepartamento.DataTextField = "Nombre";
      ddlDepartamento.DataValueField = "DepartamentoId";
      ddlDepartamento.DataBind();
    }

    protected async void BtnSave_Click(object sender, EventArgs e)
    {
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
        NIT = txtNit.Text,
        DepartamentoId = Convert.ToInt32(ddlDepartamento.SelectedValue),
        
      };

      var resp = await this.empleadoService.Save(empleado);

      string message = 1 == 1 ? "Se guardo el departamento" : "Error al guardar el departamento";
      string cssClass = 1 == 1 ? "alert alert-success mt-3" : "alert alert-danger mt-3";

      lblMensajeAlerta.Text = message;
      alerta.CssClass = cssClass;
      alerta.Visible = true;

      string url = ResolveUrl("~/About.aspx");

      Response.AppendHeader("Refresh", $"2; url={url}");

    }




  }
}