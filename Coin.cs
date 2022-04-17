using System.Collections.Generic;
using UnityEngine;

public class Coin
{
    private int position;
    private GameObject coinObject;
    private static List<Coin> coins = new List<Coin>();

    public int GetPosition()
    {
        return position;
    }

    public void SetPosition(int position)
    {
        this.position = position;
    }

    public GameObject GetCoinObject()
    {
        return coinObject;
    }

    public void SetGameObject(GameObject coinObject)
    {
        this.coinObject = coinObject;
    }

    public static void AddCoins(Coin coin)
    {
        coins.Add(coin);
    }

    public static Coin GetCoins(int index)
    {
        return coins[index];
    }

    public static int GetCoinsSize()
    {
        return coins.Count;
    }

    public static bool IsCoinOnPosition(int position)
    {
        for(int i = 0; i < coins.Count; i++)
        {
            if (Coin.GetCoins(i).GetPosition() == position) return true;
        }

        return false;
    }
}
