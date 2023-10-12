using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int startMoneyAmout = 5000;
    public float moneyCalculationInterval = 2;
    MoneyHelper moneyHelper;
    public BuildingManager buildingManager;
    public UIController uiController;
    // Start is called before the first frame update
    void Start()
    {
        moneyHelper = new MoneyHelper(startMoneyAmout);
        UpdateMoneyValueUI();
    }

    public bool SpendMoney(int amount)
    {
        if(CanIBuyIt(amount))
        {
            try
            {
                moneyHelper.ReduceMoney(amount);
                UpdateMoneyValueUI();
                return true;
            }
            catch (MoneyException)
            {
                ReloadGame();
            }
        }
        return false;
    }

    private void ReloadGame()
    {
        Debug.Log("End the game");
    }

    private bool CanIBuyIt(int amount)
    {
        if(moneyHelper.Money >= amount)
        {
            return true;
        }
        return false;
    }

    public void CalculateTownIncome()
    {
        try
        {
            moneyHelper.CalculateMoney(buildingManager.GetAllStructures());
            UpdateMoneyValueUI();
        }
        catch (MoneyException)
        {
            ReloadGame();
        }
    }

    public void AddMoney(int amount)
    {
        moneyHelper.AddMoney(amount);
        UpdateMoneyValueUI();
    }

    private void UpdateMoneyValueUI()
    {
        uiController.SetMoneyValue(moneyHelper.Money);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}