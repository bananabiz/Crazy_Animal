using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//this script can be found in the Component section under the option Character Set Up 
//Character Handler
[AddComponentMenu("Character Set Up / Character Handler")]

public class CharacterHandler : MonoBehaviour 
{
    [Header("Character")]
    //bool to tell if the player is alive
    public bool alive;
    //connection to players character controller
    public CharacterController controller;
    public int attackDamage;

    [Header("Health")]
    //max and min health
    public float maxHealth, curHealth;
    public GUIStyle healthbar, bg, barLine;

    [Header("Mana")]
    //max and min Mana
    public float maxMana, curMana;
    public GUIStyle mana;

    [Header("Stamina")]
    //max and min Stamina
    public float maxStamina, curStamina;
    public GUIStyle stamina;

    [Header("Levels and Exp")]
    //players current level
    public int level;
    //max and min experience 
    public int maxExp, curExp;
    public GUIStyle experience;

    [Header("Camera MiniMap")]
    //render texture for the mini map that we need to connect to a camera
    public RenderTexture miniMap;
    public RenderTexture faceMiniMap;
    public GUIStyle miniMapBlank;

    [Header("You Dead Canvas")]
    public GameObject youDead;

    void Start()
    {
        //set max health to 100
        maxHealth = 100f;
        //set current health to max
        curHealth = maxHealth;

        //set max mana to 100
        maxMana = 100f;
        //set current Mana to max
        curMana = maxMana;

        //set max stamina to 100
        maxStamina = 100f;
        //set current Stamina to max
        curStamina = maxStamina;

        //make sure player is alive
        alive = true;

        //max exp starts at 60
        maxExp = 60;
        curExp = 10;

        //connect the Character Controller to the controller variable
        controller = this.GetComponent<CharacterController>();
    }
   
    void Update()
    {
        //if current experience is greater or equal to the maximum experience
        if (curExp >= maxExp)
        {
            //then the current experience is equal to our experience minus the maximum amount of experience
            curExp -= maxExp;
            //our level goes up by one
            level++;
            //the maximum amount of experience is increased by 50
            maxExp += attackDamage;
        }

        if (curHealth < 0)
        {
            curHealth = 0;
            Time.timeScale = 0;
            youDead.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    
    void lateUpdate()
    {
        //if current health is greater than our maximum amount of health
        if (curHealth > maxHealth)
        {
            //then current health is equal to the max health
            curHealth = maxHealth;
        }
        //if current health is less than 0 or we are not alive
        if (curHealth < 0 || !alive)
        {
            //current health equals 0
            curHealth = 0;
        }
        //if the player is alive
        if (alive)
        {
            //and health is less than or equal to 0
            if (curHealth <= 0)
            {
                //alive is false
                alive = false;
                //controller is turned off
                controller.enabled = false;
            }
        }
        //if current Mana is greater than our maximum amount of Mana
        if (curMana > maxMana)
        {
            //then current Mana is equal to the max Mana
            curMana = maxMana;
        }
        //if current Mana is less than 0 
        if (curMana < 0)
        {
            //current Mana equals 0
            curMana = 0;
        }

        if (curStamina > maxStamina)
        {
            curStamina = maxStamina;
        }
        if (curStamina < 0)
        {
            curStamina = 0;
        }
    }
    
    void OnGUI()
    {
        //set up aspect ratio for the GUI elements
        float scrW = Screen.width / 16;
        float scrH = Screen.height / 9;

        //GUI Box on screen for the healthbar background
        GUI.Box(new Rect(1.5f * scrW, 0.25f * scrH, 4 * scrW, 0.35f * scrH), "HP", bg); //background
        //GUI Box for current health that moves in same place as the background bar
        GUI.Box(new Rect(1.5f * scrW, 0.25f * scrH, curHealth * (4 * scrW) / maxHealth, 0.35f * scrH), "", healthbar); //moving health bar
        //current Health divided by the position on screen and timesed by the total max health
        GUI.Box(new Rect(1.5f * scrW, 0.25f * scrH, 4 * scrW, 0.35f * scrH), "", barLine); //white line

        GUI.Box(new Rect(1.5f * scrW, 0.62f * scrH, 3 * scrW, 0.2f * scrH), "MP", bg); //background
        GUI.Box(new Rect(1.5f * scrW, 0.62f * scrH, curMana * (3 * scrW) / maxMana, 0.2f * scrH), "", mana); //moving exp bar
        GUI.Box(new Rect(1.5f * scrW, 0.62f * scrH, 3 * scrW, 0.2f * scrH), "", barLine); //white line

        GUI.Box(new Rect(1.5f * scrW, 0.84f * scrH, 3 * scrW, 0.2f * scrH), "ST", bg); //background
        GUI.Box(new Rect(1.5f * scrW, 0.84f * scrH, curStamina * (3 * scrW) / maxStamina, 0.2f * scrH), "", stamina); //moving exp bar
        GUI.Box(new Rect(1.5f * scrW, 0.84f * scrH, 3 * scrW, 0.2f * scrH), "", barLine);

        GUI.Box(new Rect(1.5f * scrW, 1.06f * scrH, 2 * scrW, 0.2f * scrH), "EXP", bg); //background
        GUI.Box(new Rect(1.5f * scrW, 1.06f * scrH, curExp * (2 * scrW) / maxExp, 0.2f * scrH), "", experience); //moving health bar
        GUI.Box(new Rect(1.5f * scrW, 1.06f * scrH, 2 * scrW, 0.2f * scrH), "", barLine);

        //GUI Draw Texture on the screen that has the mini map render texture attached
        GUI.DrawTexture(new Rect(12.5f * scrW, 0.25f * scrH, 3.3f * scrW, 2.6f * scrH), miniMap); //minimap Top view
        GUI.DrawTexture(new Rect(0.5f * scrW, 0.25f * scrH, 1f * scrW, 1f * scrH), faceMiniMap); //minimap Face renderer
        GUI.Box(new Rect(0.5f * scrW, 0.25f * scrH, 1f * scrW, 1.01f * scrH), "", miniMapBlank);

    }
}