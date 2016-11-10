namespace Fido.Uaf.Shared.Messages.Asm.Objects
{
    public class AuthenticateIn
    {
        /// <summary>
        /// The FIDO server Application Identity.
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// base64url [RFC4648] encoded keyIDs.
        /// </summary>
        public string[] KeyIds { get; set; }

        /// <summary>
        /// base64url [RFC4648] encoded final challenge.
        /// </summary>
        public string FinalChallenge { get; set; }

        //public Transaction[] Transactions { get; set; }
    }
}
