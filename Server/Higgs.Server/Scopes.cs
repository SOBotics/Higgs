using System.Collections.Generic;

namespace Higgs.Server
{
    public static class Scopes
    {
        public const string SCOPE_ADMIN = "admin";
        public const string SCOPE_DEV = "dev";
        public const string SCOPE_BOT_OWNER = "bot_owner";
        public const string SCOPE_BOT = "bot";
        public const string SCOPE_REVIEWER = "reviewer";

        public static Dictionary<string, string> AllScopes = new Dictionary<string, string>
        {
            {SCOPE_ADMIN, "Admin"},
            {SCOPE_DEV, "Dev"},
            {SCOPE_BOT_OWNER, "Bot owner"},
            {SCOPE_BOT, "Bot"},
            {SCOPE_REVIEWER, "Reviewer"}
        };
    }
}