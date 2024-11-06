﻿using LaMa.Via.Auctus.Application.CarManagement.CarBrands;
using LaMa.Via.Auctus.Domain.CarManagement;
using LaMa.Via.Auctus.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace LaMa.Via.Auctus.Infrastructure.CarManagement;

public class CarBrandRepository : Repository<CarBrand>, ICarBrandRepository
{
    public CarBrandRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<CarBrand?> Get(CarBrandId id, CancellationToken cancellationToken = default)
    {
        return await Entity.FirstOrDefaultAsync(brand => brand.Id == id, cancellationToken);
    }

    public async Task<CarBrand?> FindByName(string name, CancellationToken cancellationToken = default)
    {
        return await Entity.FirstOrDefaultAsync(brand => brand.Name == name, cancellationToken);
    }
}