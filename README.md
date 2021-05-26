# MVCPractice  ASP.NET CORE MVC 電商平台
## ASP.NET CORE Identity
使用ASP.NET CORE Identity作會員管理機制，會員註冊及登入, 透過IdentityRole識別會員身份, 限制存取權
管理員享有對訂單，產品管理權限，部分頁面檢視權限，管理員帳號亦只能經由管理員帳號新增

## EntityFrameWork Core
與DB溝通的工具，透過繼承Dbcontext可以定義跟資料庫溝通的行為
### Data Annotations Attributes
可設定模型中PK/FK關係,可否為NUL, 輸入數值限制

## Validation Control
資料檢驗模式

## HttpContext
存取Session裡的JSON物件，於本方案用作存取購物車物件

## Dependency Injection
解決兩個類別間耦合性過高的問題

## ViewModel
把來自一個或多個模型的資料來源變成一個物件
