using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_MoneyTransfer_06_Transaction_history : M2ServicePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var result = ExecuteM2Request<GetTransactionHistory, object>(new GetTransactionHistory());
    }
}

public class GetTransactionHistory: M2Request
{

    public override void AddFields(Dictionary<string, string> dict)
    {
        var acc = "511010592836";
        dict.Add("P1", acc);
        dict.Add("P2", DateTime.Now.AddDays(-1).ToString("MM/dd/yyyy").Replace('.', '/'));
        dict.Add("P3", DateTime.Now.AddDays(10).ToString("MM/dd/yyyy").Replace('.', '/'));
        dict.Add("P4", "5299640000136129");
    }

    public override string GetFunc()
    {
        return "010";
    }
}