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
        public void sendEmail(String nameComing)
        {
            var client = new RestClient("https://api.sendgrid.com/v3/mail/send");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", "Bearer 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");
            request.AddParameter("application/json", "{\"personalizations\":[{\"to\":[{\"email\":\"gregoire.renard@epitech.eu\",\"name\":\"greg\"}],\"subject\":\"Employee arrive\"}],\"from\":{\"email\":\"gregoire.renard@epitech.eu\",\"name\":\"IOT project\"},\"reply_to\":{\"email\":\"gregoire.renard@epitech.eu\",\"name\":\"IOT project\"},\"content\":[{\"type\":\"text/plain\",\"value\":\"The employee : " + nameComing + " arrive at work at " + DateTime.Now.ToString("H:mm, yyyy/MM/dd") + "\"}]}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
        }
    }
}
