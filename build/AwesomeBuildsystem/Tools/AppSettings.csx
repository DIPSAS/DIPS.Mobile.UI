#r "nuget:Newtonsoft.Json, 13.0.3"
#r "../DIPS.Buildsystem.Core.dll"

using Newtonsoft.Json;

internal class AppSettings
    {
        private string m_scopes = "openid offline_access";

        public AppSettings(string scopes, string redirectUri, string postLogoutRedirectUri)
        {
            Scopes = scopes;
            RedirectUri = redirectUri;
            PostLogoutRedirectUri = postLogoutRedirectUri;
        }

        [JsonProperty("RedirectUri")] public string RedirectUri { get; }
        [JsonProperty("PostLogoutRedirectUri")] public string PostLogoutRedirectUri { get; }

        public string Scopes
        {
            get => m_scopes;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }

                m_scopes = value;

                if (!Scopes.Contains("openid"))
                {
                    m_scopes += " openid";
                }
                
                if (!Scopes.Contains("offline_access"))
                {
                    m_scopes += " offline_access";
                }
            }
        }

        [JsonProperty("Restrictions")] public List<Restriction> Restrictions { get; set; } = new();
        [JsonProperty("ProductVersion")] public string ProductVersion { get; set; } = string.Empty;
        [JsonProperty("ProductIdentifier")] public string ProductIdentifier { get; set; } = string.Empty;
    }

    internal class Restriction
    {
        public string Key { get; set; }

        public string Title { get; set; }

        public string RestrictionType { get; set; }
        
        [JsonProperty("defaultValue")] public string Value { get; set; }

        public string Description { get; set; }

        public List<RestrictionOption> Options { get; set; } = new();

        public bool IsRequired { get; set; }

        public bool Anonymous { get; set; } = true;
    }

    internal class RestrictionOption
    {
        public RestrictionOption(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }