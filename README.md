# 校园闲散资源共享平台

## EF Core

要使用EF Core的CLI，请参考[Entity Framework Core tools reference - .NET CLI](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet)

需要运行以下命令
```Console
dotnet tool install --global dotnet-ef
```
 
## 前端库

使用LibMan作为本项目的客户端库获取工具，详细请参见[通过 LibMan 在 ASP.NET Core 中获取客户端库](https://docs.microsoft.com/zh-cn/aspnet/core/client-side/libman)

要使用LibMan的CLI，请参考[将 LibMan 命令行接口（CLI）用于 ASP.NET Core](https://docs.microsoft.com/zh-cn/aspnet/core/client-side/libman/libman-cli)

```Console
dotnet tool install -g Microsoft.Web.LibraryManager.Cli
```


编译前需要在项目文件夹`CampusResourceSharingPlatform.Web`内使用LibMan的CLI进行依赖包还原
```Console
libman restore
```

## IdentityUser

### 内置账户

|账号|密码|账户类型|
|:-----:|:-----:|:-----:|
|aaa290|aaa290|Administrator|

角色分级功能暂未实现

### EmailSender

使用SMTP发送验证邮件，请在用户机密或`appsettings.json`中配置SMTP服务器，如下：

```json
"EmailConfiguration": {
	"MailServer": "your smtp server and port",
	"MailPort": 0,
	"SenderName": "your name",
	"Sender": "your email",
	"Password": "your password"
}
```
