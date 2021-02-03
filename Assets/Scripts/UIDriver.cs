using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIDriver : MonoBehaviour
{
    // Start is called before the first frame update


    public Button playButton;
    public Dropdown songSelect;

    public GameObject player;

    public MusicToWorld MTW;
    public CharController CC;

    public string dropName;


    public Text scoreText;
    public Text deathScore;
    public Text pauseText;



    private Vector3 startPos = new Vector3(0, 5, -11.83f);
    private Vector3 menuPos = new Vector3(2.1f, 43.3f, 18.9f);

    private Quaternion menuRot = Quaternion.Euler(90, 0, 0);
    private Quaternion startRot = Quaternion.Euler(0, 0, 0);


    public string scoreString;

    public bool menuMode;



    void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject);

        //GameObject.Find("DeathScore").SetActive(false);


    }

    void OnEnable()
    {



    }


    void Start()

    {
        UIDisable();
        MainMenuUIEnable();


        playButton.onClick.AddListener(StartGame);

        dropName = songSelect.options[songSelect.value].text;




    }

    // Update is called once per frame
    void Update()
    {
        scoreString = "Score- " + CC.score.ToString();
        scoreText.text = scoreString;
        CheckKeys();
        //Debug.Log(player.transform.position.ToString() + "cur");

    }

    void CheckKeys()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale != 0)
            {
                PauseGame();
            }
            else
            {
                UnPauseGame();  
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;

    }

    void UnPauseGame()
    {
        Time.timeScale = 1;
    }

    void StartGame()
    {
        dropName = songSelect.options[songSelect.value].text;
        UIDisable();
        GameUIEnable();


        CC.gameObject.GetComponent<CharacterController>().enabled = false;
        player.transform.position = startPos;
        player.transform.rotation = startRot;
        CC.gameObject.GetComponent<CharacterController>().enabled = true;

        menuMode = false;
    }

    void GameUIEnable()
    {
        scoreText.gameObject.SetActive(true);


    }


    void MainMenuUIEnable()
    {
        playButton.gameObject.SetActive(true);
        songSelect.gameObject.SetActive(true);

        menuMode = true;

        //player.SetActive(false);

        player.transform.position = menuPos;
        player.transform.rotation = menuRot;



    }


    void UIDisable()
    {
        playButton.gameObject.SetActive(false);
        songSelect.gameObject.SetActive(false);
        deathScore.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        pauseText.gameObject.SetActive(false);

    }

    public void DeathUIEnable()
    {
        UIDisable();
        deathScore.text = scoreString;
        deathScore.gameObject.SetActive(true);
    }


    //public Texture2D cursorTexture;
    //public CursorMode cursorMode = CursorMode.Auto;
    //public Vector2 hotSpot = Vector2.zero;
    //void OnMouseEnter()
    //{
    //    Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    //}

    //void OnMouseExit()
    //{
    //    Cursor.SetCursor(null, Vector2.zero, cursorMode);
    //}
}
