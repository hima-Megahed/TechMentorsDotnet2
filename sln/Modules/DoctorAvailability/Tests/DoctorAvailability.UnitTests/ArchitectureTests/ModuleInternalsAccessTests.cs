using NetArchTest.Rules;

namespace DoctorAvailability.UnitTests.ArchitectureTests;

public class ModuleInternalsAccessTests
{
    private const string ModuleNamespace = "DoctorAvailability.Internal";

    [Fact]
    public void Internal_ShouldNotBeAccessedByOtherModules()
    {
        var result = Types.InNamespace($"{ModuleNamespace}.Internal")
            .ShouldNot()
            .HaveDependencyOn($"{ModuleNamespace.Replace("AppointmentsDoctor", "")}") // Other module namespaces
            .GetResult();

        Assert.True(result.IsSuccessful, "Other modules should not depend on the internals of AppointmentsDoctor.");
    }
}
