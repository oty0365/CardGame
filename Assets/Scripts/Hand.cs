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
        angle -= 1;
    }

    public Vector3 SolvePosition(float degree, int layer)
    {
        degree *= Mathf.Deg2Rad;
        float x = radius * Mathf.Sin(degree);
        float y = -radius * Mathf.Cos(degree) + radius + radius / 3.5f;
        return new Vector3(x, y, layer);
    }

    public void GettingInHand()
    {
        int totalCards = cardsInHand.Count;
        float startAngle = (totalCards - 1) * angle / 2;

        for (int i = 0; i < totalCards; i++)
        {
            float degree = startAngle - (i * angle);
            int layer = i + 1; 

            ReplaceHandCard(cardsInHand[totalCards - 1 - i], degree, layer);  
        }
    }

    public void ReplaceHandCard(GameObject card, float degree, int layer)
    {
        card.GetComponent<SortingGroup>().sortingOrder = layer;
        card.transform.position = SolvePosition(degree, layer);
        card.transform.rotation = Quaternion.Euler(0, 0, degree);
    }

    public void SpawnCard(GameObject card)
    {
        var copyCard = Instantiate(card);
        copyCard.transform.parent = gameObject.transform;
        cardsInHand.Add(copyCard);
    }
}


