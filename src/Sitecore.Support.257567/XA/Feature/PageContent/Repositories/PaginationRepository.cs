namespace Sitecore.Support.XA.Feature.PageContent.Repositories
{
  using Sitecore.XA.Feature.PageContent.Models;
  using Sitecore.XA.Foundation.RenderingVariants.Lists.Pagination;
  using Sitecore.XA.Foundation.SitecoreExtensions.Extensions;
  using System.Collections.Generic;
  using System.Linq;

  public class PaginationRepository : Sitecore.XA.Feature.PageContent.Repositories.PaginationRepository
  {
    public override List<PaginationLinkModel> CreatePagesList()
    {
      IListPagination listPagination = ListPaginationContext.GetCurrent().Get(Rendering.Parameters["ListSignature"]);
      int collapseModeTreshold = Rendering.Parameters.ParseInt("CollapseModeThreshold", 0);
      List<ListPaginationModel> source = listPagination.GetPages(collapseModeTreshold).ToList();
      List<PaginationLinkModel> list = new List<PaginationLinkModel>();
      if (!source.Any())
      {
        return list;
      }
      list.Add(CreateLinkModel("FirstLabel", listPagination.FirstPageUrl, listPagination.CurrentPage > 0));
      list.Add(CreateLinkModel("PreviousLabel", listPagination.PreviousPageUrl, listPagination.CurrentPage > 0));
      list.AddRange(from m in source
                    select new PaginationLinkModel(m));
      list.Add(CreateLinkModel("NextLabel", listPagination.NextPageUrl, listPagination.CurrentPage < listPagination.PagesCount - 1));
      list.Add(CreateLinkModel("LastLabel", listPagination.LastPageUrl, listPagination.CurrentPage < listPagination.PagesCount - 1));
      return list;
    }
  }
}