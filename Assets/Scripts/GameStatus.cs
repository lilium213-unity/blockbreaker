using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int score = 0;
    [SerializeField] int pointsPerSimpleBlock = 100;
    [SerializeField] int pointsPerSpecialBlock = 150;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool autoplay = false;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameStatus>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
            
        else
            DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddSimplePoints()
    {
        score += pointsPerSimpleBlock;
        scoreText.text = score.ToString();
    }

    public void AddSpecialPoints()
    {
        score += pointsPerSpecialBlock;
        scoreText.text = score.ToString();
    }

    public void destroy()
    {
        Destroy(gameObject);
    }

    public bool IsAutoplay()
    {
        return autoplay;
    }
}
