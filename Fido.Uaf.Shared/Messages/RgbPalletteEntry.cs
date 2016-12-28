using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages
{
    public class RgbPalletteEntry
    {
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("r", NullValueHandling = NullValueHandling.Ignore)]
        public ushort Red { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("g", NullValueHandling = NullValueHandling.Ignore)]
        public ushort Green { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty("b", NullValueHandling = NullValueHandling.Ignore)]
        public ushort Blue { get; set; }
    }
}