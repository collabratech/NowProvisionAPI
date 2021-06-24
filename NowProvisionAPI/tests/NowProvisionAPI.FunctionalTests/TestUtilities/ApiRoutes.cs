namespace NowProvisionAPI.FunctionalTests.TestUtilities
{
    public class ApiRoutes
    {
        public const string Base = "api";
        public const string Health = Base + "/health";        

public static class NowProvs
        {
            public const string Id = "{id}";
            public const string GetList = Base + "/nowProvs";
            public const string GetRecord = Base + "/nowProvs/" + Id;
            public const string Create = Base + "/nowProvs";
            public const string Delete = Base + "/nowProvs/" + Id;
            public const string Put = Base + "/nowProvs/" + Id;
            public const string Patch = Base + "/nowProvs/" + Id;
        }

        // new api route marker - do not delete
    }
}