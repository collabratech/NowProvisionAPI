# NowProvisionAPI #

The Collabra NowProvisionAPI is an API, azure ESB and HangFire project used to orchestration the NOW APIs for further customization or provisioning/rendering. 

## Documentation
<a href="https://docs.google.com/document/d/1Zra1-rc9CkmaySjzHBNK3H4mW4ikvQjlliAqzwOTYVU">Now Provision API Specification</a>

## Configuration Settings ##

✍🏼  Some of these settings are defined in appsettings.json files. In the Azure environment, these settings are defined in global variables

 - **MongoConfig:ConnectionString** - The connection string to a MongoDB database where the data is stored.
 - **MongoConfig:DbName** - The name of the database.
 - **JwtConfig:JwtIssuer** - The issuer or issuers (comma-separated, no spaces) that are allowed to call the API. This should be in a URL format, typically the root URL for the service making the call. If the service doesn't have an actual URL, a URL based on the company with the name of the service in it. *NOTE: Default set in appsettings.json.*
 - **JwtConfig:JwtAudience** - The root URL for this API. This should be specific to the environment to help prevent clients from accidentally making calls in the wrong environment. *NOTE: Default set in appsettings.json.*
 - **JwtConfig:JwtKey** - The key or keys (comma-separated, no spaces) used to verify the JWT is issued by the proper authority and hasn't been modified.

## Introduction to the Code ##

The service was built on a separate architecture in 4 different projects: Ordering.Core, Ordering.Infrastructure, Ordering.WebApi and Messages.


 #### 👨🏼‍🏫 Contact
 
 Collabra Slack Team - #team-platform
 
 Project Link: https://github.com/collabratech/NowProvisionAPI


