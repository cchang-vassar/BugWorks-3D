using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperScript : MonoBehaviour
{
    private BumperColliderScript colliderScriptF;
    private BumperColliderScript colliderScriptL;
    private BumperColliderScript colliderScriptR;
    private string deletethislater;
    public bool collided;

    void Start()
    {
        colliderScriptF = this.transform.Find("BumperMesh").transform.Find("BumperMeshFront").GetComponentInChildren<BumperColliderScript>();
        colliderScriptL = this.transform.Find("BumperMesh").transform.Find("BumperMeshLeft").GetComponentInChildren<BumperColliderScript>();
        colliderScriptR = this.transform.Find("BumperMesh").transform.Find("BumperMeshRight").GetComponentInChildren<BumperColliderScript>();
        collided = false;
    }

    void FixedUpdate()
    {
        collided = colliderScriptF.collided || colliderScriptL.collided || colliderScriptR.collided;
    }
}
