using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;
    int totalBlocks = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (totalBlocks == 0)
            sceneLoader.LoadNextScene();
    }

    public void addBlock()
    {
        totalBlocks += 1;
    }

    public void removeBlock()
    {
        totalBlocks -= 1;
    }
}
