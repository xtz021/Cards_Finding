using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    public static int score = 0;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (score < 0)
        {
            score = 0;
        }
        scoreText.text = "Match: " + score;
    }
}
