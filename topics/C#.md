## Contents

* [C# General](#c-general)
* [Entity Framework Core](#entity-framework-core-ef-core)
* [.NET Core MVC](#net-core-mvc)
* [Web frameworks](#web-frameworks)
* [Windows Presentation Foundation (WPF)](#windows-presentation-foundation-wpf)
* [CI/CD](#cicd)
* [NuGet packages](#nuget-packages)

## C# general
| Topic | Source | URL |
| --- | --- | --- |
| Documentation | Microsoft Docs | [XML Documentation Comments](https://msdn.microsoft.com/en-us/library/b2s063f7.aspx) |
| Delegates | MSDN | [Funct<T, TResult> Delegate](https://msdn.microsoft.com/en-us/library/bb549151(v=vs.110).aspx) |
| Generics | Erip Lippert | [Why are generics not inherited?](https://ericlippert.com/2013/07/15/why-are-generic-constraints-not-inherited/) |
| HtmlAgilityPack | Microsoft Docs | [XPath syntax](https://msdn.microsoft.com/en-us/library/ms256471(v=vs.110).aspx)|
| Iterators | Ray Chen | The implementation of iterators and their consequences [(Part 1)](https://blogs.msdn.microsoft.com/oldnewthing/20080812-00/?p=21273/) [(Part 2)](https://blogs.msdn.microsoft.com/oldnewthing/20080813-00/?p=21253) [(Part 3)](https://blogs.msdn.microsoft.com/oldnewthing/20080814-00/?p=21243) [(Part 4)](https://blogs.msdn.microsoft.com/oldnewthing/20080815-00/?p=21223/) |
| LINQ | Microsoft Docs | [Getting started with LINQ](https://msdn.microsoft.com/en-us/library/bb397933.aspx) |
| Logging | Serilog | [Serilog.net](https://serilog.net/) |
|  | Github| [Serilog.Sinks.Console](https://github.com/serilog/serilog-sinks-console) |
|  | Blog | [Logging scopes and exceptions](https://andrewlock.net/how-to-include-scopes-when-logging-exceptions-in-asp-net-core/) |
| Mocking | GitHub | [Moq Quickstart](https://github.com/Moq/moq4/wiki/Quickstart) |
| Scaffolding | MSDN Blogs | [Creating a custom scaffolder for Visual Studio](https://blogs.msdn.microsoft.com/webdev/2014/04/03/creating-a-custom-scaffolder-for-visual-studio/) |
| Testing | NUnit docs | [NUnit Documentation](https://github.com/nunit/docs/wiki/NUnit-Documentation) |
| Visual Studio | Microsoft Docs | [Shortcuts in Visual Studio](https://msdn.microsoft.com/en-us/library/da5kh0wa.aspx) |
| | Microsoft Docs | [Using the Task List](https://msdn.microsoft.com/en-us/library/txtwdysk.aspx) |

## Entity Framework Core (EF core)

| Topic | Source | URL |
| --- | --- | --- |
| Async| MSDN blogs | [Asynchronous Repositories](https://blogs.msdn.microsoft.com/mrtechnocal/2014/03/16/asynchronous-repositories/) |
| | CodeGuru | [Performing Asynchronous Operations using Entity Framework](http://www.codeguru.com/csharp/.net/net_framework/performing-asynchronous-operations-using-entity-framework.htm) |
| Databases | CodeProject | [Storing complex properties as JSON](https://www.codeproject.com/Articles/1166099/Entity-Framework-Storing-complex-properties-as-JSO) |
| | Microsoft Docs | [MSSQL Connection Strings](https://msdn.microsoft.com/en-us/library/jj653752(v=vs.110).aspx) |
| | Npgsql Docs | [Getting Started \[with PostgreSQL\]](http://www.npgsql.org/efcore/index.html) |
| Deployment | Microsoft Docs | [Configuring a database server for web deploy publishing](https://docs.microsoft.com/en-us/aspnet/web-forms/overview/deployment/configuring-server-environments-for-web-deployment/configuring-a-database-server-for-web-deploy-publishing)|
| | Microsoft Docs | [Deploying database projects](https://docs.microsoft.com/en-us/aspnet/web-forms/overview/deployment/web-deployment-in-the-enterprise/deploying-database-projects) |
| Fluid API | Scott Sauber | [Configuring EF Core 2.0 with IEntityTypeConfiguration](https://scottsauber.com/2017/09/11/customizing-ef-core-2-0-with-ientitytypeconfiguration/) |
| | Microsoft Docs | [Cascade Delete](https://docs.microsoft.com/en-us/ef/core/saving/cascade-delete) |
| Migrations | Brice Lambson | [How to use EFCore Migrations with layers](https://github.com/bricelam/Sample-SplitMigrations) |
| | Microsoft Docs | [Diesng-Time DbContext Creation](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation) |
| | Andrei Dzimchuk | [Managing database schema and seeding data with EF Core migrations](https://dzimchuk.net/managing-database-schema-and-seeding-data-with-ef-core-migrations/) |
| Testing| Microsoft Docs | [Implementing the Repository and Unit of Work patterns](https://docs.microsoft.com/en-gb/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application) |
| | Microsoft Docs | [Repository pattern benefits](https://msdn.microsoft.com/en-us/library/ff649690.aspx) |
|  | Microsoft Docs |[Testing with InMemory](https://docs.microsoft.com/en-gb/ef/core/miscellaneous/testing/in-memory) |
| | Stormpath | [Tutorial: Using Entity Framework Core as an In-Memory Database for ASP.NET Core](https://stormpath.com/blog/tutorial-entity-framework-core-in-memory-database-asp-net-core) |

## .NET Core MVC

| Topic | Source | URL |
| --- | --- | --- |
| dotnet CLI | GitHub | [.NET Command Line Interface](https://github.com/dotnet/cli/blob/master/README.md) |
| | Maarten Balliauw | [Extending dotnet CLI with custom tools (blog post)](https://blog.maartenballiauw.be/post/2017/04/10/extending-dotnet-cli-with-custom-tools.html) and [dotnetcli-init (GitHub)](https://github.com/maartenba/dotnetcli-init) |
| Client-side| Microsoft Docs | [Bundling and minification](https://docs.microsoft.com/en-us/aspnet/core/client-side/bundling-and-minification) |
| HTTPS | Microsoft Docs | [Enforece HTTPS in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-2.0&tabs=visual-studio) |
| | MSDN blogs | [Configuring HTTPS in ASP.NET Core across different platforms](https://blogs.msdn.microsoft.com/webdev/2017/11/29/configuring-https-in-asp-net-core-across-different-platforms/) |
| | humankode.com | [Develop Locally with HTTPS, Self-Signed Certificates and ASP.NET Core](https://www.humankode.com/asp-net-core/develop-locally-with-https-self-signed-certificates-and-asp-net-core) |
| | blinkingcaret.com | [HTTPS in ASP.NET Core from scratch](https://www.blinkingcaret.com/2017/03/01/https-asp-net-core/) |
| Middleware | Microsoft Docs | [ASP.NET Core Middleware](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware) |
| Razor pages| Microsoft Docs | [Getting started with Razor Pages](https://docs.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/razor-pages-start) |
| | learnrazorpages.com | [Friendly Routes](https://www.learnrazorpages.com/razor-pages/routing#friendly-routes) |
| Security | Microsoft Docs | [Overview of ASP.NET Core Security](https://docs.microsoft.com/en-us/aspnet/core/security/?view=aspnetcore-2.1) |
| | | [Authentication in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/index?view=aspnetcore-2.1) |
| | | [Authorization in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/authorization/index?view=aspnetcore-2.1) |
| |  | [Safe storage of app secrets in development in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-2.1&tabs=visual-studio) |
| | Jerrie Pelser | [Authenticate with OAuth 2.0 in ASP.NET Core 2.0](https://www.jerriepelser.com/blog/authenticate-oauth-aspnet-core-2/) |
| Swagger | Microsoft Docs | [ASP.NET Core Web API help pages with Open API](https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger) |
| | GitHub | [SwashBuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) |
| | GitHub issue | [Support for ASP.NET API Versioning?](https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/244) |
| | StackOverflow | [Rename Model in SwashBuckle 6 with ASP.NET Core Web API](https://stackoverflow.com/questions/40644052/rename-model-in-swashbuckle-6-swagger-with-asp-net-core-web-api) |
| Testing | Microsoft Docs | [Testing Controller logic in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-2.1) |
| | Microsoft Docs | [Razor pages testing](https://docs.microsoft.com/en-us/aspnet/core/testing/razor-pages-testing) |
| | Microsoft Docs | [Unit testing with dotnet test and xUnit](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test) |
| | Hossam Barakat | [Unit testing ASP.NET Core Tag Helper](http://www.hossambarakat.net/2016/02/29/unit-testing-asp-net-core-tag-helper/) |
| Tutorials | Microsoft Docs | [Contoso University](https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/) |
| | Microsoft Docs | [Building Your First Web API with ASP.NET Core MVC and Visual Studio](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api) |
| | Microsoft Docs| [Music Store](https://github.com/aspnet/MusicStore)|
| Validation | Microsoft Docs | [System.ComponentModel.DataAnnotations namespace](https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations(v=vs.110).aspx)
|  | Microsoft Docs | [Model validation in ASP.NET Core MVC](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation) |
| Views | Microsoft Docs | [Tag Helpers](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/intro) |
| | StackOverflow | [TagHelpers not rendering](https://stackoverflow.com/questions/46388336/dotnet-core-2-0-taghelper-is-not-rendered#answer-46403680) |
| | Microsoft Docs | [Authoring Tag Helpers](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/authoring) |
| | Microsoft Docs | [View Components](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-components) |
| | Microsoft Docs | [Serving static files](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files) |


## Web frameworks

| Topic | Source | URL |
| --- | --- | --- |
| Angular | okta.com | [Build a CRUD App with ASP.NET Core and Angular](https://developer.okta.com/blog/2018/04/26/build-crud-app-aspnetcore-angular) |
| Webpack | Microsoft docs | [Using SignalR with Webpack and TypeScript](https://docs.microsoft.com/en-us/aspnet/core/tutorials/signalr-typescript-webpack?view=aspnetcore-2.1&tabs=visual-studio) |

## Windows Presentation Foundation (WPF)

| Topic | Source | URL |
| --- | --- | --- |
| Tutorials | Microsoft Docs | [My first WPF desktop application](https://docs.microsoft.com/en-us/dotnet/framework/wpf/getting-started/walkthrough-my-first-wpf-desktop-application) |

## CI/CD

| Topic | Source | URL |
| --- | --- | --- |
| AppVeyor | Andrew Lock | [Publishing your first NuGet package with AppVeyor and MyGet](https://andrewlock.net/publishing-your-first-nuget-package-with-appveyor-and-myget/) |
| CI | Microsoft Docs | [Using .NET Core SDK and tools in Continuous Integration](https://docs.microsoft.com/en-us/dotnet/core/tools/using-ci-with-cli) |
| Travis CI | Travis docs | [Building a C#, F# or Visual Basic Project](https://docs.travis-ci.com/user/languages/csharp/) |
| | Andrew Lock | [Adding Travis CI to a a .Net Core app](https://andrewlock.net/adding-travis-ci-to-a-net-core-app/) |

## NuGet packages

| Use case | Name |
| --- | --- |
| Import external assembly Views | [AUR.NetCore.Mvc.PluginsManager](https://www.nuget.org/packages/AUR.NETCore.Mvc.PluginsManager/1.0.0#) |