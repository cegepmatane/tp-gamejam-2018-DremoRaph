using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 1;
    public float distanceCheck = 0.05f;

    private GameObject player;
    private Vector3 mLastPosition;
    private bool AIActive = true;
    private bool destinationReached = false;
    private Path m_path;
    private PathfinderEnemy m_pathfinder;
    private Animator anim;
    private MapGrid grid;

    private int nextTileID = 1;

    private void Awake()
    {
        m_pathfinder = GetComponentInChildren<PathfinderEnemy>();
        anim = GetComponentInChildren<Animator>();
        grid = FindObjectOfType<MapGrid>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Use this for initialization
    void Start()
    {
        FollowPlayerIfClose();
    }

    void FollowPlayerIfClose()
    {
        if ((player.transform.position - transform.position).magnitude <= 10)
            InitPathfinding();
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

    //public void FindNewTarget()
    //{
    //    float distance = (this.transform.position - player.transform.position).magnitude;
    //    //if (distance <= 6f)
    //    m_pathfinder.endTransform.position = player.transform.position;
    //    //else
    //    //    m_pathfinder.endTransform.position = GetNearbyWalkableTile(transform.position);

    //    InitPathfinding();
    //}

    //public Vector3 GetNearbyWalkableTile(Vector3 position)
    //{
    //    bool targetAcquired = false;
    //    Vector3 point = new Vector2();
    //    while (!targetAcquired)
    //    {
    //        point = UnityEngine.Random.insideUnitSphere * 5;
    //        point += position;

    //        MapGrid.GridPoint proutbanane = grid.WorldPointToGridPoint(point);
    //        if (grid.tableauTiles[proutbanane.x, proutbanane.y].baseCost >= 0)
    //            targetAcquired = true;
    //    }

    //    return point;
    //}

    public void InitPathfinding()
    {
        m_path = m_pathfinder.GetPath(player.transform.position);
        AIActive = true;
        destinationReached = false;
        nextTileID = 1;
    }

    public void MovePathfinding()
    {
        if (m_path == null) return;
        //if (m_path.tiles.Count == 0) return;

        Vector2 t_direction = (m_path.tiles[nextTileID].transform.position - this.transform.position);

        transform.Translate(t_direction.normalized * speed * Time.deltaTime);
        float c2 = t_direction.sqrMagnitude;

        if (c2 < Math.Pow(distanceCheck, 2))
            if (++nextTileID == m_path.tiles.Count)
            {
                AIActive = false;
                destinationReached = true;
                FollowPlayerIfClose();
            }
        //if (t_direction != Vector2.zero)
        //{
        //    float angle = Mathf.Atan2(t_direction.y, t_direction.x) * Mathf.Rad2Deg;
        //    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //}
    }
}
