namespace Fido.Uaf.Shared.Messages
{
    /// <summary>
    /// Represents a generic version with major and minor fields.
    /// </summary>
    public class Version
    {
        private readonly short major = 1;
        private readonly short minor = 0;

        /// <summary>
        /// Major version for specification.
        /// </summary>
        /// <note>
        /// For FIDO 1.0 specification:
        /// Major version MUST be 1.
        /// </note>
        public short Major
        {
            get { return major; }
        }

        /// <summary>
        /// Minor version for specification.
        /// </summary>
        /// <note>
        /// For FIDO 1.0 specification:
        /// Minor version MUST be 0.
        /// </note>
        public short Minor
        {
            get { return minor; }
        }
    }
}
