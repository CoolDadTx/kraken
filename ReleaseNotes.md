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


## P3Net.Kraken.Net.Http

[New] Added new package to support working with HTTP clients.