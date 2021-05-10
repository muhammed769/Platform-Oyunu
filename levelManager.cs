using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelManager : MonoBehaviour
{
    [SerializeField] Text scoreValueText;


    private void Start()
    {
        scoreValueText = GameObject.Find("Score Value").GetComponent<Text>();
    }

    public void NextLevel()
    {
        Time.timeScale = 1; // Next butonuna bastýgýmýzda  TEKRARDAN OYUN OYNAYABÝLMEMÝZÝ SAÐLADI BU KOD.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // GetActiveScene : Þuanda aktif olan sahne 
    }

    public void Restart()
    {
        Time.timeScale = 1; // Restart  butonuna bastýgýmýzda TEKRARDAN OYUN OYNAYABÝLMEMÝZÝ SAÐLADI BU KOD.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void  ClosePanel(string parentName)
    {
        GameObject.Find(parentName).SetActive(false);
    }


    public void AddScore(int score)
    {
        int scoreValue = int.Parse(scoreValueText.text);
        scoreValue += score;
        scoreValueText.text = scoreValue.ToString();
    }

}
