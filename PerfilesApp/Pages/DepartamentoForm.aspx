<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DepartamentoForm.aspx.cs" Inherits="PerfilesApp.Pages.DepartamentoForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div class="row">
  <div class="offset-12">
    <div class="card border">

      <div class="card-header bg-dark">
        <asp:Label 
          ID="lblTitle" 
          runat="server" 
          Text="Agregar Departamento" 
          CssClass="h3 text-white fw-bold">

        </asp:Label>
        
      </div>

      <div class="card-body">
   
        <div class="form-group">
          <asp:Label ID="lblDepartamentoId" runat="server" Text="Id" Visible="false"></asp:Label>
          <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
          <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
          <asp:Label
            ID="lblNombreError"
            runat="server"
            Visible="false"
            CssClass="text-danger">
          </asp:Label>
        </div>
        <div class="form-group">
          <asp:Label ID="lblEstado" runat="server" Text="Estado"></asp:Label>
          <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control">
            <asp:ListItem Text="Activo" Value="1"></asp:ListItem>
            <asp:ListItem Text="Inactivo" Value="0"></asp:ListItem>
          </asp:DropDownList>
        </div>
        <div class="form-group mt-3">
          <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-primary" OnClick="BtnSave_Click">
            <i class="fa-solid fa-save"></i>
            Guardar
          </asp:LinkButton>

          <asp:LinkButton runat="server" PostBackUrl="~/Default.aspx" CssClass="btn btn-secondary">
            <i class="fa-solid fa-arrow-left"></i>
            Regresar
          </asp:LinkButton>
          
        </div>

        <asp:Panel
          ID="alerta"
          class="alert alert-dismissible fade mt-3 "
          role="alert"
          runat="server"
          Visible="false">
          <span id="alertaMensaje">
            <asp:Label ID="lblMensajeAlerta" runat="server" Text="Operación exitosa."></asp:Label>
          </span>
        </asp:Panel>


    </div>
  </div>
</div>
  </div>
  <!-- Literal para inyectar el script JavaScript -->
<asp:Literal ID="litScript" runat="server"></asp:Literal>
</asp:Content>
