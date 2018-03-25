using System.Collections.Generic;

namespace Higgs.Server
{
    public static class Scopes
    {
        public const string ADMIN_VIEW_BOT_DETAILS = "admin:viewBotDetails";

        public const string ADMIN_VIEW_SCOPES = "admin:viewScopes";

        public const string ADMIN_REGISTER_BOT = "admin:registerBot";
        public const string ADMIN_EDIT_BOT = "admin:editBot";
        public const string ADMIN_DEACTIVATE_BOT = "admin:deactivateBot";

        public const string ADMIN_EDIT_BOT_SCOPE = "admin:editBotScope";

        public const string ADMIN_VIEW_USER_DETAILS = "admin:viewUserDetails";
        public const string ADMIN_EDIT_USER_SCOPE = "admin:editUserScope";

        public const string REVIEWER_SEND_FEEDBACK = "reviewer:sendFeedback";

        public const string BOT_SET_FEEDBACK_TYPES = "bot:setFeedbackTypes";
        public const string BOT_REGISTER_POST = "bot:registerPost";

        public const string BOT_SEND_FEEDBACK = "bot:sendFeedback";

        public static Dictionary<string, string> AllScopes = new Dictionary<string, string>
        {
            {ADMIN_VIEW_BOT_DETAILS, "View all available scopes in the system"},

            {ADMIN_VIEW_SCOPES, "View details about registered bots"},

            {ADMIN_REGISTER_BOT, "Register a new bot with Higgs"},
            {ADMIN_EDIT_BOT, "Edit a bots configuration"},
            {ADMIN_DEACTIVATE_BOT, "Deactivate a bot"},
            {ADMIN_EDIT_BOT_SCOPE, "Edit a bots scopes"},

            {ADMIN_VIEW_USER_DETAILS, "View all user details"},
            {ADMIN_EDIT_USER_SCOPE, "Edit a users scopes"},
            
            {REVIEWER_SEND_FEEDBACK, "Send feedback to a reported post"},

            {BOT_SET_FEEDBACK_TYPES, "Add or update feedback types"},
            {BOT_REGISTER_POST, "Register a detected post"},

            {BOT_SEND_FEEDBACK, "Send feedback to a reported post on behalf of a user"},
        };
    }
}