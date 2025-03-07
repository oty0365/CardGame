using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public float radius;
    public float angle;
    public int maxCardCount;

    public List<GameObject> cardsInHand = new List<GameObject>();
    public GameObject cardSample;
    void Start()
    {
        var originPos = gameObject.transform.position.y - radius;
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, originPos);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }   
    }
    public void OnCardDraw()
    {
        if (cardsInHand.Count + 1 <= maxCardCount)
        {
            cardsInHand.Add(cardSample);
        }

    }
    public void GettingInHand()
    {

            if (cardsInHand.Count % 2 == 0)
            {
                var pivot1 = cardsInHand.Count / 2-1;
                var pivot2 = cardsInHand.Count / 2;
                //pivot1+
                for(var i = 1; i <= pivot2; i++)
                {
                    pivot1--;
                    pivot2++;

                }
            }
       
    }
}
