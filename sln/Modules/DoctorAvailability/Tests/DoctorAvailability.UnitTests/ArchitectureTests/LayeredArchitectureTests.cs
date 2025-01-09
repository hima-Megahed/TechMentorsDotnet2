using NetArchTest.Rules;

namespace DoctorAvailability.UnitTests.ArchitectureTests;

public class LayeredArchitectureTests
{
    private const string ModuleNamespace = "DoctorAvailability";

    [Fact]
    public void Data_ShouldNotDependOnBusinessOrPresentation()
    {
        var result = Types.InNamespace($"{ModuleNamespace}.Data")
            .ShouldNot()
            .HaveDependencyOnAll($"{ModuleNamespace}.Business", $"{ModuleNamespace}.Presentation")
            .GetResult();

        Assert.True(result.IsSuccessful, "Domain layer should not depend on Business or Presentation layers.");
    }

    [Fact]
    public void Business_ShouldOnlyDependOnData()
    {
        var result = Types.InNamespace($"{ModuleNamespace}.Business")
            .Should()
            .OnlyHaveDependenciesOn($"{ModuleNamespace}.Data")
            .GetResult();

        Assert.True(result.IsSuccessful, "Business layer should only depend on Data, not Presentation.");
    }

    [Fact]
    public void Presentation_CanDependOnDataAndBusiness()
    {
        var result = Types.InNamespace($"{ModuleNamespace}.Presentation")
            .That()
            .ResideInNamespace($"{ModuleNamespace}.Presentation")
            .ShouldNot()
            .HaveDependenciesOtherThan($"{ModuleNamespace}.Business", $"{ModuleNamespace}.Data", $"{ModuleNamespace}.Shared")
            .GetResult();

        Assert.True(result.IsSuccessful, "Presentation should not depend on any other layer than Data and Business.");
    }
}
