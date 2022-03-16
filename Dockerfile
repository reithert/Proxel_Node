FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY ./target/* ./
CMD ASPNETCORE_URLS=http://*:$PORT dotnet /Proxel_Node.dll
