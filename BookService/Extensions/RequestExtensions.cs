using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookService.Extensions
{
    public static class RequestExtensions
    {
        public static Guid GetUserAccessToken(this HttpRequestMessage request)
        {
            IEnumerable<string> tokenStrings;
            request.Headers.TryGetValues("x-user-token", out tokenStrings);

            Guid token;

            if (Guid.TryParse(tokenStrings.FirstOrDefault(), out token)) {
                return token;
            }

            throw new HttpResponseException(HttpStatusCode.Forbidden);
        }
    }
}