using AppointmentBooking.Application.BookAppointment.Commands;
using AppointmentBooking.Domain.Entities;
using AppointmentBooking.Infrastructure.Registrar;
using NetArchTest.Rules;

namespace AppointmentBooking.UnitTests.ArchitectureTests;

public class CleanArchitectureTests
{
    private const string ModuleNamespace = "AppointmentBooking";
    
    [Fact]
    public void Domain_ShouldNotDependOn_BusinessOrInfrastructureOrApi()
    {
        var result = Types.InAssembly(typeof(Appointment).Assembly)
            .ShouldNot()
            .HaveDependencyOnAny($"{ModuleNamespace}.Api",
                $"{ModuleNamespace}.Application",
                $"{ModuleNamespace}.Infrastructure")
            .GetResult();

        Assert.True(result.IsSuccessful,
            "Domain layer should not depend on Application, Infrastructure or Api layers.");
    }
    
    [Fact]
    public void Application_ShouldNotDependOn_ApiOrInfrastructureOrDomain()
    {
        var result = Types.InAssembly(typeof(BookAppointmentCommand).Assembly)
            .That()
            .ResideInNamespace($"{ModuleNamespace}.Application")
            .Should()
            .NotHaveDependencyOnAny($"{ModuleNamespace}.Infrastructure",
                $"{ModuleNamespace}.Api",
                $"{ModuleNamespace}.Domain")
            .GetResult();
        
        Assert.True(result.IsSuccessful,
            "Application layer should not depend on Api, Infrastructure or Domain layers.");
    }
    
    [Fact]
    public void InfrastructureRepositories_ShouldDependOn_Domain()
    {
        var result = Types
            .InAssembly(typeof(AppointmentBookingModuleRegistrar).Assembly)
            .That()
            .HaveNameEndingWith("Repository")
            .And()
            .AreClasses()
            .Should()
            .HaveDependencyOn($"{ModuleNamespace}.Domain")
            .GetResult();

        Assert.True(result.IsSuccessful);

    }
    
    [Fact]
    public void InfrastructureServices_ShouldDependOn_Application()
    {
        var result = Types
            .InAssembly(typeof(AppointmentBookingModuleRegistrar).Assembly)
            .That()
            .HaveNameEndingWith("Service")
            .And()
            .AreClasses()
            .Should()
            .HaveDependencyOn($"{ModuleNamespace}.Application")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
    
    [Fact]
    public void InfrastructureContext_ShouldDependOn_Domain()
    {
        var result = Types
            .InAssembly(typeof(AppointmentBookingModuleRegistrar).Assembly)
            .That()
            .HaveNameEndingWith("Context")
            .And()
            .AreClasses()
            .Should()
            .HaveDependencyOn($"{ModuleNamespace}.Domain")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}