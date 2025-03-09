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
    public float radDegree;
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
            cardsInHand.Add(cardSample);
            GettingInHand();
        }

    }
    public void GettingInHand()
    {

        if (cardsInHand.Count % 2 == 0)
        {
            var pivot1 = cardsInHand.Count / 2-1;
            var pivot2 = cardsInHand.Count / 2;
            var degree = radius / 2;
            SpawnCard(cardsInHand[pivot1],degree);
            SpawnCard(cardsInHand[pivot2],-1*degree);

            for (var i = 1; i < pivot2; i++)
            {
                pivot1--;
                pivot2++;
                radDegree += radius;
                SpawnCard(cardsInHand[pivot1], radDegree);
                SpawnCard(cardsInHand[pivot2], -1 * radDegree);
            }
        }
        else
        {
            var pivot = cardsInHand.Count / 2;
            var pivot1 = pivot + 1;
            var pivot2 = pivot - 1;
            SpawnCard(cardsInHand[pivot], radDegree);
            for(var i = 1;i< pivot; i++)
            {
                radDegree += radius;
                SpawnCard(cardsInHand[pivot1++], radDegree);
                SpawnCard(cardsInHand[pivot2++], -1 * radDegree);
            }
        }
       
    }
    public Vector2 SolvePosition(float degree)
    {
        degree *= Mathf.Deg2Rad;
        var y = radius * Mathf.Cos(degree);
        var x = radius * Mathf.Sin(degree);
        return new Vector2(x, y);
    }
    public void SpawnCard(GameObject card, float degree)
    {
        var copyCard = Instantiate(card,gameObject.transform.position,Quaternion.Euler(0, 0, -1*degree));
        copyCard.transform.parent = gameObject.transform;
        var distance = SolvePosition(degree);
        copyCard.gameObject.transform.position = distance;
    }
}
