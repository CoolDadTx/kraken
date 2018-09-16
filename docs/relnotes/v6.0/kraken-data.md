# Kraken Data 6.0

- [New Package](#new-package)
- [P3Net.Kraken.Data Namespace](#p3net-kraken-data-namespace)
- [P3Net.Kraken.Data.Common Namespace](#p3net-kraken-data-common-namespace)

[Version Release Notes](readme.md)

## New Package

**[Breaking]** To support .NET Standard and allow for the new SDK project format limitations the `P3Net.Kraken.Data` assembly has been moved to its own package. Clients will need to update their package references.

## P3Net.Kraken.Data Namespace

#### DataReaderExtensions Class

- [New] `GetDate` method added to work with `Date` data types.
- [New] `GetDateOrDefault` method added to work with `Date` data types.
- `GetBooleanOrDefault` has been updated to use `TypeConversion.ToBooleanOrDefault`.

#### DataRowExtensions Class

- [New] `GetDateValueOrDefault` method added to work with `Date` data types.
- [New] `TryGetDateValueOrDefault` method added to work with `Date` data types.
- [New] `TryGetDateTimeValue` method added to be symmetric with other types. 
- `GetBooleanOrDefault` has been updated to use `TypeConversion.ToBooleanOrDefault`.
- `TryGetBooleanOrDefault` has been updated to use `TypeConversion.ToBooleanOrDefault`.

## P3Net.Kraken.Data.Common Namespace

#### ConnectionManager Class

- **[Breaking]** The `ExecuteQueryWithResults` method has changed the return type changed from `Array` to `IEnumerable<T>`.
- **[Breaking]** The `QueryParameters` method has changed the return type from `Array` to `IEnumerable<DataParameter>`.
- **[Breaking]** The `WithParameters` method was moved to an extension method so it can return the correct parameter type.
- [New] Async support was added to the methods that execute queries.
- [New] `ConnectionString` was switched from an internal property to a public property so it is accessible for reading outside the type and its derived types.

#### DataCommandExtensions Class

[New] This is a new type that provides some extensions for working with `DataCommand`.

- [New] `WithParameters` was moved from `DataCommand` to this type so it can return the correctly typed command.

#### InputParameter Class

- [New] `OfType` method added for cases where the value is not known at creation.

#### InputOutputParameter Class

- [New] `OfType` method for cases where the value is not known at creation.