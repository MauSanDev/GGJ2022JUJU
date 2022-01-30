using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{
    [Range(0, 10)] [SerializeField] private float scale;
    [SerializeField] private float sparkSpinSpeed, ringSpinSpeed, scaleSpeed;
    [SerializeField] private int sparkAmount;
    [SerializeField] private ParticleSystem sparkFX;
    [SerializeField] private TrailRenderer trail;

    private float trailStartTimeLenght;

    [SerializeField] private Transform sparkSpinControl, ringSpingControl;

    private List<ParticleSystem> sparkFXS = new List<ParticleSystem>();
    void Start()
    {
        trailStartTimeLenght = trail.time;
        CreateSparks();
    }

    private void CreateSparks()
    {
        for (int i = 0; i < sparkAmount; i++)
        {
            float radians = 2 * Mathf.PI / sparkAmount * i;
            float vertical = Mathf.Sin(radians);
            float horizontal = Mathf.Cos(radians);
            
            Vector3 spawnDir = new Vector3(horizontal,vertical, 0);
            Vector3 spawnPos = transform.position + spawnDir;

            ParticleSystem spark = Instantiate(sparkFX, sparkSpinControl);
            spark.transform.position = spawnPos;

            Vector3 relativePos = spark.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.forward);

            spark.transform.rotation = rotation;
            
            sparkFXS.Add(spark);
        }
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(scale, scale, scale), scaleSpeed * Time.deltaTime);
        sparkSpinControl.Rotate(transform.forward, sparkSpinSpeed * Time.deltaTime);
        ringSpingControl.Rotate(transform.forward, ringSpinSpeed * Time.deltaTime);
    }

    public void EnableParticles(bool enable)
    {
        foreach (ParticleSystem spark in sparkFXS)
        {
            if (enable)
            {
                spark.Play();
            }
            else
            {
                spark.Stop();
            }
        }

        if (enable)
        {
            trail.time = trailStartTimeLenght;
        }
        else
        {
            trail.time = 0;
        }
    }
}
