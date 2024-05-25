using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameOver = false;
    [SerializeField] private GameObject itemListPrefab;
    [SerializeField] private GameObject listGameBoardField;
    [SerializeField] private GameObject saveRecordGO;

    TimeManager tm;
    List<PlayerScore> listScore;
    BinaryWriter bw;
    private bool once = false;
    Vector3 defaultScale = new Vector3(1,1,1);
    float r1;
    float g1;
    float b1;
    bool corShowSaveRecordPanel_IsRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        tm = FindObjectOfType<TimeManager>();
        listScore = new List<PlayerScore>();
        GenerateDefaultColors();
        ReadListScore();
        
        Input.backButtonLeavesApp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver && !once)
        {
            once = true;
            WriteListScore();
            ReadListScore();
            if(listScore.Count < 10)
            {
                Debug.Log("" + 1);
                StartCoroutine(ShowSaveRecordPanel());
            }
            else if(tm.GetClearedTime() < listScore[9].time)
            {
                Debug.Log("" + 2);
                StartCoroutine(ShowSaveRecordPanel());
            }
            if(!corShowSaveRecordPanel_IsRunning)
            {
                Debug.Log("" + 3);
                SortScoreList();
                GenerateScoreBoard();
            }
        }
    }

    IEnumerator ShowSaveRecordPanel()
    {
        corShowSaveRecordPanel_IsRunning = true;
        yield return new WaitForSeconds(1.5f);
        saveRecordGO.SetActive(true);
        corShowSaveRecordPanel_IsRunning = false;
    }

    private void GenerateDefaultColors()
    {
        r1 = itemListPrefab.GetComponent<Image>().color.r;
        g1 = itemListPrefab.GetComponent<Image>().color.g;
        b1 = itemListPrefab.GetComponent<Image>().color.b;
    }

    private void OnApplicationQuit()
    {
        SortScoreList();
        WriteListScore();
    }

    public void AddScore(PlayerScore ps)
    {
        listScore.Add(ps);
    }

    public void GenerateScoreBoard()
    {
        Debug.Log("" + 1);
        for (int i = 0; i < listScore.Count; i++)
        {
            GameObject scoreGO = (GameObject)Instantiate(itemListPrefab);
            scoreGO.transform.SetParent(listGameBoardField.transform);
            scoreGO.transform.Find("Rank").GetComponent<Text>().text = (i + 1).ToString();
            scoreGO.transform.Find("PlayerName").GetComponent<Text>().text = listScore[i].playerName;
            scoreGO.transform.Find("TimeScore").GetComponent<Text>().text = TimeManager.ConvertTime(listScore[i].time);
            scoreGO.transform.localScale = defaultScale;

            // set rows color
            if ((i + 1) % 2 == 1)
            {
                scoreGO.GetComponent<Image>().color = new Color(r1, g1, b1, 0f);
            }
            else
            {
                scoreGO.GetComponent<Image>().color = new Color(r1, g1, b1, 1f);
            }
        }
    }

    public void SortScoreList()
    {
        for(int i = 0; i < listScore.Count - 1; i++)
            for(int j = i+1; j < listScore.Count; j++)
            {
                if(listScore[i].time > listScore[j].time)
                {
                    PlayerScore ps = listScore[i];
                    listScore[i] = listScore[j];
                    listScore[j] = ps;
                }
            }
        while (listScore.Count > 10)
        {
            listScore.RemoveAt(10);
        }
    }

    private void WriteListScore()
    {
        using (FileStream fileStream = File.Open("score.bin", FileMode.OpenOrCreate))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            try
            {
                binaryFormatter.Serialize(fileStream, listScore);
                Debug.Log("Data saved!!!");
            }
            catch(System.Exception e)
            {
                Debug.Log("" + e.ToString());
            }
        }
    }

    private void WriteListScore1()
    {
        if (!File.Exists("score.bin"))
        {
            File.Create("score.bin");
        }
        using(bw = new BinaryWriter(new FileStream("score.bin", FileMode.Open)))
        {
            foreach (PlayerScore ps in listScore)
            {
                bw.Write(ps.playerName);
                bw.Write(ps.time);
            }
        }
    }

    private void ReadListScore()
    {
        if(File.Exists("score.bin"))
        {
            using (FileStream fileStream = File.Open("score.bin", FileMode.Open))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                try
                {
                    listScore = (List<PlayerScore>)binaryFormatter.Deserialize(fileStream);
                    Debug.Log("Read data!!!");
                }
                catch (System.Exception e)
                {
                    Debug.Log("" + e.ToString());
                }
            }
        }
    }


    private void ReadListScore1()
    {
        if (File.Exists("score.bin"))
        {
            listScore = new List<PlayerScore>();
            using (BinaryReader br = new BinaryReader(new FileStream("score.bin", FileMode.Open)))
            {
                while(br.PeekChar() != -1)
                {
                    PlayerScore ps = new PlayerScore();
                    ps.playerName = br.ReadString();
                    ps.time = br.ReadInt32();
                    listScore.Add(ps);
                }
            } 
        }
        else
        {
            Debug.Log("Couldn't find score.bin file");
            File.Create("score.bin");
            Debug.Log("Created score.bin");
        }
    }

}
