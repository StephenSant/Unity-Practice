using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Menus
{
    public class PauseMenu : MonoBehaviour
    {

        #region Variables
        public GameObject pauseMenu;
        public bool Paused;
        public GameObject player;
        public GameObject optionsPanel;
        public GameObject pausePanel;
        public Slider sensitivityY;
        public Slider sensitivityX;
        #endregion

        // Use this for initialization
        void Start()
        {
            player = GameObject.Find("Player");
            pauseMenu = GameObject.Find("Pause Menu");
            Paused = false;
        }

        public void MainMenuButton()
        {
            SceneManager.LoadScene("GUI Scene");
        }

        public void OptionMenuButton()
        {
            pausePanel.active = false;
            optionsPanel.active = true;
            sensitivityX.value = player.GetComponent<Player.CharacterMovement>().sensitivityX;
            sensitivityY.value = player.GetComponent<Player.CharacterMovement>().sensitivityY;
        }

        public void SenSliders()
        {
            player.GetComponent<Player.CharacterMovement>().sensitivityX = sensitivityX.value;
            player.GetComponent<Player.CharacterMovement>().sensitivityY = sensitivityY.value;
        }

        public void OptionsBack()
        {
            optionsPanel.active = false;
            pausePanel.active = true;
        }

        public void UnpauseButton()
        {
            Paused = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && Paused)
            {
                if (optionsPanel.active) { OptionsBack(); }
                else {Paused = false; }
                
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && !Paused)
            {
                Paused = true;
                pausePanel.active = true;
            }

            if (Paused)
            {
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                pausePanel.active = false;
                optionsPanel.active = false;
            }
        }
    }
}
