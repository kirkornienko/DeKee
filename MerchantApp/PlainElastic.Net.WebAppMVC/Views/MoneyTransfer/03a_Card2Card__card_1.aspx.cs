using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_MoneyTransfer_03a_Card2Card__card_1 : M2ServicePage
{
    protected void Resume_OnClick(object sender, EventArgs e)
    {
        var data = FetchDataItem<Card2CardRequest>(container);
        SetSessionData(data);


        Response.Redirect("03a_Card2Card__card_2.aspx");

        
    }

}