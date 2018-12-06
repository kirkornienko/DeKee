using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Serialization;

public partial class Pages_MoneyTransfer_GlobalTest : M2ServicePage
{
    private Dictionary<string, string> ParseToDictionary(string str)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();

        var pairs = str.Split('&');
        foreach (var p in pairs)
        {
            var items = p.Split('=');
            if (items.Length == 2)
            {
                result.Add(items[0], items[1]);
            }
            else
                continue;
        }

        return result;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        var requests = new M2RequestDummy[]
        {

        new M2RequestDummy()
        {
            Name = "Create CardHolder",
            Method = "001",
            Fields = ParseToDictionary("P1=*&P2=Joe&P3=none&P4=Smith&P5=address1&P6=address2&P7=miami&P8=fl&P9=33456&P10=us&P11=8831891679&P12=07041979 ")
        },
        new M2RequestDummy()
        {
            Name = "Change Card Status",
            Method = "004",
            Fields = ParseToDictionary("P1=5299640000136129&P2=4&P3=1 ")
        },
        new M2RequestDummy()
        {
            Name = "Change Card PIN ",
            Method = "005",
            Fields = ParseToDictionary("P1=5299640000136129&P2=0000&P3=1X48SHS568394")
        },
        new M2RequestDummy()
        {
            Name = "Deposit to Card Number",
            Method = "007",
            Fields = ParseToDictionary("P1=5299640000136129&P2=100000000&P3=&P4=0&P8=initial ")
        },
        new M2RequestDummy()
        {
            Name = "Message Response Lookup",
            Method = "008",
            Fields = ParseToDictionary("P1=00005009237171925  ")
        },
        new M2RequestDummy()
        {
            Name = "View Statement Summary",
            Method = "009",
            Fields = ParseToDictionary("P1=5299640000136129&P2=11/05/2008&P3=12/17/2008")
        },
        new M2RequestDummy()
        {
            Name = "View Statement Details by Card Number",
            Method = "010",
            Fields = ParseToDictionary("P1=5299640000136129&P2=03/01/2004&P3=06/07/2004&P4=0 ")
        },
        new M2RequestDummy() ///////!!!!!!!!!!!!!!!!!
        {
            Name = "Assign Card to Cardholder",
            Method = "011",
            Fields = ParseToDictionary("P1=CCH ID&P2=5299640000136129&P3= ")
        },
        new M2RequestDummy()
        {
            Name = "Validate PIN",
            Method = "012",
            Fields = ParseToDictionary("P1=5299640000136129&P2=384740SW0000 ")
        },
        //10 = new M2RequestDummy()
        //{
        //    Method = "022",
        //    Fields = ParseToDictionary("")
        //},
        new M2RequestDummy()
        {
            Name = "Get Card Status",
            Method = "023",
            Fields = ParseToDictionary("P1=4023540100000041&P2= ")
        },
        new M2RequestDummy()
        {
            Name = "Get Card Account Balance",
            Method = "024",
            Fields = ParseToDictionary("P1=5299640000136129")
        },
        new M2RequestDummy()
        {
            Name = "Cardholder Fees",
            Method = "030",
            Fields = ParseToDictionary("P1=5299640000136129&P2=100&P3=FeeCharge")
        },
        new M2RequestDummy()
        {
            Name = "Adjustment",
            Method = "031",
            Fields = ParseToDictionary("P1=5299640000136129&P2=200&P3=100&P4=ServiceFee ")
        },
        new M2RequestDummy()
        {
            Name = "Card Linking",
            Method = "032",
            Fields = ParseToDictionary("P1=4023540100000041&P2=4023540100000058 ")
        },
        new M2RequestDummy()
        {
            Name = "Card Inquiry",
            Method = "033",
            Fields = ParseToDictionary("P1=5299640000136129")
        },
        new M2RequestDummy()
        {
            Name = "Cash Out",
            Method = "034",
            Fields = ParseToDictionary("P1=5299640000136129&P2=charles&P3=none&P4=wenkel&P5=address1&P6=address2&P7=miami&P8=fl&P9=33456&P10=US ")
        },
        new M2RequestDummy()
        {
            Name = "Card to Card Transfer",
            Method = "035",
            Fields = ParseToDictionary("P1=5299640000136129&P2=200&P3=GBP&P4=5299640000136145")
        },
        new M2RequestDummy()
        {
            Name = "Update Cardholder",
            Method = "036",
            Fields = ParseToDictionary("P1=Mendez&P2=none&P3=Salazara&P4=address1&P5=adress2&P6=miami&P7=fl&P8=33456&P9=us&P10=8866890679&P11=07041979&P12=1234567890&P13=5299640000136129")
        },
        new M2RequestDummy()
        {
            Name = "PIN Reset",
            Method = "37",
            Fields = ParseToDictionary("P1=5299640000136129")
        },
        new M2RequestDummy()
        {
            Name = "PIN Request",
            Method = "PINRequest",
            Fields = ParseToDictionary("P1=5299640000136129")
        },
        //23 = new M2RequestDummy()
        //{
        //    Method = "039",
        //    Fields = ParseToDictionary("")
        //},
        new M2RequestDummy()
        {
            Name = "Unlock Bad PIN Tries",
            Method = "040",
            Fields = ParseToDictionary("P1=5299640000136129")
        },
        //25 = new M2RequestDummy()
        //{
        //    Method = "056",
        //    Fields = ParseToDictionary("")
        //},
        new M2RequestDummy()
        {
            Name = "Request Direct Deposit Number",
            Method = "057",
            Fields = ParseToDictionary("P1=5299640000136129")
        },
        new M2RequestDummy()
        {
            Name = "Get Real-Time Currency Exchange Rates",
            Method = "Multicurrency",
            Fields = ParseToDictionary("To=USD&From=USD ")
        },
        //new M2RequestDummy()
        //{
        //    Method = "InstantCardIssue",
        //    Fields = ParseToDictionary("")
        //},
        //new M2RequestDummy()
        //{
        //    Method = "BankAccountUpdate",
        //    Fields = ParseToDictionary("")
        //},
        //new M2RequestDummy()
        //{
        //    Method = "BankAccountInsert",
        //    Fields = ParseToDictionary("")
        //},
        //new M2RequestDummy()
        //{
        //    Method = "BankAccountInquiry",
        //    Fields = ParseToDictionary("")
        //},
        //new M2RequestDummy()
        //{
        //    Method = "BankAccountTransfer",
        //    Fields = ParseToDictionary("")
        //},
        new M2RequestDummy()
        {
            Name = "Validate Cardholder Verification Data (CVV2)",
            Method = "ValidateCVV2",
            Fields = ParseToDictionary("P1=5299640000136129&P2=1612&P3=111")
        },
        new M2RequestDummy()
        {
            Name = "Get Dynamic CVV (CVV3)",
            Method = "GetCVV3",
            Fields = ParseToDictionary("P1=5299640000136129&P2=1612&P3=111")
        },
        new M2RequestDummy()
        {
            Name = "Validate Micro Deposit",
            Method = "ValidateMicroDeposit",
            Fields = ParseToDictionary("P1=5299640000136129&P2=854094")
        },
        new M2RequestDummy()
        {
            Name = "Change Password",
            Method = "ChangePswd",
            Fields = ParseToDictionary("P1=newapipswd")
        },
        new M2RequestDummy()
        {
            Name = "Message Heart Beat",
            Method = "025",
            Fields = ParseToDictionary("")
        }
        };

