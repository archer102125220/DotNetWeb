# DotNetWeb 部署說明

本文件記錄 ASP.NET Core Razor Pages 專案 (DotNetWeb) 的部署前準備與常見環境部署方式。

## 1. 部署前準備

### 1.1 發行專案 (Publish)

在將專案部署到生產環境前，需先將程式碼編譯並發行為適合生產環境的檔案：

```bash
# 在專案根目錄執行發行指令
# -c Release: 使用 Release 模式編譯 (會進行最佳化)
# -o ./publish: 指定輸出目錄為當前目錄下的 publish 資料夾
dotnet publish -c Release -o ./publish
```

執行完成後，`publish` 資料夾內會包含執行應用程式所需的所有檔案（`.dll`、組態檔、靜態資源等）。

### 1.2 環境變數設定

ASP.NET Core 依賴 `ASPNETCORE_ENVIRONMENT` 環境變數來決定載入哪一個設定檔（如 `appsettings.Production.json`）。

- **開發環境**: `ASPNETCORE_ENVIRONMENT=Development`
- **生產環境**: `ASPNETCORE_ENVIRONMENT=Production`

確保在部署的目標環境中，正確設定此變數。

---

## 2. 部署策略

### 方式一：Docker 容器化部署 (推薦)

使用 Docker 可以確保開發與生產環境的一致性，且易於擴展。

**1. 建立 Dockerfile**
在專案根目錄建立 `Dockerfile`：

```dockerfile
# 階段 1: 建置環境
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["DotNetWeb.csproj", "./"]
RUN dotnet restore "DotNetWeb.csproj"
COPY . .
RUN dotnet publish "DotNetWeb.csproj" -c Release -o /app/publish

# 階段 2: 執行環境
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DotNetWeb.dll"]
```

**2. 建立與執行 Image**

```bash
# 建立 Image
docker build -t dotnetweb:latest .

# 執行 Container (將本機的 8080 port 映射到容器的 80 port)
docker run -d -p 8080:8080 -e ASPNETCORE_ENVIRONMENT=Production --name my_dotnetweb dotnetweb:latest
```

### 方式二：Linux 環境部署 (Nginx + Kestrel)

在 Linux 環境中，通常會使用 Kestrel 作為內部應用程式伺服器，並搭配 Nginx 作為反向代理伺服器（Reverse Proxy），以處理 HTTPS、靜態檔案與對外連線。

**1. 設定 Systemd 服務 (確保 Kestrel 持續執行)**
建立 `/etc/systemd/system/dotnetweb.service`：

```ini
[Unit]
Description=ASP.NET Core DotNetWeb App

[Service]
WorkingDirectory=/var/www/dotnetweb/publish
ExecStart=/usr/bin/dotnet /var/www/dotnetweb/publish/DotNetWeb.dll
Restart=always
# 發生崩潰後等待 10 秒再重啟
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=dotnetweb
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
```

**2. 啟動服務**
```bash
sudo systemctl enable dotnetweb.service
sudo systemctl start dotnetweb.service
```

**3. 設定 Nginx 反向代理**
在 `/etc/nginx/sites-available/dotnetweb` 設定檔加入：

```nginx
server {
    listen 80;
    server_name example.com; # 替換為您的網域

    location / {
        proxy_pass         http://localhost:5000; # Kestrel 預設或指定的 Port
        proxy_http_version 1.1;
        proxy_set_header   Upgrade $http_upgrade;
        proxy_set_header   Connection keep-alive;
        proxy_set_header   Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header   X-Forwarded-Proto $scheme;
    }
}
```

### 方式三：Windows 環境部署 (IIS)

在 Windows 伺服器上部署，通常會搭配 IIS (Internet Information Services)。

**1. 安裝 .NET Hosting Bundle**
至微軟官網下載並安裝與專案對應版本的 **.NET Core Hosting Bundle**，這會安裝 .NET 執行階段以及 IIS 的 ASP.NET Core 模組。

**2. 設定 IIS 站台**
- 在 IIS 管理員中「新增網站」。
- 將「實體路徑」指向 `publish` 資料夾。
- 應用程式集區的「.NET CLR 版本」應設定為 **[沒有 Managed 程式碼]** (No Managed Code)，因為 Kestrel 會自行處理程序。

**3. 環境變數設定**
若要在 IIS 中設定環境變數，可透過發行目錄下的 `web.config`：

```xml
<configuration>
  <location path="." inheritInChildApplications="false">
    <system.webServer>
      <handlers>
        <add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModuleV2" resourceType="Unspecified" />
      </handlers>
      <aspNetCore processPath="dotnet" arguments=".\DotNetWeb.dll" stdoutLogEnabled="false" stdoutLogFile=".\logs\stdout" hostingModel="inprocess">
        <environmentVariables>
          <environmentVariable name="ASPNETCORE_ENVIRONMENT" value="Production" />
        </environmentVariables>
      </aspNetCore>
    </system.webServer>
  </location>
</configuration>
```
