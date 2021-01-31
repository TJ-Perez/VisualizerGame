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
        DontDestroyOnLoad(transform.gameObject);

        playButton.onClick.AddListener(PlayGame);

        GameObject.Find("Player").SetActive(false);

        MTW = GameObject.Find("Music Player").GetComponent<MusicToWorld>();

        Debug.Log(songSelect.options[songSelect.value].text);
        dropName = songSelect.options[songSelect.value].text;
        MTW.test = 1;

    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void PlayGame()
    {
        Debug.Log("pressed");
        //SceneManager.UnloadSceneAsync("World");
        SceneManager.LoadScene("World");

        MTW.test = 1;
        dropName = songSelect.options[songSelect.value].text;
    }
}
