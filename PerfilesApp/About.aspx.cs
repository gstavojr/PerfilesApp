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
      var empleadosList = await this.empleadoService.GetObjects();

      return empleadosList;

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



  }
}