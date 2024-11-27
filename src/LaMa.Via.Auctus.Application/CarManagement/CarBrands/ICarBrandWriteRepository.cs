using LaMa.Via.Auctus.Application.Abstractions;
using LaMa.Via.Auctus.Domain.CarManagement;

namespace LaMa.Via.Auctus.Application.CarManagement.CarBrands;

public interface ICarBrandWriteRepository : IWriteRepository
{ 
    Task<CarBrand?> FindByName(string name, CancellationToken cancellationToken = default); 
    Task<CarBrand?> Get(CarBrandId id, CancellationToken cancellationToken = default); 
    Task Add(CarBrand brand, CancellationToken cancellationToken = default); 
}