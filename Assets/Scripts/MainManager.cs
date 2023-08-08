using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public List<string> sceneSequence;

    public int sceneIterator = 0;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        sceneSequence = new List<string>
        {
            "HallwayScene", "HallwayScene1", "HallwayScene"
        };
    }
}
