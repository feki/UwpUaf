 * RegistrationRequest:

    ```
    dictionary RegistrationRequest {
        required OperationHeader header;
        required ServerChallenge challenge;
        required DOMString       username;
        required Policy          policy;
    };
    ```

 * AuthenticationRequest:

    ```
    dictionary AuthenticationRequest {
        required OperationHeader header;
        required ServerChallenge challenge;
        Transaction[]            transaction;
        required Policy          policy;
    };
    ```

 * DeregistrationRequest

    ```
    dictionary DeregistrationRequest {
        required OperationHeader           header;
        required DeregisterAuthenticator[] authenticators;
    };
    ```

Spoločné, čo majú tieto správy je `OperationHeader`. Preto je nutné najskôr deserializovať túto časť správy a z hlavičky zistiť verziu protokolu (z vlastností `header.uvp.major` a `header.uvp.minor`). Vyberáme len protokol verzie `1.0`.

 * OperationHeader

    ```
    dictionary OperationHeader {
        required Version   upv;
        required Operation op;
        DOMString          appID;
        DOMString          serverData;
        Extension[]        exts;
    };
    ```
