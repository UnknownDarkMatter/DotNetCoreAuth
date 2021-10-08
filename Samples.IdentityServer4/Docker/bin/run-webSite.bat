SET ASPNETCORE_ENVIRONMENT=Development
SET IDENTITY_HOSTNAME=localhost
SET IDENTITY_PORT=5001
SET IDENTITY_PROTOCOL=https

CD C:\sources\DotNetCoreAuth\Samples.IdentityServer4\Docker\bin\WebSite
dotnet ProCodeGuide.IdServer4.Client.dll
