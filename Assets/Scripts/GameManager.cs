using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI playerOneScoreText;
    public TextMeshProUGUI playerTwoScoreText;

    public GameObject ball;
    private Vector3 ballStartingPosition = new Vector3(0, 0, 1);

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
    }

    // Update is called once per frame
    void Update()
    {
        //checking to see if a ball needs to be spawned
        int numOfBalls = GameObject.FindGameObjectsWithTag("Ball").Length;
        if (numOfBalls < 1)
        {
            StartCoroutine(SpawnBall());
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

        Instantiate(ball, ballStartingPosition, ball.transform.rotation);
        ballFlasher.SetActive(false);

    }

    void PlaySoundEffect(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }


}
