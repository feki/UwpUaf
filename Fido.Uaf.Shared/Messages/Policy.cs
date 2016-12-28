using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Messages
{
    /// <summary>
    /// Contains a specification of accepted authenticators and a specification of disallowed authenticators.
    /// </summary>
    public class Policy
    {
        /// <summary>
        /// This field is a two-dimensional array describing the required authenticator characteristics
        /// for the server to accept either a FIDO registration, or authentication operation for a particular purpose.
        ///
        /// This two-dimensional array can be seen as a list of sets. List elements (i.e.the sets) are alternatives(OR condition).
        ///
        /// All elements within a set MUST be combined:
        ///
        /// The first array index indicates OR conditions(i.e.the list). Any set of authenticator(s)
        /// satisfying these MatchCriteria in the first index is acceptable to the server for this operation.
        ///
        /// Sub-arrays of MatchCriteria in the second index(i.e.the set) indicate that multiple
        /// authenticators(i.e.each set element) MUST be registered or authenticated to be accepted by the server.
        ///
        /// The MatchCriteria array represents ordered preferences by the server.Servers MUST put their preferred
        /// authenticators first, and FIDO UAF Clients SHOULD respect those preferences, either by presenting
        /// authenticator options to the user in the same order, or by offering to perform the operation
        /// using only the highest-preference authenticator(s).
        /// </summary>
        [JsonProperty("accepted", Required = Required.Always)]
        public MatchCriteria[][] Accepted { get; set; }

        /// <summary>
        /// Any authenticator that matches any of MatchCriteria contained in the field disallowed
        /// MUST be excluded from eligibility for the operation, regardless of whether it matches
        /// any MatchCriteria present in the Accepted list, or not.
        /// </summary>
        [JsonProperty("disallowed", NullValueHandling = NullValueHandling.Ignore)]
        public MatchCriteria[] Disallowed { get; set; }
    }
}
