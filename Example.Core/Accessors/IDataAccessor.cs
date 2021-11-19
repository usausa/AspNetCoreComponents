namespace Example.Accessors;

using System.Collections.Generic;

using System.Threading.Tasks;

using Example.Models.Entity;
using Smart.Data.Accessor.Attributes;

[DataAccessor]
public interface IDataAccessor
{
    [Query]
    ValueTask<List<DataEntity>> QueryDataListAsync(bool? flag, int limit, int offset);

    [ExecuteScalar]
    ValueTask<int> CountDataAsync(bool? flag);
}
