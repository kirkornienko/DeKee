using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_MoneyTransfer_03a_Card2Card__card_3 : M2ServicePage
{
    protected void btnConfirm_OnClick(object sender, EventArgs e)
    {
        Card2CardRequest data;

        if (TryGetSessionData(out data))
        {
            var result = ExecuteM2Request<Card2CardRequest, object>(data);
            Response.Redirect("Result.aspx");
        }

    }
}