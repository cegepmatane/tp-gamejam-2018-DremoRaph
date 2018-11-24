using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float ptsVie;
    public float speed = 1;
    public float distanceCheck = 0.05f;

    private Vector3 mLastPosition;
    private bool AIActive = true;
    private bool destinationReached = false;
    private Path m_path;
    private Pathfinder m_pathfinder;
    private Animator anim;
   
    [Header("Sounds")]
    public AudioClip deathSound;

    private int nextTileID = 1;

	// Use this for initialization
	void Start () {
        m_pathfinder = GameObject.FindObjectOfType<Pathfinder>();
        anim = GetComponent<Animator>();
        InitPathfinding();
    }
	
	// Update is called once per frame
	void Update () {

        float speed = (transform.position - this.mLastPosition).magnitude / Time.deltaTime;
        this.mLastPosition = transform.position;

        anim.SetBool("isWalking", speed > 0.1f ? true: false);
        if (destinationReached) return;
        if (AIActive)
            MovePathfinding();
        else
            MoveRaycast();

    }

    public void InitPathfinding()
    {
        m_path = m_pathfinder.GetPath(transform);
        AIActive = true;
        destinationReached = false;
        nextTileID = 1;
    }

    public void MovePathfinding()
    {
        Vector3 t_direction = (m_path.tiles[nextTileID].transform.position - this.transform.position);

        //transform.right = t_direction;
        transform.Translate(t_direction.normalized * speed * Time.deltaTime);
        float c2 = t_direction.sqrMagnitude;


        if (c2 < Math.Pow(distanceCheck, 2))
            if (++nextTileID == m_path.tiles.Count)
            {
                AIActive = false;
                destinationReached = true;
            }
    }

    public void MoveRaycast()
    {

    }
}
