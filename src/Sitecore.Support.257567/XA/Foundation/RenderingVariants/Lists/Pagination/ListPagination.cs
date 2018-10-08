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
    public ListPagination(string signature) : base(signature)
    {
    }

    public override int CurrentPage
    {
      get
      {
        int @int = MainUtil.GetInt(WebUtil.GetQueryString(ListSignature ?? string.Empty), 0);
        if (@int <= 0)
        {
          return 0;
        }
        if (@int >= PagesCount)
        {
          return PagesCount - 1;
        }
        return @int;
      }
    }

    public override string FirstPageUrl => CreateUrl(0);

    public override string PreviousPageUrl => CreateUrl((CurrentPage - 1 >= 0) ? (CurrentPage - 1) : 0);

    public override string NextPageUrl => CreateUrl((CurrentPage + 1 >= PagesCount) ? (PagesCount - 1) : (CurrentPage + 1));

    public override string LastPageUrl => CreateUrl(PagesCount - 1);

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
      List<ListPaginationModel> list = Enumerable.Range(num, num2).Select(CreatePageModel).ToList();
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
        Label = p + 1 + string.Empty,
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