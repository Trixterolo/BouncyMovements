using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb2d;

    public float overlapRadius = 3f; // Radius of the overlap circle
    [SerializeField] bool canIncreaseSpeed = false;

    //Tweening
    public float shakeDuration = 0.5f;
    public float shakeStrength = 0.5f;

    private Vector3 originalScale;


    //VFX
    [SerializeField] private ParticleSystem particleSystem;

    //UI
    [SerializeField] int playerHealth = 10;
    [SerializeField] int playerScore = 0;

    [SerializeField] TextMeshProUGUI hP;
    [SerializeField] TextMeshProUGUI speed;

    [SerializeField] TextMeshProUGUI score;

    //Restart
    [SerializeField] GameObject restartGO;

    private void Awake()
    {
        restartGO.SetActive(false);
    }

    private void Start()
    {

        originalScale = transform.localScale;
        particleSystem.Stop();

        playerHealth = 10;
        playerScore = 0;

        hP.text = "HP: " + playerHealth.ToString();
        speed.text = "Speed: " + moveSpeed.ToString();

        score.text = "Score: " + playerScore.ToString();


        Launch();


    }

    private void Launch()
    {
        //random movement start
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb2d.velocity = new Vector3(moveSpeed * x, moveSpeed * y);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canIncreaseSpeed)
        {
            canIncreaseSpeed = false; // Reset the flag

            // Increase moveSpeed and apply new velocity
            moveSpeed *= 1.25f;
            rb2d.velocity = rb2d.velocity.normalized * moveSpeed;

            //Color change
            IncreaseColorChange();

            particleSystem.Play();

            //doshake with scale and rotation
            Shake();
            CameraShaker.Invoke();
           
            
            //speed UI
            float myFloat = moveSpeed;
            int myInt = (int)myFloat;
            speed.text = "Speed: " + myInt.ToString();

            //Update score UI
            UpdateScore();
            score.text = "Score: " + playerScore.ToString();

        }
        else if (Input.GetKeyDown(KeyCode.Space) && !canIncreaseSpeed)
        {
            // Decrease moveSpeed and apply new velocity
            moveSpeed /= 2f;
            rb2d.velocity = rb2d.velocity.normalized * moveSpeed;

            //Color change
            DecreaseColorChange();

            //doshake with scale and rotation
            Shake();

            //health UI
            playerHealth -= 1;
            hP.text = "HP: " + playerHealth.ToString();

            //speed UI
            float myFloat = moveSpeed;
            int myInt = (int)myFloat;
            speed.text = "Speed: " + myInt.ToString();

        }

        if(moveSpeed <= 1f || playerHealth <= 0) 
        {
            Destroy(gameObject);
            restartGO.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    private void FixedUpdate()
    {
        // Check for overlap with walls
        canIncreaseSpeed = IsOverlappingWall();
    }


    private bool IsOverlappingWall()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, overlapRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject == gameObject) // Ignore self
                continue;

            if (collider.gameObject.CompareTag("Wall"))
                return true;
        }


        return false;

    }


    private void OnDrawGizmos()
    {
        if (canIncreaseSpeed)
        {
  
            Gizmos.color = Color.green;

        }
        else
        {
            Gizmos.color = Color.red; // Set the color of the Gizmos
        }

        // Draw the overlap circle in the Scene view
        Gizmos.DrawWireSphere(transform.position, overlapRadius);
    }

    public void Shake()
    {
        //reset scale
        transform.localScale = originalScale;

        //shake scale
        transform.DOComplete();
        Transform player = transform;


        if (moveSpeed <= 10)
        {
            transform.DOShakeRotation(shakeDuration, 0.75f);
            transform.DOShakeScale(shakeDuration, 0.75f);

        }


        if (moveSpeed > 10f && moveSpeed <= 25f)
        {
            transform.DOShakeRotation(shakeDuration, 0.1f);
            transform.DOShakeScale(shakeDuration, 0.1f);

        }


        if (moveSpeed > 25f && moveSpeed <= 50f)
        {
            transform.DOShakeRotation(shakeDuration, 1.25f);
            transform.DOShakeScale(shakeDuration, 1.25f);

        }

        if (moveSpeed > 50f && moveSpeed <= 75f)
        {

            transform.DOShakeRotation(shakeDuration, 1.5f);
            transform.DOShakeScale(shakeDuration, 1.5f);
        }
        if (moveSpeed > 75f && moveSpeed <= 125f)
        {
            transform.DOShakeRotation(shakeDuration, 1.75f);
            transform.DOShakeScale(shakeDuration, 1.75f);

        }

        if (moveSpeed > 125f)
        {
            transform.DOShakeRotation(shakeDuration, 2f);
            transform.DOShakeScale(shakeDuration, 2f);

        }
    }

    private void IncreaseColorChange()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Sequence colorSequence = DOTween.Sequence();

        colorSequence.Append(spriteRenderer.DOColor(Color.cyan, 0.05f));
        colorSequence.AppendInterval(0.05f);
        colorSequence.Append(spriteRenderer.DOColor(Color.black, 0.05f));

        colorSequence.SetLoops(3);
    }

    private void DecreaseColorChange()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Sequence colorSequence = DOTween.Sequence();

        colorSequence.Append(spriteRenderer.DOColor(Color.yellow, 0.05f));
        colorSequence.AppendInterval(0.05f);
        colorSequence.Append(spriteRenderer.DOColor(Color.black, 0.05f));

        colorSequence.SetLoops(3);
    }


    private void UpdateScore()
    {
        if (moveSpeed <= 10)
        {
            playerScore += 100;
        }


        if (moveSpeed > 10f && moveSpeed <= 25f)
        {
            playerScore += 200;

        }


        if (moveSpeed > 25f && moveSpeed <= 50f)
        {
            playerScore += 400;


        }

        if (moveSpeed > 50f && moveSpeed <= 75f)
        {
            playerScore += 800;

        }
        if (moveSpeed > 75f && moveSpeed <= 125f)
        {
            playerScore += 1600;

        }

        if (moveSpeed > 125f)
        {
            playerScore += 3200;

        }
    }

}
