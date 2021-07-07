using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{   
    [SerializeField] float secondsToWait = 1f ;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip seccessAudio;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem seccessParticles;
    AudioSource audioSource;
    

    bool isTransitioning = false;
    bool collisionDisabled = false;
     void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }
     void Update() 
        {
            RespondToDebugKeys();
        }
    void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
        LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }
    private void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning || collisionDisabled) { return; }

        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("the object is friendly!");
                break; 
            case "Finish":
                StartSuccessSequence();
                break; 
            default: 
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    { 
        isTransitioning = true;
        audioSource.Stop();
        crashParticles.Play();
        audioSource.PlayOneShot(crashAudio);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", secondsToWait);   
       
    }
    void StartSuccessSequence()
    {
        
         isTransitioning = true; 
         audioSource.Stop();
         seccessParticles.Play();
         audioSource.PlayOneShot(seccessAudio);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", secondsToWait);   
    }
    
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1 ; 
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
        nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
       
    }
    void ReloadLevel()
    {  
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

