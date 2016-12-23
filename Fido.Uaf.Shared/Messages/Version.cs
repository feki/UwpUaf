using Newtonsoft.Json;
using System;

namespace Fido.Uaf.Shared.Messages
{
    /// <summary>
    /// Represents a generic version with major and minor fields.
    /// </summary>
    public class Version
    {
        /// <summary>
        /// Major version for specification.
        /// </summary>
        /// <note>
        /// For FIDO 1.0 specification:
        /// Major version MUST be 1.
        /// </note>
        [JsonProperty("major")]
        public ushort Major { get; set; } = 1;

        /// <summary>
        /// Minor version for specification.
        /// </summary>
        /// <note>
        /// For FIDO 1.0 specification:
        /// Minor version MUST be 0.
        /// </note>
        [JsonProperty("minor")]
        public ushort Minor { get; set; } = 1;

        public Version Clone()
        {
            return (Version)MemberwiseClone();
        }

        public static Version GetVersion_1_0()
        {
            return new Version { Major = 1, Minor = 0 };
        }
    }
}
