using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Rigidbody2D birdRigidBody;

    public LogicManagerScript logicManagerScript;
    public AudioSource musicSource;
    public float flapStrength = 2;
    public float rotateStrength = 5;
    public float flapAnimationLength = 0.1f;

    private bool alive = true;
    private SpriteRenderer spriteRenderer;
    private Sprite wingDownSprite;
    private Sprite wingUpSprite;
    private float flapAnimationTimer = 0;

    private void handleFlapAnimation()
    {
        if (flapAnimationLength < flapAnimationTimer)
        {
            spriteRenderer.sprite = wingDownSprite;
        }
        else
        {
            flapAnimationTimer += Time.deltaTime;
        }
    }

    private IEnumerator waitForAudio(AudioSource audioSource)
    {
        yield return new WaitUntil(() => audioSource.isPlaying == false);
        Destroy(audioSource);
    }

    private void handleFlapAudio()
    {
        AudioClip flapSound = Resources.Load<AudioClip>("flap");
        AudioSource flapAudio = gameObject.AddComponent<AudioSource>();
        flapAudio.PlayOneShot(flapSound);
        StartCoroutine(waitForAudio(flapAudio));
    }

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        wingDownSprite = Resources.Load<Sprite>("birb1");
        wingUpSprite = Resources.Load<Sprite>("birb2");
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(
            0.0f,
            0.0f,
            birdRigidBody.velocity.y * rotateStrength
        );

        handleFlapAnimation();

        if (alive)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
            {
                handleFlapAudio();
                spriteRenderer.sprite = wingUpSprite;
                birdRigidBody.velocity = Vector2.up * flapStrength;
                flapAnimationTimer = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        alive = false;
        logicManagerScript.gameOver();
        // this.GetComponent<Collider2D>().enabled = false;
        birdRigidBody.velocity = Vector2.left * 3f + Vector2.up * 2f;
        rotateStrength *= 10;
        musicSource.pitch = 0.5f;
    }
}
