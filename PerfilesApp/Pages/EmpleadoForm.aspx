<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmpleadoForm.aspx.cs" Inherits="PerfilesApp.Pages.EmpleadoForm" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="row">
  <div class="offset-12">
    <div class="card border">

      <div class="card-header bg-dark">
        <asp:Label 
          ID="lblTitle" 
          runat="server" 
          Text="Agregar Empleado" 
          CssClass="h3 text-white fw-bold">

        </asp:Label>
        
      </div>

      <div class="card-body">
   
        <div class="form-group">
          <asp:Label ID="lblEmpleadoId" runat="server" Text="Id" Visible="false"></asp:Label>
          <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
          <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group">
          <asp:Label ID="Apellido" runat="server" Text="Apellido"></asp:Label>
          <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
        </div>


        <!--Dpi-->
        <div class="form-group">
          <asp:Label ID="lblDpi" runat="server" Text="DPI"></asp:Label>
          <asp:TextBox ID="txtDpi" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <!--Fecha de Nacimiento-->
        <div class="form-group">
          <asp:Label ID="lblFechaNacimiento" runat="server" Text="Fecha de Nacimiento"></asp:Label>
          <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
        </div>

        <!--Sexo-->
        <div class="form-group">
          <asp:Label ID="lblSexo" runat="server" Text="Sexo"></asp:Label>
          <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-control">
            <asp:ListItem Text="Masculino" Value="Masculino"></asp:ListItem>
            <asp:ListItem Text="Femenino" Value="Femenino"></asp:ListItem>
          </asp:DropDownList>
        </div>

        <!--Fecha de Ingreso a laboral-->
        <div class="form-group">
          <asp:Label ID="lblFechaIngreso" runat="server" Text="Fecha de Ingreso"></asp:Label>
          <asp:TextBox ID="txtFechaIngreso" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
         </div>

        <!--Direccion-->
        <div class="form-group">
          <asp:Label ID="lblDireccion" runat="server" Text="Direccion"></asp:Label>
          <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <!--NIT-->
        <div class="form-group">
          <asp:Label ID="lblNit" runat="server" Text="NIT"></asp:Label>
          <asp:TextBox ID="txtNit" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <!--Departamento-->
        <div class="form-group">
          <asp:Label ID="lblDepartamento" runat="server" Text="Departamento"></asp:Label>
          <asp:DropDownList ID="ddlDepartamento" runat="server" CssClass="form-control">
          </asp:DropDownList>
        </div>


        <div class="form-group mt-3">
          <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-primary" OnClick="BtnSave_Click">
            <i class="fa-solid fa-save"></i>
            Guardar
          </asp:LinkButton>

          <asp:LinkButton runat="server" PostBackUrl="~/About.aspx" CssClass="btn btn-secondary">
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
</asp:Content>
