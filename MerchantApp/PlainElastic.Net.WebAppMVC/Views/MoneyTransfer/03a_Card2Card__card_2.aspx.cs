using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_MoneyTransfer_03a_Card2Card__card_2 : M2ServicePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Card2CardRequest data;
        if (TryGetSessionData(out data))
        {
            DisplayDetailedData(data, container);
            Amount1.Text = AmountFormatted.Text;

            GetCardBalanceRequest request = new GetCardBalanceRequest()
            {
                PAN = data.Sender
            };
            var response = ExecuteM2Request<GetCardBalanceRequest, object>(request);
        }
        else
        {
            Response.Redirect("03a_Card2Card__card_1.aspx");
        }
    }

    protected void Resume_OnClick(object sender, EventArgs e)
    {
        Card2CardRequest data;
        if (TryGetSessionData(out data))
        {
            Response.Redirect("03a_Card2Card__card_3.aspx");
        }
        else
        {
            Response.Redirect("03a_Card2Card__card_1.aspx");
        }
    }
}

public class GetCardBalanceRequest : M2Request
{

    public override void AddFields(Dictionary<string, string> dict)
    {
        dict.Add("P1", PAN);
    }

    public override string GetFunc()
    {
        return "033";
    }

    public string PAN { get; set; }
}