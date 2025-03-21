using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Perfiles.AccesoDatos.Repository;
using PerfilesApp.Models;

namespace PerfilesApp
{
  public partial class _Default : Page
  {
    private DepartamentoRespository departamentoRespository = new DepartamentoRespository();
    protected void Page_Load(object sender, EventArgs e)
    {
      this.GetDepartamentos();
    }

    private void GetDepartamentos()
    {
      var response = this.departamentoRespository.GetDepartamentos();
      if (!response.isSucces)
      {
        this.SetMessageAlert(response.Message, "alert alert-danger mt-1");
        return;
      }
      List<Departamento> departamentos = (List<Departamento>)response.Data;

      foreach (var departamento in departamentos)
      {
        Console.WriteLine($"DepartamentoId: {departamento.DepartamentoId}, Nombre: {departamento.Nombre}, Estado: {departamento.Estado}");
      }

      gvDepartamentos.DataSource = departamentos;
      gvDepartamentos.DataBind();
    }

    protected void BtnNew_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Pages/DepartamentoForm.aspx?Id=0");
    }

    protected void BtnEdit_Click(object sender, EventArgs e)
    {
      LinkButton btn = (LinkButton)sender;
      string departamentoId = btn.CommandArgument;
      Response.Redirect($"~/Pages/DepartamentoForm.aspx?Id={departamentoId}");
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
      LinkButton btn = (LinkButton)sender;
      string departamentoId = btn.CommandArgument;
      var response = this.departamentoRespository.DeleteDepartamento(Convert.ToInt32(departamentoId));

      if (!response.isSucces)
      {
        this.SetMessageAlert(response.Message, "alert alert-danger mt-1");
        return;
      }
      this.GetDepartamentos();
      
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