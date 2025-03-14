using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Perfiles.AccesoDatos.Repository;
using PerfilesApp.Models;

namespace PerfilesApp.Pages
{
  public partial class DepartamentoForm : System.Web.UI.Page
  {

    private int departamentoId;
    private DepartamentoRespository departamentoRespository = new DepartamentoRespository();
    protected void Page_Load(object sender, EventArgs e)
    {
      //string titlePage = !IsPostBack && GetDepartamentoIdFromQueryString
      string departamentoId = Request.QueryString["id"];
      string titlePage = departamentoId != null && departamentoId != "0" ? "Editar" : "Agregar";

      if (!IsPostBack)
      {
        lblTitle.Text = $"{titlePage} Departamento";
        this.departamentoId = departamentoId != null && departamentoId != "0" ? Convert.ToInt32(departamentoId) : 0;
        lblDepartamentoId.Text = this.departamentoId.ToString();

        if (this.departamentoId != 0)
        {
          this.GetDepartamento();
        }


      }

    }

    protected void GetDepartamento()
    {
      Departamento departamentoSearched = this.departamentoRespository.GetDepartamento(this.departamentoId);
      if (departamentoSearched == null) return;

      txtNombre.Text = departamentoSearched.Nombre;
      ddlEstado.SelectedValue = departamentoSearched.Estado.ToString();
      lblDepartamentoId.Text = departamentoSearched.DepartamentoId.ToString();

    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {



      Departamento departamento = new Departamento
      {
        DepartamentoId = Convert.ToInt32(lblDepartamentoId.Text),
        Nombre = txtNombre.Text,
        Estado = ddlEstado.SelectedValue != "0" ? Convert.ToInt32(ddlEstado.SelectedValue) : 0
      };

      bool resp = departamento.DepartamentoId == 0
       ? this.departamentoRespository.CreateDepartamento(departamento)
       : this.departamentoRespository.UpdateDepartamento(departamento);

      string message = resp ? "Se guardo el departamento" : "Error al guardar el departamento";
      string cssClass = resp ? "alert alert-success mt-3" : "alert alert-danger mt-3";

      lblMensajeAlerta.Text = message;
      alerta.CssClass = cssClass;
      alerta.Visible = true;

      string url = ResolveUrl("~/Default.aspx");

      Response.AppendHeader("Refresh", $"2; url={url}");

    }

  }
}