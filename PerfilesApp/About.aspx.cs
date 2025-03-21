using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using PerfilesApp.Models;
using PerfilesApp.Service;

namespace PerfilesApp
{
  public partial class About : Page
  {

    private EmpleadoService empleadoService;

    public About()
    {

      this.empleadoService = new EmpleadoService();

    }

    protected async void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        await LoadEmpleadosView();
      }
    }

    protected void BtnEdit_Click(object sender, EventArgs e)
    {
      LinkButton btn = (LinkButton)sender;
      string id = btn.CommandArgument;
      Response.Redirect($"~/Pages/EmpleadoForm.aspx?Id={id}");
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Pages/EmpleadoForm.aspx?Id=0");
    }


    private async Task<IEnumerable<Empleado>> GetEmpleados()
    {
      var response = await this.empleadoService.GetObjects();

      if (!response.isSucces)
      {
        this.SetMessageAlert(response.Message, "alert alert-danger");
        return new List<Empleado>();
      }


      return response.Data;

    }

    protected async Task LoadEmpleadosView()
    {
      gvEmpleados.DataSource = await this.GetEmpleados();
      gvEmpleados.DataBind();


    }

    protected async void BtnBuscar_Click(object sender, EventArgs e)
    {

      if (txtFechaFin.Text == "" || txtFechaInicio.Text == "")
      {
        return;
      }

      DateTime fechaIngreo = DateTime.Parse(txtFechaInicio.Text);
      DateTime fechaFin = DateTime.Parse(txtFechaFin.Text);



      var empleados = await this.GetEmpleados();

      if (empleados == null || !empleados.Any() )
      {
        return;
      }




      var empleadosFiltrados = empleados.Where(x => x.FechaIngreso >= fechaIngreo && x.FechaIngreso <= fechaFin).ToList();
      gvEmpleados.DataSource = empleadosFiltrados;
      gvEmpleados.DataBind();


    }

    protected async void BtnClear_Click(object sender, EventArgs e)
    {
      txtFechaInicio.Text = string.Empty;
      txtFechaFin.Text = string.Empty;
      gvEmpleados.DataSource = await this.GetEmpleados(); ;
      gvEmpleados.DataBind();
    }

    protected async void BtnDelete_Click(object sender, EventArgs e)
    {
      LinkButton btn = (LinkButton)sender;
      string id = btn.CommandArgument;
      var response = await this.empleadoService.Delete(Convert.ToInt32(id));
      if (!response.isSucces)
      {
        this.SetMessageAlert(response.Message, "alert alert-danger");
        return;
      }
      this.SetMessageAlert($"Se elimino el empleado con id {id}", "alert alert-success");
      await LoadEmpleadosView();
    }

    protected void BtnCloseAlert(object sender, EventArgs e)
    {
      alerta.Visible = false;
    }

    private void SetMessageAlert(string message, string cssClass)
    {
      lblMensajeAlerta.Text = message;
      alerta.CssClass = cssClass;
      alerta.Visible = true;
    }



  }
}