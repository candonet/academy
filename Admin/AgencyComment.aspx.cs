﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Data;
using System.IO;

public partial class Admin_AgencyComment : System.Web.UI.Page
{
    public static string CheckNull(object InputValue, int i)
    {
        if (i == 1)
        {
            if (string.IsNullOrEmpty((string)InputValue) == true)
                return "NULL";
            else if ((string)InputValue == string.Empty)
                return "NULL";
            else return "N'" + (string)InputValue.ToString() + "'";
        }
        else
        {
            if (string.IsNullOrEmpty((string)InputValue) == true)
                return "NULL";
            else if ((string)InputValue == string.Empty)
                return "NULL";
            else return (string)InputValue.ToString();
        }
    }
    private void MessageBox(string MessageText)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type='text/javascript'>");
        sb.Append("alert('" + MessageText + "')");
        sb.Append("</script>");
        Page.RegisterStartupScript("show2", sb.ToString());
    }
    protected void CheckSafe()
    {
        if ((Session["User"]) == null)
        {
            Session["Error"] = "شما مجاز به دیدن این صفحه نیستید";
            Page.Response.Redirect("~/Admin/Login.aspx");
        }
        else
        {
            (Session["User"]) = (Session["User"]);
        }
    }

    protected void FillGridView()
    {
        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["MyConString"].ConnectionString;
        SqlConnection con = new SqlConnection(constring);
        try
        {
            SqlCommand cmd = new SqlCommand("SELECT  * FROM (select AgencyComment.ID as aydi, AgencyComment.Title as onvan,AgencyDetail.nam as esm ,AgencyComment.PostDate,1 sortby,AgencyComment.sts  from AgencyComment,AgencyDetail where AgencyDetail.ID=AgencyComment.AgencyID AND AgencyComment.sts=0 union all select AgencyComment.ID as aydi,AgencyComment.Title as onvan,AgencyDetail.nam as esm ,AgencyComment.PostDate,2 sortby,AgencyComment.sts  from AgencyComment,AgencyDetail where AgencyDetail.ID=AgencyComment.AgencyID AND AgencyComment.sts=1 ) dum ORDER   BY sortby", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridView1.DataSource = ds.Tables[0].DefaultView;
            GridView1.DataBind();
        }
        catch (Exception exp)
        {
            MessageBox(exp.Message);
        }
    }

    protected void page_PreRender(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckSafe();
        if (!IsPostBack)
        {

            TBDetail.Visible = false;
            ViewDetails.Visible = false;
            Session["Comment"] = null;
        }
        FillGridView();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {

            e.Row.Attributes["onmouseover"] = "this.originalstyle=this.style.backgroundColor;this.style.cursor='hand';this.style.backgroundColor='#ffccff';";
            e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';this.style.backgroundColor=this.originalstyle;";
            e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(GridView1, "Select$" + e.Row.RowIndex.ToString()));


        }
        if (e.Row.Cells[4].Text.ToString() == "True")
        {
            e.Row.Cells[4].Text = "تایید شده";
            e.Row.Cells[4].CssClass = "TaedShode";
        }
        if (e.Row.Cells[4].Text.ToString() == "False")
        {
            e.Row.Cells[4].CssClass = "TaedNashode";
            e.Row.Cells[4].Text = "تایید نشده";
        }

    }
    protected void GridView1_DataBinding(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FillGridView();
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        CheckSafe();
        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["MyConString"].ConnectionString;
        SqlConnection con = new SqlConnection(constring);
        try
        {
            SqlCommand cmd = new SqlCommand("Update AgencyComment Set sts = 1 where ID = " + Session["Comment"].ToString(), con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Label7.Text = "عملیات با موفقیت انجام شد";
            Label7.ForeColor = Color.Green;
            FillGridView();
            GridView1.SelectedIndex = -1;
            TBDetail.Visible = false;
            ViewDetails.Visible = false;
        }
        catch (Exception exp)
        {
            MessageBox(exp.Message);
        }
    }
    protected void LoadComment()
    {
        txt1.Text = "";
        txt2.Text = "";
        HyperLink1.NavigateUrl = "";
        txt4.Text = "";
        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["MyConString"].ConnectionString;
        SqlConnection con = new SqlConnection(constring);
        try
        {
            SqlCommand cmd = new SqlCommand("select AgencyComment.Title as onvan,AgencyDetail.nam as esm , agencyWatcher.PicAdd,AgencyComment.Tozihat FROM AgencyComment,AgencyDetail,agencyWatcher where AgencyComment.AgencyID = AgencyDetail.ID AND agencyWatcher.PostID = " + Session["Comment"].ToString() + " AND AgencyComment.ID = " + Session["Comment"].ToString(), con);
            SqlDataReader dr = null;
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                txt1.Text = dr[0].ToString();
                txt2.Text = dr[1].ToString();
                HyperLink1.NavigateUrl = dr[2].ToString();
                txt4.Text = dr[3].ToString().Replace("<br/>", "\n");
            }
            con.Close();

        }
        catch (Exception exp)
        {
            MessageBox(exp.Message);
        }
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        CheckSafe();
        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["MyConString"].ConnectionString;
        SqlConnection con = new SqlConnection(constring);
        try
        {
            SqlCommand cmd = new SqlCommand("Update AgencyComment Set sts = 0 where ID = " + Session["Comment"].ToString(), con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Label7.Text = "عملیات با موفقیت انجام شد";
            Label7.ForeColor = Color.Green;
            FillGridView();
            GridView1.SelectedIndex = -1;
            TBDetail.Visible = false;
            ViewDetails.Visible = false;
        }
        catch (Exception exp)
        {
            MessageBox(exp.Message);
        }
    }
    protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            Session["Comment"] = GridView1.SelectedRow.Cells[0].Text.ToString();
            LoadComment();
            TBDetail.Visible = true;
            ViewDetails.Visible = true;
        }
        catch (Exception exp)
        {

            Label7.Text = exp.Message;
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CheckSafe();
        string constring = System.Configuration.ConfigurationManager.ConnectionStrings["MyConString"].ConnectionString;
        SqlConnection con = new SqlConnection(constring);
        try
        {
            e.Cancel = true;
            string CurrentID = GridView1.Rows[e.RowIndex].Cells[0].Text.ToString();
            string myQ = "";
            myQ += "DELETE FROM AgencyWatcher WHERE PostID = " + CurrentID + ";";
            myQ += "DELETE FROM VoteAgency WHERE CommentID = " + CurrentID + ";";
            myQ += "DELETE FROM AgencyComment WHERE ID = " + CurrentID + ";";
            SqlCommand cmd = new SqlCommand(myQ, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            FillGridView();
        }
        catch (Exception exp)
        {
            MessageBox(exp.Message);
        }
    }
}