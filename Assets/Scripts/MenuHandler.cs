using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;//interacts withe scene changing
using UnityEngine.UI;//interacting with GUI elements
using UnityEngine.EventSystems;// BUTTONS!


public class MenuHandler : MonoBehaviour
{
    #region Variables
    public GameObject mainMenu, optionMenu;
    public bool showOptions;
    public Slider volSlider, brightSlider;
    public AudioSource mainAudio;
    public Light dirLight;
    #endregion

    public void LoadGame()//starts the game
    {
        SceneManager.LoadScene(1);
    }
    public void ToggleOptions()
    {
        OptionToggle();

    }
    bool OptionToggle()
    {
        if (showOptions)//showoptions == true;
        {
            showOptions = false;
            mainMenu.SetActive(true);
            optionMenu.SetActive(false);
            return true;
        }
        else 
        {
            showOptions = true;
            mainMenu.SetActive(false);
            optionMenu.SetActive(true);
            volSlider = GameObject.Find("Sound Slider").GetComponent<Slider>();
            mainAudio = GameObject.Find("hello world").GetComponent<AudioSource>();
            volSlider.value = mainAudio.volume;
            return false;
        }
    }
    public void Volume()
    {
        mainAudio.volume = volSlider.value;
    }
    public void Brightness()
    {
        
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
        Debug.Log("QUIT!");
    }
}
