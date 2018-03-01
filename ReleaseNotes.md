# Version 6.0

## P3Net.Kraken

### ```TypeConversion```

- [New] ```TryConvertToBooleean``` added to convert to boolean values using the rules defined in other areas of the library.

## P3Net.Kraken.Configuration

- [Breaking Change] Removed ```ConfigurationElementEx``` and ```ConfigurationSectionEx```.
- [New] Added ```ConfigurationElementCollection<T>``` and ```ConfigurationElementCollection<TValue, TKey>```.

## P3Net.Kraken.Data

### ```ConnectionManager```

- [New] (Preliminary) Added Async support for query methods.
- [Breaking Change] Changed return type of ```ExecuteQueryWithResults``` from ```T[]``` to ```IEnumerable<T>```.
- [Breaking Change] Changed return type of ```QueryParameters``` from ```DataParameter[]``` to ```IEnumerable<DataParameter>```.
- [Breaking Change] Moved ```WithParameters``` to ```DataCommandExtensions``` as an extension method so it returns the correct type of the command.

- [New] ```ConnectionString``` 
This property specifies the connection string being used. When set (by the constructor or a derived type) the value
is analyzed to determine if it is a connection string or name. When specifying a connection string name, if the name cannot be found then an exception is thrown rather than waiting until the connection is created later.


### ```DataCommand```

- [New] Added ```OfType``` to ```InputParameter```, ```InputOutputParameter``` for cases where the value is not known at creation.

### ```DataCommandExtensions```

[New] Added type for ```DataCommand``` extensions.

### ```DataReaderExtensions```

- ```GetBooleanOrDefault``` has been updated to use ```TypeConversion.ToBooleanOrDefault```.
- [New] Added ```GetDate``` and ```GetDateOrDefault``` methods to work with ```Date``` values.

### ```DataRowExtensions```

- ```GetBooleanValueOrDefault``` and ```TryGetBooleanValue``` have been updated to use ```TypeConversion.ToBooleanOrDefault```.
- [New] Added ```GetDateValueOrDefault``` and ```TryGetDateValue``` methods to work with ```Date``` values.
- [New] Added ```TryGetDateTimeValue``` to be symmetric with other types.

## P3Net.Kraken.Linq [New]

Added new namespace and ```LinqExtensions``` to include some asynchronous LINQ commands. The separate namespace prevents collisions with other implementations (such as Entity Framework).


## P3Net.Kraken.Net.Http [New]

Added new package and assembly to support working with HTTP clients.

- [New] Added ```ErrorResponse``` to represent a standardized response for errors.
- [New] Added ```HeaderLinkUrl``` to work with LINK elements in headers.
- [New] Added ```HttpClientExtensions``` with extensions for working with ```HttpClient``` and JSON.
- [New] Added ```HttpClientManager``` to help manage the lifetime of ```HttpClient``` instances.
- [New] Added ```HttpContentExtensions``` with extensions for working with ```HttpContent```.
- [New] Added ```HttpResponseHeadersExtensions``` with extensions for working with ```HttpResponseHeaders```.
- [New] Added ```HttpResponseMessageExtensions``` with extensions for working with ```HttpResponseMessage```.
- [New] Added ```Standardheaders``` to work with standard HTTP headers.
- [New] Added ```StandardLinkTypes``` to identify standard LINK element types.