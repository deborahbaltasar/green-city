public interface IResourceManager
{
    float MoneyCalculationInterval { get; }
    int StartMoneyAmount { get; }
    int DemolitionPrice { get; }

    void AddMoney(int amount);
    void CalculateTownIncome();
    bool CanIBuyIt(int amount);
    int HowManyStructuresCanIPlace(int placementCost, int count);
    bool SpendMoney(int amount);
}
