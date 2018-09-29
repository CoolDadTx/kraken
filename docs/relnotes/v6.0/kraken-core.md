# Kraken Core 6.0

- [P3Net.Kraken Namespace](#p3net-kraken-namespace)
- [P3Net.Kraken.Configuration Namespace](#p3net-kraken-configuration-namespace)
- [P3Net.Kraken.Interop Namespace](#p3net-kraken-interop-namespace)
- [P3Net.Kraken.IO Namespace](#p3net-kraken-io-namespace)
- [P3Net.Kraken.Linq Namespace](#p3net-kraken-linq-namespace)
- [P3Net.Kraken.Reflection Namespace](#p3net-kraken-reflection-namespace)
- [P3Net.Kraken.Win32 Namespace](#p3net-kraken-win32-namespace)

[Version Release Notes](readme.md)

## P3Net.Kraken Namespace

#### RandomExtensions Class

Added new class to provide extension methods for working with random values. [Details](https://github.com/CoolDadTx/kraken/issues/3)

- [New] `NextDate` method returns a random date, optionally limited to a range of dates.

#### StringExtensions Class

- [New] `Coalesce` method added to find the first non-null string. [Details](https://github.com/CoolDadTx/kraken/issues/11)
- [New] `EnsureSurroundedWith` method added to combine `EnsureStartsWith` and `EnsureEndsWith`. [Details](https://github.com/CoolDadTx/kraken/issues/11)
- [New] `IndexOfAll` method added to find all occurrences of a set of characters in a string.
- [New] `Truncate` method added to truncate a string to a specific length. [Details](https://github.com/CoolDadTx/kraken/issues/11)

#### TypeConversion Class

- [New] `TryConvertToBooleean` added to convert to boolean values using the rules defined in other areas of the library.

## P3Net.Kraken.Configuration Namespace

This namespace is not available for .NET Standard. It can continue to be used by .NET Framework applications.

#### ConfigurationElementCollection`T Class

[New] This type has been added to support generic [ConfigurationElementCollection](https://docs.microsoft.com/en-us/dotnet/api/system.configuration.configurationelementcollection) collections using string keys. There is also a more generic version that can be used for keys other than a string.

#### ConfigurationElementEx Class

**[Breaking]** This type has been removed.

#### ConfigurationSectionEx Class

**[Breaking]** This type has been removed.

- [New] Added `ConfigurationElementCollection<T>` and `ConfigurationElementCollection<TValue, TKey>` generic classes for working with configuration collections.

## P3Net.Kraken.Interop Namespace

Microsoft has deprecated [SecureString](https://github.com/dotnet/platform-compat/blob/master/docs/DE0001.md).

## P3Net.Kraken.IO Namespace

#### FileSystemHelper Class

**[Breaking]** This pre-`PathExtensions` class was removed from the project. It wasn't used by the library.

#### PathExtensions Class

Some of the methods in this class rely on Windows-specific calls. They remain unchanged in .NET Framework. For .NET Standard they have been modified to not rely on Windows.

- `CompactPath` uses a different algorithm to mimic `PathCompactPathEx`.
- `GetCommonPath` uses a different algorithm to mimic `PathCommonPrefix`.
- `GetRelativePath` uses a different algorithm to mimic `PathRelativePathTo`.
- `IsRelative` uses a different algorithm to mimic `PathIsRelative`.
- `IsUNC` uses a different algorithm to mimic `PathIsUNC`.

- `GetRelativePath` now has an overload that specifies whether the bases path is a file or folder. This helps with cases where you are using a dotted path and the parser gets confused. The existing method calls the new implementation with the flag set to `false` as this is the common case.

## P3Net.Kraken.Linq Nmaespace

[New] Added a new namespace to contain LINQ functionality. This keeps the Kraken-specific LINQ functionality separate from other libraries such as Entity Framework.

#### LinqExtensions Class

[New] This class adds some extension methods to [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1) including some asynchronous LINQ commands.

## P3Net.Kraken.Reflection Namespace

#### AssemblyDetails Class

- *[Deprecated]* `BuildDate` is deprecated. It relied on linker date embedded in the PE file but with [deterministic builds](https://gist.github.com/aelij/b20271f4bd0ab1298e49068b388b54ae) that information is no longer set.

## P3Net.Kraken.Win32 Namespace

This namespace is not available for .NET Standard. It can continue to be used by .NET Framework applications.