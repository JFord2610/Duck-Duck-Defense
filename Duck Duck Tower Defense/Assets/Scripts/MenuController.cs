using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenuCanvas = null;
    public GameObject pauseMenuCanvas = null;
    [SerializeField] Slider volumeSlider = null;

    public void PlayButton_Click()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void OptionButton_Click()
    {
        mainMenuCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(true);
    }

    public void OptionBackButton_Click()
    {
        mainMenuCanvas.SetActive(true);
        pauseMenuCanvas.SetActive(false);
    }

    public void ExitButton_Clicked()
    {
        Application.Quit();
    }

    public void VolumeSlider_OnChanged()
    {
        float value = volumeSlider.value;
        Camera.main.GetComponent<AudioSource>().volume = value;
    }
}
