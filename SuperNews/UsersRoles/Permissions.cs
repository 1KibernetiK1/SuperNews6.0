using System.Collections.Generic;

namespace SuperNews.UsersRoles
{
    public static class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete"
            };
        }
        public static class News
        {
            public const string View = "Permissions.News.View";
            public const string Create = "Permissions.News.Create";
            public const string Edit = "Permissions.News.Edit";
            public const string Delete = "Permissions.News.Delete";
        }
    }
}
