using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//아직 인덱싱 문제가 있으나 지금 당장 신경쓰지 않아도 된다.
public class Hand : MonoBehaviour
{
    public float radius;
    public float angle;
    public int maxCardCount;
    public List<GameObject> cardsInHand = new List<GameObject>();
    public GameObject cardSample;
    void Start()
    {
        var originPos = gameObject.transform.position.y - radius * 2;
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
        if (cardsInHand.Count % 2 == 0)
        {
            int pivot1 = cardsInHand.Count / 2 - 1;
            int pivot2 = cardsInHand.Count / 2;
            float degree = angle / 2;

            ReplaceHandCard(cardsInHand[pivot1], degree,pivot1);
            ReplaceHandCard(cardsInHand[pivot2], -degree, pivot2);

            for (int i = 1; i < pivot2; i++)
            {
                pivot1--;
                pivot2++;
                degree += angle;
                ReplaceHandCard(cardsInHand[pivot1], degree,pivot1);
                ReplaceHandCard(cardsInHand[pivot2], -degree,pivot2);
            }
        }
        else
        {
            int pivot = cardsInHand.Count / 2;
            int pivot1 = pivot - 1;
            int pivot2 = pivot + 1;

            ReplaceHandCard(cardsInHand[pivot], 0, pivot);

            for (int i = 1; i <= pivot; i++)
            {
                float newDegree = i * angle;
                ReplaceHandCard(cardsInHand[pivot1--], newDegree, pivot1);
                ReplaceHandCard(cardsInHand[pivot2++], -newDegree,pivot2);
            }
        }
    }

    public void ReplaceHandCard(GameObject card, float degree,int layer)
    {
        card.GetComponent<SortingGroup>().sortingOrder = layer;
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