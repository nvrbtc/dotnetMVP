namespace dotnetMVP.Types
{
    public class JwtOptions
    {
        public string SymmetricKey { get; set; } = null!;
        public string ValidAudience { get; set; } = null!;
        public string ValidIssuer { get; set; } = null!;

        public bool ValidateLifetime { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateKey { get; set; }

        public List<string> ValidAlgotrithms = []; //Get actual name from SecureAlgorithm
    }
}
