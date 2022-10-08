using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTTEK.Contact.Business.HttpClients.Handler
{
    public class UpdateHeaderHandler : DelegatingHandler
    {
        //TODO : Alper Identity Server a vakit kalır ise header a token yazılacak vs vs vs.
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken);
        }
    }
}
