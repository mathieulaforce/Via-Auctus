﻿using LaMa.Via.Auctus.Domain.CarManagement;

namespace LaMa.Via.Auctus.Application.CarManagement.CarModels;

public interface ICarModelRepository
{
    Task<CarModel?> Get(CarModelId id, CancellationToken cancellationToken = default);
    Task<CarModel?> FindByName(string name, CancellationToken cancellationToken = default);
    Task Add(CarModel carModel, CancellationToken cancellationToken = default);
}