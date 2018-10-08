namespace Sitecore.Support.XA.Feature.PageContent.Controllers
{
  using Sitecore.XA.Feature.PageContent.Repositories;

  public class PaginationController : Sitecore.Supoprt.XA.Foundation.RenderingVariants.Controllers.Base.PaginableController
  {
    protected IPaginationRepository PaginationRepository;

    public PaginationController(IPaginationRepository paginationRepository)
    {
      PaginationRepository = paginationRepository;
    }

    protected override object GetModel()
    {
      return PaginationRepository.GetModel();
    }
  }
}