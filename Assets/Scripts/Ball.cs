using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Paddle paddle1;
    [SerializeField] Vector2 distanceToPaddle = new Vector2( 0f, 1f );
    [SerializeField] float ballSpeed = 15f;
    [SerializeField] float minAngle = 16f;
    [Range(164f, 16f)] [SerializeField] float launchAngle = 16f;
    [SerializeField] AudioClip[] bounceSounds;
    
    bool stuckToPaddle = true;
    float minVelocity;

    AudioSource audioSource;
    new Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        StickToPaddle();
        audioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody2D>();
        CalculateMinVectorVelocity();
    }

    private void CalculateMinVectorVelocity()
    {
        minVelocity = Mathf.Sin(Mathf.Deg2Rad * minAngle) * ballSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(stuckToPaddle)
        {
            LockBallToPaddle();
            LaunchOnClick();
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + distanceToPaddle;
    }

    public void StickToPaddle()
    {
        stuckToPaddle = true;
    }

    private void LaunchOnClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            stuckToPaddle = false;
            SetLaunchVelocity();
            //More stuff
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityBeforeTweak = rigidbody.velocity;
        if (!stuckToPaddle)
        {
            AudioClip clip = bounceSounds[Random.Range(0, bounceSounds.Length)];
            audioSource.PlayOneShot(clip);
            TweakVelocity();
        }
    }

    private void SetLaunchVelocity()
    {
        float xVelocity = Mathf.Cos(Mathf.Deg2Rad * launchAngle) * ballSpeed;
        float yVelocity = Mathf.Sin(Mathf.Deg2Rad * launchAngle) * ballSpeed;

        rigidbody.velocity = new Vector2(xVelocity, yVelocity);
    }

    private void TweakVelocity()
    {
        float xSpeed = rigidbody.velocity.x;
        float ySpeed = rigidbody.velocity.y;

        if (Mathf.Abs(xSpeed) < minVelocity) {
            TweakXVelocity(xSpeed, ySpeed);
        } else if (Mathf.Abs(ySpeed) < minVelocity) {
            TweakYVelocity(xSpeed, ySpeed);
        }
    }

    private void TweakYVelocity(float xSpeed, float ySpeed)
    {
        float xCorrectedSpeed = GetCorrectSpeedForMinimumAngle();
        rigidbody.velocity =
            new Vector2(
                xSpeed > 0 ? xCorrectedSpeed : -xCorrectedSpeed,
                ySpeed > 0 ? minVelocity : -minVelocity
            );
    }

    private void TweakXVelocity(float xSpeed, float ySpeed)
    {
        float yCorrectedSpeed = GetCorrectSpeedForMinimumAngle();
        rigidbody.velocity =
            new Vector2(
                xSpeed > 0 ? minVelocity : -minVelocity,
                ySpeed > 0 ? yCorrectedSpeed : -yCorrectedSpeed
            );
    }

    private float GetCorrectSpeedForMinimumAngle()
    {
        return Mathf.Sqrt(Mathf.Pow(ballSpeed, 2) - Mathf.Pow(minVelocity, 2));
    }
}
