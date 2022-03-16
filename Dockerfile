FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY ./target/* ./
CMD dotnet /Proxel_Node.dll
