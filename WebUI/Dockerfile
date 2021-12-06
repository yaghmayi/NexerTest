# Build
FROM microsoft/dotnet:2.1-sdk as build-env
WORKDIR /source
COPY . .
RUN dotnet publish TranslationTool2.Web/TranslationTool2.Web.csproj -o /publish -c Release -r linux-x64
 
# Publish
FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /publish .

# Install GIT
RUN apt-get update \
	&& apt-get install -y git
	
# Make ssh dir
RUN mkdir /root/.ssh/

# Copy over private key, and set permissions
ADD id_rsa /root/.ssh/id_rsa
# Set readonly permissions
RUN chmod 400 ~/.ssh/id_rsa

# Adding git.playngo.com's IP in hosts file
# RUN echo "10.1.10.70 git.playngo.com" >> /etc/hosts
# Create known_hosts
RUN touch /root/.ssh/known_hosts
# Add bitbuckets fingerPrint to known_hosts file
#RUN ssh-keyscan -p 7999 git.playngo.com >> /root/.ssh/known_hosts
RUN ssh-keyscan -p 7999 10.1.10.70 >> /root/.ssh/known_hosts
	
# Change directory and clone repo
RUN mkdir /usr/git \
	&& mkdir /usr/git/translationtooltest \
    && cd /usr/git/translationtooltest \
    && git clone ssh://git@10.1.10.70:7999/it/translationtooltest.git

# Needed for EPPlus.Core nuget package
RUN apt-get update \
    && apt-get install -y --no-install-recommends libgdiplus libc6-dev \
    && rm -rf /var/lib/apt/lists/*
	
ENTRYPOINT ["dotnet", "AyoubTest.WebTest.dll"]

