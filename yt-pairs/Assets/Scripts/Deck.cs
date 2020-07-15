using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{


    public List<Sprite> collection;
    private List<GameObject> cards;

    [SerializeField]
    private GameObject cardPrefab;
    [SerializeField]
    private Transform cardParent;

    private void Awake()
    {
        
        cards = new List<GameObject>();
    }

    private void Start()
    {
        CreateDeck();
       // ShuffleCards();
        DisplayCards();
    }

    private void CreateDeck()
    {
        for (int i = 0; i < collection.Count; i++)
        {
            for(int j = 0; j < 2; j++)
            {
                GameObject c = Instantiate(cardPrefab,cardParent);
                c.GetComponent<Card>().cardId = i;
                c.GetComponent<Card>().foreImg = collection[i];
                cards.Add(c);
            }

        }
    }

    private void ShuffleCards()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            //swap i-th card with random card
            GameObject tmp = cards[i];
            int rIndex = Random.Range(0, cards.Count);
            cards[i] = cards[rIndex];
            cards[rIndex] = tmp;
        }
    }

    private void DisplayCards()
    {
        float space = 0.5f;

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                cards[i * 8 + j].transform.position = new Vector2(i + i*space, j+ j*space);
                
            }
        }
    }

}
