using ErrorOr;
using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Application.CarManagement.CarBrands.Models;

namespace LaMa.Via.Auctus.Application.CarManagement.CarBrands.Lookup;

internal class LookupCarBrandQueryHandler(ICarBrandReadRepository carBrandReadRepository)
    : IQueryHandler<LookupCarBrandQuery, IReadOnlyCollection<CarBrandSummary>>
{
    public async Task<ErrorOr<IReadOnlyCollection<CarBrandSummary>>> Handle(LookupCarBrandQuery request,
        CancellationToken cancellationToken)
    {
        var carBrands = await carBrandReadRepository.Lookup(cancellationToken);
        return ErrorOrFactory.From(carBrands);
    }
}