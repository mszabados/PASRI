FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 50606
EXPOSE 44309

# ---- BEGIN ADDITIONAL TOOLING ----- #
# ----- dos2unix: used to prevent windows from converting our line-endings
# RUN apt-get -qy update && apt-get -qy install dos2unix 
# -----  Required to curl the https://packages.microsoft.com in the following lines
# RUN apt-get -qy install --no-upgrade --no-install-recommends apt-transport-https apt-utils curl software-properties-common iputils-ping net-tools gnupg 
# -----  Add the microsoft packages list to the apt-repository-list
# RUN curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
# RUN curl https://packages.microsoft.com/config/debian/9/prod.list > /etc/apt/sources.list.d/mssql-release.list
# -----  Add the mssql-tools to the container
# RUN apt-get -qy update && ACCEPT_EULA=Y apt-get install -qy msodbcsql17 mssql-tools unixodbc-dev locales
# -----  Allow mssql-tools to be executed from the default bash prompt
# RUN echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
# RUN echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
# -----  Ensure the en_US locale is generated so sqlcmd will run successfully
# RUN apt-get install -qy 
# RUN echo "en_US.UTF-8 UTF-8" > /etc/locale.gen
# RUN locale-gen
# ---- END ADDITIONAL TOOLING ----- #

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY PASRI/PASRI.API.csproj PASRI/
RUN dotnet restore PASRI/PASRI.API.csproj
COPY . .
WORKDIR /src/PASRI
RUN dotnet build PASRI.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish PASRI.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PASRI.API.dll"]
