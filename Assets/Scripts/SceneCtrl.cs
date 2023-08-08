using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneCtrl : MonoBehaviour
{
    public Slider stressSlider;
    public string sceneName;

    public void ChangeScene()
    {
        // Get the next scene name from the MainManager persistant data
        sceneName = MainManager.Instance.sceneSequence[MainManager.Instance.sceneIterator];
        print("sceneName = " + sceneName.ToString());

        SceneManager.LoadScene(sceneName);
        double stressLevel = stressSlider.value;

        //append the current scene and the reported stress value to the public csv stringbuilder
        MainManager.Instance.csv.Append('\n').Append(sceneName).Append(',').Append(stressLevel.ToString());

        // Increment the sceneIterator object until all of the scenes have been traversed
        if ((MainManager.Instance.sceneIterator) < MainManager.Instance.sceneSequence.Count)
        {
            MainManager.Instance.sceneIterator++;
        }
        //Write data to file after all scenes have been encountered
        else
        {
            string filepath = "Data/" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
            File.WriteAllText(filepath, MainManager.Instance.csv.ToString());
        }
    }
}