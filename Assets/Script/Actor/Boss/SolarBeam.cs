using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LaserSO;

public class SolarBeam : MonoBehaviour
{
    private ParticleSystem chargingOrb;
    private GameObject sphere;
    private CircleCollider2D circleCollider2D;
    private SpriteRenderer spriteRendererSphere;
    private GameObject beam;
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRendererBeam;

    private bool isCharging;
    private bool beamShooted;
    private bool beamDespawn;

    private float colorTransition;

    //private bool isBeamFinished;
    //public bool IsBeamFinished { get { return isBeamFinished; } }

    #region UnityMessages
    private void Awake()
    {
        chargingOrb = GetComponentInChildren<ParticleSystem>();
        boxCollider2D = GetComponentInChildren<BoxCollider2D>();
        spriteRendererBeam = boxCollider2D.gameObject.GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponentInChildren<CircleCollider2D>();
        spriteRendererSphere = circleCollider2D.gameObject.GetComponent<SpriteRenderer>();

        beam = boxCollider2D.gameObject;
        sphere = circleCollider2D.gameObject;

        chargingOrb.gameObject.SetActive(true);
        beam.SetActive(false);
        sphere.SetActive(true);

        spriteRendererBeam.color = Color.red;
        spriteRendererSphere.color = Color.yellow;


        boxCollider2D.size = new Vector2(0f, 1f);
        spriteRendererBeam.size = new Vector2(0f, 1f);
        sphere.transform.localScale = Vector3.zero;

        colorTransition = 2f;

        isCharging = true;
        beamShooted = false;
        beamDespawn = false;
        //isBeamFinished = false;
    }

    private void OnEnable()
    {
        EventController.RestartGameUI += DeactivateBeam;
        EventController.ClearStage += DeactivateBeam;
        EventController.BossDeath += DeactivateBeam;


        gameObject.transform.localScale = Vector3.one;
        beam.transform.localPosition = Vector3.zero;
        chargingOrb.Play();
        chargingOrb.gameObject.SetActive(true);
        beam.SetActive(false);
        sphere.SetActive(true);

        spriteRendererBeam.color = Color.red;
        spriteRendererSphere.color = Color.yellow;

        beam = boxCollider2D.gameObject;
        sphere = circleCollider2D.gameObject;

        boxCollider2D.size = new Vector2(0f, 1f);
        spriteRendererBeam.size = new Vector2(0f, 1f);
        sphere.transform.localScale = Vector3.zero;

        isCharging = true;
        beamShooted = false;
        beamDespawn = false;
        //isBeamFinished = false;

        colorTransition = 2f;

        EventController.RaiseOnBossSolarBeamCharge();
    }

    private void OnDisable()
    {
        EventController.RestartGameUI -= DeactivateBeam;
        EventController.ClearStage -= DeactivateBeam;
        EventController.BossDeath -= DeactivateBeam;

    }
    private void Update()
    {
        if (GameManager.Instance.GameState == GameManager.State.Playing)
        {
            if (isCharging)
            {
                sphere.transform.localScale += new Vector3(1f, 1f, 1f) * Time.deltaTime;
                Color sphereColor = spriteRendererSphere.color;
                spriteRendererSphere.color = Color.Lerp(sphereColor, Color.red, Time.deltaTime / colorTransition);
                colorTransition -= Time.deltaTime;
                if (sphere.transform.localScale.magnitude >= (2 * Vector3.one.magnitude))
                {
                    isCharging = false;
                    spriteRendererSphere.color = Color.red;

                    chargingOrb.Stop();
                    StartCoroutine(ShootBeam());
                }
            }
            else
            {
                if (beamShooted && spriteRendererBeam.size.x < 7f)
                {
                    beam.transform.localPosition += new Vector3(100f, 0f, 0f) * Time.deltaTime;
                    spriteRendererBeam.size += new Vector2(100f, 0f) * Time.deltaTime;
                    boxCollider2D.size += new Vector2(100f, 0f) * Time.deltaTime;
                }
                if (beamDespawn)
                {
                    if (gameObject.transform.localScale.y > 0)
                    {
                        gameObject.transform.localScale -= new Vector3(0, 1f, 0) * Time.deltaTime;
                    }
                    else
                    {
                        //isBeamFinished = true;
                        Factory.Instance.deactiveSolarBeam(gameObject);
                    }
                }
            }
        }
    }
    #endregion
    
    private void DeactivateBeam()
    {
        Factory.Instance.deactiveSolarBeam(gameObject);
    }
    private IEnumerator ShootBeam()
    {
        beam.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        EventController.RaiseOnBossSolarBeamShoot();
        chargingOrb.gameObject.SetActive(false);
        beamShooted = true;
        yield return new WaitForSeconds(0.8f);
        beamDespawn = true;
    }
}
