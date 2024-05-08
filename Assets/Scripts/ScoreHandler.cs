using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject currentScore;
    public TextMeshProUGUI currentScoreText;


    void Start()
    {
        currentScoreText = currentScore.GetComponent<TextMeshProUGUI>();
        currentScoreText.text = PlayerPrefs.GetInt("Score").ToString();
        Debug.Log(PlayerPrefs.GetInt("Score"));
    }


}
