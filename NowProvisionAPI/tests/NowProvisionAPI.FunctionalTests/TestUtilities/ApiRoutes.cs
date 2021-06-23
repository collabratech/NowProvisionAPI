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

public static class Propertys
        {
            public const string PropertyId = "{propertyId}";
            public const string GetList = Base + "/propertys";
            public const string GetRecord = Base + "/propertys/" + PropertyId;
            public const string Create = Base + "/propertys";
            public const string Delete = Base + "/propertys/" + PropertyId;
            public const string Put = Base + "/propertys/" + PropertyId;
            public const string Patch = Base + "/propertys/" + PropertyId;
        }

public static class Agents
        {
            public const string AgentId = "{agentId}";
            public const string GetList = Base + "/agents";
            public const string GetRecord = Base + "/agents/" + AgentId;
            public const string Create = Base + "/agents";
            public const string Delete = Base + "/agents/" + AgentId;
            public const string Put = Base + "/agents/" + AgentId;
            public const string Patch = Base + "/agents/" + AgentId;
        }

public static class Offices
        {
            public const string OfficeId = "{officeId}";
            public const string GetList = Base + "/offices";
            public const string GetRecord = Base + "/offices/" + OfficeId;
            public const string Create = Base + "/offices";
            public const string Delete = Base + "/offices/" + OfficeId;
            public const string Put = Base + "/offices/" + OfficeId;
            public const string Patch = Base + "/offices/" + OfficeId;
        }

        // new api route marker - do not delete
    }
}