using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float ptsVie;
    public float speed = 1;
    public float distanceCheck = 0.05f;
    
    private bool AIActive = true;
    private Path m_path;
   
    [Header("Sounds")]
    public AudioClip deathSound;

    private int nextTileID = 1;

	// Use this for initialization
	void Start () {
        m_path = GameObject.FindObjectOfType<Pathfinder>().GetPath(transform);
        transform.position = m_path.tiles[0].transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (!AIActive) return;

        Vector2 t_direction = (m_path.tiles[nextTileID].transform.position - this.transform.position);

        transform.Translate(t_direction.normalized * speed * Time.deltaTime);

        float c2 = t_direction.sqrMagnitude;


        if (c2 < Math.Pow(distanceCheck, 2))
            if (++nextTileID == m_path.tiles.Count)
                StartCoroutine(die());

    }

    IEnumerator die()
    {
        Debug.Log("Dead");
        AudioSource t_as = GetComponent<AudioSource>();
        t_as.PlayOneShot(deathSound);
        AIActive = false;
        GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(deathSound.length);

        Destroy(gameObject);
    }
}
