using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Infra.WebApi.AppCode.IoCDependencyInjection;

/// <summary>
/// 依赖自动注入
/// </summary>
public class DependencyAutoInjection : Autofac.Module
{
    public string _projectName { get; set; }
    public DependencyAutoInjection(string projectName)
    {
        _projectName = projectName;
    }

    protected override void Load(ContainerBuilder builder)
    {
        //业务逻辑层程序集
        Assembly service = Assembly.Load($"Cloud.{_projectName}.Service");
        //数据库访问层程序集
        Assembly repository = Assembly.Load($"Cloud.{_projectName}.Repository");

        //自动注入
        builder.RegisterAssemblyTypes(service).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces();
        //自动注入
        builder.RegisterAssemblyTypes(repository).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();
    }
}
