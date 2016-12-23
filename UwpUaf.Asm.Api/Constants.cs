namespace UwpUaf.Asm.Api
{
    public static class Constants
    {
        /**
         * It's too long. App service name can be at most 39 characters long.
         *
         * public const string UwpUafAsmOperationProtocolScheme = "org.fidoalliance.fido-operation.fido.uaf-asm+json";
         */
        public const string UwpUafAsmOperationProtocolScheme = "org.fidoalliance.fido.uaf-asm+json";

        public const string AsmMessageKey = "message";
    }
}
