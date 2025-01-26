using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField]
    public float endGameTimeout = 10f;
    [SerializeField]
    public float allowMovementAfterTime = 10f;
    [SerializeField]
    private HUD hud;
    [SerializeField]
    private FadeController fadeController;
    [SerializeField]
    private float victoryPercent=0.75f;

    [SerializeField]
    private int allowedSounds = 3;

    private int soundPool = 0;
    private WaitForSeconds soundWait = new(0.2f);

    private bool soundAllowed;
    
    private List<StudioEventEmitter> studioEventEmitters=new();

    private BubbleController bubble;
     
    private bool victoryAchieved = false;

    private int furnitureCount = 0;
    private int furnitureMovedOut = 0;
    
        bool ending;
        
    private static PlayManager _instance;
    public static PlayManager Instance { 
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<PlayManager>();
            }
            return _instance;
        }
    }

    public void Start()
    {
        bubble = FindAnyObjectByType<BubbleController>();
        StartCoroutine(AllowMovement());
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private IEnumerator AllowMovement() {
        yield return new WaitForSeconds(allowMovementAfterTime);
        bubble.AllowMovement();
        soundAllowed = true;
    }

    public void Update()
    {
        if (Input.GetButton("LeaveGame") &&!ending)
        {
            ending = true;
            StartCoroutine(EndLevel(0));
        }
        if (victoryAchieved && !ending)
        {
            if (Input.GetButton("ContinueGame"))
            {
                ending = true;
            } else if (Input.GetButton("QuitGame"))
            {
                ending = true;
                StartCoroutine(EndLevel(0));
            }
        }
    }

    public void RegisterFurniture(FurnitureBehaviour furniture) {
        foreach (StudioEventEmitter emitter in furniture.GetComponentsInChildren<StudioEventEmitter>()) {
                if (emitter.PlayEvent == EmitterGameEvent.TriggerEnter) {
                    studioEventEmitters.Add(emitter);
                    furniture.setTriggerEnterSound(emitter);
                } else if (emitter.PlayEvent == EmitterGameEvent.TriggerExit) {
                    studioEventEmitters.Add(emitter);
                    furniture.setTriggerExitSound(emitter);
                }
                emitter.PlayEvent = 0;

        }
        ++furnitureCount;
    }

    public bool requestSound(StudioEventEmitter emitter) {
        if (!soundAllowed) {
            return false;
        }

        if (soundPool < allowedSounds) {
            emitter.Play();
            soundPool++;
            StartCoroutine(FreeSoundPool());
        }
/*
        int playing = 0;
        foreach (StudioEventEmitter emitter in studioEventEmitters) {
            if (emitter.IsPlaying()) {
                playing++;
            }
        }
        Debug.Log(playing);
        if (playing >= allowedSounds) {
            return false;
        }
*/
        return true;
    }

    private IEnumerator FreeSoundPool() {
        yield return soundWait;

        soundPool--;
    }

    public void FurnitureMovedOut()
    {
        ++furnitureMovedOut;
        float percentDone = furnitureMovedOut / (float)furnitureCount;
        hud?.UpdateProgress(percentDone);
        if (furnitureMovedOut >= furnitureCount)
        {
            StartCoroutine(EndLevel(endGameTimeout));
        }
        if (percentDone >= victoryPercent && victoryAchieved == false)
        {
            victoryAchieved = true;
            bubble.EnoughMovedOut();
        }
    }

    private IEnumerator EndLevel(float endGameTimeout) {
        ending = true;
        
        yield return new WaitForSeconds(endGameTimeout);

        fadeController.StartFade(0);
    }
}
