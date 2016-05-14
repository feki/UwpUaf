using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages
{
    public class RgbPalletteEntry
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("r")]
        public ushort Red { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("g")]
        public ushort Green { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("b")]
        public ushort Blue { get; set; }
    }
}