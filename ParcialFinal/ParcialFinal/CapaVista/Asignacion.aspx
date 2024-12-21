<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaMenu.Master" AutoEventWireup="true" CodeBehind="Asignacion.aspx.cs" Inherits="ParcialFinal.CapaVista.Asignacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                    <h2>Asignacion</h2> 
    <br />
    <div>
        <asp:GridView ID="GridViewAsignacion" runat="server"></asp:GridView>
    </div>
     <br />
 <br />
 <div>
     <asp:Label ID="LasignacionID" runat="server" Text="ID Asignacion"></asp:Label>
     <asp:TextBox ID="TasignacionID" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="LempleadoID" runat="server" Text="ID Empleado"></asp:Label>
     <asp:TextBox ID="TempleadoID" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="LproyectoID" runat="server" Text="ID Proyecto"></asp:Label>
     <asp:TextBox ID="TproyectoID" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="LfechaAsignacion" runat="server" Text="Fecha Asignacion"></asp:Label>
     <asp:TextBox ID="TfechaAsignacion" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="LestadoAsignacion" runat="server" Text="Estado:"></asp:Label>
<asp:DropDownList ID="DropDownListEstadoAsignacion" runat="server">
    <asp:ListItem>Inactivo</asp:ListItem>
    <asp:ListItem>Activo</asp:ListItem>
</asp:DropDownList>

 </div>
 <div>
     <br />
     <asp:Button ID="Bagregar" runat="server" Text="Agregar" OnClick="Bagregar_Click" style="height: 29px" />
     <asp:Button ID="Bborrar" runat="server" Text="Borrar" OnClick="Bborrar_Click" />
 </div>
</asp:Content>
