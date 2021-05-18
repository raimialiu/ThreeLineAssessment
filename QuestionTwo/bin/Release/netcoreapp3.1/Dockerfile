FROM mcr.microsoft.com/dotnet/sdk:3.1 AS runtime
WORKDIR /app
COPY . .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet QuestionTwo.dll