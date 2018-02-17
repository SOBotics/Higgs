using System.Collections.Generic;

namespace Higgs.Server
{
    public static class Scopes
    {
	    public const string ADMIN_REGISTER_BOT = "admin:registerBot";
	    public const string ADMIN_EDIT_BOT = "admin:editBot";
	    public const string ADMIN_DELETE_BOT = "admin:deleteBot";
		
		public static Dictionary<string, string> AllScopes = new Dictionary<string, string>
	    {
		    {ADMIN_REGISTER_BOT, "Register a new bot with Higgs"},
		    {ADMIN_EDIT_BOT, "Edit a bots configuration"},
		    {ADMIN_DELETE_BOT, "Delete a bot from Higgs"}
	    };
	}
}
