using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuDriver : MonoBehaviour
{
    // Start is called before the first frame update


    public Button playButton;
    public Dropdown songSelect;

    private GameObject player;

    public MusicToWorld MTW;

    public string dropName;

    void Awake()
    {

        SceneManager.LoadScene("World", LoadSceneMode.Additive);
        DontDestroyOnLoad(transform.gameObject);

    }

    void OnEnable()
    {
        playButton.onClick.AddListener(PlayGame);



    }


    void Start()
    {
        MTW = GameObject.Find("Music Player").GetComponent<MusicToWorld>();

        Debug.Log(songSelect.options[songSelect.value].text);
        dropName = songSelect.options[songSelect.value].text;
        MTW.test = 1;


        GameObject.Find("Player").SetActive(false);
        GameObject.Find("WorldEventSystem").SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlayGame()
    {
        Debug.Log("pressed");
        //SceneManager.UnloadSceneAsync("World");
        SceneManager.LoadScene("World", LoadSceneMode.Single);

        MTW.test = 1;
        dropName = songSelect.options[songSelect.value].text;
    }
}
