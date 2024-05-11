using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{
    [SerializeField] private GameObject denemeObje;

    [Header("Inputs")]
    private bool TABInput;
    private bool spaceInput;
    private bool rightClick, rightClickUp;
    private bool leftClick;
    private bool WInput;


    [Header("SpaceShip")]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Slider cooldownSlider;
    private Rigidbody shipRb;
    private Vector3 targetRotation;


    [Header("Speed")]
    [SerializeField] private float forceInput;
    [SerializeField] private float maxSpeed;
    [SerializeField] private Vector3 addForceOffset;
    private float force;

    
    [Header("Speed Power Up")]
    [SerializeField] private GameObject camHolder;
    [SerializeField] private float powerFocusTime, powerDuration, powerCooldown;
    private bool isPowerUsed;
    private bool isForcePowerFinished = true;
    private bool isCharged;
    private float spaceTimeCounter;


    [Header("Grab")]
    [SerializeField] private LayerMask IgnoreRayCast;
    [SerializeField] private float maxCollectDist, minGrabDist;
    [SerializeField] private Transform camTransform;
    [SerializeField] private Image CrossImage;
    [SerializeField] private Color crossHairColor, crossHairColorGrab;
    private GameObject hitObject;
    private float holdObjPos;


    [Header("Interact")] 
    internal RaycastHit hit;


    [Header("Side Move")]
    [SerializeField] private float sideTurnSpeed;
    [SerializeField] private float sideTurnForce;
    [SerializeField] private float sideMoveCooldown;
    private bool canSideMove;
    private bool isSideMoveFinished;


    [Header("FX")]
    [SerializeField] private AudioSource engineSFX;
    [SerializeField] private GameObject mainRocketVFX;
    [SerializeField] private AudioSource dashSFX;
    [SerializeField] private ParticleSystem dashVFX;
    [SerializeField] private ParticleSystem chargeVFX;


    void Start()
    {
        canSideMove = true;
        isSideMoveFinished = true;
        dashSFX.mute = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        shipRb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        spaceInput = Input.GetKey(KeyCode.Space);
        leftClick = Input.GetMouseButton(0);
        rightClick = Input.GetMouseButton(1);
        rightClickUp = Input.GetMouseButtonUp(1);
        TABInput = Input.GetKey(KeyCode.Tab);
        WInput = Input.GetKey(KeyCode.W);

        //Debug.DrawRay(camTransform.localPosition, camTransform.forward * maxCollectDist, Color.green);


        if(Input.GetKey(KeyCode.W))
        {   
            denemeObje.transform.position = camTransform.position + camTransform.forward * maxCollectDist;

            if(TABInput)
            {
                targetRotation = transform.forward;
            }else
            {
                targetRotation = denemeObje.transform.position + addForceOffset - transform.position;
            }


            // İkisi de ayrı güzel dene bak dayı. Bu daha drift gibi alttaki daha küresel dönüyo
            transform.rotation = Quaternion.Lerp(transform.rotation,
                                                Quaternion.LookRotation(targetRotation), 
                                                rotationSpeed * Time.deltaTime);

            /*float rotationAngle = Mathf.Min(rotationSpeed * Time.deltaTime, 
                                  Quaternion.Angle(transform.rotation, Quaternion.LookRotation(targetRotation)));

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetRotation),
                                                  rotationAngle / rotationSpeed);*/
        }


        if(Physics.Raycast(camTransform.localPosition, camTransform.forward, out hit, maxCollectDist, ~IgnoreRayCast))
        {
            hitObject = hit.collider.gameObject;

            //Debug.Log("Touched any object: " + hitObject);

            
            if(hitObject.TryGetComponent(out IInteractable interactable))
            {
                if(!hitObject.CompareTag("Dont"))
                {
                    CrossImage.color = crossHairColorGrab;
                }

                if(leftClick)
                {
                    interactable.OnInteract();
                }
            }else
            {
                CrossImage.color = crossHairColor;
            }

        }else if(!rightClick && canSideMove)
        {
            CrossImage.color = crossHairColor;
        }


        if(spaceInput && !isPowerUsed)
        {
            spaceTimeCounter += Time.deltaTime;
        }else
        {
            spaceTimeCounter = 0;
        }
        

        if(spaceTimeCounter > powerFocusTime || isCharged)
        {
            isForcePowerFinished = false;
            isCharged = true;

            // Hızlandırma
            if(Input.GetKeyUp(KeyCode.Space))
            {
                force = forceInput * 10;
                StartCoroutine(PowerDelay());
                dashVFX.Play();
                dashSFX.mute = false;
                chargeVFX.Stop();
            }
        }else if (spaceTimeCounter > 0) // Charge esnasında gemiyi yavaşlat
        {
            //Debug.Log("Charging");

            camHolder.transform.localEulerAngles = new Vector3(0, 0, 0);
            force = forceInput / 5;
            chargeVFX.Play();

        }else if(isForcePowerFinished)
        {
            force = forceInput;
            chargeVFX.Stop();
        }


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
    }


    private void FixedUpdate()
    {
        // Kameranın baktığı yöne doğru f uygula
        if(WInput)
        {
            shipRb.AddForce(transform.forward * force);

            mainRocketVFX.SetActive(true);
            engineSFX.volume = Mathf.Lerp(engineSFX.volume, 0.2f, 0.2f);

        }else
        {
            mainRocketVFX.SetActive(false);
            engineSFX.volume = Mathf.Lerp(engineSFX.volume, 0f, 0.2f);
        }

        // Geminin son hızını belirle
        if(isForcePowerFinished)
        {
            shipRb.velocity = Vector3.ClampMagnitude(shipRb.velocity, maxSpeed);
        }
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
        //Debug.Log("Side Move");

        canSideMove = false;
        isSideMoveFinished = false;

        float sideMoveValue = 0;
        // Hız vektörüne göre dönme kuvveti hesapla
        Vector3 turnDirection = Vector3.Cross(Vector3.up, shipRb.velocity.normalized);

        // Sağa dönme kuvvetini uygula
        shipRb.AddForce(-direction * sideTurnForce * turnDirection, ForceMode.Impulse);

        while(sideMoveValue < 360f)
        {
            sideMoveValue = Mathf.Lerp(sideMoveValue, 365, sideTurnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, direction * sideMoveValue);

            yield return null;
        }

        isSideMoveFinished = true;
        cooldownSlider.value = sideMoveCooldown;

        yield return new WaitForSeconds(sideMoveCooldown);
        canSideMove = true;
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("a");
        if(other.gameObject.TryGetComponent(out IInteractable interactable))
        {
            if(other.gameObject.CompareTag("Dont"))
            {
                interactable.OnInteract();
            }
        }

    }
}
