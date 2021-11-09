#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MentorsBlog/MentorsBlog.csproj", "MentorsBlog/"]
COPY ["MentorsBlog.Core.Common/MentorsBlog.Core.Common.csproj", "MentorsBlog.Core.Common/"]
COPY ["MentorsBlog.Core.Web/MentorsBlog.Core.Web.csproj", "MentorsBlog.Core.Web/"]
COPY ["MentorsBlog.Core.DataAccess/MentorsBlog.Core.DataAccess.csproj", "MentorsBlog.Core.DataAccess/"]
COPY ["MentorsBlog.Application.Service/MentorsBlog.Application.Service.csproj", "MentorsBlog.Application.Service/"]
COPY ["MentorsBlog.Application.Domain/MentorsBlog.Application.Domain.csproj", "MentorsBlog.Application.Domain/"]
RUN dotnet restore "MentorsBlog/MentorsBlog.csproj"
COPY . .
WORKDIR "/src/MentorsBlog"
RUN ASPNETCORE_ENVIRONMENT="prod" \
    dotnet build "MentorsBlog.csproj" -c Release -o /app/build

FROM build AS publish
RUN ASPNETCORE_ENVIRONMENT="prod" \
    dotnet publish "MentorsBlog.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MentorsBlog.dll"]
