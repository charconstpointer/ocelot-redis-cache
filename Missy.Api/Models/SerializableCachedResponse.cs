using System;
using System.Collections.Generic;
using System.Net;
using Ocelot.Cache;

namespace Missy.Api.Models
{
    [Serializable]
    public class SerializableCachedResponse
    {
        public SerializableCachedResponse(CachedResponse cachedResponse)
        {
            StatusCode = cachedResponse.StatusCode;
            Headers = cachedResponse.Headers;
            ContentHeaders = cachedResponse.ContentHeaders;
            Body = cachedResponse.Body;
            ReasonPhrase = cachedResponse.ReasonPhrase;
        }

        public HttpStatusCode StatusCode { get; set; }

        public Dictionary<string, IEnumerable<string>> Headers { get; set; }

        public Dictionary<string, IEnumerable<string>> ContentHeaders { get; set; }

        public string Body { get; set; }

        public string ReasonPhrase { get; set; }
    }
}