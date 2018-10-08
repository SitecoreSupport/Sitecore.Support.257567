namespace Sitecore.Support.XA.Feature.PageContent.Controllers
{
  using Sitecore.XA.Feature.PageContent.Repositories;
  using Sitecore.XA.Foundation.RenderingVariants.Controllers.Base;
  using Sitecore.XA.Foundation.RenderingVariants.Lists.Pagination;
  using System.Collections.Generic;
  using System.Linq;

  public class PageListController : Sitecore.Supoprt.XA.Foundation.RenderingVariants.Controllers.Base.PaginableController
  {
    protected readonly IPageListRepository PageListRepository;

    protected override IListPagination PaginationConfiguration => ListPaginationContext.GetCurrent().Get(base.Rendering.Parameters["ListSignature"]) ?? new Sitecore.Support.XA.Foundation.RenderingVariants.Lists.Pagination.ListPagination((IDictionary<string, string>)base.Rendering.Parameters.ToDictionary((KeyValuePair<string, string> p) => p.Key, (KeyValuePair<string, string> p) => p.Value), PageListRepository.Items.Count());

    public PageListController(IPageListRepository pageListRepository)
    {
      PageListRepository = pageListRepository;
    }

    protected override object GetModel()
    {
      return PageListRepository.GetModel(PaginationConfiguration);
    }
  }
}