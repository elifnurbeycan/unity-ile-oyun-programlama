using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
public class UIManager_sc : MonoBehaviour
{

    [SerializeField]
    TMP_Text scoreText;

    [SerializeField]
    Sprite[] livesSprites;

    [SerializeField]
    Image livesImg;

    [SerializeField]
    TMP_Text gameOverText;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = "Score : 0";
        livesImg.sprite = livesSprites[3];
        gameOverText.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score " + score;
    }

    public void UptadeLives(int currentLives)
    {
        livesImg.sprite = livesSprites[currentLives];

        if (currentLives == 0)
        {
            gameOverText.gameObject.SetActive(true);
            StartCoroutine(GameOverFlickerRoutine());
        }
    }
    

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
