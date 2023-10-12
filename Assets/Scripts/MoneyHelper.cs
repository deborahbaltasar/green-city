using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyHelper
{
    private int money;
    public MoneyHelper(int startMoneyAmout)
    {
        this.money = startMoneyAmout;
    }

    public int Money { get => money; 
        private set 
        { 
            if(value < 0)
            {
                money = 0;
                throw new MoneyException("Not enough money");
            }
            else
            {
                money = value; 
            }
        } 
    }

    public void ReduceMoney(int amout)
    {
        Money -= amout;
    }

    public void AddMoney(int amout)
    {
        Money += amout;
    }

    public void CalculateMoney(IEnumerable<StructureBaseSO> buildings)
    {
        CollectIncome(buildings);
        ReduceUpkeep(buildings);
    }

    private void ReduceUpkeep(IEnumerable<StructureBaseSO> buildings)
    {
        foreach (var structure in buildings)
        {
            Money -= structure.upkeepCost;
        }
    }

    private void CollectIncome(IEnumerable<StructureBaseSO> buildings)
    {
        foreach (var structure in buildings)
        {
            Money += structure.GetIncome();
        }
    }
}
    