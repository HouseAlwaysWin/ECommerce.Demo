ARG REPO=mcr.microsoft.com/dotnet/runtime
ARG REPO=mcr.microsoft.com/dotnet/aspnet
FROM $REPO:3.1-alpine3.12



ENV \
    # Unset ASPNETCORE_URLS from aspnet base image
    ASPNETCORE_URLS= \
    # Disable the invariant mode (set in base image)
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    # Enable correct mode for dotnet watch (only mode supported in a container)
    DOTNET_USE_POLLING_FILE_WATCHER=true \
    LC_ALL=en_US.UTF-8 \
    LANG=en_US.UTF-8 \
    # Skip extraction of XML docs - generally not useful within an image/container - helps performance
    NUGET_XMLDOC_MODE=skip \
    # PowerShell telemetry for docker image usage
    POWERSHELL_DISTRIBUTION_CHANNEL=PSDocker-DotnetCoreSDK-Alpine-3.12

# Add dependencies for disabling invariant mode (set in base image)
RUN apk add --no-cache icu-libs

# Install .NET Core SDK
RUN dotnet_sdk_version=3.1.404 \
    && wget -O dotnet.tar.gz https://dotnetcli.azureedge.net/dotnet/Sdk/$dotnet_sdk_version/dotnet-sdk-$dotnet_sdk_version-linux-musl-x64.tar.gz \
    && dotnet_sha512='c6e73e88c69fa2c81eb572a64206fa6e94cb376230a05f14028c35aab202975c857973f9b5fac849c60d22f37563d8d53689c2605571e3b922bda2489e12346d' \
    && echo "$dotnet_sha512  dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -C /usr/share/dotnet -oxzf dotnet.tar.gz ./packs ./sdk ./templates ./LICENSE.txt ./ThirdPartyNotices.txt \
    && rm dotnet.tar.gz \
    # Trigger first run experience by running arbitrary cmd
    && dotnet help

# Install PowerShell global tool
RUN powershell_version=7.0.3 \
    && wget -O PowerShell.Linux.Alpine.$powershell_version.nupkg https://pwshtool.blob.core.windows.net/tool/$powershell_version/PowerShell.Linux.Alpine.$powershell_version.nupkg \
    && powershell_sha512='f9f5af68271319a2c42f54bf8dd0eca1a7d11d88582afeadcb3c3ede5175d74b015bed0b668ac3579508efc285690bd1103c249ab55d6664a6754afaa15f3686' \
    && echo "$powershell_sha512  PowerShell.Linux.Alpine.$powershell_version.nupkg" | sha512sum -c - \
    && mkdir -p /usr/share/powershell \
    && dotnet tool install --add-source / --tool-path /usr/share/powershell --version $powershell_version PowerShell.Linux.Alpine \
    && dotnet nuget locals all --clear \
    && rm PowerShell.Linux.Alpine.$powershell_version.nupkg \
    && chmod 755 /usr/share/powershell/pwsh \
    && ln -s /usr/share/powershell/pwsh /usr/bin/pwsh \
    # To reduce image size, remove the copy nupkg that nuget keeps.
    && find /usr/share/powershell -print | grep -i '.*[.]nupkg$' | xargs rm \
    # Add ncurses-terminfo-base to resolve psreadline dependency
    && apk add --no-cache ncurses-terminfo-base

# Install ASP.NET Core
RUN aspnetcore_version=3.1.10 \
    && wget -O aspnetcore.tar.gz https://dotnetcli.azureedge.net/dotnet/aspnetcore/Runtime/$aspnetcore_version/aspnetcore-runtime-$aspnetcore_version-linux-musl-x64.tar.gz \
    && aspnetcore_sha512='1a596c6f413c1f37ec6d3f0be74a19a9614d2321b5ab75290d5722ae824206dedf05e8773deac17330c4e9eff97caa56f5e59f5a6fd5d3543d3b8b4f67bbc8b3' \
    && echo "$aspnetcore_sha512  aspnetcore.tar.gz" | sha512sum -c - \
    && tar -ozxf aspnetcore.tar.gz -C /usr/share/dotnet ./shared/Microsoft.AspNetCore.App \
    && rm aspnetcore.tar.gz

RUN apk update && apk add bash && apk add procps


COPY . /app
WORKDIR /app

# To Create Database
RUN chmod +x ./dbbuild.sh
CMD /bin/bash ./dbbuild.sh

# Install entityframework tool in container
RUN dotnet tool install --global dotnet-ef 
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]

# Expose your port
EXPOSE 5001/tcp

# For Debugging need to install vsdgg
# more to see :https://github.com/OmniSharp/omnisharp-vscode/wiki/Attaching-to-remote-processes
RUN wget https://aka.ms/getvsdbgsh -O - 2>/dev/null | /bin/sh /dev/stdin -v latest -l ~/vsdbg

# Make a permission for entries
RUN chmod +x ./entrypoint.sh
RUN chmod +x /root/vsdbg/vsdbg

ENTRYPOINT [ "./entrypoint.sh" ]