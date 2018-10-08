namespace Sitecore.Support.XA.Foundation.RenderingVariants.Lists.Pagination
{
  using Sitecore;
  using Sitecore.Web;
  using Sitecore.XA.Foundation.RenderingVariants.Lists.Pagination;
  using Sitecore.XA.Foundation.SitecoreExtensions.Extensions;
  using System;
  using System.Collections.Generic;
  using System.Collections.Specialized;
  using System.Linq;
  using System.Web;

  public class ListPagination : Sitecore.XA.Foundation.RenderingVariants.Lists.Pagination.ListPagination
  {
    public override int Skip => Offset + PageSize * (CurrentPage - 1);
    public ListPagination(string signature) : base(signature)
    {
    }
    public ListPagination(IDictionary<string, string> renderingParameters, int itemsCount) : base(renderingParameters, itemsCount)
    {
    }

    public ListPagination(NameValueCollection renderingParameters, int itemsCount) : base(renderingParameters, itemsCount)
    {
    }
    public override int CurrentPage
    {
      get
      {
        int @int = MainUtil.GetInt(WebUtil.GetQueryString(ListSignature ?? string.Empty), 0);
        if (@int <= 0)
        {
          return 1;
        }
        if (@int >= PagesCount)
        {
          return PagesCount;
        }
        return @int;
      }
    }

    public override string FirstPageUrl => CreateUrl(1);

    public override string PreviousPageUrl => CreateUrl(CurrentPage <= 1 ? 1 : CurrentPage - 1);

    public override string NextPageUrl => CreateUrl(CurrentPage >= PagesCount ? PagesCount : CurrentPage + 1);

    public override string LastPageUrl => CreateUrl(PagesCount);

    public new IEnumerable<ListPaginationModel> GetPages(int collapseModeTreshold)
    {
      if (PagesCount <= 1)
      {
        return Enumerable.Empty<ListPaginationModel>();
      }
      if (collapseModeTreshold == 0)
      {
        return new List<ListPaginationModel>
            {
                CreateEmptyModel()
            };
      }
      int num = 0;
      int num2 = PagesCount;
      if (collapseModeTreshold >= 1 && collapseModeTreshold < PagesCount)
      {
        num = ComputeStartPage(collapseModeTreshold, num);
        num2 = collapseModeTreshold;
      }
      List<ListPaginationModel> list = Enumerable.Range(num+1, num2).Select(CreatePageModel).ToList();
      if (num > 0)
      {
        list.Insert(0, CreateDotsModel());
      }
      if (num + num2 < PagesCount)
      {
        list.Add(CreateDotsModel());
      }
      return list;
    }

    protected override ListPaginationModel CreatePageModel(int p)
    {
      return new ListPaginationModel
      {
        Label = p + string.Empty,
        Url = CreateUrl(p),
        CssClass = ((p == CurrentPage) ? "active" : string.Empty),
        IsLink = (p != CurrentPage),
        IsPage = true
      };
    }

    private ListPaginationModel CreateDotsModel()
    {
      return new ListPaginationModel
      {
        Label = "...",
        CssClass = "more",
        IsLink = false,
        IsPage = true
      };
    }

    private ListPaginationModel CreateEmptyModel()
    {
      return new ListPaginationModel
      {
        Label = string.Empty,
        CssClass = "more",
        IsLink = false,
        IsPage = true
      };
    }

  }
}