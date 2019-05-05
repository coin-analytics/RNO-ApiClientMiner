using System;
using System.IO;
using System.Text;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace coinminner
{
    public class CustomAPI
    {
        private string BaseURI = "https://api.coinanalytics.dev/";

        private String KickLog(string wallet, int weight, int thread, CPUReport report)
        {
            try
            {
                StringBuilder APIkick = new StringBuilder();
                string archi = report.Name;
                int hertz = 1;
                
                APIkick.Append("wallet=" + wallet);
                APIkick.Append("weight=" + weight.ToString());
                APIkick.Append("archi" + report.Name);
                APIkick.Append("hertz" + $"{report.Hertz:.2f}");
                APIkick.Append("threads=" + thread);

                byte[] bytes = Encoding.UTF8.GetBytes(APIkick.ToString());
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(this.BaseURI + "/api/report/kick"));
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.ContentLength = (long)bytes.Length;

                Stream requestStream = httpWebRequest.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();

                string AXC = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.Default).ReadToEnd();
                string ErrorAXC = JObject.Parse(AXC)["message"].ToString();
                if (JObject.Parse(AXC)["status"].ToString() == "9001")
                {
                    return ErrorAXC;
                }
            }
            catch (Exception ex)
            {
                return "CA : 서버가 응답하지 않습니다.(Code: `SERVER_0x00d1`)" + Environment.NewLine +
                       "지속적인 오류 발생 시 다음 메시지를 [ c01n.4n4lyt1cs@gmail.com ]에 제보해주시기 바랍니다." +
                       Environment.NewLine + Environment.NewLine + ex.Message;
            }

            return "";
        }
    }
}