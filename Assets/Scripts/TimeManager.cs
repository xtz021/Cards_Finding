using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private Text textTimer;
    private int min = 0, sec = 0;
    private GameManager gm;
    float time = 0f;
    bool once = false;
    private int clearedTime;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        clearedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gm.gameOver)
        {
            time += Time.deltaTime;
            CalculateTimer();
            if (sec >= 10)
                textTimer.text = "Time: " + min + ":" + sec;
            else
                textTimer.text = "Time: " + min + ":0" + sec;
        }
        else if(!once)
        {
            once = true;
            clearedTime = GetTimeSec();
        }
    }

    private void CalculateTimer()
    {
        int iTime = Mathf.FloorToInt(time);
        min = iTime / 60;
        sec = iTime % 60;
    }

    public static string ConvertTime(float time)
    {
        int iTime = Mathf.FloorToInt(time);
        int minute = iTime / 60;
        int second = iTime % 60;
        if(second >= 10)
            return "" + minute + ":" + second;
        else
            return "" + minute + ":0" + second;
    }

    public int GetClearedTime()
    {
        int t = clearedTime;
        return t;
    }

    public int GetTimeSec()
    {
        int iTime = Mathf.FloorToInt(time);
        return iTime;
    }

}
