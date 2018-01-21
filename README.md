## MittoAgSMS application HOW-TO

Based on reqirements, it has been developed webapi2 .NET Framework 4.5 application whose main functionalities are:
1. Sending SMS messages
2. Filtering of sent SMS messages
3. Statistic of sent SMS messages
4. Display countries data

All the projects and files inside Visual Studio solution are grouped among several solution folders:

**Business Logic** – layer where business logic is implemented

*MittoAgSMS.BusinessLogic*<br/>
*MittoAgSMS.BusinessLogic.Abstractions*

**Data model** - layer where is implemented business model as well as data model

*MittoAgSMS.BusinessModel*<br/>
*MittoAgSMS.DataModel*

**Repositories** - layer which abstracts data source (database)

*MittoAgSMS.DataAccessLayer*<br/>
*MittoAgSMS.DataAccessLayer.Abstractions*

**Services** - layer where are located services which are using repositories and other services as well

*MittoAgSMS.Services*<br/>
*MittoAgSMS.Services.Abstractions*

**WebApi** – layer where controllers and Service Api methods are located.

*MittoAgSMS.WebAPI*<br/>
*MittoAgSMS.Tests - mesto gde su unit testovi*

Solution explorer looks like this:<br/>
![Solution explorer](/MD_images/solution.png)

## Prerequisites

Before first application run, it is necessary to install *NuGet Package Manger and NuGet CLI (Command Line Interface)*. 
It is possible to install them on different ways. If it is available traffic to nuget.org and chocolaty.org, it is possible to install NuGet CLI
with tool called *chocolatey*. 
After successful instalation command of Nuget packages restore should be run. The command is **dotnet restore**.

![Npm](/MD_images/npm.png)

In this project are used following Nuget packages:

**Autofac.Webapi2** - it is used for IoC container configuration. In Global.asax it have been registered by method
 `IocConfig.Configure();`

**Automapper** - used for mapping business model into data model and vice versa. In Global.asax it have been registered by method `AutoMapperConfig.Initialize();`

**Serilog**, **Serilog.Sinks.File** - used for logging. In this project it has a role to mock SMS sending. That has been implemented in the service SendSmsServiceMock. 
Logs are located in the folder /SMS_log

**EntityFramework** - used for database abstraction.

**Moq** - The mocking framework who mocks services and their methods calls and is used for class isolation in tests. In project Tests are located Business logic Unit tests.

## Sequence diagrams

![Countriessms](/MD_images/countriessms.png)

*get countries*
![Filtersms](/MD_images/filtersms.png)

*filter sms*
![Sendsms](/MD_images/sendsms.png)

*send sms*
![Statisticssms](/MD_images/statisticssms.png)

*get statistics*

## Architecture and class diagram<br/>

To build application it is used the three layers architecture<br/>
![Threelayers](/MD_images/threelayers.png)

This is class diagram<br/>
![Classdiagram](/MD_images/classdiagram.png)

## Functionalities<br/>

it is available to define response format in the query itself. The response can be either *XML* or *JSON*. It is done in the class `WebApiConfig.cs`<br/>

            `config.Routes.MapHttpRoute(
            name: "SMS",
            routeTemplate: "{controller}/{action}.{ext}");`

            `config.Routes.MapHttpRoute(
            name: "Common",
            routeTemplate: "{action}.{ext}",
            defaults: new { controller = "Common" });`

In the same class it is defined datetime serialisation from the request. It is set to be the UTC.

`new Newtonsoft.Json.JsonSerializerSettings
{
    DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc,
    DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat
};`

Since SMS message has a limit of 160 characters per message, it is implemented text splitting and creating more messages and sending them. It is implemented in method
`SendSmsBusinessLogic.SplitIntoChunks()`. This method takes the mesage text and returns a list of strings which are long 160 charaters or less. 
The alghoritm complexity is O(n).

## Further improvements<br/>
In the next version it is needed to implement add new modules:

**Caching** - the caching module

**Auth** - module for authentication and authorisation

**HealthCheck** - module for checking the health of the services

**Swagger** - tool for documentation generation


## Database location

The connection string is located in web.config file. The database is called MittoSMS. In this project SQL Server 2014 is used. Model has two tables:<br/>

![Model](/MD_images/model.png)

MobileCountryCode is foreign key in table SentSms, but primary key in table Country.
This is detailed model<br/>

![Model2](/MD_images/model2.png)

Table Country has some initial data:<br/>

![Modeldata](/MD_images/modeldata.png)

## Publishing on IIS

It is needed to publish Api first<br/>

![Publish](/MD_images/publish.png)

and choose option Folder

![Folder](/MD_images/folder.png)

On IIS it is needed to add new Web site

![Addwebsite](/MD_images/addwebsite.png)

with this configurations

![Websiteconfig](/MD_images/websiteconfig.png)

Port 80 is default and might be used, so it is better to choose some other one.

The application pool will be created, so it is necessary to configure it this way

![Apppool](/MD_images/apppool.png)

### Database access under selected user

The application pool user needs to be added to the database users **Database Server -> Security -> Logins**.
If he is not added, it is needed to add him or the error will be thrown - run time error exception Login failed for user '***'.







