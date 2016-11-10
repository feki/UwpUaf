namespace Fido.Uaf.Shared.Messages.Asm.Objects
{
    public class AuthenticateOut
    {
        /// <summary>
        /// FIDO UAF authenticator registration assertion, base64url-encoded.
        /// </summary>
        public string Assertion { get; set; }

        /// <summary>
        /// AssertionScheme identifiers are defined in the UAF Protocol specification[UAFProtocol].
        /// </summary>
        public string AssertionScheme { get; set; }
    }
}
