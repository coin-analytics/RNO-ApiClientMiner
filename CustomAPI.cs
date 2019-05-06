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
        private const string BaseURI = "https://api.coinanalytics.dev";

        public String KickLog(string wallet, string weight, int thread, CPUReport report)
        {
            try
            {
                StringBuilder APIkick = new StringBuilder();
                string archi = report.Name;
                int hertz = 1;
                
                APIkick.Append("wallet=" + wallet);
                APIkick.Append("&weight=" + weight);
                APIkick.Append("&archi=" + report.Name);
                APIkick.Append("&hertz=" + $"{report.Hertz:F2}");
                APIkick.Append("&threads=" + thread);

                byte[] bytes = Encoding.UTF8.GetBytes(APIkick.ToString());
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(CustomAPI.BaseURI + "/api/report/kick"));
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                httpWebRequest.ContentLength = (long)bytes.Length;

                Stream requestStream = httpWebRequest.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();

                string payload = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.Default).ReadToEnd();
                var jsonData = JObject.Parse(payload);
                if (jsonData["status"].ToString() != $"{1:D}")
                {
                    return jsonData["message"].ToString();
                }
            }
            catch (Exception ex)
            {
                return "CA : 서버가 응답하지 않습니다.(Code: `SERVER_0x00d1`)" + Environment.NewLine +
                       "지속적인 오류 발생 시 다음 메시지를 [ c01n.4n4lyt1cs@gmail.com ]에 제보해주시기 바랍니다." +
                       Environment.NewLine + Environment.NewLine + ex.ToString();
            }

            return "";
        }


        public String MinedCoin(string wallet, string weight, int cores, int solve_time, string coin, string hashString,
            int nonce, int nBit)
        {
            try
            {
                var caReport = new StringBuilder();
                caReport.Append("wallet=" + wallet);
                caReport.Append("&weight=" + weight);
                caReport.Append("&cores=" + cores);
                caReport.Append("&solve_time=" + solve_time);
                caReport.Append("&coin=" + coin);
                caReport.Append("&hashString=" + hashString);
                caReport.Append("&nonce=" + nonce);
                caReport.Append("&nBit=" + nBit);

                var bytes = Encoding.UTF8.GetBytes(caReport.ToString());
                var req = (HttpWebRequest) WebRequest.Create(new Uri(CustomAPI.BaseURI + "/api/report/mined"));

                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.ContentLength = (long) bytes.Length;

                var requestStream = req.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();

                var payload = new StreamReader(req.GetResponse().GetResponseStream(), Encoding.Default).ReadToEnd();
                var jsonData = JObject.Parse(payload);
                if (jsonData["status"].ToString() != $"{1:D}")
                {
                    return jsonData["message"].ToString();
                }
            }
            catch (Exception ex)
            {
                return "CA : 서버가 응답하지 않습니다.(Code: `SERVER_0x00d1`)" + Environment.NewLine +
                       "지속적인 오류 발생 시 다음 메시지를 [ c01n.4n4lyt1cs@gmail.com ]에 제보해주시기 바랍니다." +
                       Environment.NewLine + Environment.NewLine + ex.ToString();
            }

            return "";
        }

    }
}