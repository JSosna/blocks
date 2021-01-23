using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseMenu : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HandleViewDistanceSliderChange(float value)
    {
        CrossSceneData.ViewDistance = (int)value;
    }

    public void HandleFrequencySliderChange(float value)
    {
        CrossSceneData.Frequency = value;
    }

    public void HandlePowerSliderChange(float value)
    {
        CrossSceneData.Strength = value;
    }
}
