using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsManager : MonoBehaviour
{
    public static int faceUpNumb = 0;
    
    /// <summary>
    /// List of all the cards can be showned on the gameboard
    /// </summary>
    [SerializeField] private List<Sprite> cardsFaceList;

    /// <summary>
    /// List of all the cards can be showned on the gameboard
    /// </summary>
    [SerializeField] private List<CardHandler> cardsSlotList;
    [SerializeField] private Sprite backCard;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] Text inGameTimeText;
    [SerializeField] Text gameOverTimeText;

    public List<CardHandler> faceUpCardList = new List<CardHandler>();

    private int cardsRemain;
    private List<int> value = new List<int>();
    private List<int> generated = new List<int>();
    private GameManager gm;
    private bool once = false;

    public int posJustClicked;

    // Start is called before the first frame update
    void Start()
    {
        SetUpCards();
        cardsRemain = cardsSlotList.Count;
        gm = FindObjectOfType<GameManager>();
    }
    

    // Update is called once per frame
    void Update()
    {
        if(!gm.gameOver)
        {
            CheckFlippedCards();
            if(cardsRemain <= 0)        // The game will be over if there's no card remains
            {
                gm.gameOver = true;
            }
            return;
        }
        if(gm.gameOver && !once)
        {
            once = true;
            gameOverScreen.SetActive(true);
            gameOverTimeText.text = inGameTimeText.text;
        }
    }
    
    
    private void CheckFlippedCards()
    {
        if (faceUpCardList.Count >= 2)
        {
            int a = GetValueCard(faceUpCardList[0]);
            int b = GetValueCard(faceUpCardList[1]);
            if (a != b)
            {
                faceUpCardList[0].FlipCard(backCard);
                faceUpCardList[1].FlipCard(backCard);
            }
            else
            {
                ScoreManager.score++;
                faceUpCardList[1].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                faceUpCardList[0].GetComponent<Image>().color = new Color(1, 1, 1, 0);
                cardsRemain -= 2;
            }
            faceUpCardList.Clear();
        }
    }


    private void SetUpCards()
    {
        for (int i = 0; i < cardsSlotList.Count; i+=2)
        {
            int rd;
            do
            {
                rd = Random.Range(0, cardsFaceList.Count);
            } while (generated.Contains(rd));
            value.Add(rd);
            value.Add(rd);
            generated.Add(rd);
        }
        ShuffleCards(value);
    }

    public int FindCardPos(CardHandler ch)
    {
        return cardsSlotList.IndexOf(ch);
    }
    
    public void ShuffleCards(List<int> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            int value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public Sprite FaceCardOfPos(int pos)
    {
        return cardsFaceList[value[pos]];
    }

    public int GetValueCard(CardHandler ch)
    {
        int index = FindCardPos(ch);
        return value[index];
    }
}
