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
        var originPos = gameObject.transform.position.y - radius*2;
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, originPos);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            OnCardDraw();
        }   
    }
    public void OnCardDraw()
    {
        if (cardsInHand.Count + 1 <= maxCardCount)
        {
            SpawnCard(cardSample);
            GettingInHand();
        }

    }
    public Vector2 SolvePosition(float degree)
    {
        degree *= Mathf.Deg2Rad;
        float x = radius * Mathf.Sin(degree);
        float y = -radius * Mathf.Cos(degree) + radius; 
        return new Vector2(x, y);
    }

    public void GettingInHand()
    {
        if (cardsInHand.Count == 0) return; 

        if (cardsInHand.Count % 2 == 0)
        {
            int pivot1 = cardsInHand.Count / 2 - 1;
            int pivot2 = cardsInHand.Count / 2;
            float degree = angle / 2;

            if (pivot1 >= 0 && pivot1 < cardsInHand.Count)
                ReplaceHandCard(cardsInHand[pivot1], degree);

            if (pivot2 >= 0 && pivot2 < cardsInHand.Count)
                ReplaceHandCard(cardsInHand[pivot2], -degree);

            for (int i = 1; i < pivot2; i++)
            {
                pivot1--;
                pivot2++;
                degree += angle;

                if (pivot1 >= 0 && pivot1 < cardsInHand.Count)
                    ReplaceHandCard(cardsInHand[pivot1], degree);

                if (pivot2 >= 0 && pivot2 < cardsInHand.Count)
                    ReplaceHandCard(cardsInHand[pivot2], -degree);
            }
        }
        else
        {
            int pivot = cardsInHand.Count / 2;
            int pivot1 = pivot + 1;
            int pivot2 = pivot - 1;

            if (pivot >= 0 && pivot < cardsInHand.Count)
                ReplaceHandCard(cardsInHand[pivot], 0);

            for (int i = 1; i <= pivot; i++)
            {
                float newDegree = i * angle;

                if (pivot1 >= 0 && pivot1 < cardsInHand.Count)
                    ReplaceHandCard(cardsInHand[pivot1++], newDegree);

                if (pivot2 >= 0 && pivot2 < cardsInHand.Count)
                    ReplaceHandCard(cardsInHand[pivot2--], -newDegree);
            }
        }
    }


    public void ReplaceHandCard(GameObject card, float degree)
    {
        card.transform.position = SolvePosition(degree);
        card.transform.rotation = Quaternion.Euler(0, 0, degree); 
    }
    public void SpawnCard(GameObject card)
    {
        var copyCard = Instantiate(card);
        copyCard.transform.parent = gameObject.transform;
        cardsInHand.Add(copyCard);
    }
}
