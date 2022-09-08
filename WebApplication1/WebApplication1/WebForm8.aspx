<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm8.aspx.cs" Inherits="WebApplication1.WebForm8" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-family:Arial">
            <asp:Button ID="btnGetDataFromDB" runat="server" Text="Get Data From Database" OnClick="btnGetDataFromDB_Click" />
            <asp:GridView ID="gvStudents" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCancelingEdit="gvStudents_RowCancelingEdit" OnRowEditing="gvStudents_RowEditing" OnRowUpdating="gvStudents_RowUpdating" OnSelectedIndexChanged="gvStudents_SelectedIndexChanged" OnRowDeleting="gvStudents_RowDeleting">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                    <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                    <asp:BoundField DataField="TotalMarks" HeaderText="TotalMarks" SortExpression="TotalMarks" />
                </Columns>
            </asp:GridView>
            <asp:Button ID="btnUpdateDB" runat="server" Text="Update Database Table" OnClick="btnUpdateDB_Click" />
            <asp:Label ID="lblMessage" runat="server"></asp:Label>

           
        </div>
    </form>
</body>
</html>
