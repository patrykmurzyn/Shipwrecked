using UnityEngine;
using UnityEngine.UI;
using TMPro;

[DefaultExecutionOrder(2)]

public class CoinManage : MonoBehaviour
{
    private float rotSpeed = 50f;
    private static GameObject score;

    private void SetCoinPosition()
    {

        foreach (Transform i in transform)
        {
            Coin temp = new Coin();

            temp.SetGameObject(i.gameObject);

            Coin.AddCoins(temp);
        }

        for (int i = 0; i < Coin.GetCoinsSize(); i++)
        {
            Coin.GetCoins(i).SetPosition(Turtle.GetTurtles(i).GetPosition());

            Debug.Log("Coin: " + Coin.GetCoins(i).GetPosition());

            Coin.GetCoins(i).GetCoinObject().transform.position = Turtle.GetTurtles(i).GetTurtleObject().transform.position
                + new Vector3(0f, 0.9f, 0f);
        }
    }

    public static void AddPointIfPlayerIsOnCoin()
    {
        
        for(int i = 0; i < Coin.GetCoinsSize(); i++)
        {
            if(Coin.GetCoins(i).GetPosition() == PlayerManage.GetPlayerPosition())
            {
                Debug.Log("CoinPos: " + Coin.GetCoins(i).GetPosition() + ", GetPlayerPosition(): " + Box.GetBoxes(PlayerManage.GetPlayerPosition()).GetPosition());
                PlayerManage.AddPlayerPoint();

                int turtleIndex;

                do
                {
                    turtleIndex = Random.Range(0, 40);

                } while (Coin.IsCoinOnPosition(Turtle.GetTurtles(turtleIndex).GetPosition())
                || Turtle.GetTurtles(turtleIndex).GetState() != 0);

                int newPosition = Turtle.GetTurtles(turtleIndex).GetPosition();

                Coin.GetCoins(i).SetPosition(newPosition);

                Coin.GetCoins(i).GetCoinObject().transform.position = Turtle.GetTurtles(turtleIndex).GetTurtleObject().transform.position
                    + new Vector3(0f, 0.9f, 0f);

                score.GetComponent<TextMeshProUGUI>().text = "Score: " + PlayerManage.GetPlayerPoints();
            }
        }
    }

    private void RotateCoin()
    {
        for (int i = 0; i < Coin.GetCoinsSize(); i++)
        {
            Coin.GetCoins(i).GetCoinObject().transform.Rotate(0, rotSpeed * Time.deltaTime, 0);
        }
    }
    
    void Start()
    {
        score = GameObject.Find("Score");

        SetCoinPosition();
    }

    void Update()
    {
        RotateCoin();
    }
}
