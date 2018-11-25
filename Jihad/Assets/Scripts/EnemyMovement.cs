using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 1;
    public float distanceCheck = 0.05f;

    private Vector3 mLastPosition;
    private bool AIActive = true;
    private bool destinationReached = false;
    private Path m_path;
    private Pathfinder m_pathfinder;
    private Animator anim;
    public GameObject player;

    private int nextTileID = 1;

    // Use this for initialization
    void Start()
    {
        m_pathfinder = GetComponentInChildren<Pathfinder>();

        InitPathfinding();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = (transform.position - this.mLastPosition).magnitude / Time.deltaTime;
        anim.SetFloat("MovementX", (transform.position - this.mLastPosition).x);
        anim.SetFloat("MovementY", (transform.position - this.mLastPosition).y);
        this.mLastPosition = transform.position;
        
        if (destinationReached) return;
        if (AIActive)
            MovePathfinding();

    }

    public void FindNewTarget()
    {
        float distance = (this.transform.position - player.transform.position).magnitude;
        if (distance <= 6f)
            m_pathfinder.endTransform.position = player.transform.position;
        
    }

    public void InitPathfinding()
    {
        m_path = m_pathfinder.GetPath(transform);
        if (m_path == null) return;
        AIActive = true;
        destinationReached = false;
        nextTileID = 1;
    }

    public void MovePathfinding()
    {
        if (m_path == null) return;
        if (m_path.tiles.Count == 0) return;

        Vector2 t_direction = (m_path.tiles[nextTileID].transform.position - this.transform.position);

        //if (t_direction != Vector2.zero)
        //{
        //    float angle = Mathf.Atan2(t_direction.y, t_direction.x) * Mathf.Rad2Deg;
        //    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //}
        transform.Translate(t_direction.normalized * speed * Time.deltaTime);
        float c2 = t_direction.sqrMagnitude;

        if (c2 < Math.Pow(distanceCheck, 2))
            if (++nextTileID == m_path.tiles.Count)
            {
                AIActive = false;
                destinationReached = true;
                FindNewTarget();
            }
    }

}
