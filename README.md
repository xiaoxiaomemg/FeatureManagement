# FeatureManagement 的使用
官方教程：https://docs.microsoft.com/en-us/azure/azure-app-configuration/use-feature-flags-dotnet-core

前提：
1.记得更新你的SDK至最新版本
2.因为3.0非正式版 所以配置你的vs 启用SDK预览  位置：工具--设置--预览功能--使用SDK预览，此功能需要重启，如果重启仍不生效，恭喜 跟我踩了同样的坑，哈哈哈哈哈 来吧 直接更新你的VS  重启就好了 奥VS2019以上
3.安装所需要的包
包源：https://www.nuget.org/packages/

先建个web程序



1. 来 安装我们的包
 Install-Package Microsoft.FeatureManagement.AspNetCore -Version 2.0.0-preview-010610001-1263


2. 打开Startup.cs 添加以下代码 ConfigureServices 
public void ConfigureServices(IServiceCollection services)
{
    services.AddFeatureManagement();
    services.AddControllersWithViews();
}


 3. appsettings.json 是feature flags的配置源 在此文件进行配置
格式如下
"FeatureManagement": {
    "FeatureA": true, // Feature flag set to on
    "FeatureB": false, // Feature flag set to off
    "FeatureC": {
        "EnabledFor": [
            {
                "Name": "Percentage",
                "Parameters": {
                    "Value": 50
                }
            }
        ]
    }
}

主要分两部分：【 feature名称】+【过滤器列表】
【注】filters 过滤是按顺序遍历的 一旦有条件满足开启feature 剩余filters将直接跳过

4. 在 Index.cshtml 通过使用 feature 标签来进行相应配置 运行即可
@page
@model IndexModel
@{
    ViewData["Title"] = "Home page";
}
@using Microsoft.FeatureManagement
@inject IFeatureManager FeatureManager
@addTagHelper *,Microsoft.FeatureManagement.AspNetCore
@{ 
    ViewData["Title"] = "Home Page";
}

    <div class="text-center">
        <h1 class="display-4">Welcome</h1>
        <p>Learn about <a href="https://docs.microsoft.com/aspnet/core">building Web apps with ASP.NET Core</a>.</p>
        <feature name="NewFeatureFlag" requirement="All">
            <a asp-action="NewFeature"> Go to the new feature</a>
        </feature>
        <feature name="RandomFlag">
            <h2>I am a random flag! Guess when will I show up!</h2>
        </feature>
        <feature name="TimeFeatureFlag">
            <h2>I will go when the time out!</h2>
        </feature>
        <p>@DateTimeOffset.UtcNow.ToString()</p>

        <feature name="BrowserFeatureFlag">
            <p>You can see me only on Edge</p>
        </feature>
    </div>
