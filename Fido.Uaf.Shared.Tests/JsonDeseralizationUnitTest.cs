﻿using Fido.Uaf.Shared.Messages;
using Fido.Uaf.Shared.Utils;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Newtonsoft.Json;

namespace Fido.Uaf.Shared.Tests
{
    [TestClass]
    public class JsonDeseralizationUnitTest
    {
        private string registerRequestJson = @"[{
    ""header"": {
        ""upv"": {
            ""major"": 1,
            ""minor"": 0
        },
        ""op"": ""Reg"",
        ""appID"": ""https://uaf-test-1.noknoktest.com:8443/SampleApp/uaf/facets"",
        ""serverData"": ""IjycjPZYiWMaQ1tKLrJROiXQHmYG0tSSYGjP5mgjsDaM17RQgq0dl3NNDDTx9d-aSR_6hGgclrU2F2Yj-12S67v5VmQHj4eWVseLulHdpk2v_hHtKSvv_DFqL4n2IiUY6XZWVbOnvg""
    },
    ""challenge"": ""H9iW9yA9aAXF_lelQoi_DhUk514Ad8Tqv0zCnCqKDpo"",
    ""username"": ""apa"",
    ""policy"": {
        ""accepted"": [
            [{
                ""userVerification"": 512,
                ""keyProtection"": 1,
                ""tcDisplay"": 1,
                ""authenticationAlgorithms"": [
                    1
                ],
                ""assertionSchemes"": [
                    ""UAFV1TLV""
                ]
            }],
            [{
                ""userVerification"": 4,
                ""keyProtection"": 1,
                ""tcDisplay"": 1,
                ""authenticationAlgorithms"": [
                    1
                ],
                ""assertionSchemes"": [
                    ""UAFV1TLV""
                ]
            }],
            [{
                ""userVerification"": 4,
                ""keyProtection"": 1,
                ""tcDisplay"": 1,
                ""authenticationAlgorithms"": [
                    2
                ]
            }],
            [{
                ""userVerification"": 2,
                ""keyProtection"": 4,
                ""tcDisplay"": 1,
                ""authenticationAlgorithms"": [
                    2
                ]
            }],
            [{
                ""userVerification"": 4,
                ""keyProtection"": 2,
                ""tcDisplay"": 1,
                ""authenticationAlgorithms"": [
                    1,
                    3
                ]
            }],
            [{
                ""userVerification"": 2,
                ""keyProtection"": 2,
                ""authenticationAlgorithms"": [
                    2
                ]
            }],
            [{
                ""userVerification"": 32,
                ""keyProtection"": 2,
                ""assertionSchemes"": [
                    ""UAFV1TLV""
                ]
            }, {
                ""userVerification"": 2,
                ""authenticationAlgorithms"": [
                    1,
                    3
                ],
                ""assertionSchemes"": [
                    ""UAFV1TLV""
                ]
            }, {
                ""userVerification"": 2,
                ""authenticationAlgorithms"": [
                    1,
                    3
                ],
                ""assertionSchemes"": [
                    ""UAFV1TLV""
                ]
            }, {
                ""userVerification"": 4,
                ""keyProtection"": 1,
                ""authenticationAlgorithms"": [
                    1,
                    3
                ],
                ""assertionSchemes"": [
                    ""UAFV1TLV""
                ]
            }]
        ],
        ""disallowed"": [{
            ""userVerification"": 512,
            ""keyProtection"": 16,
            ""assertionSchemes"": [
                ""UAFV1TLV""
            ]
        }, {
            ""userVerification"": 256,
            ""keyProtection"": 16
        }, {
            ""aaid"": [
                ""ABCD#ABCD""
            ],
            ""keyIDs"": [
                ""RfY_RDhsf4z5PCOhnZExMeVloZZmK0hxaSi10tkY_c4""
            ]
        }]
    }
}]";

        [TestMethod]
        public void RegistrationRequest_Deserialize_DontThrowException()
        {
            var regUtils = new RegistrationUtils();
            var regRequest = regUtils.ParseRegistrationRequestUafMessage(registerRequestJson);
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(regRequest));
            Assert.IsInstanceOfType(regRequest, typeof(RegistrationRequest));
        }
    }
}
