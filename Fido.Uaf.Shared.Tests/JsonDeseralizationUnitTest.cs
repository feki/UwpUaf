using Fido.Uaf.Shared;
using Fido.Uaf.Shared.Messages;
using Fido.Uaf.Shared.Utils;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Tests
{
    [TestClass]
    public class JsonDeseralizationUnitTest
    {
        [TestMethod]
        public void RegistrationRequest_Deserialize_DontThrowException()
        {
            var regUtils = new RegistrationUtils();
            var regRequest = regUtils.ParseRegistrationRequestUafMessage(Samples.RegisterRequestJson);
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(regRequest));
            Assert.IsInstanceOfType(regRequest, typeof(RegistrationRequest));
        }
    }
}
