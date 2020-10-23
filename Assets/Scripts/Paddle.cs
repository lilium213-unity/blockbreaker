using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float paddleHeight = 0.3f;
    [SerializeField] float paddleLeftLimit = 1f;
    [SerializeField] float paddleRightLimit = 15f;
    GameStatus gameStatus;
    Ball ball;

    private void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        float paddleXPos = GetPaddlePosition();
        Vector2 paddlePos = new Vector2(paddleXPos, paddleHeight);
        transform.position = paddlePos;
        
    }

    private float GetPaddlePosition()
    {
        if(!gameStatus.IsAutoplay())
            return Mathf.Clamp(Input.mousePosition.x / Screen.width * screenWidthInUnits, paddleLeftLimit, paddleRightLimit);
        else
            return Mathf.Clamp(ball.transform.position.x, paddleLeftLimit, paddleRightLimit);
    }
}
