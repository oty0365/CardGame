using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Rendering;


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
            CheckCardAngle();
        }

    }
    public void CheckCardAngle()
    {
        angle-=1;
    }
    public Vector3 SolvePosition(float degree,int layer)
    {
        degree *= Mathf.Deg2Rad;
        float x = radius * Mathf.Sin(degree);
        float y = -radius * Mathf.Cos(degree) + radius+radius/3.5f;
        return new Vector3(x, y,layer);
    }

    public void GettingInHand()
    {
        if (cardsInHand.Count % 2 == 0)
        {
            int pivot1 = cardsInHand.Count / 2 - 1;
            int pivot2 = cardsInHand.Count / 2;
            float degree = angle / 2;
            var pivot = pivot2;
            ReplaceHandCard(cardsInHand[pivot1], degree,pivot1);
            ReplaceHandCard(cardsInHand[pivot2], -1*degree, pivot2);
            
            for (int i = 1; i < pivot; i++)
            {
                pivot1--;
                pivot2++;
                degree += angle;
                ReplaceHandCard(cardsInHand[pivot1], degree,pivot1);
                ReplaceHandCard(cardsInHand[pivot2], -1*degree,pivot2);
            }
        }
        else
        {
            int pivot = cardsInHand.Count / 2;
            int pivot1 = pivot - 1;
            int pivot2 = pivot + 1;

            ReplaceHandCard(cardsInHand[pivot], 0, pivot+1);

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
        card.transform.position = SolvePosition(degree,layer);
        card.transform.rotation = Quaternion.Euler(0, 0, degree);
    }
    public void SpawnCard(GameObject card)
    {
        var copyCard = Instantiate(card);
        copyCard.transform.parent = gameObject.transform;
        cardsInHand.Add(copyCard);
    }
}
