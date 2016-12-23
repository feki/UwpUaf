namespace UwpUaf.Client.Api
{
    public static class Constants
    {
        /**
         * It's too long. App service name can be at most 39 characters long.
         *
         * public const string UwpUafClientOperationProtocolScheme = "org.fidoalliance.fido-operation.fido.uaf-client+json";
         */
        public const string UwpUafClientOperationProtocolScheme = "org.fidoalliance.fido.uaf-client+json";

        public const string ClientMessageKey = "message";
        public const string ClientDiscoveryDataKey = "discoveryData";
        public const string ClientErrorCodeKey = "errorCode";
        public const string ClientChannelBindingsKey = "channelBindings";
        public const string ClientResponseCodeKey = "responseCode";
        public const string UafIntentTypeKey = "UAFIntentType";
        public const string AuthenticatorIdToPackageFamilyNameDictionaryKey = "AuthenticatorIdToPackageFamilyNameDictionary";

        public static class UafIntentType
        {
            public const string Discover = "DISCOVER";
            public const string DiscoverResult = "DISCOVER_RESULT";
            public const string CheckPolicy = "CHECK_POLICY";
            public const string CheckPolicyResult = "CHECK_POLICY_RESULT";
            public const string UafOperation = "UAF_OPERATION";
            public const string UafOperationResult = "UAF_OPERATION_RESULT";
            public const string UafOperationCompletionStatus = "UAF_OPERATION_COMPLETION_STATUS";
        }
    }
}
