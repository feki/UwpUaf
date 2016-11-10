namespace Fido.Uaf.Shared.Messages.Asm.Objects
{
    public class AppRegistration
    {
        /// <summary>
        /// The FIDO server Application Identity.
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// List of key identifiers associated with the AppId.
        /// </summary>
        public string[] KeyIds { get; set; }
    }
}
