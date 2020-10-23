using System;
using UnityEngine;

public abstract class Block : MonoBehaviour
{
    Level level;
    protected GameStatus gameStatus;
    [SerializeField] AudioClip destroyed;
    [SerializeField] GameObject blockSparkle;
    [SerializeField] Sprite[] hitSprites;

    int timesHit = 0;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tag == "Breakable")
        {
            timesHit++;
            if (timesHit > hitSprites.Length)
                DestroyBlock();
            else
            {
                ShowNextHitSprite();
            }
        }
    }

    private void ShowNextHitSprite()
    {
        if (hitSprites[timesHit - 1])
            GetComponent<SpriteRenderer>().sprite = hitSprites[timesHit - 1];
        else
            Debug.LogError($"Sprite for block with name {gameObject.name} missing");
    }

    private void DestroyBlock()
    {
        level.removeBlock();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(destroyed, Camera.main.transform.position);
        AddPoints();
        TriggerSparklesVFX();
    }

    void Start()
    {
        level = FindObjectOfType<Level>();
        gameStatus = FindObjectOfType<GameStatus>();

        if (tag == "Breakable")
            level.addBlock();
    }

    public abstract void AddPoints();

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparkle, transform.position, transform.rotation);
        Destroy(sparkles, 2);
    }
}
