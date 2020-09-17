# C#

[top]: #contents

- [C](#c)
  - [C# general](#c-general)
  - [Testing](#testing)
  - [Logging](#logging)
  - [Serialization](#serialization)
  - [MSBuild](#msbuild)
  - [CLI](#cli)
  - [Entity Framework Core (EF core)](#entity-framework-core-ef-core)
  - [.NET Core MVC](#net-core-mvc)
  - [Blazor](#blazor)
  - [Classic ASP.NET](#classic-aspnet)
  - [Web frameworks](#web-frameworks)
  - [Windows Presentation Foundation (WPF)](#windows-presentation-foundation-wpf)
  - [CI/CD](#cicd)
  - [NuGet packages](#nuget-packages)

## C# general

| Topic | Source | URL |
| --- | --- | --- |
| Code style | documentation.help | [StyleCop rules](https://documentation.help/StyleCop/StyleCop%20Rules.html) |
| | computerhope.com | [ Hungarian notation prefixes](https://www.computerhope.com/jargon/h/hungarian-notation.htm#hungarian) |
| Documentation | Microsoft Docs | [XML Documentation Comments](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc) |
| Delegates | MSDN | [Funct<T, TResult> Delegate](https://docs.microsoft.com/en-us/dotnet/api/system.func-2) |
| Garbage collection | GitHub | [.NET memory performance analysis](https://github.com/Maoni0/mem-doc/blob/master/doc/.NETMemoryPerformanceAnalysis.md) |
| Generics | Eric Lippert | [Why are generics not inherited?](https://ericlippert.com/2013/07/15/why-are-generic-constraints-not-inherited/) |
| HtmlAgilityPack | Microsoft Docs | [XPath syntax](https://docs.microsoft.com/en-us/previous-versions/dotnet/netframework-4.0/ms256471(v=vs.100))|
| Identity | Andrew Lock | [Introduction to Authentication with ASP.NET Core](https://andrewlock.net/introduction-to-authentication-with-asp-net-core) |
| | | identityserver.io | [Quickstart Overview](http://docs.identityserver.io/en/latest/quickstarts/0_overview.html) |
| Iterators | Ray Chen | The implementation of iterators and their consequences [(Part 1)](https://blogs.msdn.microsoft.com/oldnewthing/20080812-00/?p=21273/) [(Part 2)](https://blogs.msdn.microsoft.com/oldnewthing/20080813-00/?p=21253) [(Part 3)](https://blogs.msdn.microsoft.com/oldnewthing/20080814-00/?p=21243) [(Part 4)](https://blogs.msdn.microsoft.com/oldnewthing/20080815-00/?p=21223/) |
| LINQ | Microsoft Docs | [Getting started with LINQ](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/) |
| Operators | Microsoft Docs | [Using indexers](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/indexers/using-indexers) |
| | | [Implicit keyword](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/implicit) |
| Patterns | Visual Studio magazine | [Pattern matching in C# 7.0 case blocks](https://visualstudiomagazine.com/articles/2017/02/01/pattern-matching.aspx) |
| RegEx | Microsoft Docs | [Best practices for Regular Expressions in .NET](https://docs.microsoft.com/en-us/dotnet/standard/base-types/best-practices) |
| | | [Character classes in Regular Expressions](https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions) |
| Scaffolding | MSDN Blogs | [Creating a custom scaffolder for Visual Studio](https://blogs.msdn.microsoft.com/webdev/2014/04/03/creating-a-custom-scaffolder-for-visual-studio/) |
| Spans | MSDN | [All about Span: exploring a new .NET mainstay](https://docs.microsoft.com/en-us/archive/msdn-magazine/2018/january/csharp-all-about-span-exploring-a-new-net-mainstay) |
| Validation | odetocode.com | [Manual validation with data annotations](https://odetocode.com/blogs/scott/archive/2011/06/29/manual-validation-with-data-annotations.aspx) |
| Visual Studio | Microsoft Docs | [Shortcuts in Visual Studio](https://docs.microsoft.com/en-gb/visualstudio/ide/default-keyboard-shortcuts-in-visual-studio) |
| | Microsoft Docs | [Using the Task List](https://docs.microsoft.com/en-gb/visualstudio/ide/using-the-task-list) |
| YamlDotNet | cyotek.com | [Using custom type converters with YamlDotNet - Part 1](https://www.cyotek.com/blog/using-custom-type-converters-with-csharp-and-yamldotnet-part-1) and [Part 2](https://www.cyotek.com/blog/using-custom-type-converters-with-csharp-and-yamldotnet-part-2) |
| | GitHub | [Serialize comments](https://github.com/aaubry/YamlDotNet/issues/152) |

[Back to top][top]

## Testing

| Topic | Source | URL |
| --- | --- | --- |
| Fluent assertions | NFluent | [NFluent](http://www.n-fluent.net/) |
| Mocking | GitHub | [Moq Quickstart](https://github.com/Moq/moq4/wiki/Quickstart) |
| NUnit | NUnit docs | [NUnit Documentation](https://github.com/nunit/docs/wiki/NUnit-Documentation) |
| xUnit | xUnit docs | [Getting started with xUnit](https://xunit.github.io/docs/getting-started-desktop#add-xunit-runner-ref) |
| | Andrew Lock | [Using parameterised tests in xUnit](https://andrewlock.net/creating-parameterised-tests-in-xunit-with-inlinedata-classdata-and-memberdata/) |
| | Brendan Connolly | [Organizing tests with xUnit Traits](http://www.brendanconnolly.net/organizing-tests-with-xunit-traits/) |

[Back to top][top]

## Logging

| Topic | Source | URL |
| --- | --- | --- |
| Serilog | serilog.net | [Serilog.net](https://serilog.net/) |
|  | Github| [Serilog.Sinks.Console](https://github.com/serilog/serilog-sinks-console) |
|  | Blog | [Logging scopes and exceptions](https://andrewlock.net/how-to-include-scopes-when-logging-exceptions-in-asp-net-core/) |

[Back to top][top]

## Serialization

| Topic | Source | URL |
| --- | --- | --- |
| YamlDotNet | cyotek.com | Using custom type converters with C# and YamlDotNet [part 1](https://www.cyotek.com/blog/using-custom-type-converters-with-csharp-and-yamldotnet-part-1) and [part 2](https://www.cyotek.com/blog/using-custom-type-converters-with-csharp-and-yamldotnet-part-2)|

[Back to top][top]

## MSBuild

| Topic | Source | URL |
| --- | --- | --- |
| Macros | Microsoft Docs | [Common macros for build commands and properties](https://docs.microsoft.com/en-us/cpp/ide/common-macros-for-build-commands-and-properties?view=vs-2017) |
| Properties | Microsoft Docs | [MSBuild well-known and reserved properties](https://docs.microsoft.com/en-us/visualstudio/msbuild/msbuild-reserved-and-well-known-properties?view=vs-2017) |
| | | [Common MSBuild properties](https://docs.microsoft.com/en-us/visualstudio/msbuild/common-msbuild-project-properties?view=vs-2017) |
| | | [Property element](https://docs.microsoft.com/en-us/visualstudio/msbuild/property-element-msbuild?view=vs-2017) |
| Tasks | Microsoft Docs | [Message task](https://docs.microsoft.com/en-us/visualstudio/msbuild/message-task?view=vs-2017) |
| | | [Copy Task](https://docs.microsoft.com/en-us/visualstudio/msbuild/copy-task?view=vs-2017) |
| | GitHub | [.NET Core - Tasks named "AfterBuild" and "AfterPublish" are ignore](https://github.com/dotnet/cli/issues/8304) |

[Back to top][top]

## CLI

| Topic | Source | URL |
| --- | --- | --- |
| Console apps | GitHub | [CommandLineUtils](https://natemcmaster.github.io/CommandLineUtils/docs/intro.html) |
| | | [CommandLineUtils - Options](https://natemcmaster.github.io/CommandLineUtils/docs/options.html?tabs=using-attributes) |
| | | [CommandLineUtils - Arguments](https://natemcmaster.github.io/CommandLineUtils/docs/arguments.html?tabs=using-attributes) |
| | | [CommandLineUtils - integration with generic Host](https://natemcmaster.github.io/CommandLineUtils/docs/advanced/generic-host.html) |
| | Andrew Lock | [Using dependency injection in a .NET Core console app](https://andrewlock.net/using-dependency-injection-in-a-net-core-console-application/) |
| .NET global tools | Microsoft Docs | [Create a global tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools-how-to-create) |
| | Nate McMaster | [.NET Core Global tools and gotchas](https://natemcmaster.com/blog/2018/02/02/dotnet-global-tool/) |

[Back to top][top]

## Entity Framework Core (EF core)

| Topic | Source | URL |
| --- | --- | --- |
| Databases | CodeProject | [Storing complex properties as JSON](https://www.codeproject.com/Articles/1166099/Entity-Framework-Storing-complex-properties-as-JSO) |
| | Microsoft Docs | [Connection String Syntax](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/connection-string-syntax) |
| | Npgsql Docs | [Getting Started \[with PostgreSQL\]](http://www.npgsql.org/efcore/index.html) |
| Deployment | Microsoft Docs | [Configuring a database server for web deploy publishing](https://docs.microsoft.com/en-us/aspnet/web-forms/overview/deployment/configuring-server-environments-for-web-deployment/configuring-a-database-server-for-web-deploy-publishing)|
| | Microsoft Docs | [Deploying database projects](https://docs.microsoft.com/en-us/aspnet/web-forms/overview/deployment/web-deployment-in-the-enterprise/deploying-database-projects) |
| Fluent API | Scott Sauber | [Configuring EF Core 2.0 with IEntityTypeConfiguration](https://scottsauber.com/2017/09/11/customizing-ef-core-2-0-with-ientitytypeconfiguration/) |
| | Microsoft Docs | [Cascade Delete](https://docs.microsoft.com/en-us/ef/core/saving/cascade-delete) |
| Migrations | Brice Lambson | [How to use EFCore Migrations with layers](https://github.com/bricelam/Sample-SplitMigrations) |
| | Microsoft Docs | [Diesng-Time DbContext Creation](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation) |
| | Andrei Dzimchuk | [Managing database schema and seeding data with EF Core migrations](https://dzimchuk.net/managing-database-schema-and-seeding-data-with-ef-core-migrations/) |
| Shadow properties | StackOverflow | [Turning off shadow property generation](https://stackoverflow.com/questions/51127947/turning-off-shadow-property-generation) |
| Testing| Microsoft Docs | [Implementing the Repository and Unit of Work patterns](https://docs.microsoft.com/en-gb/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application) |
| | Microsoft Docs | [Repository pattern benefits](https://docs.microsoft.com/en-us/previous-versions/msp-n-p/ff649690(v=pandp.10)) |
|  | Microsoft Docs |[Testing with InMemory](https://docs.microsoft.com/en-gb/ef/core/miscellaneous/testing/in-memory) |
| | Stormpath | [Tutorial: Using Entity Framework Core as an In-Memory Database for ASP.NET Core](https://stormpath.com/blog/tutorial-entity-framework-core-in-memory-database-asp-net-core) |
| Views | Microsoft Docs | [Keyless entity types](https://docs.microsoft.com/en-us/ef/core/modeling/keyless-entity-types) |

[Back to top][top]

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
| Razor class libraries | Microsoft Docs | [Create reusable UI using the Razor class library project in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/razor-pages/ui-class?view=aspnetcore-3.1&tabs=visual-studio) |
| Security | Microsoft Docs | [Overview of ASP.NET Core Security](https://docs.microsoft.com/en-us/aspnet/core/security/?view=aspnetcore-2.1) |
| | | [Authentication in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/index?view=aspnetcore-2.1) |
| | | [Authorization in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/authorization/index?view=aspnetcore-2.1) |
| |  | [Safe storage of app secrets in development in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-2.1&tabs=visual-studio) |
| | Jerrie Pelser | [Authenticate with OAuth 2.0 in ASP.NET Core 2.0](https://www.jerriepelser.com/blog/authenticate-oauth-aspnet-core-2/) |
| Swagger/OpenAPI | Microsoft Docs | [ASP.NET Core Web API help pages with Open API](https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger) |
| | GitHub | [SwashBuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) |
| | GitHub issue | [Support for ASP.NET API Versioning?](https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/244) |
| | StackOverflow | [Rename Model in SwashBuckle 6 with ASP.NET Core Web API](https://stackoverflow.com/questions/40644052/rename-model-in-swashbuckle-6-swagger-with-asp-net-core-web-api) |
| | GitHub | [201 CREATED shows up as status 200 in Swagger UI](https://github.com/domaindrivendev/Swashbuckle/issues/702) |
| Testing | Microsoft Docs | [Testing Controller logic in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-2.1) |
| | Microsoft Docs | [Razor pages testing](https://docs.microsoft.com/en-us/aspnet/core/testing/razor-pages-testing) |
| | Microsoft Docs | [Unit testing with dotnet test and xUnit](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test) |
| | Hossam Barakat | [Unit testing ASP.NET Core Tag Helper](http://www.hossambarakat.net/2016/02/29/unit-testing-asp-net-core-tag-helper/) |
| | Mark Macneil | [Painless integration testing with ASP.Net Core Web API](https://fullstackmark.com/post/20/painless-integration-testing-with-aspnet-core-web-api) |
| Tutorials | Microsoft Docs | [Contoso University](https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/) |
| | Microsoft Docs | [Building Your First Web API with ASP.NET Core MVC and Visual Studio](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api) |
| | Microsoft Docs| [Music Store](https://github.com/aspnet/MusicStore)|
| Validation | Microsoft Docs | [System.ComponentModel.DataAnnotations namespace](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations)
|  | Microsoft Docs | [Model validation in ASP.NET Core MVC](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation) |
| Views | Microsoft Docs | [Tag Helpers](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/intro) |
| | StackOverflow | [TagHelpers not rendering](https://stackoverflow.com/questions/46388336/dotnet-core-2-0-taghelper-is-not-rendered#answer-46403680) |
| | Microsoft Docs | [Authoring Tag Helpers](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/authoring) |
| | Microsoft Docs | [View Components](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-components) |
| | Microsoft Docs | [Serving static files](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files) |

[Back to top][top]

## Blazor

| Topic | Source | URL |
| --- | --- | --- |
| General | Microsoft Docs | [Hosting models](https://docs.microsoft.com/en-us/aspnet/core/blazor/hosting-models?view=aspnetcore-3.0) |
| Razor components | Microsoft Docs | [Create and use ASP.NET Core Razor components](https://docs.microsoft.com/en-us/aspnet/core/blazor/components?view=aspnetcore-3.0) |

[Back to top][top]

## Classic ASP.NET

| Topic | Source | URL |
| --- | --- | --- |
| Application config | Microsoft Docs | [Web Deploy Parameterization](https://docs.microsoft.com/en-us/iis/publish/using-web-deploy/web-deploy-parameterization) |
| | | [Configuring parameters for Web Package Deployment](https://docs.microsoft.com/en-us/aspnet/web-forms/overview/deployment/web-deployment-in-the-enterprise/configuring-parameters-for-web-package-deployment) |
| Dependency Injection | Autofac docs | [Adapters and Decorators](https://autofac.readthedocs.io/en/latest/advanced/adapters-decorators.html) |


[Back to top][top]

## Web frameworks

| Topic | Source | URL |
| --- | --- | --- |
| Angular | okta.com | [Build a CRUD App with ASP.NET Core and Angular](https://developer.okta.com/blog/2018/04/26/build-crud-app-aspnetcore-angular) |
| SignalR | Microsoft Docs | [Introduction to ASP.NET Core SignalR](https://docs.microsoft.com/en-gb/aspnet/core/tutorials/signalr?view=aspnetcore-3.1) |
| | | [Use Hubs in SignalR for ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/signalr/hubs?view=aspnetcore-3.1) |
| | MSDN Magazine | [Discovering ASP.NET Core SignalR](https://docs.microsoft.com/en-us/archive/msdn-magazine/2018/april/cutting-edge-discovering-asp-net-core-signalr) |
| Single Page Applications | GitHub | [AspNetCore JavaScriptServices documentation](https://github.com/aspnet/JavaScriptServices) |
| Webpack | Microsoft docs | [Using SignalR with Webpack and TypeScript](https://docs.microsoft.com/en-us/aspnet/core/tutorials/signalr-typescript-webpack?view=aspnetcore-2.1&tabs=visual-studio) |

[Back to top][top]

## Windows Presentation Foundation (WPF)

| Topic | Source | URL |
| --- | --- | --- |
| Tutorials | Microsoft Docs | [My first WPF desktop application](https://docs.microsoft.com/en-us/dotnet/framework/wpf/getting-started/walkthrough-my-first-wpf-desktop-application) |

[Back to top][top]

## CI/CD

| Topic | Source | URL |
| --- | --- | --- |
| AppVeyor | Andrew Lock | [Publishing your first NuGet package with AppVeyor and MyGet](https://andrewlock.net/publishing-your-first-nuget-package-with-appveyor-and-myget/) |
| CI | Microsoft Docs | [Using .NET Core SDK and tools in Continuous Integration](https://docs.microsoft.com/en-us/dotnet/core/tools/using-ci-with-cli) |
| Travis CI | Travis docs | [Building a C#, F# or Visual Basic Project](https://docs.travis-ci.com/user/languages/csharp/) |
| | Andrew Lock | [Adding Travis CI to a a .Net Core app](https://andrewlock.net/adding-travis-ci-to-a-net-core-app/) |

[Back to top][top]

## NuGet packages

| Topic | Source | URL |
| --- | --- | --- |
| Project dependencies | jerriepelser.com | [Analyzing project dependencies part 1](https://www.jerriepelser.com/blog/analyze-dotnet-project-dependencies-part-1/) |
| NuGet | Microsoft Docs | [Quickstart: Create and publish a package (dotnet CLI)](https://docs.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-the-dotnet-cli) |
| | | [NuGet config file](https://docs.microsoft.com/en-us/nuget/reference/nuget-config-file) |
| .nuspec | Microsoft Docs | [Creating the .nuspec file](https://docs.microsoft.com/en-us/nuget/create-packages/creating-a-package#creating-the-nuspec-file) |
| | | [.nuspec reference](https://docs.microsoft.com/en-us/nuget/reference/nuspec) |
| | | [Replacement tokens](https://docs.microsoft.com/en-gb/nuget/reference/nuspec#replacement-tokens) |
| | | [.nuspec - including content files](https://docs.microsoft.com/en-us/nuget/reference/nuspec#including-content-files) |

[Back to top][top]

