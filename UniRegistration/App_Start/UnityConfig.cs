using Models;
using Repositories;
using Services;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace UniRegistration
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IStudentRepository, StudentRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IResultRepository<Result>, ResultRepository>();
            container.RegisterType<StudentService>();
            container.RegisterType<UserService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}