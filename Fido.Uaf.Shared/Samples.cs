namespace Fido.Uaf.Shared
{
    public class Samples
    {
        public const string RegisterRequestJson = @"[{
    ""header"": {
        ""upv"": {
            ""major"": 1,
            ""minor"": 0
        },
        ""op"": ""Reg"",
        ""appID"": ""https://test.uwpuaf.com:8443/fido/uwpUaf/facets"",
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

        public const string AuthenticationRequestJson = @"[{
    ""header"": {
        ""upv"": {
            ""major"": 1,
            ""minor"": 0
        },
        ""op"": ""Auth"",
        ""appID"": ""https://test.uwpuaf.com:8443/fido/uwpUaf/facets"",
        ""serverData"": ""5s7n8-7_LDAtRIKKYqbAtTTOezVKCjl2mPorYzbpxRrZ-_3wWroMXsF_pLYjNVm_l7bplAx4bkEwK6ibil9EHGfdfKOQ1q0tyEkNJFOgqdjVmLioroxgThlj8Istpt7q""
    },
    ""challenge"": ""HQ1VkTUQC1NJDOo6OOWdxewrb9i5WthjfKIehFxpeuU"",
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
        }]
    }
}]
";

        public const string DeregistrationRequestJson = @"[{
    ""header"": {
        ""op"": ""Dereg"",
        ""upv"": {
            ""major"": 1,
            ""minor"": 0
        },
        ""appID"": ""https://test.uwpuaf.com:8443/fido/uwpUaf/facets""
    },
    ""authenticators"": [{
        ""aaid"": ""ABCD#ABCD"",
        ""keyID"": ""ZMCPn92yHv1Ip-iCiBb6i4ADq6ZOv569KFQCvYSJfNg""
    }]
}]";
    }
}
