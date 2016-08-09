## Cake.Raygun [![Build status](https://ci.appveyor.com/api/projects/status/h2g53uc2y01nhuim/branch/master?svg=true)](https://ci.appveyor.com/project/ghuntley/cake-raygun/branch/master)
Objective-C debug symbols (dSYM) are essential to debugging Xamarin iOS application crashes. This Cake add-in uploads these symbols to Raygun.io which creates delightfully readable stack traces which aid you in tracking down those bugs. As Raygun adds additional functionality to their API, these features will also be made available in this add-in.

## Installation

Add the following reference to your cake build script:

```csharp
#addin "Cake.Raygun"
```

## Usage

```csharp
var filePath = new FilePath(@"./artifacts/ios/appstore/MyCoolApp.app.dSYM.zip");
var settings = new RaygunSymbolSettings() { ApplicationIdentifier = "", Username = "", Password = "" };

UploadSymbolsToRaygun(filePath, settings);
```

## See also
* https://raygun.com/docs/workflow/raygun-sidekick
* https://raygun.com/blog/2014/05/jenkins-dsym-upload-to-raygun/
