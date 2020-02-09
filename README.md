# 校园闲散资源共享平台
 
## 前端库

使用LibMan作为本项目的客户端库获取工具，详细请参见[通过 LibMan 在 ASP.NET Core 中获取客户端库](https://docs.microsoft.com/zh-cn/aspnet/core/client-side/libman)

要使用LibMan的CLI，请参考[将 LibMan 命令行接口（CLI）用于 ASP.NET Core](https://docs.microsoft.com/zh-cn/aspnet/core/client-side/libman/libman-cli)

编译前需要在项目文件夹`CampusResourceSharingPlatform.Web`内使用LibMan的CLI进行依赖包还原
```
libman restore
```

## IdentityUser

###内置账户
|账号|密码|账户类型|
|:-----:|:-----:|:-----:|
|aaa290|aaa290|未建立角色|

###备注

使用用户名进行用户的注册与登录，在`Startup.cs`中设置
```csharp
options.SignIn.RequireConfirmedAccount = false
```
