using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMR11.Core
{
    public class StatusRedirectionHandler : DelegatingHandler
    {
        public StatusRedirectionHandler()
            : base(new HttpClientHandler())
        {
        }

        public StatusRedirectionHandler(HttpMessageHandler innerHandler)
        {
            base.InnerHandler = innerHandler;
        }


        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            
            if (response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
            {
                Console.WriteLine("Changing status code.");

                response.StatusCode = System.Net.HttpStatusCode.OK;
                response.ReasonPhrase = "OK";
            }

            return response;
        }
    }
}
