namespace Components.Services
{
    internal class Release
    {
        public Release(int id, string version, DateTime uploadedAt, Uri installUri)
        {
            Id = id;
            Version = version;
            UploadedAt = uploadedAt;
            InstallUri = installUri;
        }
        
        public int Id { get; }
        public string Version { get; }
        public DateTime UploadedAt { get; }
        public Uri InstallUri { get; }
    }
}