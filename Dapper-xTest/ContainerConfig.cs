using Autofac;
using Autofac.Extras.DynamicProxy;
using DapperReposotity;
using MySql.Data.MySqlClient;
using System.Data;


namespace Dapper_xTest
{
    public static class ContainerConfig
    {
        static IContainer container = null;
        static ContainerConfig()
        {
            var builder = new ContainerBuilder();
            RegisterTypes(builder);
            container = builder.Build();
        }
        private static string connectionString = "Server=localhost; Port=3306;Pooling=true;Max Pool Size =1024; Database=anno_shiro; Uid=root; Pwd=root;";
        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<SqlLog>();
            builder.RegisterInstance<IDbConnection>(new MySqlConnection(connectionString));

            builder.RegisterGeneric(typeof(DapperRepository<>)).As(typeof(IRepository<>)).InstancePerDependency();

            var assembly = typeof(DapperRepository<>).Assembly;
            builder.RegisterAssemblyTypes(assembly)
               .AsImplementedInterfaces()
               .InstancePerDependency()
               .InterceptedBy(typeof(SqlLog)).EnableInterfaceInterceptors(); ;
        }

        public static T Resolve<T>(ILifetimeScope scope = null) where T : class
        {
            if (scope == null)
            {
                scope = container.BeginLifetimeScope();
            }
            return (T)scope.Resolve(typeof(T));
        }


    }
}
