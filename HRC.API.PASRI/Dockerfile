### ------------------------------------------------------------
### If you receive an error debugging with docker compose: 
### "An error occurred while sending the request" from the Microsoft.VisualStudio.Docker.Compose.Targets
### and you are disconnected from the network, ensure that https://aka.ms/getvsdbgps1 is 
### saved to your %TEMP% folder and docker compose will not attempt to download it again.
### ------------------------------------------------------------

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app

# Expose the NAT'd HTTP
EXPOSE 50606

# Expose the NAT'd HTTPS
EXPOSE 44309

# ---- BEGIN ADDITIONAL TOOLING ----- #
#
# ----- dos2unix: used to prevent windows from converting our line-endings
# RUN apt-get -qy update && apt-get -qy install dos2unix 
#
# ----- Get curl and unzip to install the VS debugger
# RUN apt-get -qy install --no-upgrade --no-install-recommends curl unzip 
#
# -----  Required to curl the https://packages.microsoft.com in the following lines
# RUN apt-get -qy install --no-upgrade --no-install-recommends apt-transport-https apt-utils software-properties-common iputils-ping net-tools gnupg 
#
# -----  Add the microsoft packages list to the apt-repository-list
# RUN curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
# RUN curl https://packages.microsoft.com/config/debian/9/prod.list > /etc/apt/sources.list.d/mssql-release.list
#
# -----  Add the mssql-tools to the container
# RUN apt-get -qy update && ACCEPT_EULA=Y apt-get install -qy msodbcsql17 mssql-tools unixodbc-dev locales
#
# -----  Allow mssql-tools to be executed from the default bash prompt
# RUN echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
# RUN echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
#
# -----  Ensure the en_US locale is generated so sqlcmd will run successfully
# RUN apt-get install -qy 
# RUN echo "en_US.UTF-8 UTF-8" > /etc/locale.gen
# RUN locale-gen
#
# ---- END ADDITIONAL TOOLING ----- #

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY HRC.API.PASRI/HRC.API.PASRI.csproj PASRI/
RUN dotnet restore HRC.API.PASRI/HRC.API.PASRI.csproj
COPY . .
WORKDIR /src/PASRI
RUN dotnet build HRC.API.PASRI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish HRC.API.PASRI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "HRC.API.PASRI.dll"]
