using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Serialization;
using System.Text;
using B4U.MT.Handlers.API.Card;
using B4U.MT.Handlers.Structs.IO.Security;

public partial class Pages_MoneyTransfer_04_New_card : M2ServicePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void AddCard_OnClick(object sender, EventArgs e)
    {
        var data = base.FetchDataItem<NewCardRequest>(this.container);

        LoginRequest login = new LoginRequest()
        {
            Login = "zakhrchuk.denis",
            Password = "Helloworld11"
        };
        var lresp = Execute<LoginResponse, LoginRequest>(login, FePath + "/Security.svc/Login");


        NewCardStartRequest req = new NewCardStartRequest()
        {

        };
        var response = Execute<NewCardStartResponse, NewCardStartRequest>(req, FePath + "/Card.svc/NewCardStart");
        Response.Redirect("Result.aspx");
        return;
    }
}

[DataContract]
public class NewCardRequest : IM2Request
{
    [DataMember]
    public string CardType {get; set;}
    [DataMember]
    public string CardClass { get; set; }
    [DataMember]
    public string Currency { get; set; }
    [DataMember]
    public string Limit { get; set; }
    [DataMember]
    public string Comission { get; set; }
    [DataMember]
    public string ComissionCard { get; set; }
    [DataMember]
    public string Delivery { get; set; }
    [DataMember]
    public string Adress { get; set; }

    public string GetQuery()
    {
        var dict = new Dictionary<string, string>();

        dict.Add("CUSR", "aeb4utestapi@BANK4U");
        dict.Add("CPWD", "Helloworld01!");
        
        dict.Add("FUNC", "InstantCardIssue");
        dict.Add("MSGID", "3");
        dict.Add("cardholderid", "*");
        //dict.Add("middlename", "");
        dict.Add("firstname", "Pavel");
        dict.Add("LastName", "Tanaev");
        dict.Add("address1", HttpUtility.UrlEncode(Adress));
        dict.Add("city", "Maitland");
        dict.Add("state", "FL");
        dict.Add("zip", "88677");
        dict.Add("country", "US");
        dict.Add("secondaryaddress1", HttpUtility.UrlEncode(Adress));
        
        dict.Add("city2", "Kiev");
        dict.Add("state2", "Kiev region");
        dict.Add("zip2", "01001");
        dict.Add("countrycode2", "UA");

        dict.Add("email", "zakharchukd@gmail.com");
        dict.Add("dob", DateTime.Today.ToString("MMddyyyy"));
        dict.Add("id_type", "PASSPORT");
        dict.Add("id_number", "CM783940");
        dict.Add("id_issuer", "UA");

        dict.Add("ssn", "111223333");
        dict.Add("Remote_Host", "24.73.221.210");
        //dict.Add("24.73.221.210", "");

        dict.Add("BIN", "52996400");


        
        var query = dict.Select(p => p.Key + "=" + p.Value).Aggregate((h, t) => h + "&" + t);

        return query;
    }
}