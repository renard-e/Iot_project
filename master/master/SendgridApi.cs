using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace master
{
    class SendgridApi
    {

        public void sendEmailAlert()
        {
            var client = new RestClient("https://api.sendgrid.com/v3/mail/send");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", "Bearer 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");
            request.AddParameter("application/json", "{\"personalizations\":[{\"to\":[{\"email\":\"gregoire.renard@epitech.eu\",\"name\":\"greg\"}],\"subject\":\"Employee arrive\"}],\"from\":{\"email\":\"gregoire.renard@epitech.eu\",\"name\":\"IOT project\"},\"reply_to\":{\"email\":\"gregoire.renard@epitech.eu\",\"name\":\"IOT project\"},\"content\":[{\"type\":\"text/plain\",\"value\":\"ALERT INTRUSION AT " + DateTime.Now.ToString("H:mm, yyyy/MM/dd") + "\"}]}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine("Status code response send mail : " + response.StatusCode + "\ndescription : " + response.StatusDescription);
        }

        public void sendEmail(String nameComing)
        {
            if (!String.IsNullOrEmpty(nameComing))
            {
                var client = new RestClient("https://api.sendgrid.com/v3/mail/send");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", "Bearer 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");
                request.AddParameter("application/json", "{\"personalizations\":[{\"to\":[{\"email\":\"gregoire.renard@epitech.eu\",\"name\":\"greg\"}],\"subject\":\"Employee arrive\"}],\"from\":{\"email\":\"gregoire.renard@epitech.eu\",\"name\":\"IOT project\"},\"reply_to\":{\"email\":\"gregoire.renard@epitech.eu\",\"name\":\"IOT project\"},\"content\":[{\"type\":\"text/plain\",\"value\":\"The employee : " + nameComing + " arrive at work at " + DateTime.Now.ToString("H:mm, yyyy/MM/dd") + "\"}]}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Console.WriteLine("Status code response send mail : " + response.StatusCode + "\ndescription : " + response.StatusDescription);
            }
        }

        public void sendEmailExit(String nameComing)
        {
            if (!String.IsNullOrEmpty(nameComing))
            {
                var client = new RestClient("https://api.sendgrid.com/v3/mail/send");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", "Bearer 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");
                request.AddParameter("application/json", "{\"personalizations\":[{\"to\":[{\"email\":\"gregoire.renard@epitech.eu\",\"name\":\"greg\"}],\"subject\":\"Employee arrive\"}],\"from\":{\"email\":\"gregoire.renard@epitech.eu\",\"name\":\"IOT project\"},\"reply_to\":{\"email\":\"gregoire.renard@epitech.eu\",\"name\":\"IOT project\"},\"content\":[{\"type\":\"text/plain\",\"value\":\"The employee : " + nameComing + " leaves work at " + DateTime.Now.ToString("H:mm, yyyy/MM/dd") + "\"}]}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Console.WriteLine("Status code response send mail : " + response.StatusCode + "\ndescription : " + response.StatusDescription);
            }
        }
    }
}
