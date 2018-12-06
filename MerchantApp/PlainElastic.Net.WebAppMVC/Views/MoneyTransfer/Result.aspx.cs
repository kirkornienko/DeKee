using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_MoneyTransfer_Result : HandyPage
{
    protected override void OnPreRender(EventArgs e)
    {

        try
        {
            lblRsult.Text = Session["Result"].ToString() ?? "No info";
        }
        catch
        {
            lblRsult.Text = "Session expired";
        }
    }
}