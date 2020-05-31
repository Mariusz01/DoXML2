<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DoXML2._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h3>Export z bazy do pliku XML</h3>
    </div>
    <div>
        <table>
            <tr>
                <td>Wybierz plik: </td>
                <td>
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="258px" />
                </td>
                <td>
                    <asp:Button ID="ButtonImport" runat="server" Text="Import Danych" OnClick="ButtonImport_Click" />
                </td>
            </tr>
        </table>
        <div>
            <br />
            <asp:Label ID="LabelMessage" runat="server" Font-Bold="true" />
            <br />
            <asp:GridView ID="GridViewData" runat="server" AutoGenerateColumns="false">
                <EmptyDataTemplate>
                    <div style="padding:10px">
                        Data not found.
                    </div>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField HeaderText="Nr:" DataField="Id" />
                    <asp:BoundField HeaderText="Imię:" DataField="Imie" />
                    <asp:BoundField HeaderText="Nazwisko:" DataField="Nazwisko" />
                    <asp:BoundField HeaderText="Miasto:" DataField="Miasto" />
                    <asp:BoundField HeaderText="Ulica:" DataField="Ulica" />
                    <asp:BoundField HeaderText="Nr Domu:" DataField="NrDomu" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="ButtonExport" runat="server" Text="Export" OnClick="ButtonExport_Click" />
        </div>
    </div>




    <div class="row">
        <div class="col-md-4">
           
            
        </div>
        <div class="col-md-4">
            
        </div>
        <div class="col-md-4">
           
        </div>
    </div>

</asp:Content>
