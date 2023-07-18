using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Summary description for APIKeyMessageHandler
/// </summary>
public class APIKeyMessageHandler:DelegatingHandler
{
    public APIKeyMessageHandler()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private const string APIKeyToCheck = "86842a38-310c-48cf-a64a-98ef66c1e775";

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
    {
        bool validKey = false;
        IEnumerable<string> requestHeaders;
        var checkAPIKeyExists = httpRequestMessage.Headers.TryGetValues("APIKey", out requestHeaders);
        if (checkAPIKeyExists)
        {
            if (requestHeaders.FirstOrDefault().Equals(APIKeyToCheck))
            {
                validKey = true;
            }
        }

        if (!validKey)
        {
            return httpRequestMessage.CreateResponse(System.Net.HttpStatusCode.Forbidden, "Invalid API Key");
        }

        var response = await base.SendAsync(httpRequestMessage, cancellationToken);
        return response;
    }
}