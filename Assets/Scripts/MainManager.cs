using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public List<string> sceneSequence;

    public int sceneIterator = 0;

    public bool startScene = true;

    public StringBuilder csv = new StringBuilder("scene, stress_level");

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
            "HallwayScene", "HallwayScene1"
        };

        string joined1 = string.Join(",", sceneSequence.ToArray());
        print(joined1);
        sceneSequence.Shuffle();
        string joined2 = string.Join(",", sceneSequence.ToArray());
        print(joined2);
    }
}

static class MyExtensions
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}