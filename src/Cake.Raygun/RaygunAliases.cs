using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.Tooling;
using Cake.Raygun;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Newtonsoft.Json;

namespace Cake.Raygun
{
    [CakeAliasCategory("Raygun")]
    [CakeNamespaceImport("Cake.Raygun")]
    public static class RaygunAliases
    {
        [CakeMethodAlias]
        public static void UploadSymbolsToRaygun(this ICakeContext context, FilePath filePath, RaygunSymbolSettings settings)
        {
            Ensure.ArgumentNotNull(filePath, nameof(filePath));

            Ensure.ArgumentNotNullOrWhiteSpace(settings.ApplicationIdentifier, nameof(settings.ApplicationIdentifier));
            Ensure.ArgumentNotNullOrWhiteSpace(settings.Username, nameof(settings.Username));
            Ensure.ArgumentNotNullOrWhiteSpace(settings.Password, nameof(settings.Password));

            var symbol = filePath.FullPath;
            if (!File.Exists(symbol))
            {
                throw new FileNotFoundException($"'{symbol}' could not be found.");
            }

            var bytes = File.ReadAllBytes(symbol);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://app.raygun.com");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes($"{settings.Username}:{settings.Password}")));

                using (var content = new MultipartFormDataContent())
                {
                    content.Add(new StreamContent(new MemoryStream(bytes)), "DsymFile", filePath.GetFilename().ToString());

                    var httpResponse = client.PostAsync($"/dashboard/{settings.ApplicationIdentifier}/settings/symbols", content).Result;
                    httpResponse.EnsureSuccessStatusCode();

                    var json = httpResponse.Content.ReadAsStringAsync().Result;

                    var results = JsonConvert.DeserializeObject<RaygunSymbolUploadResponse>(json);


                    if (!results.Status.Equals("Success", StringComparison.InvariantCultureIgnoreCase))
                    {
                        throw new InvalidOperationException(results.Message);
                    }

                    context.Log.Information(results.Message);

                }
            }
        }
    }
}