using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Fido.Uaf.Shared.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UwpUaf.Shared;
using UwpUaf.Asm.Api;
using UwpUaf.Client.Api.Facet;

namespace UwpUaf.Client.Api.Operations
{
    abstract class OperationBase
    {
        protected readonly OperationRequestBase operation;
        protected readonly ClientApi clientApi;
        protected readonly IAsmApi asmApi;

        protected static OperationRequestBase GetOperationRequestFromUafMessage(string uafMessage)
        {
            var message = UafMessageUtils.GetUafV10Message(uafMessage);
            var operationBase = UafMessageUtils.ParseMessage(message);

            return operationBase;
        }

        public ChannelBinding ChannelBinding { get; set; }
        public string CallerPackageFamilyName { get; set; }
        public IClientProtocolOperationHandlers Handlers { get; internal set; }

        public static OperationBase GetOperationFromUafMessage(string uafMessage)
        {
            var req = GetOperationRequestFromUafMessage(uafMessage);
            OperationBase op = null;
            switch (req.Header.Op)
            {
                case Operation.Reg:
                    op = new RegOperation(req as RegistrationRequest);
                    break;
                case Operation.Auth:
                    op = new AuthOperation(req as AuthenticationRequest);
                    break;
                case Operation.Dereg:
                    op = new DeregOperation(req as DeregistrationRequest);
                    break;
            }

            return op;
        }

        protected OperationBase(OperationRequestBase operation)
        {
            this.operation = operation;
            asmApi = new AsmApi();
            clientApi = ClientApi.Instance;
        }

        public bool CheckMandatoryFields()
        {
            return operation.ValidateMandatoryFields();
        }

        protected static async Task<bool> CheckTrustFacetIdAsync(string appId, string facetId)
        {
            if (appId.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                using (var http = new HttpClient())
                {
                    var uri = new Uri(appId);
                    var response = await http.GetAsync(uri);
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        return false;
                    }

                    var trustedFacetId = await response.Content.ReadAsStringAsync();
                    var facetIdListResponse = JsonConvert.DeserializeObject<FacetIdListResponse>(trustedFacetId);
                    var facetIdList = facetIdListResponse.TrustedFacets.FirstOrDefault(t => t.Version.Major == 1 && t.Version.Minor == 0);
                    if (facetIdList == null)
                    {
                        return false;
                    }

                    return facetIdList.Ids.Any(s => s.Equals(appId));
                }
            }
            else
            {
                return string.IsNullOrEmpty(appId) || appId.Equals(facetId);
            }
        }

        protected static string CreateFinalChallengeParams(RegAuthOperationRequestBase reg, ChannelBinding channelBinding, string facetId)
        {
            var appId = string.IsNullOrEmpty(reg.Header.AppId) ? facetId : reg.Header.AppId;

            var fcp = new FinalChallengeParams
            {
                AppId = appId,
                Challenge = reg.Challenge,
                ChannelBinding = channelBinding,
                FacetId = facetId
            };

            return fcp.SerializeToBase64UrlString();
        }

        public abstract Task<OperationResponseBase> ProcessOperationAsync();
    }
}
