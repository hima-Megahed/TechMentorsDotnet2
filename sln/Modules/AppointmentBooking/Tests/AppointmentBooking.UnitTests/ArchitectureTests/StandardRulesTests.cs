using NetArchTest.Rules;

namespace AppointmentBooking.UnitTests.ArchitectureTests;

public class StandardRulesTests
{
    private const string ModuleNamespace = "AppointmentBooking.Internal";

    [Fact]
    public void Interfaces_ShouldStartWithI()
    {
        var result = Types.InNamespace(ModuleNamespace)
            .That()
            .AreInterfaces()
            .Should()
            .HaveNameStartingWith("I")
            .GetResult();

        Assert.True(result.IsSuccessful, "All interfaces should start with the letter 'I'.");
    }
}