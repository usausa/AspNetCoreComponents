namespace Example.Web.Areas.Default.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Example.Models.Paging;
    using Example.Services;
    using Example.Web.Areas.Default.Models;
    using Example.Web.Infrastructure.Filters;
    using Example.Web.Infrastructure.Mvc;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [AllowAnonymous]
    public class DashboardController : BaseDefaultController
    {
        private const int PageSize = 10;

        private IMapper Mapper { get; }

        private DataService DataService { get; }

        public DashboardController(
            IMapper mapper,
            DataService dataService)
        {
            Mapper = mapper;
            DataService = dataService;
        }

        [DefaultRoute]
        [PageCorrect]
        [HttpGet]
        public async ValueTask<IActionResult> Index([FromQuery] DashboardIndexForm form)
        {
            if (ModelState.IsValid && form.Go)
            {
                var parameter = Mapper.Map<DataSearchParameter>(form).SetSize(PageSize);
                ViewBag.Paged = await DataService.QueryAccountPagedAsync(parameter);
            }

            return View(form);
        }
    }
}
