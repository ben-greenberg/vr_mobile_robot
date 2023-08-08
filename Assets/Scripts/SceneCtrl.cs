using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneCtrl : MonoBehaviour
{
    public Slider stressSlider;
    public string sceneName;

    public void ChangeScene()
    {
        //sceneName = "HallwayScene1";
        // Get the next scene name from the MainManager persistant data
        sceneName = MainManager.Instance.sceneSequence[MainManager.Instance.sceneIterator];
        print("sceneName = " + sceneName.ToString());

        SceneManager.LoadScene(sceneName);
        double stressLevel = stressSlider.value;
        this.SaveData(stressLevel);

        // Increment the sceneIterator object
        MainManager.Instance.sceneIterator++;
    }

    public void SaveData(double stressLevel)
    {
        //print("stress level: " + stressLevel);
        var sb = new StringBuilder("scene, stress_level");
        sb.Append('\n').Append(sceneName).Append(',').Append(stressLevel.ToString());
        print(sb.ToString());
    }
}