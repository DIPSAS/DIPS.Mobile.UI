#load "../Logging/Logger.csx"
#load "altool.csx"
#r "nuget:Microsoft.IdentityModel , 7.0.0"
#r "nuget:System.IdentityModel.Tokens.Jwt, 7.4.1"
#r "nuget:BouncyCastle.Cryptography, 2.3.0"
#r "nuget:Newtonsoft.Json, 13.0.3"
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Parameters;
using System.Net.Http;
using System.Net.Http.Headers;
public static class AppStoreConnect
{
    public static string Jwt {get; private set;}

    public static Task<bool> UploadPackage(string ipaFilePath, string appleId, string bundleId, string bundleVersion, string apiKeyId, string apiKeyLocation, string apiIssuer, bool shouldExit = true)
    {
        return altool.UploadPackage(ipaFilePath, appleId, bundleId, bundleVersion, apiKeyId, apiKeyLocation, apiIssuer, shouldExit);
    }

    public static Task<bool> VerifyPackage(string ipaFilePath, string apiKeyId, string apiKeyLocation, string apiIssuer)
    {   
        return altool.ValidateApp(ipaFilePath, apiKeyId, apiKeyLocation, apiIssuer);
    }

    public static async Task<string> GetBuildId(string appleId, string version)
    {
        var httpClient = GetHttpClient();

        var response = await httpClient.GetAsync($"builds?filter[app]={appleId}");
        response.EnsureSuccessStatusCode();
        if(response != null){
            if (response.Content != null)
            {
                var json = await response.Content.ReadAsStringAsync();
                var anonymousTypeObject = new { 
                    Data = new[] 
                    { new { Id = "", Attributes =  new { Version = "" } } } 
                    };
                var builds = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
                foreach (var build in builds.Data)
                {
                    var isSame = build.Attributes.Version == version;
                    if(isSame){
                        return build.Id;
                    }
                }
                return "";
            }
        }
        return "";
    }

    public static async Task<string> GetWhatsNewId(string buildId)
    {
        var httpClient = GetHttpClient();

        var response = await httpClient.GetAsync($"betaBuildLocalizations?filter[build]={buildId}");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var anonymousTypeObject = new { 
            Data = new[]
            { new { Id = "", Attributes = new { WhatsNew = "", Locale = ""} } }
        };
        var betaBuildLocalizations = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
        return betaBuildLocalizations.Data.FirstOrDefault(data => data.Attributes.Locale == "no").Id;
    }


    public static async Task ModifyWhatsNew(string appleId, string version, string whatsNew)
    {
        var buildId = await GetBuildId(appleId,version);
        var whatsNewId = await GetWhatsNewId(buildId);
        await ModifyWhatsNew(whatsNewId, whatsNew);
    }

    public static async Task ModifyWhatsNew(string whatsNewId, string whatsNew)
    {
        var httpClient = GetHttpClient();

        var anonymousTypeObject = new { 
            data = new {
                id = whatsNewId,
                type = "betaBuildLocalizations",
                attributes = new {
                    whatsNew = whatsNew
                }
            }
        };

        var json = JsonConvert.SerializeObject(anonymousTypeObject);
        var response = await httpClient.PatchAsync($"betaBuildLocalizations/{whatsNewId}", new StringContent(json,Encoding.UTF8, "application/json"));
        response.EnsureSuccessStatusCode();
        
    }


    private static HttpClient GetHttpClient()
    {
        var httpclient = new HttpClient()
        {
            BaseAddress = new Uri("https://api.appstoreconnect.apple.com/v1/")
        };

        if(Jwt is null) {
            Logger.LogError($"Need to run {nameof(Authenticate)} before running this method", true);
        }
        else{
            if(!httpclient.DefaultRequestHeaders.Contains("Authorization"))
            {
                httpclient.DefaultRequestHeaders.Add("Authorization", Jwt);
            }
        }

        
        return httpclient;
    }

    public static string AuthenticateFromPrivateKeyFile(string privateKeyFilePath, string issuerId, string keyId, int expireInMinutes = 5)
    {
        var privateKey = File.ReadAllText(privateKeyFilePath).Replace("-----BEGIN PRIVATE KEY-----", "").Replace
            ("-----END PRIVATE KEY-----", "").Replace("\r", "");
        return Authenticate(privateKey, issuerId, keyId, expireInMinutes);
    }

    //https://developer.apple.com/documentation/appstoreconnectapi/generating_tokens_for_api_requests
    public static string Authenticate(string privateKey, string issuerId, string keyId, int expireInMinutes = 5)
    {
        //https://developer.apple.com/documentation/appstoreconnectapi/generating_tokens_for_api_requests#3878467
        if(expireInMinutes > 20)
        {
            throw new Exception("Can not expire more than 20 minutes");
        }
        var audience = "appstoreconnect-v1";
        var signatureAlgorithm = GetEllipticCurveAlgorithm(privateKey);

        var eCDsaSecurityKey = new ECDsaSecurityKey(signatureAlgorithm)
        {
            KeyId = keyId
        };

        var handler = new JwtSecurityTokenHandler();   
        JwtSecurityToken token = handler.CreateJwtSecurityToken(
            issuer: issuerId,
            audience: audience,
            expires: DateTime.UtcNow.AddMinutes(expireInMinutes), 
            issuedAt: DateTime.UtcNow,
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(eCDsaSecurityKey, SecurityAlgorithms.EcdsaSha256));

        return Jwt = token.RawData;
    }
    
    private static ECDsa GetEllipticCurveAlgorithm(string privateKey)
    {
        var keyParams = (ECPrivateKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKey));

        var normalizedEcPoint = keyParams.Parameters.G.Multiply(keyParams.D).Normalize();

        return ECDsa.Create(new ECParameters
        {
            Curve = ECCurve.CreateFromValue(keyParams.PublicKeyParamSet.Id),
            D = keyParams.D.ToByteArrayUnsigned(),
            Q =
            {
                X = normalizedEcPoint.XCoord.GetEncoded(),
                Y = normalizedEcPoint.YCoord.GetEncoded()
            }
        });
    }
}