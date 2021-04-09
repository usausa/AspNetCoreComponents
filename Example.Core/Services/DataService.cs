namespace Example.Services
{
    using System.Threading.Tasks;

    using Example.Accessors;
    using Example.Models.Entity;
    using Example.Models.Paging;

    using Smart.Data.Accessor;

    public class DataSearchParameter : Pageable
    {
        public bool? Flag { get; set; }
    }

    public class DataService
    {
        private IDataAccessor DataAccessor { get; }

        public DataService(
            IAccessorResolver<IDataAccessor> dataAccessor)
        {
            DataAccessor = dataAccessor.Accessor;
        }

        public async ValueTask<Paged<DataEntity>> QueryAccountPagedAsync(DataSearchParameter parameter)
        {
            var list = await DataAccessor.QueryDataListAsync(parameter.Flag, parameter.Size, parameter.Offset).ConfigureAwait(false);
            var count = await DataAccessor.CountDataAsync(parameter.Flag).ConfigureAwait(false);
            return new Paged<DataEntity>(parameter, list, count);
        }
    }
}
