﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneCtrl : MonoBehaviour
{
    public Slider arousalSlider;
    public Slider pleasureSlider;
    public string sceneName;

    public void ChangeScene()
    {
        // Increment the sceneIterator object until all of the scenes have been traversed
        if ((MainManager.Instance.sceneIterator + 1) < MainManager.Instance.sceneSequence.Count)
        {
            if (MainManager.Instance.startScene)
            {
                sceneName = MainManager.Instance.sceneSequence[MainManager.Instance.sceneIterator];
                SceneManager.LoadScene(sceneName);
                MainManager.Instance.startScene = false;
                return;
            }

            AppendCurrentData();

            // load next scene
            MainManager.Instance.sceneIterator++;
            sceneName = MainManager.Instance.sceneSequence[MainManager.Instance.sceneIterator];
            SceneManager.LoadScene(sceneName);

            print("scene_iterator: " + MainManager.Instance.sceneIterator);
        }
        //Write data to file after all scenes have been encountered, and go to FinishScene
        else
        {
            AppendCurrentData();
            string filepath = "Data/" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
            File.WriteAllText(filepath, MainManager.Instance.csv.ToString());
            sceneName = "FinishScene";
            SceneManager.LoadScene(sceneName);
        }
    }
    private void AppendCurrentData()
    {
        // Get the next scene name from the MainManager persistant data
        sceneName = MainManager.Instance.sceneSequence[MainManager.Instance.sceneIterator];

        double arousalLevel = arousalSlider.value;
        double pleasureLevel = pleasureSlider.value;

        //append the current scene and the reported arousal and pleasure values to the public csv stringbuilder
        MainManager.Instance.csv.Append('\n').Append(sceneName).Append(',').Append(arousalLevel.ToString()).Append(',').Append(pleasureLevel.ToString());
    }
}