﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CamaraBehaviour : MonoBehaviour
{

    //Attributes

    private Vector3 offset;
    private GameObject prefab;
    public RaycastHit rayinfo;
    private Ray ray;
    private List<Collider> reset  = new List<Collider>();
    private bool setOffset = true;


    //Methods

    /**
     * Makes the camera follow the player.
     * */
    void FollowPlayer()
    {
        if (!setOffset)
            transform.position = ResourceManager.playerPosition + this.offset;
    }

    /**
 * Sets Opacity for a material.
 * */
    void SetAlpha(GameObject go, float alpha)
    {
        Material[] materialList = go.renderer.materials;
        for (int i = 0; i < materialList.Length; i++)
        {
            Color color = materialList[i].color;
            color.a = alpha;
            materialList[i].color = color;
        }
    }

    void CheckRay()
    {
        RaycastHit[] RaycastList = Physics.RaycastAll(transform.position, ResourceManager.playerPosition - transform.position);
        {
            foreach (RaycastHit i in RaycastList)
            {
                Collider other = i.collider;
                if (other.gameObject.tag == "Building")
                {
                    SetAlpha(other.gameObject, 0.5f);
                    reset.Add(other);
                }
                else if (other.gameObject.tag == "Building2")
                {
                    reset.Add(other);
                    SetAlpha(other.gameObject, 0.1f);
                }
                else if (other.gameObject.tag.Contains("Transparent"))
                {
                    SetAlpha(other.gameObject, 0.5f);
                    reset.Add(other);
                }
            }
            ResetFade(RaycastList);
        }
    }

    void ResetFade(RaycastHit[] currentList)
    {
        List<Collider> colliderList = new List<Collider>();
        foreach(RaycastHit i in currentList)
        {
            colliderList.Add(i.collider);
        }
        List<Collider> removeList = new List<Collider>();
            foreach (Collider i in reset)
            {
                if (!colliderList.Contains(i))
                {
                    SetAlpha(i.gameObject, 1);
                    removeList.Remove(i);
                }
            }
            
        foreach(Collider i in removeList)
        {
            reset.Remove(i);
        }
        
    }


    //Actions

    void Start()
    {
        ResourceManager.cam = transform;
    }

    void SetOffset()
    {
        if (setOffset)
        {
            this.offset = transform.position - ResourceManager.playerPosition;
            setOffset = false;
        }
    }


    void FixedUpdate()
    {
        CheckRay();
        FollowPlayer();
        SetOffset();
    }
}
