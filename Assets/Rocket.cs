using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour 
{
    Rigidbody rigidbody;
    AudioSource audioSource;

    [SerializeField] AudioClip engineSound;
    [SerializeField] AudioClip blastSound;
    [SerializeField] AudioClip winSound;
    [SerializeField] float rotationThrust = 150f;
    [SerializeField] float liftingThrust = 10f;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    enum State {Dying,Alive,Transcending}
    State state = State.Alive;

	void Start () 
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	void Update () 
    {
        if(state == State.Dying || state == State.Transcending){return;}
        RespondToThrustInput();
        RespondToRotationInput();
	}

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; } //ignore collision when dead

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;

            case "Finish":
                ExecuteSuccessSequence();
                break;
            default:
                ExecuteDeathSequence();
                break;
        }
    }

    private void ExecuteSuccessSequence()
    {
        mainEngineParticles.Stop();
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(winSound);
        successParticles.Play();
        Invoke("LoadScene", 2f);
    }

    private void ExecuteDeathSequence()
    {
        mainEngineParticles.Stop();
        deathParticles.Play();
        audioSource.Stop();
        state = State.Dying;
        audioSource.PlayOneShot(blastSound);
        Invoke("LoadScene", 2f);
    }


    private void LoadScene()
    {
        if(state == State.Dying){
            SceneManager.LoadScene(0);
        }else if(state == State.Transcending){
            SceneManager.LoadScene(1);
        }
         //todo Allow for more than 2 levels
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Thrust();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void RespondToRotationInput()
    {
        rigidbody.freezeRotation = true; //take manual control of rotation
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThrust * Time.deltaTime);
        }
        rigidbody.freezeRotation = false;
    }


    private void Thrust()
    {
        rigidbody.AddRelativeForce(Vector3.up * liftingThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(engineSound);
        }
        mainEngineParticles.Play();
    }
}
