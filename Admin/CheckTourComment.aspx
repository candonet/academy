﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="CheckTourComment.aspx.cs" Inherits="CheckTourComment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>مدیریت پیام های تورها</title>
    <style>
        .TaedNashode
        {
            font-size:13.5px;
            font-family:"BYekan";
            color:red;
            
        }
        .TaedShode
        {
            font-size:13.5px;
            font-family:"BYekan";
            color:green;
        }
        .HeaderDataList
        {
             font-size:14.5px;
             font-family:"BYekan";
             font-weight:normal;
        }
        .tdEmpty
        {
            font-size:13.5px;
            font-family:"BYekan";
            padding-left:5px;
            padding-right:5px;
        }
        .auto-style1
        {
            text-align: left;
            vertical-align:top;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="Form1" runat="server">
        <h2>مدیریت نظرات تور ها</h2>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False" OnPageIndexChanging="GridView1_PageIndexChanging" Width="450px" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting">
            <Columns>
                <asp:BoundField HeaderText="" DataField="ID" >
                <HeaderStyle CssClass="HeaderDataList" />
                <ItemStyle CssClass="tdEmpty" HorizontalAlign="Center"/>
                </asp:BoundField>
                <asp:BoundField HeaderText="نام تور" DataField="TourName" >
                <HeaderStyle CssClass="HeaderDataList" />
                <ItemStyle CssClass="tdEmpty" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="تاریخ نظر" DataField="PostDate" >
                <HeaderStyle CssClass="HeaderDataList" />
                <ItemStyle CssClass="tdEmpty" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField HeaderText="وضعیت" datafield="STS" >
                <HeaderStyle CssClass="HeaderDataList" />
                <ItemStyle CssClass="tdEmpty" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="حذف" OnClientClick="return confirm('آیا شما مطمئن به حذف هستید؟'); "></asp:Button>
                    </ItemTemplate>
                    <HeaderStyle Width="50px" />
                </asp:TemplateField>
            </Columns>
            
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#487575" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#275353" />
            
            </asp:GridView>
        <h2 id="ViewDetails" runat="server">بررسی نظر</h2>

                    <table style="width: 400px;" runat="server" id="TBDetail">
                        <tr>
                            <td style="width: 99px;" class="auto-style1">نام نویسنده:</td>
                            <td class="auto-style8">
                                <asp:TextBox ID="txt1" runat="server" Width="196px" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 99px;" class="auto-style1">ایمیل:</td>
                            <td class="auto-style8">
                                <asp:TextBox ID="txt2" runat="server" Width="196px" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">متن کامل دیدگاه :</td>
                            <td class="auto-style9">
                                <asp:TextBox ID="txt4" runat="server" Width="196px" Height="104px" TextMode="MultiLine" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 99px;height: 30px;" class="auto-style1">&nbsp;</td>
                            <td style="height: 30px;" class="auto-style8">
                                &nbsp;<asp:Button ID="Button5" runat="server" Text="تایید" Width="50px" OnClick="Button5_Click" />
                                &nbsp;<asp:Button ID="Button6" runat="server" Text="تعلیق" Width="50px" OnClick="Button6_Click" />
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 99px;" class="auto-style1">&nbsp;</td>
                            <td class="auto-style8">
                                <asp:Label ID="Label7" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
    </form>
</asp:Content>

