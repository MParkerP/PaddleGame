using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI playerOneScoreText;
    public TextMeshProUGUI playerTwoScoreText;

    public GameObject ball;
    private Vector3 ballStartingPosition = new Vector3(0, 0, 1);
    public GameObject currentBall;
    public Rigidbody2D currentBallRb;

    public GameObject ballFlasher;
    private SpriteRenderer ballFlashRenderer;
    private Color initialColor;
    private Color invisibleColor = new Color(1, 1, 1, 0);

    private int playerOneScore = 0;
    private int playerTwoScore = 0;

    private AudioSource spawnAudioSource;
    private AudioSource scoreAudioSource;
    public AudioClip spawnDing;
    public AudioClip scoreSound;
    public AudioMixer mainMixer;
    public AudioMixerGroup effectsAudioMixerGroup;
    public AudioMixerGroup musicAudioMixerGroup;
    private float effectsVolume;
    private float musicVolume;

    public Slider pauseMusicSlider;
    public Slider pauseEffectsSlider;

    public GameObject pauseMenu;

    public GameObject player1;
    public Rigidbody2D player1Rb;
    public GameObject player2;
    public Rigidbody2D player2Rb;

    private AudioSource gameMusic;
    public AudioClip gameMusicsound;




    // Start is called before the first frame update
    void Start()
    {

        //set the text of both scores to the var score (0)
        playerOneScoreText.text = playerOneScore.ToString();
        playerTwoScoreText.text = playerTwoScore.ToString();

        //access the sprite renderer of the flashing ball and get its starting color (white)
        ballFlashRenderer = ballFlasher.GetComponent<SpriteRenderer>();
        initialColor = ballFlashRenderer.color;

        //get the first audio source for one sound effect and add one for another
        spawnAudioSource = GetComponent<AudioSource>();
        scoreAudioSource = gameObject.AddComponent<AudioSource>();
        gameMusic = gameObject.AddComponent<AudioSource>();
        gameMusic.clip = gameMusicsound;

        //set the right mixer to audio sources so they can be controlled properly
        spawnAudioSource.outputAudioMixerGroup= effectsAudioMixerGroup;
        scoreAudioSource.outputAudioMixerGroup = effectsAudioMixerGroup;
        gameMusic.outputAudioMixerGroup = musicAudioMixerGroup;
        gameMusic.loop = true;
        gameMusic.Play();


        mainMixer.GetFloat("effectsVolume", out effectsVolume);
        pauseEffectsSlider.value = effectsVolume;

        mainMixer.GetFloat("musicVolume", out musicVolume);
        pauseMusicSlider.value = musicVolume;

        StartCoroutine(SpawnBall());



    }

    void FixedUpdate()
    {
        //checking to see if a ball needs to be spawned
        int numOfBalls = GameObject.FindGameObjectsWithTag("Ball").Length;
        if (numOfBalls < 1)
        {
            StartCoroutine(SpawnBall());
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu.activeSelf)
            {
                PauseGame();
            }
            else 
            {
                UnpauseGame();
            }
        }
    }

    //play score sound and change score/text
    public void UpdatePlayerOneScore(int amount)
    {
        PlaySoundEffect(scoreAudioSource, scoreSound);
        playerOneScore += amount;
        playerOneScoreText.text = playerOneScore.ToString();
    }

    //play score sound and change score/text
    public void UpdatePlayerTwoScore(int amount)
    {
        PlaySoundEffect(scoreAudioSource, scoreSound);
        playerTwoScore += amount;
        playerTwoScoreText.text = playerTwoScore.ToString();
    }

    //"flash" a ball with sound to warn of spawning ball before spawing prefab
    IEnumerator SpawnBall()
    {
        for(int i = 0; i < 3; i++)
        {
            spawnAudioSource.clip = spawnDing;
            spawnAudioSource.volume = 0.25f;

            ballFlasher.SetActive(true);
            spawnAudioSource.Play();
            yield return new WaitForSeconds(0.5f);

            ballFlashRenderer.color = invisibleColor;
            yield return new WaitForSeconds(0.5f);

            ballFlashRenderer.color = initialColor;
            spawnAudioSource.Play();
        }
        GameObject newBall = Instantiate(ball, ballStartingPosition, ball.transform.rotation);

        currentBall = newBall;
        currentBallRb = newBall.GetComponent<Rigidbody2D>();
        ballFlasher.SetActive(false);

    }

    void PlaySoundEffect(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameMusic.Pause();
        player1Rb.constraints = RigidbodyConstraints2D.FreezeAll;
        player2Rb.constraints = RigidbodyConstraints2D.FreezeAll;
        if (currentBall != null)
        {
            currentBallRb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public void UnpauseGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameMusic.Play();
        player2Rb.constraints = RigidbodyConstraints2D.None;
        player2Rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        player1Rb.constraints = RigidbodyConstraints2D.None;
        player1Rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        if (currentBall != null)
        {
            currentBallRb.constraints = RigidbodyConstraints2D.None;
        }
    }
}
