using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseMenu : MonoBehaviour
{

    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        CrossSceneData.Strength = 50;
        CrossSceneData.Frequency = 1.5f;
    }

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

    public void HandleSoundVolumeSliderChange(float value) {
        CrossSceneData.SoundVolume = value;
        AudioListener.volume = value;
    }

    public void HandleFrequencySliderChange(float value)
    {
        CrossSceneData.Frequency = value;
    }

    public void HandlePowerSliderChange(float value)
    {
        CrossSceneData.Strength = value;
    }

    public void Click() {
        FindObjectOfType<AudioManager>().Play("Click");
    }
}
