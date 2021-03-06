﻿using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {

    public GameObject bubble;
    public GameObject needDropFigure;

    DROP_TYPE currentNeedDrop;
    public DROP_TYPE CurrentNeedDrop
    {
        get { return currentNeedDrop; }
        set { currentNeedDrop = value; }
    }


    public Sprite[] dropSprites;

    int[] seq = { 0,1,0, 2, 1, 0,};
    int seqIndex = 0;

    public event Action<Flower> OnFeeded;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        bubble.transform.localScale = (health / maxHealth) * Vector3.one;
    }

    public void SetNeedType()
    {
        CurrentNeedDrop = (DROP_TYPE)seq[seqIndex];
        needDropFigure.GetComponent<SpriteRenderer>().sprite = dropSprites[seq[seqIndex]];
    }

    float health = 100.0f;
    float maxHealth = 100.0f;
    float timeToAbsorb = 3.0f;
    public void TouchVacuum()
    {
        health -= Time.deltaTime * maxHealth / timeToAbsorb;

        if (health / maxHealth < LevelManager.Instance.absorbThreshouldRatio)
        {
            var seq = DOTween.Sequence();
            OnFeeded.Invoke(this);
            seq.Append(DOTween.To(() => health, x => health = x, 0, LevelManager.Instance.absorbThreshouldTime));
            seq.AppendCallback(() =>
            {                
                health = maxHealth;
                seqIndex++;
                SetNeedType();
            });
        }
    }
}
