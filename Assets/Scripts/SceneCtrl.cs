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

    public void ChangeScene(string sceneChange)
    {
        sceneName = sceneChange;
        SceneManager.LoadScene(sceneChange);
        double stressLevel = stressSlider.value;
        this.SaveData(stressLevel);
    }

    public void SaveData(double stressLevel)
    {
        print("stress level: " + stressLevel);
        var sb = new StringBuilder("scene, stress_level");
        sb.Append('\n').Append(sceneName).Append(',').Append(stressLevel.ToString());
        print(sb.ToString());
    }
}
