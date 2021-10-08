########## Image microsoft ##########
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /home/IdentityServer
ENV ASPNETCORE_ENVIRONMENT=Development
#ENV ASPNETCORE_URLS=http://+:5000
COPY bin/IdentityServer/ /home/IdentityServer/
EXPOSE 5000
CMD ["dotnet", "/home/IdentityServer/ProCodeGuide.Samples.IdentityServer4.dll"]