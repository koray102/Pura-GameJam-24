using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{
    public GameObject spawnpoint;

    [Header("SpaceShip")]
    public float transitionDelay;
    public Transform lookDirTransform;
    private Rigidbody btrflyRb;
    [SerializeField] private AudioSource engineSFX;
    [SerializeField] private GameObject mainRocketVFX;
    [SerializeField] private AudioSource dashSFX;
    [SerializeField] private ParticleSystem dashVFX;
    [SerializeField] private ParticleSystem chargeVFX;
    [SerializeField] private Slider cooldownSlider;


    [Header("Speed")]
    public float forceInput;
    public float maxSpeed;
    private Vector3 btrflyVelo;
    private float force;
    private bool leftClick;

    
    [Header("Speed Power Up")]
    public float powerFocusTime, powerDuration, powerCooldown;
    public GameObject camHolder;
    private bool isPowerUsed;
    private bool isForcePowerFinished = true;
    private bool spaceInput;
    private bool isCharged;
    private float spaceTimeCounter;
    private float powerCooldownCounter;


    [Header("Grab")]
    public float maxCollectDist, minGrabDist;
    public Transform camTransform;
    public LayerMask IgnoreRayCast;
    [SerializeField] private Image CrossImage;
    [SerializeField] private Color crossHairColor, crossHairColorGrab;
    private GameObject hitObject;
    private bool rightClick, rightClickUp;
    private bool isHold;
    private Ray r;
    private float holdObjPos;


    [Header("Interact")] 
    internal RaycastHit hit;


    [Header("Side Move")]
    [SerializeField] private float sideTurnSpeed;
    [SerializeField] private float sideTurnForce;
    [SerializeField] private float sideMoveDuration;
    [SerializeField] private float sideMoveCooldown;
    [SerializeField] private GameObject lookAt;
    private bool canSideMove;
    private bool isSideMoveFinished;


    void Start()
    {
        canSideMove = true;
        isSideMoveFinished = true;
        dashSFX.mute = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        btrflyRb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        spaceInput = Input.GetKey(KeyCode.Space);
        leftClick = Input.GetMouseButton(0);
        rightClick = Input.GetMouseButton(1);
        rightClickUp = Input.GetMouseButtonUp(1);

        r = new Ray(origin: camTransform.position, direction: camTransform.forward);
        Debug.DrawRay(camTransform.localPosition, camTransform.forward * maxCollectDist, Color.green);

        if(Physics.Raycast(camTransform.localPosition, camTransform.forward, out hit, maxCollectDist, ~IgnoreRayCast))
        {
            hitObject = hit.collider.gameObject;

            Debug.Log("Touched any object: " + hitObject);

            
            if(hitObject.TryGetComponent(out IInteractable interactable))
            {
                CrossImage.color = crossHairColorGrab;

                if(leftClick)
                {
                    interactable.OnInteract();
                }
            }else
            {
                CrossImage.color = crossHairColor;
            }

        }else if(!isHold && !rightClick && canSideMove)
        {
            CrossImage.color = crossHairColor;
        }


        if(spaceInput && !isPowerUsed)
        {
            spaceTimeCounter += Time.deltaTime;
        }else
        {
            spaceTimeCounter = 0;
            powerCooldownCounter += Time.deltaTime;
        }
        

        if(spaceTimeCounter > powerFocusTime || isCharged)
        {
            isForcePowerFinished = false;
            powerCooldownCounter = 0;
            isCharged = true;
            camHolder.transform.localEulerAngles = new Vector3(0, 0, 0);

            // Hızlandırma
            if(Input.GetKeyUp(KeyCode.Space))
            {
                force = forceInput * 20;
                StartCoroutine(PowerDelay());
                dashVFX.Play();
                dashSFX.mute = false;
                chargeVFX.Stop();
                
                /*if(powerVFX.isStopped)
                {
                    Debug.Log("Dash started");
                    isDashStarted = true;

                    powerFocusVFX.Stop();
                    powerVFX.Play();
                    dashSFX.PlayOneShot(dashSFX.clip);
                }*/
            }
        }else if (spaceTimeCounter > 0) // Charge esnasında gemiyi yavaşlat
        {
            force = forceInput / 5;
            chargeVFX.Play();

            /*if(powerFocusVFX.isStopped)
            {
                chargeSFX.Play();
                powerFocusVFX.Play();
            }*/

        }else if(isForcePowerFinished)
        {
            force = forceInput;
            //chargeSFX.Stop();
            chargeVFX.Stop();
        }


        if(spaceTimeCounter == 0)
        {
            //powerFocusTimeSliderObj.SetActive(false);
        }else
        {
            //powerFocusTimeSliderObj.SetActive(true);
            //powerFocusTimeSlider.value = spaceTimeCounter / powerFocusTime;
        }
        //powerCooldownSlider.value = -powerCooldownCounter / (powerCooldown - powerDuration);


        if(Input.GetKey(KeyCode.A) && canSideMove)
        {
            StartCoroutine(SideMove(1));
        }else if(Input.GetKey(KeyCode.D) && canSideMove)
        {
            StartCoroutine(SideMove(-1));
        }


        if(isSideMoveFinished)
        {
            cooldownSlider.value -= Time.deltaTime;
        }else
        {
            cooldownSlider.value = 0;
        }

        if(isSideMoveFinished && force == forceInput)
        {
            btrflyRb.velocity = Vector3.ClampMagnitude(btrflyRb.velocity, maxSpeed);
            transform.forward += Vector3.Lerp(transform.forward, btrflyVelo.normalized, transitionDelay * Time.deltaTime);
        }
    }


    private void FixedUpdate()
    {
        // Kameranın baktığı yöne doğru f uygula
        if(Input.GetKey(KeyCode.W))
        {
            mainRocketVFX.SetActive(true);
            engineSFX.volume = Mathf.Lerp(engineSFX.volume, 0.2f, 0.2f);
            if(Input.GetKey(KeyCode.Tab))
            {
                btrflyRb.AddForce(transform.forward * force);
            }else
            {
                btrflyRb.AddForce(lookDirTransform.forward * force);
            }
        }else
        {
            mainRocketVFX.SetActive(false);
            engineSFX.volume = Mathf.Lerp(engineSFX.volume, 0f, 0.2f);
        }

        // Geminin son hızını belirle
        if(isForcePowerFinished && force == forceInput)
        {
            btrflyRb.velocity = Vector3.ClampMagnitude(btrflyRb.velocity, maxSpeed);
        }

        // Gemiyi hızına göre döndür
        btrflyVelo = btrflyRb.velocity;
    }


    IEnumerator PowerDelay()
    {
        isPowerUsed = true;
        isCharged = false;

        yield return new WaitForSeconds(powerDuration);
        Debug.Log("Dash finished");

        isForcePowerFinished = true;
        dashVFX.Stop();
        dashSFX.mute = true;
        camHolder.transform.localEulerAngles = new Vector3(-15, 0, 0);
        holdObjPos = Mathf.Clamp(holdObjPos, minGrabDist, maxCollectDist);

        yield return new WaitForSeconds(powerCooldown - powerDuration);
        isPowerUsed = false;
    }


    private IEnumerator SideMove(float direction)
    {
        canSideMove = false;
        isSideMoveFinished = false;

        transitionDelay /= 50;
        btrflyRb.AddForce(-direction * sideTurnForce * transform.right, ForceMode.Impulse);

        float sideMoveValue;
        sideMoveValue = 0;
        while(sideMoveValue < 360f)
        {
            Debug.Log("side");

            sideMoveValue = Mathf.Lerp(sideMoveValue, 365, sideTurnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y,direction * sideMoveValue);

            yield return null;
        }

        isSideMoveFinished = true;
        cooldownSlider.value = sideMoveCooldown;
        transitionDelay *= 50;

        yield return new WaitForSeconds(sideMoveCooldown);
        canSideMove = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            interactable.OnInteract();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            gameObject.transform.position = spawnpoint.transform.position;
        }
    }
}