        foreach (var request in requests)
        {
            var result = ExecuteM2Request<M2RequestDummy, object>(request);
        }
        DisplayDetailedData(new Data() { Items = requests.ToList() }, dataContainer);
    }
}

[Serializable]
[DataContract]
public class Data
{
    [DataMember]
    public List<M2RequestDummy> Items { get; set; } 
}

[Serializable]
[DataContract]
public class M2RequestDummy : M2Request
{
    



    public M2RequestDummy() { }

    public Dictionary<string, string> Fields { get; set; }
    
    [DataMember]
    public string Method { get; set; }

    [DataMember]
    public string Request
    {
        get
        {
            return base.GetQuery();
        }
    }
    [DataMember]
    public string Reponse
    {
        get
        {
            return HttpUtility.HtmlEncode(Result);
        }
    }
    [DataMember]
    public string Comment
    {
        get
        {
            return "";
        }
    }



    [DataMember]
    public string Status
    {
        get
        {
            return Result != null && Result.Contains("0000") ? "Ok" : "Not Ok";
        }
    }

    [DataMember]
    public string Name
    {
        get;
        set;
    }

    public override void AddFields(Dictionary<string, string> dict)
    {
        foreach (var p in Fields)
        {
            dict.Add(p.Key, p.Value);
        }
    }

    public override string GetFunc()
    {
        return Method;
    }
}