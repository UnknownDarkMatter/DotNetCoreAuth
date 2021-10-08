using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ProCodeGuide.Samples.IdentityServer4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseKestrel(options => options.ConfigureEndpoints());
                });

    }

    public static class KestrelServerOptionsExtensions
    {
        public static void ConfigureEndpoints(this KestrelServerOptions options)
        {
            var address = IPAddress.Any;
            options.Listen(address, 5000);
            options.Listen(address, 5001,
                    listenOptions =>
                    {
                        var certificate = LoadCertificate();
                        listenOptions.UseHttps(certificate);
                    });
        }

        private static X509Certificate2 LoadCertificate()
        {
            //#### pour générer un certificat SSL
            //dotnet dev-certs https -p token-jwt-secret-http2 -ep C:\sources\...\token-jwt-secret-http2.pfx
            //l'export de certificat créé depuis inetmgr provoque une erreur NS_ERROR_NET_INADEQUATE_SECURITY

            var x509certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(
                $"{Environment.CurrentDirectory}/token-jwt-secret-http2.pfx", "token-jwt-secret-http2");
            return x509certificate;
        }
    }

}
