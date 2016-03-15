using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class dateofbirth : System.Web.UI.Page
{
    DataClassesDataContext lnq_obj = new DataClassesDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return; 
        fill_data();
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string abc = ddl_day.SelectedValue + "-" + ddl_month.SelectedValue + "-" + ddl_year.SelectedValue;
        string total_exper = ddl_work_year.SelectedValue + "-" + ddl_work_month.SelectedValue;
        string sal = ddl_lakh.SelectedValue + "-" + ddl_thousand.SelectedValue;
        lnq_obj.insert_dof(abc, total_exper,sal);
        lnq_obj.SubmitChanges();

        fill_data();
    }
    private void fill_data()
    {
        
        var id = (from a in lnq_obj.dof_msts
                  select new
                  {
                      code = a.intglcode,
                      dateofbirth = a.dof,
                      totalwork=a.totalwork,
                      salary = a.salary
                  }).ToList();
        GridView1.DataSource = id;
        GridView1.DataBind();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int code = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Values.ToString());
        ViewState["id"] = code;
        var id = (from a in lnq_obj.dof_msts
                  select a).Single();
             
        string s = id.dof;
        //
        // Split string on spaces.
        // This will separate all the words.
        //
        string[] words = s.Split('-');

        ddl_day.SelectedValue = words[0].ToString();
        ddl_month.SelectedValue = words[1].ToString();
        ddl_year.SelectedValue = words[2].ToString();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        lnq_obj.delete_dof(Convert.ToInt32(ViewState["id"].ToString()));
        lnq_obj.SubmitChanges();
        fill_data();
    }
    protected void btn_update_Click(object sender, EventArgs e)
    {
        try
        {
            string abc = ddl_day.SelectedValue + "-" + ddl_month.SelectedValue + "-" + ddl_year.SelectedValue;
            string total_exper = ddl_work_year.SelectedValue + "" + ddl_work_month.SelectedValue;
            string sal = ddl_lakh.SelectedValue + "-" + ddl_thousand.SelectedValue;
            lnq_obj.update_dof(Convert.ToInt32(ViewState["id"].ToString()), abc, total_exper, sal);
            lnq_obj.SubmitChanges();

            fill_data();
            Response.Redirect("dateofbirth.aspx");
        }
        catch (Exception ex)
        {
            
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {

            GridView1.PageIndex = e.NewPageIndex;
            fill_data();
        }
        catch (Exception ex)
        {


        }
    }
}