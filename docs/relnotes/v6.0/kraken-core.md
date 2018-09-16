# Kraken Core 6.0

- [P3Net.Kraken Namespace](#p3net-kraken-namespace)
- [P3Net.Kraken.Configuration Namespace](#p3net-kraken-configuration-namespace)
- [P3Net.Kraken.Linq Namespace](#p3net-kraken-linq-namespace)

[Version Release Notes](readme.md)

## P3Net.Kraken Namespace

#### TypeConversion Class

- [New] `TryConvertToBooleean` added to convert to boolean values using the rules defined in other areas of the library.

## P3Net.Kraken.Configuration Namespace

#### ConfigurationElementCollection`T Class

[New] This type has been added to support generic [ConfigurationElementCollection](https://docs.microsoft.com/en-us/dotnet/api/system.configuration.configurationelementcollection) collections using string keys. There is also a more generic version that can be used for keys other than a string.

#### ConfigurationElementEx Class

**[Breaking]** This type has been removed.

#### ConfigurationSectionEx Class

**[Breaking]** This type has been removed.

- [New] Added `ConfigurationElementCollection<T>` and `ConfigurationElementCollection<TValue, TKey>` generic classes for working with configuration collections.

## P3Net.Kraken.Linq Nmaespace

[New] Added a new namespace to contain LINQ functionality. This keeps the Kraken-specific LINQ functionality separate from other libraries such as Entity Framework.

#### LinqExtensions Class

[New] This class adds some extension methods to [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1) including some asynchronous LINQ commands.
