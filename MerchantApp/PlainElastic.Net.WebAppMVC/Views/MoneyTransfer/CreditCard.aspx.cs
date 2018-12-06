using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_MoneyTransfer_CreditCard : M2ServicePage
{
    protected void CreditCard_OnClick(object sender, EventArgs e)
    {
        var data = FetchDataItem<CreditCardRequest>(container);
        var result = ExecuteM2Request<CreditCardRequest, object>(data);

        Response.Redirect("Result.aspx");
    }
}

public class CreditCardRequest : IM2Request
{
    public string PAN { get; set; }
    public string Amount { get; set; }

    public string GetQuery()
    {
        var dict = new Dictionary<string, string>();

        dict.Add("CUSR", "aeb4utestapi@BANK4U");
        dict.Add("CPWD", "Helloworld01!");

        dict.Add("FUNC", "007");
        dict.Add("MSGID", "3");

        dict.Add("P1", PAN);
        dict.Add("P2", Amount);
        dict.Add("P4", "0");
        dict.Add("P8", "Test Credit");

        var query = dict.Select(p => p.Key + "=" + p.Value).Aggregate((h, t) => h + "&" + t);

        return query;
    }
}