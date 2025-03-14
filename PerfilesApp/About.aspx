<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="PerfilesApp.About" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <h1 class="text-center">Empleados <i class="fa-solid fa-user"></i>
  </h1>


  <div class="row">
    <div class="col-9 d-flex align-items-center mb-3">
      <div class="form-group">
        <label for="txtFechaInicio" class="me-2">Desde:</label>
        <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="form-control me-3" TextMode="Date"></asp:TextBox>
      </div>
      <div class="form-group ms-1">
        <label for="txtFechaFin" class="me-2">Hasta:</label>
        <asp:TextBox ID="txtFechaFin" runat="server" CssClass="form-control me-3" TextMode="Date"></asp:TextBox>
      </div>
      <div class="form-group ms-1 d-flex align-items-center">
        <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-success mt-4" Text="Buscar" OnClick="BtnBuscar_Click" />
        <asp:LinkButton 
          ID="btnClear" 
          runat="server" 
          CssClass="btn btn-danger mt-4 ms-2" 
          OnClick="BtnClear_Click">
          <i class="fa-solid fa-trash"></i>
         </asp:LinkButton>
      </div>
      
    </div>
    <div class="col-3 mb-3">
      <asp:LinkButton
        class="btn btn-primary mt-3 me-auto"
        ID="btnAddEmpleado"
        OnClick="BtnNew_Click"
        runat="server">
        <i class="fa-solid fa-plus"></i> Nuevo Empleado
      </asp:LinkButton>
    </div>
  </div>

  <div class="row">
    <div class="offset-12">
      <div class="card border">
        <div class="card-header bg-dark">
          <h5 class="text-white">
            <strong>Lista de Empleados
            </strong>
          </h5>

        </div>

        <div class="card-body">
          <asp:GridView
            ID="gvEmpleados"
            runat="server"
            CssClass="table table-striped table-bordered table-hover"
            AutoGenerateColumns="False"
            DataKeyNames="DepartamentoId">
            <Columns>
              <asp:BoundField DataField="EmpleadoId" HeaderText="Id" SortExpression="Id" />
              <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
              <asp:BoundField DataField="Apellido" HeaderText="Apellido" SortExpression="Apellido" />
              <asp:BoundField DataField="Dpi" HeaderText="Dpi" SortExpression="Dpi" />
              <asp:BoundField
                DataField="FechaNacimiento"
                HeaderText="FechaNacimiento"
                DataFormatString="{0:dd/MM/yyyy}"
                HtmlEncode="False"
                SortExpression="FechaNacimiento" />
              <asp:BoundField DataField="Edad" HeaderText="Edad" SortExpression="Edad" />
              <asp:BoundField DataField="Sexo" HeaderText="Sexo" SortExpression="Sexo" />
              <asp:BoundField
                DataField="FechaIngreso"
                HeaderText="FechaIngreso"
                DataFormatString="{0:dd/MM/yyyy}"
                HtmlEncode="False"
                SortExpression="FechaIngreso" />
              <asp:BoundField DataField="TiempoServicio" HeaderText="Laborando" SortExpression="TiempoServicio" />
              <asp:BoundField DataField="Direccion" HeaderText="Direccion" SortExpression="Direccion" />
              <asp:BoundField DataField="NIT" HeaderText="NIT" SortExpression="NIT" />
              <asp:BoundField DataField="Departamento" HeaderText="Departamento" SortExpression="Departamento" />
              <asp:TemplateField HeaderText="Estado Departamento">
                <ItemTemplate>
                  <%# Convert.ToInt32(Eval("EstadoDepartamento")) == 1 ? "Activo" : "Inactivo" %>
                </ItemTemplate>
              </asp:TemplateField>


              <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                  <asp:LinkButton
                    ID="btnEdit"
                    runat="server"
                    CommandName="Edit"
                    OnClick="BtnEdit_Click"
                    CommandArgument='<%# Eval("EmpleadoId ") %>'
                    CssClass="btn btn-warning">
                  <i class="fa-solid fa-edit"></i> Editar
                  </asp:LinkButton>
                  <asp:LinkButton
                    ID="btnDelete"
                    runat="server"
                    CommandName="Delete"
                    CommandArgument='<%# Eval("EmpleadoId ") %>'
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
