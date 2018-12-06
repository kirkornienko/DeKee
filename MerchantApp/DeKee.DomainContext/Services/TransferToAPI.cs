using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DeKee.DomainContext.Services
{
    public class TransferToAPI : BaseAPI, ITransferToService
    {
        private string transferToLogin = "bank4you";
        private string transferToToken = "6Rne9HAotc";

        public static class MethodURIs
        {
            public static readonly string TopUp = "";
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            StringBuilder sBuilder = new StringBuilder();


            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private string generateRequestKey(string transactionId)
        {
            var source = $"{transferToLogin}{transferToToken}{transactionId}";
            using (MD5 md5Hash = MD5.Create())
            {
                return GetMd5Hash(md5Hash, source);
            }
            
        }
        

        public Dto.CheckResponse TopUpCheck(string phoneNumber)
        {
            var dataF = @"<xml>
<login>{0}</login>
<key>{1}</key>
<md5>{2}</md5>
<destination_msisdn>{3}</destination_msisdn>
<delivered_amount_info>1</delivered_amount_info>
<return_service_fee>1</return_service_fee>
<action>msisdn_info</action>
</xml>";
            var transId = getTransactionId();
            var data = string.Format(dataF, transferToLogin, transId, generateRequestKey(transId), phoneNumber);

            //var response = POST(MethodURIs.TopUp, data);
            var responseDto = POSTXmlDto<Dto.CheckResponse>("", data);
            return responseDto;
        }

        

        private string getTransactionId()
        {
            return DateTime.Now.Ticks.ToString();
        }

        public Dto.TopUpResponse TopUpConfirm(Dto.TopUpRequest input)
        {
            var dataF = @"<xml>
  <login>{0}</login>
  <key>{1}</key>
  <md5>{2}</md5>
  <msisdn>{3}</msisdn>
  <sms>Bank4You Live</sms>
  <destination_msisdn>{4}</destination_msisdn>
  <product>{5}</product>
  <sender_sms>no</sender_sms>
  <sender_text></sender_text>
  <delivered_amount_info>1</delivered_amount_info>
  <reserved_id>{6}</reserved_id>
  <return_timestamp>1</return_timestamp>
  <return_version>1</return_version>
  <return_service_fee>1</return_service_fee>
  <action>topup</action>
</xml>";
            var int_prod = int.TryParse(input.product, out int i) ? i.ToString() : input.product.Split('.', ',')[0];
            var transId = getTransactionId();
            var data = string.Format(dataF, transferToLogin, transId, generateRequestKey(transId), 
                input.msisdn, input.destination_msisdn, int_prod, input.ReservedId);

            //var response = POST(MethodURIs.TopUp, data);
            var responseDto = POSTXmlDto<Dto.TopUpResponse>("", data);
            return responseDto;
            
        }

        public Dto.ReserveIdResponse TopUpPrepare()
        {
            var dataF = @"<xml>
<login>{0}</login>
<key>{1}</key>
<md5>{2}</md5>
<action>reserve_id</action>
</xml>	";

            var transId = getTransactionId();
            var data = string.Format(dataF, transferToLogin, transId, generateRequestKey(transId));

            //var response = POST(MethodURIs.TopUp, data);
            var responseDto = POSTXmlDto<Dto.ReserveIdResponse>("", data);
            return responseDto;
        }
    }
}
