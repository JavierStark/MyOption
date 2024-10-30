using FluentAssertions;

namespace MyOption;

public class Tests
{
    [Test]
    public void WhenNoneIsNone()
    {
        OurOption.None().IsNone.Should().BeTrue();
    }

    [Test]
    public void WhenSomeIsNotNone()
    {
        OurOption.Some(1).IsSome.Should().BeTrue();
    }

    [Test]
    public void ZeroIsSome()
    {
        OurOption.Some(0).IsSome.Should().BeTrue();
    }

    [Test]
    public void MatchWhenSome()
    {
        OurOption.Some(0).Match(
            none: Assert.Fail,
            some: x => x.Should().Be(0)
        );
    }

    [Test]
    public void MatchWhenNone()
    {
        OurOption.None().Match(
            none: Assert.Pass,
            some: x => Assert.Fail()
        );
    }

    [Test]
    public void MapWhenSome()
    {
        OurOption.Some(1).Map(x => x * 2).Match(
            none: Assert.Fail,
            some: x => x.Should().Be(2)
        );
    }

    [Test]
    public void MapWhenNone()
    {
        OurOption.None().Map(x => x * 2).IsNone.Should().BeTrue();
    }

    [Test]
    public void BindWhenNone()
    {
    }
}

class N
{
    public int Substract(int a, int b)
    {
        
    }
}

public class OurOption
{
    private readonly int n;
    public bool IsNone { get; } = true;
    public bool IsSome => !IsNone;
    public OurOption(int n)
    {
        this.n = n;
        IsNone = false;
    }

    private OurOption()
    {
        
    }

    public static OurOption None()
    {
        return new OurOption();
    }

    public static OurOption Some(int n)
    {
        return new OurOption(n);
    }

    public void Match(Action none, Action<int> some)
    {
        if (IsNone)
            none();
        else
            some(n);
    }

    public OurOption Map(Func<int, int> func)
    {
        return IsNone ? this : new OurOption(func(n));
    }
}