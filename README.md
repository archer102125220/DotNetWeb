# DotNetWeb

這是一個用於學習目的的 .NET 專案。

## 什麼是 Razor Pages？
Razor Pages 是 ASP.NET Core 中的一種頁面導向 (Page-Based) 網頁開發模型。它將 UI 邏輯（HTML/Razor）和頁面模型（C# 程式碼）組織在同一個檔案結構中（`.cshtml` 與 `.cshtml.cs`），讓開發更加直覺且聚焦於單一頁面的功能。相較於傳統的 MVC (Model-View-Controller) 架構，它大幅減少了在不同資料夾間切換的麻煩。

### 什麼情況適合使用 Razor Pages？
- **頁面導向的應用程式**：當您的應用程式大部分邏輯都是針對個別頁面進行資料讀寫（例如簡單的 CRUD 操作、表單提交）時。
- **輕量級專案與快速開發**：相比 MVC 需要建立 Controller、Action 與 View，Razor Pages 透過單純的 Page Model 設計，讓開發速度更快，非常適合中小型 Web 應用。
- **高內聚力的程式碼組織**：若希望將與某特定頁面高度相關的 HTML 結構與後端邏輯綁定在一起，以提高程式碼的可維護性時。

## 建立專案
以下紀錄了如何透過指令建立 .NET Web 專案：

### 建立 Razor Pages Web 專案 (DotNetWeb)
```bash
dotnet new webapp -n DotNetWeb
```

### 建立 MVC Web 專案 (DotNetMvcWeb)
```bash
dotnet new mvc -n DotNetMvcWeb
```

## 加入與設定 `.gitignore`
要為 .NET 專案加入最佳實務的 `.gitignore`，可以使用 .NET CLI 提供的內建指令，這樣就不需要手動設定要排除的建置暫存檔（如 `bin/`、`obj/`）與開發工具設定檔（如 Visual Studio、VS Code）：

```bash
# 在專案根目錄下產生針對 .NET 專案的 .gitignore 檔案
dotnet new gitignore
```

## 如何啟動專案
您可以透過以下指令啟動此專案：

**一般執行模式：**
```bash
dotnet run
```

**開發者模式 (熱重載 Hot Reload)：**
（推薦使用此模式，當你修改程式碼並存檔時，API 伺服器會自動重新載入，無須手動重啟）
```bash
dotnet watch run
```
