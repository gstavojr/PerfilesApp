<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PerfilesApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

  <h1 class="text-center">Departamentos <i class="fa-solid fa-building"></i>
  </h1>
  <div class="row">
    <div class="offset-9 col-3 mb-3">
      <asp:LinkButton
        class="btn btn-primary mt-3 me-auto"
        ID="btnAddDepartamento"
        OnClick="BtnNew_Click"
        runat="server">
      <i class="fa-solid fa-plus"></i> Nuevo Departamento
      </asp:LinkButton>
    </div>
  </div>


  <div class="row">
    <div class="offset-12">
      <div class="card border">
        <div class="card-header bg-dark">
          <h5 class="text-white">
            <strong>Lista de Departamentos
            </strong>
          </h5>

        </div>

        <div class="card-body">
          <asp:GridView
            ID="gvDepartamentos"
            runat="server"
            CssClass="table table-striped table-bordered table-hover"
            AutoGenerateColumns="False"
            DataKeyNames="DepartamentoId">
            <Columns>
              <asp:BoundField DataField="DepartamentoId" HeaderText="Id" SortExpression="Id" />
              <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
              <asp:BoundField DataField="NombreEstado" HeaderText="Estado" SortExpression="Descripcion" />
              <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                  <asp:LinkButton
                    ID="btnEdit"
                    runat="server"
                    CommandName="Edit"
                    OnClick="BtnEdit_Click"
                    CommandArgument='<%# Eval("DepartamentoId ") %>'
                    CssClass="btn btn-warning">
                    <i class="fa-solid fa-edit"></i> Editar
                  </asp:LinkButton>
                  <asp:LinkButton
                    ID="btnDelete"
                    runat="server"
                    CommandName="Delete"
                    OnClick="BtnDelete_Click"
                    CommandArgument='<%# Eval("DepartamentoId ") %>'
                    CssClass="btn btn-danger">
                    <i class="fa-solid fa-trash"></i> Eliminar
                  </asp:LinkButton>
                </ItemTemplate>
              </asp:TemplateField>
            </Columns>
          </asp:GridView>
        </div>
      </div>
    </div>




  </div>


</asp:Content>
