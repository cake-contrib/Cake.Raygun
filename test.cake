
#addin "nuget:http://nuget:3128/api/odata?package=Cake.Raygun"
using Cake.Core.IO;

Task("RunIntegrationTests")
    .Does (() =>
{
	var filePath = new FilePath(@"C:\Dropbox\OSS\Cake.Raygun\MyCoolApp.app.dSYM.zip");
	var settings = new RaygunSymbolSettings() { ApplicationIdentifier = "", Username = "", Password = "" };

	UploadSymbolsToRaygun(filePath, settings);
});

RunTarget("RunIntegrationTests");
