using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLogic : MonoBehaviour
{
    CharacterController m_characterController;
    Animator animator;
    float m_moveSpeed = 5.0f;
    Vector3 m_moveVector;
    float m_verticalVelocity;
    float m_gravity=12.0f;
    float animationDuration = 3.0f;
    bool isDead = false;
    float startTime;
    Score score;
    AudioSource audioSource;
    [SerializeField] AudioClip[] scaryAudio;
    float timeScaryAudio = 9f;
    bool isSliding = false;
    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        score = GetComponentInChildren<Score>();
        startTime = Time.time;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            return;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Running Slide"))
        {
            m_characterController.height = 1.0f;
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            m_characterController.center = new Vector3(0f, 0.5f, 0f);
        }
        else
        {
            m_characterController.height = 2.0f;
            transform.rotation = Quaternion.EulerAngles(0f, 0f, 0f);
            m_characterController.center = new Vector3(0f, 1f, 0f);
        }
        if(Time.time-startTime <animationDuration)
        {
            m_characterController.Move(Vector3.forward * m_moveSpeed * Time.deltaTime);
            return;
        }
        m_moveVector = Vector3.zero;

        if(m_characterController.isGrounded)
        {
            m_verticalVelocity = -0.5f;
        }
        else
        {
            m_verticalVelocity -= m_gravity*Time.deltaTime;
        }

        m_moveVector.x = Input.GetAxisRaw("Horizontal") * m_moveSpeed;
        if (Input.GetMouseButton(0))
        {
            if(Input.mousePosition.x>Screen.width/2)
            {
                m_moveVector.x = m_moveSpeed;
            }
            else
            {
                m_moveVector.x = -m_moveSpeed;
            }
        }

        m_moveVector.y = m_verticalVelocity;
        m_moveVector.z = m_moveSpeed;

        m_characterController.Move(m_moveVector* Time.deltaTime);

        timeScaryAudio -= Time.deltaTime;
        if(timeScaryAudio<=0.0f)
        {
            audioSource.PlayOneShot(scaryAudio[Random.Range(0, scaryAudio.Length)]);
            timeScaryAudio = 9.0f;
        }
    }

    public void SetSpeed(float modifier)
    {
        m_moveSpeed =3.0f* modifier;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag=="Gravity")
        {
            Death();
            audioSource.Pause();
            return;
        }
        if ((hit.point.z>transform.position.z+0.1f)&& hit.gameObject.tag=="Object")
        {
            Death();
            audioSource.Pause();
            animator.SetTrigger("Death");
        }
        if ((hit.point.z > transform.position.z + 0.1f) && hit.gameObject.tag == "Object2")
        {
            Death();
            audioSource.Pause();
            animator.SetTrigger("Death2");
        }
    }

    public void Death()
    {
        isDead = true;
        score.Death();
        GameManager.instance.Death();
    }

    public void Slide()
    {
        animator.SetTrigger("Slide");
        isSliding = true;
    }

}
