using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     [SerializeField] TMP_Text timetext;
     [SerializeField] PlayerController playerController;
     [SerializeField] GameObject GameoverText;
     [SerializeField] float StartTime = 5f;

     float timeleft;
     bool gameover = false;

    public bool Gameover{ get {return gameover;}}
    void Start()
    {
        timeleft = StartTime;
    }

    void Update()
    {
        bool flowControl = DecreaseTime();
        if (!flowControl)
        {
            return;
        }
    }

    public bool returngameover()
    {
        return gameover;
    }

    private bool DecreaseTime()
    {
        if (gameover) return false;
        timeleft -= Time.deltaTime;
        timetext.text = timeleft.ToString("F1");

        if (timeleft <= 0f)
        {
            PlayerGameover();
        }

        return true;
    }

    void PlayerGameover()
    {
        gameover = true;
        playerController.enabled = false;
        GameoverText.SetActive(true);
        Time.timeScale = .1f;
    }
}
