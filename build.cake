#tool "nuget:?package=xunit.runner.console"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var zendeskUrl = Argument("zendeskUrl", "");
var zendeskUsername = Argument("zendeskUsername", "");
var zendeskToken = Argument("zendeskToken", "");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory("./src/Example/bin") + Directory(configuration);

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore("./ZendeskApiClient.sln");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    var buildSettings = new DotNetCoreBuildSettings()
    {
        Configuration = configuration,
        Verbosity = DotNetCoreVerbosity.Normal
    };
    DotNetCoreBuild("./ZendeskApiClient.sln", buildSettings);
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    var projects = GetFiles("./test/**/*.Tests.csproj");
    
    foreach(var project in projects)
    {
        DotNetCoreTest(
            project.FullPath,
            new DotNetCoreTestSettings()
            {
                Configuration = configuration
            }
        );
    }
});

Task("Run-Integration-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    var projects = GetFiles("./test/**/*.IntegrationTests.csproj");
    
    foreach(var project in projects)
    {
        DotNetCoreTest(
            project.FullPath,
            new DotNetCoreTestSettings()
            {
                Configuration = configuration,
                EnvironmentVariables = new Dictionary<string, string>
                {
                    { "ZendeskApi_Credentials_Url", zendeskUrl },
                    { "ZendeskApi_Credentials_Username", zendeskUsername },
                    { "ZendeskApi_Credentials_Token", zendeskToken }
                }
            }
        );
    }
});

Task("Package")
    .IsDependentOn("Build")
    .Does(() =>
{
    var settings = new DotNetCorePackSettings
    {
        Configuration = "Release",
        OutputDirectory = "./artifacts/"
    };

    DotNetCorePack("./src/ZendeskApi.Client/ZendeskApi.Client.csproj", settings);
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Run-Unit-Tests");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
