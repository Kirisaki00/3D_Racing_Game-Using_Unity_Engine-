using UnityEngine;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject playerCar;
    public GameObject[] aiCars;

    void Start()
    {
        // Disable cars at start
        playerCar.GetComponent<CarController>().enabled = false;

        foreach (GameObject ai in aiCars)
        {
            ai.GetComponent<AICarController>().enabled = false;
        }

        // VERY IMPORTANT LINE 😈
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        Debug.Log("VIDEO FINISHED");

        // Enable player
        playerCar.GetComponent<CarController>().enabled = true;

        // Enable AI
        foreach (GameObject ai in aiCars)
        {
            ai.GetComponent<AICarController>().enabled = true;
        }

        // Hide video
        videoPlayer.gameObject.SetActive(false);
    }
}