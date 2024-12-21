<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaMenu.Master" AutoEventWireup="true" CodeBehind="Empleado.aspx.cs" Inherits="ParcialFinal.CapaVista.Empleados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                <h2>Empleados</h2> 
    <br />
    <div>
        <asp:GridView ID="GridViewEmpleado" runat="server"></asp:GridView>
    </div>
     <br />
 <br />
 <div>
     <asp:Label ID="LempleadoID" runat="server" Text="ID Empleado"></asp:Label>
     <asp:TextBox ID="TempleadoID" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="LnumCarnet" runat="server" Text="Numero de carnet"></asp:Label>
     <asp:TextBox ID="TnumCarnet" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="LnombreEmpleado" runat="server" Text="Nombre"></asp:Label>
     <asp:TextBox ID="TnombreEmpleado" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="LfechaNacimiento" runat="server" Text="Fecha Nacimiento"></asp:Label>
     <asp:TextBox ID="TfechaNacimiento" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="LCategoriaEmpleado" runat="server" Text="Categoria empleado:"></asp:Label>
<asp:DropDownList ID="DropDownListCategoriaEmpleado" runat="server">
    <asp:ListItem>Administrador</asp:ListItem>
    <asp:ListItem>Operador</asp:ListItem>
    <asp:ListItem>Peón</asp:ListItem>
</asp:DropDownList>
     <br />
     <asp:Label ID="Lsalario" runat="server" Text="Salario"></asp:Label>
     <asp:TextBox ID="Tsalario" runat="server"></asp:TextBox>
     <br />
      <asp:Label ID="Ldireccion" runat="server" Text="Dirección"></asp:Label>
     <asp:TextBox ID="Tdireccion" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="Ltelefono" runat="server" Text="Telefono"></asp:Label>
     <asp:TextBox ID="Ttelefono" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="Lcorreo" runat="server" Text="Correo"></asp:Label>
     <asp:TextBox ID="Tcorreo" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="LestadoEmpleado" runat="server" Text="Estado:"></asp:Label>
<asp:DropDownList ID="DropDownListEstadoEmpleado" runat="server">
    <asp:ListItem>Inactivo</asp:ListItem>
    <asp:ListItem>Activo</asp:ListItem>
</asp:DropDownList>

 </div>
 <div>
     <br />
     <asp:Button ID="Bagregar" runat="server" Text="Agregar" OnClick="Bagregar_Click" />
     <asp:Button ID="BconsultarFiltro" runat="server" Text="Consultar" OnClick="BconsultarFiltro_Click" />
     <asp:Button ID="Bborrar" runat="server" Text="Borrar" OnClick="Bborrar_Click" style="height: 29px" />
 </div>
</asp:Content>
