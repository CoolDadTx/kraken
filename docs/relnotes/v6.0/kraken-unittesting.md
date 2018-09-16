# Kraken Unit Testing 6.0

- [P3Net.Kraken.UnitTesting Namespace](#p3net-kraken-unittesting-namespace)

[Version Release Notes](readme.md)

## P3Net.Kraken.UnitTesting Namespace

#### AssertionExtensions Class
 
- *[Deprecated]* `ShouldThrowArgumentException` is deprecated. Use `Should().Throw<ArgumentException>` instead.
- *[Deprecated]* `ShouldThrowArgumentNullException` is deprecated. Use `Should().Throw<ArgumentNullException>` instead.
- *[Deprecated]* `ShouldThrowArgumentOutOfRangeException` is deprecated. Use `Should().Throw<ArgumentOutOfRangeException>` instead.

#### UnitTest Class

- `AssertAdministrator` is not available in .NET Standard.
- `User` has been modified to return `IPrincipal` instead of `WindowsPrincipal`. For the .NET Framework it continues to behave as before. For .NET Standard it simply returns [Thread.CurrentPrincipal](https://docs.microsoft.com/en-us/dotnet/api/system.threading.thread.currentprincipal).
- `IsUserAdministrator` is not available in .NET Standard.