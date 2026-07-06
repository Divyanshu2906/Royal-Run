using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoretext;
    [SerializeField] GameManager gameManager;

    int score = 0;

    public void IncreaseScore(int amount)
    {
        if(gameManager.Gameover) return;
        score += amount;
        scoretext.text = score.ToString();
    }
}
