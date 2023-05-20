using Application.Contract.Services;
using EShop.Domain.Enums;

namespace EShop.Application.Contract.Service;

public interface IOrderPackingCostStrategy:IService
{

   int CalculateCost( );

}