# Kraken Data 6.1

- [Connection String Changes](#connection-string-changes)
- [P3Net.Kraken.Data.Common Namespace](#p3net-kraken-data-common-namespace)
- [P3Net.Kraken.Data.Sql Namespace](#p3net-kraken-data-sql-namespace)

[Version Release Notes](readme.md)

## Connection String Changes

After much thought the connection string handling for `ConnectionManager` has been partially reverted back. During the 6.0 timeframe support for specifying either a connection string or name was removed because the `Configuration` namespace is not used in .NET Standard. In 6.1 the functionality has been partially restored by extension methods.

The `ConnectionManagerExtensions` class provides some fluent methods for setting the connection string. The methods will hopefully make the code more clear on what type of connection string is being specified.

These changes impact all the `ConnectionManager`-based types.

[Details](https://github.com/CoolDadTx/kraken/issues/25)

## P3Net.Kraken.Data.Common Namespace

#### ConnectionManager Class

- **[New]** The default constructor has been added back in. It was removed in 6.0 but is now the preferred way to create a connection.
- **[Deprecated]** The constructor accepting a connection string has been deprecated. Code should use one of the extension methods instead. The existing constructor has been reverted to its pre-6.0 behavior where it supports either a connection string or name. 
- The `ConnectionString` property can now be set by anyone. Derived types can override the `SetConnectionString` method to validate it if needed.

#### ConnectionManagerExtensions Class

[New] This is a new type that provides some extensions for working with `ConnectionManager`. The primary use for this type right now is to provide a fluent way to set the connection string. The fluent interface will make it easier to distinguish the usage of connection strings.

- [New] `WithConnectionString` replaces the constructor on `ConnectionManager` that accepts a connection string. 
- [New] `WithConnectionStringName` provides a way to set the connection string using a name from the configuration file. At this time this method is only available on .NET Framework.

#### DbProviderFactoryConnectionManager Class

- **[New]** The default constructor has been added back in. It was removed in 6.0 but is now the preferred way to create a connection.
- **[Deprecated]** The constructor accepting a connection string has been deprecated. Code should use one of the extension methods instead. The existing constructor has been reverted to its pre-6.0 behavior where it supports either a connection string or name. 

## P3Net.Kraken.Data.Sql Namespace

#### SqlConnectionManager Class

- **[New]** The default constructor has been added back in. It was removed in 6.0 but is now the preferred way to create a connection.
- **[Deprecated]** The constructor accepting a connection string has been deprecated. Code should use one of the extension methods instead. The existing constructor has been reverted to its pre-6.0 behavior where it supports either a connection string or name. 
