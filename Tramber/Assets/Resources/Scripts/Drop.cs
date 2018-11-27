﻿using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour {

    public enum DROP_TYPE{
        WATER_DROP,
        SUNSHINE_DROP,
        O2_DROP,
    };


    public DROP_TYPE type;

    float health = 100.0f;
    float maxHealth = 100.0f;
    float timeToAbsorb = 3.0f;

    public event Action<Drop> OnAbsorbed;

    VacuumSensor sensor;
    public VacuumSensor Sensor
    {
        get { return sensor; }
        set { sensor = value; }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.localScale = (health / maxHealth) * Vector3.one;
	}

    public void TouchVacuum()
    {
        health -= Time.deltaTime * maxHealth / timeToAbsorb;

        if(health / maxHealth < LevelManager.Instance.absorbThreshouldRatio)
        {
            var seq = DOTween.Sequence();
            seq.Append(DOTween.To(()=> health, x=> health = x, 0, LevelManager.Instance.absorbThreshouldTime));
            seq.AppendCallback(() =>
            {
                OnAbsorbed.Invoke(this);
            });
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger Enter Drop");
    }
}
