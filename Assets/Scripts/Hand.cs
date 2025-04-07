using System.Collections;
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
    [Header("카드가 움직이는 위치")]
    public Vector3 cardPosition;

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



        StartCoroutine(DrawReplace());
    }

    IEnumerator DrawReplace()
    {
        int totalCards = cardsInHand.Count;
        float startAngle = (totalCards - 1) * angle / 2;
        int layer = 1;

        yield return StartCoroutine(DrawHandCard(cardsInHand[totalCards - 1], startAngle, layer));

        for (int i = 0; i < totalCards - 1; i++)
        {
            float degree = startAngle - ((i + 1) * angle);
            int currentLayer = layer + i + 1;

            yield return StartCoroutine(ReplaceHandCard(cardsInHand[totalCards - 2 - i], degree, currentLayer));
        }
    }
    
    public IEnumerator DrawHandCard(GameObject card, float degree, int layer)
    {
        card.GetComponent<SortingGroup>().sortingOrder = layer;

        yield return StartCoroutine(MoveCardCoroutine(card, SolvePosition(degree, layer), 5f, cardPosition));

        card.transform.rotation = Quaternion.Euler(0, 0, degree);
    }

    public IEnumerator ReplaceHandCard(GameObject card, float degree, int layer)
    {
        card.GetComponent<SortingGroup>().sortingOrder = layer;
        card.transform.position = SolvePosition(degree, layer);
        card.transform.rotation = Quaternion.Euler(0, 0, degree);
        yield break;
    }

    public void SpawnCard(GameObject card)
    {
        var copyCard = Instantiate(card);
        copyCard.transform.parent = gameObject.transform;
        cardsInHand.Add(copyCard);
    }
    
    private IEnumerator MoveCardCoroutine(GameObject card, Vector3 targetPosition, float speed, Vector3 firstPosition)
    {
        card.transform.position = firstPosition;
        while (Vector3.Distance(card.transform.position, targetPosition) > 0.01f)
        {
            card.transform.position = Vector3.MoveTowards(card.transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        card.transform.position = targetPosition;
    }
}


