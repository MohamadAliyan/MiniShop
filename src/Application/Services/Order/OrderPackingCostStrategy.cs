using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Application.Contract.Service;
using EShop.Domain.Enums;

namespace EShop.Application.Services;


public class OrderPackingCost:Service
{
    private readonly IOrderPackingCostStrategy _orderPackingCostStrategy;

    public OrderPackingCost(IOrderPackingCostStrategy orderPackingCostStrategy)
    {
        _orderPackingCostStrategy = orderPackingCostStrategy;
    }

    public int CalculateCost()
    {
        return _orderPackingCostStrategy.CalculateCost();
    }
}


public class NormalOrderPackingCostStrategy : IOrderPackingCostStrategy
{
    private int ShippingCost => Constant.PostCost;

    public int CalculateCost()
    {
        return ShippingCost;
    }
}
public class BreackableOrderPackingCostStrategy : IOrderPackingCostStrategy
{
    private int ShippingCost => Constant.ExpressPostCost;

    public int CalculateCost()
    {
        return ShippingCost;
    }
}
