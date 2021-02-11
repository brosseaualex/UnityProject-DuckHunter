using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public abstract class BaseWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private int nbBullets = 0;
    [SerializeField] private float bulletSpread = 0f;
    [field: SerializeField] public float RateOfFire { get; set; } = 0f;
    public float TimeBeforeNextShot { get; set; } = 0f;
    [field: SerializeField] public float GunRange { get; set; } = 50f;
    [field: SerializeField] public float BulletTrailSize { get; set; } = 0.1f;
    public GameObject GunTip { get; set; }
    [field: SerializeField] public LineRenderer BulletTrailPrefab { get; set; }
    [field: SerializeField] public ParticleSystem MuzzleFlashParticles { get; set; }
    [field: SerializeField] public ParticleSystem CartridgeEjectionParticles { get; set; }
    [field: SerializeField] public AudioSource AudioSource { get; set; }
    [field: SerializeField] public XRSocketInteractor XRSocketInteractor { get; set; }
    [field: SerializeField] public SphereCollider AmmoReloadCollider { get; set; }
    [field: SerializeField] public int CurrentAmmo { get; set; } = 0;
    [field: SerializeField] public int MaxAmmo { get; set; } = 10;
    [field: SerializeField] public bool HasClip { get; set; } = false;
    [field: SerializeField] public LayerMask GunHitLayers { get; set; }

    void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        XRSocketInteractor = GetComponent<XRSocketInteractor>();
        //meshCollider = GetComponent<MeshCollider>();
        //Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Debug.Log(xRSocketInteractor.name);
        //Debug.Log(xRSocketInteractor.selectTarget);

        /*if (xRSocketInteractor.selectTarget == null)
        {
            xRSocketInteractor.socketActive = true;
        }*/

        /*if (xRSocketInteractor.selectTarget != null)
        {
            //Destroy(xRSocketInteractor.selectTarget.gameObject);
            Rigidbody rb = xRSocketInteractor.selectTarget.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;

            BoxCollider bc = xRSocketInteractor.selectTarget.gameObject.GetComponent<BoxCollider>();
            bc.isTrigger = true;

            xRSocketInteractor.selectTarget.gameObject.transform.SetParent(this.transform);

            xRSocketInteractor.selectTarget.gameObject.SetActive(false);


        }*/
    }

    public virtual void Shoot()
    {
        if (Time.time >= TimeBeforeNextShot)
        {
            if (CurrentAmmo > 0)
            {
                AudioSource.Play();
                MuzzleFlashParticles.Play();
                CartridgeEjectionParticles.Play();

                for (int i = 0; i < nbBullets; i++)
                {
                    Vector3 bulletDirection = GunTip.transform.TransformDirection(Vector3.forward);
                    Vector3 spread = Random.insideUnitCircle * bulletSpread;
                    bulletDirection += spread.x * GunTip.transform.right;
                    bulletDirection += spread.y * GunTip.transform.up;

                    Ray ray = new Ray(GunTip.transform.position, bulletDirection);
                    RaycastHit raycastHit;
                    Vector3 lineRendererEnd;

                    if (Physics.Raycast(ray, out raycastHit, GunRange, GunHitLayers.value))
                    {
                        lineRendererEnd = raycastHit.point;

                        if (XRInputDebugger.Instance.inputDebugEnabled)
                        {
                            string debugMessage = "Raycast on: " + raycastHit.transform.name;
                            Debug.Log(debugMessage);
                            XRInputDebugger.Instance.DebugLogInGame(debugMessage);
                        }
                    }
                    else
                    {
                        lineRendererEnd = ray.origin + ray.direction * GunRange;
                    }

                    LineRenderer bulletTrailClone = Instantiate(BulletTrailPrefab);
                    bulletTrailClone.widthMultiplier = BulletTrailSize;
                    bulletTrailClone.SetPositions(new Vector3[] { GunTip.transform.position, lineRendererEnd });

                    StartCoroutine(LineRendererFade.Instance.FadeLineRenderer(bulletTrailClone));
                }

                CurrentAmmo--;

                TimeBeforeNextShot = Time.time + RateOfFire;
            }
        }
    }

    public virtual void DropClip()
    {
        XRSocketInteractor.socketActive = false;
        /*meshCollider.enabled = false;
        Rigidbody.isKinematic = true;

        Rigidbody rb = xRSocketInteractor.selectTarget.gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;

        BoxCollider bc = xRSocketInteractor.selectTarget.gameObject.GetComponent<BoxCollider>();
        bc.isTrigger = false;

        xRSocketInteractor.selectTarget.gameObject.SetActive(true);*/


        /*if (XRInputDebugger.Instance.inputDebugEnabled)
        {
            string debugMessage = name + " Reload";
            Debug.Log(debugMessage);
            XRInputDebugger.Instance.DebugLogInGame(debugMessage);
        }*/
    }

    public void InsertAmmo()
    {


    }
}
