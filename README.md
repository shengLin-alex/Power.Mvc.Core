# Power.Mvc.Package dotnet core version

* Power.Mvc.Helper 共用程式庫，包含各種常用程式，如匯出 Excel、將指定路徑壓縮為 zip，etc...

* Power.Mvc.Repository 實作相依性反轉(IOC)之儲存庫程式，ORM 使用 EF6，提供 DbContextFactory 與其介面，同時封裝基本 CRUD

* Power.Mvc.Repository.Dapper 如 Power.Mvc.Repository，ORM 改為使用 Dapper + SimpleCRUD，提供 DbConnectionFactory 與其介面

* Power.Mvc.Web 為實際 Web 應用程式層所使用之程式庫
