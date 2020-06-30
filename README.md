# Development
## Môi trường
- ASP.NET Core 3.1
- C# 8
- SQL Server
- IIS Express/Kestrel
- Visual Studio 2019/Rider 2019
- [Seq](https://datalust.co/seq)

## Cơ sở dữ liệu
- Import dữ liệu từ 3 file .bacpac
- Update lại connection string tương ứng trong `appsettings.json` của IdentityServer (identity.bacpac), ProjectHealthReport.Web (lastchance.bacpac) và Notification.Web (hangfire.bacpac)

## Chạy chương trình theo thứ tự
- IdentityServer
- ProjectHealthReport.Web
- Notification.Web

## Bản mới nhất
https://github.com/vuhieptran95/responsibility-chain

# Technical aspects 

## Add migration scripts
To add a new migration, run this command from repository's root

```
dotnet ef migrations add <migration_name> --startup-project ProjectHealthReport.Web/ --project ProjectHealthReport.Domains
```

```
dotnet ef migrations add <migration_name> --startup-project IdentityServer/ --project IdentityServer.Features
```

