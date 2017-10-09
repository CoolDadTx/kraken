P3Net.Kraken
-------------
1) [Breaking Change] Removed ConfigurationElementEx and ConfigurationSectionEx
2) [Preliminary] Added ConfigurationElementCollection<T> and ConfigurationElementCollection<TValue, TKey>

P3Net.Kraken.Data
----------------------
1) Added OfType to InputParameter, InputOutputParameter for cases where the value is not known at creation.
2) [Preliminary] Added Async support to ConnectionManager
3) [Breaking Change] Changed return type of ExecuteQueryWithResults from T[] to IEnumerable<T>
4) [Breaking Change] Changed return type of QueryParameters from DataParameter[] to IEnumerable<DataParameter>

