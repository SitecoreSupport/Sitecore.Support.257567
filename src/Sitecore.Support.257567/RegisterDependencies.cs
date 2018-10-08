using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.XA.Feature.PageContent.Repositories;
using Sitecore.XA.Foundation.RenderingVariants.Lists.Pagination;

namespace Sitecore.Support
{
  public class RegisterDependencies : IServicesConfigurator
  {
    public void Configure(IServiceCollection serviceCollection)
    {
      serviceCollection.AddSingleton<IPaginationRepository, Sitecore.Support.XA.Feature.PageContent.Repositories.PaginationRepository>();
      serviceCollection.AddTransient<Sitecore.Support.XA.Feature.PageContent.Controllers.PaginationController>();
      serviceCollection.AddTransient<Sitecore.Support.XA.Feature.PageContent.Controllers.PageListController>();
    }
  }
}