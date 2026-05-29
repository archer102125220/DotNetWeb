# 專案結構說明 (DotNetWeb)

這份文件旨在說明本 ASP.NET Core Razor Pages 專案的資料夾結構與重要設定檔，幫助開發者快速了解專案組成。

## 資料夾結構

- **`Pages/`**
  - **用途**: 存放 Razor Pages 頁面檔案（`.cshtml`）及其對應的後端邏輯檔（Code-Behind，`.cshtml.cs`）。這是 Razor Pages 專案中處理 UI 與頁面請求的核心。
  - **重要檔案**:
    - `_ViewImports.cshtml`: 用於全域匯入命名空間 (namespace) 或 Tag Helper，讓所有 Razor 頁面都能使用。
    - `_ViewStart.cshtml`: 設定所有頁面的預設 Layout (排版)。
    - `Shared/`: 存放共用的視圖元件，如 `_Layout.cshtml` (主版頁面)。

- **`wwwroot/`**
  - **用途**: 存放網頁前端的靜態資源檔案（Static Files）。
  - **內容**: 包含 CSS 樣式表、JavaScript 腳本、圖片檔案等。這裡的檔案可以直接透過瀏覽器公開存取。

- **`Models/`**
  - **用途**: 存放資料模型 (Data Models) 或檢視模型 (View Models)。
  - **說明**: 這些類別通常用於定義資料庫結構、或是用來在頁面與後端之間傳遞資料結構。

- **`Services/`**
  - **用途**: 存放業務邏輯與外部服務整合的類別。
  - **說明**: 將商業邏輯從 Razor Pages 的後端程式碼中抽離，以保持程式碼整潔並提升可測試性 (通常會搭配依賴注入 Dependency Injection 來使用)。

- **`Properties/`**
  - **用途**: 存放專案本地開發與執行時的環境設定檔。
  - **重要檔案**: `launchSettings.json` (詳見下方說明)。

## 重要設定檔

- **`Program.cs`**
  - **用途**: 應用程式的進入點 (Entry Point) 與核心設定檔。
  - **說明**: 負責註冊依賴注入 (Dependency Injection) 的服務、以及設定 HTTP 請求處理管線 (Middleware Pipeline)，例如設定靜態檔案路由、例外處理，以及啟動 Razor Pages 等。

- **`appsettings.json` / `appsettings.Development.json`**
  - **用途**: 應用程式的全域組態設定檔。
  - **說明**: 用來存放如資料庫連線字串 (Connection Strings)、API 金鑰、Log 層級等設定。`.Development.json` 會在開發環境下覆蓋預設的 `appsettings.json` 設定。

- **`Properties/launchSettings.json`**
  - **用途**: 開發環境的啟動設定檔。
  - **說明**: 僅在本地開發 (Local Development) 時生效。用來設定專案啟動時的 URL 埠號 (Ports)、設定本機啟動設定、以及環境變數（例如將 `ASPNETCORE_ENVIRONMENT` 設為 `Development`）。

- **`DotNetWeb.csproj`**
  - **用途**: MSBuild C# 專案檔。
  - **說明**: 紀錄專案的目標框架 (Target Framework)、引用的 NuGet 套件與其版本，以及其他編譯與專案層級的建置設定。

- **`.gitignore`**
  - **用途**: Git 版本控制忽略清單。
  - **說明**: 指定哪些檔案或資料夾 (例如編譯產生的 `bin/`, `obj/` 或本地環境的 `.env`) 不應該被推送到 Git 儲存庫中，以保持專案庫乾淨。
