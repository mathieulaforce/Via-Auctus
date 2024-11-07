using System.Reflection;
using FluentValidation;
using LaMa.Via.Auctus.Application.Abstractions;
using NetArchTest.Rules;

namespace LaMa.Via.Auctus.Architecture.Tests;

public class ApplicationTests
{
    private readonly Assembly ApplicationAssembly = ViaAuctusAssemblies.ApplicationAssembly;
    
    [Fact]
    public void GivenCommandHandlerShouldHaveNameEndingWithCommandHandler()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Or()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .HaveNameEndingWith("CommandHandler")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void GivenCommandHandlerShouldNotBePublic()
    {
        TestResult result = Types.InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<>))
            .Or()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .NotBePublic()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    // [Fact]
    // public void QueryHandler_ShouldHave_NameEndingWith_QueryHandler()
    // {
    //     var result = Types.InAssembly(ApplicationAssembly)
    //         .That()
    //         .ImplementInterface(typeof(IQueryHandler<,>))
    //         .Should()
    //         .HaveNameEndingWith("QueryHandler")
    //         .GetResult();
    //
    //     result.IsSuccessful.Should().BeTrue();
    // }
    //
    // [Fact]
    // public void QueryHandler_Should_NotBePublic()
    // {
    //     var result = Types.InAssembly(ApplicationAssembly)
    //         .That()
    //         .ImplementInterface(typeof(IQueryHandler<,>))
    //         .Should()
    //         .NotBePublic()
    //         .GetResult();
    //
    //     result.IsSuccessful.Should().BeTrue();
    // }

    [Fact]
    public void GivenValidatorShouldHaveNameEndingWithValidator()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .Inherit(typeof(AbstractValidator<>))
            .Should()
            .HaveNameEndingWith("Validator")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void GivenValidatorShouldNotBePublic()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .Inherit(typeof(AbstractValidator<>))
            .Should()
            .NotBePublic()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}