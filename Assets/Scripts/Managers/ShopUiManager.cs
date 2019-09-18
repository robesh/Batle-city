using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUiManager : MonoBehaviour
{
    public Text _tBuyTower, _tBuyBase, _tBuyWheels, _tPriceTower, _tPriceBase, _tPriceWheels;

    private int _iBuyTower, _iBuyBase, _iBuyWheels, _iPriceTower, _iPriceBase, _iPriceWheels;

    public void SetTowerText(string name, int price)
    {
        _tBuyTower.text = name;
        _tPriceTower.text = price.ToString();
    }

    public void SetBaseText(string name, int price)
    {
        _tBuyBase.text = name;
        _tPriceBase.text = price.ToString();
    }

    public void SetWheelsText(string name, int price)
    {
        _tBuyWheels.text = name;
        _tPriceWheels.text = price.ToString();
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
}
