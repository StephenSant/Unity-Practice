using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace Player
{
    //this script can be found in the Component section under the option Character Set Up 
    //Character Handler
    [AddComponentMenu("FirstPerson/PlayerHandler")]
    public class CharacterHandler : MonoBehaviour
    {
        #region Variables
        #region Character
        [Header("Character")]
        //bool to tell if the player is alive
        public IsLiving alive = IsLiving.Alive;
        //connection to players character controller
        public CharacterController controller;
        public CharacterMovement movement;
        #endregion
        #region Health
        [Header("Health")]
        //max health 
        public float maxHealth;
        //current health
        public float curHealth;
        public GUIStyle healthColor;
        public GUIStyle healthColorBackground;
        #endregion
        #region Level and Exp
        [Header("Levels and Exp")]
        //players current level
        public int level;
        //max and current experience
        public int maxExp, curExp;
        public GUIStyle expColor, expColorBackground;
        #endregion
        #region MiniMap
        [Header("Camera Connection")]
        //render texture for the mini map that we need to connect to a camera
        public RenderTexture miniMap;
        #endregion
        #endregion
        #region Start
        private void Start()
        {
            //set max health to 100
            maxHealth = 100f;
            //set current health to max
            curHealth = maxHealth;
            //make sure player is alive
            alive = IsLiving.Alive;
            //max exp starts at 60
            maxExp = 60;
            //connect the Character Controller to the controller variable
            controller = GetComponent<CharacterController>();
            movement = GetComponent<CharacterMovement>();
        }
        #endregion
        #region Update
        private void Update()
        {
            //if our current experience is greater or equal to the maximum experience
            if (curExp >= maxExp)
            {

                //then the current experience is equal to our experience minus the maximum amount of experience
                curExp -= maxExp;
                //our level goes up by one
                level++;
                //the maximum amount of experience is increased by 50
                maxExp += 50;
            }
            if (movement.isPaused)
            {

            }
        }
        #endregion
        #region LateUpdate
        private void LateUpdate()
        {
            //if our current health is greater than our maximum amount of health
            if (curHealth > maxHealth)
            {
                //then our current health is equal to the max health
                curHealth = maxHealth;
            }
            //if our current health is less than 0 or we are not alive
            if (curHealth < 0 || alive == IsLiving.Dead)
            {
                //current health equals 0
                curHealth = 0;
            }
            switch (alive)
            {
                case IsLiving.Alive:
                    if (curHealth == 0)
                    {
                        alive = IsLiving.Dead;
                    }
                    break;
                case IsLiving.Dead:
                    Die();
                    break;
            }
        }
        #endregion
        private void Die()
        {
            controller.enabled = false;
        }
        #region OnGUI
        private void OnGUI()
        {
            //set up our aspect ratio for the GUI elements
            float scrW = Screen.width / 16;
            float scrH = Screen.height / 9;
            if (!movement.isPaused)
            {
                //GUI Box on screen for the healthbar background
                GUI.Box(new Rect(scrW * 6, scrH * 0.25f, scrW * 4, scrH * 0.5f), "", healthColorBackground);
                //GUI Box for current health that moves in same place as the background bar
                //current Health divided by the posistion on screen and timesed by the total max health
                GUI.Box(new Rect(scrW * 6, scrH * 0.25f, curHealth * (scrW * 4) / maxHealth, scrH * 0.5f), "", healthColor);

                //GUI Box on screen for the experience background
                GUI.Box(new Rect(scrW * 6, scrH * 0.75f, scrW * 4, scrH * 0.25f), "", expColorBackground);
                //GUI Box for current experience that moves in same place as the background bar
                //current experience divided by the posistion on screen and timesed by the total max experience
                GUI.Box(new Rect(scrW * 6, scrH * 0.75f, curExp * (scrW * 4) / maxExp, scrH * 0.25f), "", expColor);
                //GUI Draw Texture on the screen that has the mini map render texture attached 
                GUI.DrawTexture(new Rect(13.75f * scrW, 0.25f * scrH, 2 * scrW, 2 * scrH), miniMap);
            }
        }

        #endregion
    }
    public enum IsLiving
    {
        Alive,
        Dead
    }
}









