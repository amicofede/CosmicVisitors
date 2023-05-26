using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    #region UnityMessages
    private void Awake()
    {
        chargingOrb = GetComponentInChildren<ParticleSystem>();
        chargingOrb.gameObject.SetActive(true);

        boxCollider2D = GetComponentInChildren<BoxCollider2D>();
        spriteRendererBeam = boxCollider2D.gameObject.GetComponent<SpriteRenderer>();
        spriteRendererBeam.color = Color.red;
        beam = boxCollider2D.gameObject;
        beam.transform.position = Vector3.zero;
        //spriteRendererBeam.size = 0;
        beam.SetActive(false);

        Debug.Log(spriteRendererBeam.gameObject.name);

        circleCollider2D = GetComponentInChildren<CircleCollider2D>();
        spriteRendererSphere = circleCollider2D.gameObject.GetComponent<SpriteRenderer>();
        spriteRendererSphere.color = Color.yellow;
        sphere = circleCollider2D.gameObject;
        sphere.transform.localScale = Vector3.zero;
        sphere.SetActive(true);

        Debug.Log(spriteRendererSphere.gameObject.name);

        isCharging = true;
    }

    private void Update()
    {
        if (isCharging)
        {
            sphere.transform.localScale += new Vector3(0.0005f, 0.0005f, 0.0005f);
            if (sphere.transform.localScale.magnitude >= (2 * Vector3.one.magnitude))
            {
                isCharging = false;
                beam.SetActive(true);
            }
        }
        else
        {

        }
    }
    #endregion



}
