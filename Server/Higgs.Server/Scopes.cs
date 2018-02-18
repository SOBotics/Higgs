using System.Collections.Generic;

namespace Higgs.Server
{
    public static class Scopes
    {
	    public const string ADMIN_VIEW_BOT_DETAILS = "admin:viewBotDetails";

		public const string ADMIN_REGISTER_BOT = "admin:registerBot";
	    public const string ADMIN_EDIT_BOT = "admin:editBot";
	    public const string ADMIN_DEACTIVATE_BOT = "admin:deactivateBot";

	    public const string ADMIN_ADD_BOT_SCOPE = "admin:addBotScope";
	    public const string ADMIN_REMOVE_BOT_SCOPE = "admin:removeBotScope";

		public const string ADMIN_VIEW_USER_DETAILS = "admin:viewUserDetails";
	    public const string ADMIN_ADD_USER_SCOPE = "admin:addUserScope";
	    public const string ADMIN_REMOVE_USER_SCOPE = "admin:removeUserScope";

	    public const string REVIEWER_SEND_FEEDBACK = "reviewer:sendFeedback";

		public const string BOT_SET_FEEDBACK_TYPES = "bot:setFeedbackTypes";
	    public const string BOT_REGISTER_POST = "bot:registerPost";

	    public static Dictionary<string, string> AllScopes = new Dictionary<string, string>
	    {
		    { ADMIN_VIEW_BOT_DETAILS, "View details about registered bots"},

			{ ADMIN_REGISTER_BOT, "Register a new bot with Higgs"},
		    { ADMIN_EDIT_BOT, "Edit a bots configuration"},
		    { ADMIN_DEACTIVATE_BOT, "Deactivate a bot"},
		    { ADMIN_ADD_BOT_SCOPE, "Add a scope to a bot" },
		    { ADMIN_REMOVE_BOT_SCOPE, "Remove a scope from a bot" },
			
		    { ADMIN_VIEW_USER_DETAILS, "View all user details" },
		    { ADMIN_ADD_USER_SCOPE, "Add a scope to a user" },
		    { ADMIN_REMOVE_USER_SCOPE, "Remove a scope from a user" },

		    { REVIEWER_SEND_FEEDBACK, "Send feedback to a reported post" },

		    { BOT_SET_FEEDBACK_TYPES, "Add or update feedback types"},
			{ BOT_REGISTER_POST, "Register a detected post"},
		};
    }
}
