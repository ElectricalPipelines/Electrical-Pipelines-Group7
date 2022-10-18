using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System.Threading;

public class SceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    Snap[] snaps;
    public static int restartCount = 1;
    public bool nextStage { get; set; } = true;
    Timer timer;
    public Text restartText;
    GameObject nextStageObj;
    void Start()
    {
        nextStageObj = GameObject.Find("Next");
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        LoadSnap();
        restartText.text = "Press R to restart: " + restartCount;
        nextStageObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetKeyDown("r"))
        {
            Restart();
        }
            if (snaps != null && snaps.Length > 0)
        {
            nextStage = true;
            foreach (Snap snap in snaps)
            {
                if (!snap.IsSnapped)
                {
                    nextStage = false;
                }
            }
        }
        if (nextStage)
        {
            GameObject[] go = GameObject.FindGameObjectsWithTag("End");
            if (go != null && go.Length > 0)
            {
                foreach (GameObject g in go)
                {
                    SpriteRenderer spriteR = g.GetComponent<SpriteRenderer>();
                    spriteR.sprite = SpriteManager.Instance.GetListSprite("start")[0];
                }
            }
            nextStageObj.SetActive(true);
        }
        if (!Timer.timerIsRunning) Restart();
    }
    
    void LoadSnap()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Snap");
        if (go != null && go.Length > 0)
        {
            snaps = new Snap[go.Length];
            int index = 0;
            for (int i = go.Length - 1; i >= 0; i--)
            {
                Snap snap = go[i].GetComponent<Snap>();
                if (snap == null)
                {
                    Debug.LogError("Snap is null");
                    continue;
                }
                snaps[index] = snap;
                index++;
            }
        }
    }

    public void Restart()
    {
        if (restartCount > 0)
        {
            restartCount--;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            restartText.text = "Press R to restart: " + restartCount;
        }
        else
        {
            SceneManager.LoadScene("GameOver");
            Destroy(this);
        }
            
    }

    public void LoadNextStage()
    {
        if (nextStage)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

