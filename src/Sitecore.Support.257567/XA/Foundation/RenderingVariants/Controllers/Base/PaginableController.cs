namespace Sitecore.Supoprt.XA.Foundation.RenderingVariants.Controllers.Base
{
  using Microsoft.Extensions.DependencyInjection;
  using Sitecore.Data;
  using Sitecore.DependencyInjection;
  using Sitecore.Mvc.Presentation;
  using Sitecore.XA.Foundation.Mvc.Controllers;
  using Sitecore.XA.Foundation.Mvc.Wrappers;
  using Sitecore.XA.Foundation.RenderingVariants.Lists.Pagination;
  using Sitecore.XA.Foundation.RenderingVariants.Repositories;
  using Sitecore.XA.Foundation.RenderingVariants.Services;
  using Sitecore.XA.Foundation.SitecoreExtensions.Interfaces;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web.Mvc;

  public class PaginableController : Sitecore.XA.Foundation.RenderingVariants.Controllers.Base.PaginableController
  {
    protected override IListPagination PaginationConfiguration => ListPaginationContext.GetCurrent().Get(base.Rendering.Parameters["ListSignature"]) ?? new Sitecore.Support.XA.Foundation.RenderingVariants.Lists.Pagination.ListPagination(base.Rendering.Parameters.ToDictionary((KeyValuePair<string, string> p) => p.Key, (KeyValuePair<string, string> p) => p.Value), ListRepository.GetItems().Count());

    protected override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      if (!ListPaginationContext.GetCurrent().Initialized)
      {
        IPaginationService paginationService = ServiceLocator.ServiceProvider.GetService<IPaginationService>();
        IEnumerable<Sitecore.Mvc.Presentation.Rendering> enumerable = from r in Sitecore.Mvc.Presentation.PageContext.Current.PageDefinition.Renderings
                                                                      where paginationService.IsPaginationEnabledRendering(ID.Parse(r.RenderingItemPath), null)
                                                                      select r;
        foreach (Sitecore.Mvc.Presentation.Rendering item in enumerable)
        {
          IRendering rendering = new Sitecore.XA.Foundation.Mvc.Wrappers.Rendering(item);
          IListPagination pagination;
          if (item.Parameters.Contains("SourceType"))
          {
            pagination = new Sitecore.Support.XA.Foundation.RenderingVariants.Lists.Pagination.ListPagination(item.Parameters.ToDictionary((KeyValuePair<string, string> p) => p.Key, (KeyValuePair<string, string> p) => p.Value), ListRepository.GetItems(rendering, item.Parameters["SourceType"], base.IsEdit).Count());
          }
          else
          {
            int num = ListRepository.GetItems().Count();
            pagination = ((num > 0) ? new Sitecore.Support.XA.Foundation.RenderingVariants.Lists.Pagination.ListPagination(item.Parameters.ToDictionary((KeyValuePair<string, string> p) => p.Key, (KeyValuePair<string, string> p) => p.Value), num) : new Sitecore.Support.XA.Foundation.RenderingVariants.Lists.Pagination.ListPagination(item.Parameters.ToDictionary((KeyValuePair<string, string> p) => p.Key, (KeyValuePair<string, string> p) => p.Value), ListRepository.GetDatasourceItemsCount(rendering, base.IsEdit)));
          }
          if (!ListPaginationContext.GetCurrent().Contains(pagination))
          {
            ListPaginationContext.GetCurrent().Add(pagination);
          }
        }
        ListPaginationContext.GetCurrent().Initialized = true;
      }
    }
  }
}