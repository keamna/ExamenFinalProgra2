<%@ Page Title="" Language="C#" MasterPageFile="~/PlantillaMenu.Master" AutoEventWireup="true" CodeBehind="Proyecto.aspx.cs" Inherits="ParcialFinal.CapaVista.Proyecto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                    <h2>Proyectos</h2> 
    <br />
    <div>
        <asp:GridView ID="GridViewProyectos" runat="server"></asp:GridView>
    </div>
     <br />
 <br />
 <div>
     <asp:Label ID="LproyectoID" runat="server" Text="ID Proyecto"></asp:Label>
     <asp:TextBox ID="TproyectoID" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="LcodigoProyecto" runat="server" Text="Codigo Proyecto"></asp:Label>
     <asp:TextBox ID="TcodigoProyecto" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="Lnombre" runat="server" Text="Nombre Proyecto"></asp:Label>
     <asp:TextBox ID="TnombreProyecto" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="LfechaInicio" runat="server" Text="Fecha Inicio"></asp:Label>
     <asp:TextBox ID="TfechaInicio" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="LfechaFin" runat="server" Text="Fecha Fin"></asp:Label>
     <asp:TextBox ID="TfechaFin" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="LestadoProyecto" runat="server" Text="Estado:"></asp:Label>
<asp:DropDownList ID="DropDownListEstadoProyecto" runat="server">
    <asp:ListItem>Inactivo</asp:ListItem>
    <asp:ListItem>Activo</asp:ListItem>
</asp:DropDownList>

 </div>
 <div>
     <br />
     <asp:Button ID="Bagregar" runat="server" Text="Agregar" OnClick="Bagregar_Click" />
     <asp:Button ID="Bborrar" runat="server" Text="Borrar" OnClick="Bborrar_Click" />
 </div>
</asp:Content>
