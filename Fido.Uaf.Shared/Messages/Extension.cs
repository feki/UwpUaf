namespace Fido.Uaf.Shared.Messages
{
    /// <summary>
    /// FIDO extensions can appear in several places, including the UAF protocol messages,
    /// authenticator commands, or in the assertion signed by the authenticator.
    /// 
    /// Each extension has an identifier, and the namespace for extension identifiers is FIDO
    /// UAF global (i.e. doesn't depend on the message where the extension is present). 
    /// 
    /// Extensions can be defined in a way such that a processing entity which doesn't
    /// understand the meaning of a specific extension MUST abort processing, or they
    /// can be specified in a way that unknown extension can (safely) be ignored.
    /// 
    /// Extension processing rules are defined in each section where extensions are allowed.
    /// 
    /// Generic extensions used in various operations.
    /// </summary>
    public class Extension
    {
        private string id;
        private string data;
        private bool failIfUnknown;

        /// <summary>
        /// Identifies the extension.
        /// </summary>
        /// <remarks>
        /// string[1..32]
        /// </remarks>
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Contains arbitrary data with a semantics agreed between server and client. The data is base64url-encoded.
        /// </summary>
        /// <remarks>
        /// This field MAY be empty.
        /// </remarks>
        public string Data
        {
            get { return data; }
            set { data = value; }
        }

        /// <summary>
        /// Indicates whether unknown extensions must be ignored (`false`) or must lead to an error (`true`).
        /// 
        ///  * A value of false indicates that unknown extensions MUST be ignored
        ///  * A value of true indicates that unknown extensions MUST result in an error.
        /// </summary>
        /// <note>
        /// The FIDO UAF Client might (a) process an extension or (b) pass the extension through to the ASM.
        /// Unknown extensions must be passed through.
        /// 
        /// The ASM might (a) process an extension or (b) pass the extension through to the FIDO authenticator.
        /// Unknown extensions must be passed through.
        /// 
        /// The FIDO authenticator must handle the extension or ignore it (only if it doesn't know how to handle
        /// it and `fail_if_unknown` is not set). If the FIDO authenticator doesn't understand the meaning of the
        /// extension and `fail_if_unknown is set`, it must generate an error (see definition of `fail_if_unknown` above).
        /// 
        /// When passing through an extension to the next entity, the `fail_if_unknown` flag must be preserved
        /// (see [UAFASM](https://fidoalliance.org/specs/fido-uaf-v1.0-ps-20141208/fido-uaf-asm-api-v1.0-ps-20141208.html)
        /// [UAFAuthnrCommands](https://fidoalliance.org/specs/fido-uaf-v1.0-ps-20141208/fido-uaf-authnr-cmds-v1.0-ps-20141208.html)).
        /// 
        /// FIDO protocol messages are not signed. If the security depends on an extension being known or processed,
        /// then such extension should be accompanied by a related (and signed) extension in the authenticator assertion
        /// (e.g. `TAG_UAFV1_REG_ASSERTION`, `TAG_UAFV1_AUTH_ASSERTION`). If the security has been increased (e.g. the
        /// FIDO authenticator according to the description in the metadata statement accepts multiple fingers but
        /// in this specific case indicates that the finger used at registration was also used for authentication)
        /// there is no need to mark the extension as `fail_if_unknown` (i.e. tag 0x3E12 should be used
        /// [UAFAuthnrCommands](https://fidoalliance.org/specs/fido-uaf-v1.0-ps-20141208/fido-uaf-authnr-cmds-v1.0-ps-20141208.html)).
        /// If the security has been degraded (e.g. the FIDO authenticator according to the description in the metadata
        /// statement accepts only the finger used at registration for authentication but in this specific case indicates
        /// that a different finger was used for authentication) the extension must be marked as `fail_if_unknown`
        /// (i.e. tag 0x3E11 must be used [UAFAuthnrCommands](https://fidoalliance.org/specs/fido-uaf-v1.0-ps-20141208/fido-uaf-authnr-cmds-v1.0-ps-20141208.html)). 
        /// </note>
        public bool FailIfUnknown
        {
            get { return failIfUnknown; }
            set { failIfUnknown = value; }
        }
    }
}