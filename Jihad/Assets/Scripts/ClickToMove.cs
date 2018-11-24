using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace CompleteProject
{

    public class ClickToMove : MonoBehaviour
    {

        public float shootDistance = 10f;
        public float shootRate = .5f;
        public PlayerShooting shootingScript;
       
        public GameObject destinationPoint;
        public MapGrid grid;
        
        private PlayerController playerCtrl;
        //private NavMeshAgent navMeshAgent;
        private Transform targetedEnemy;
        private Ray shootRay;
        private RaycastHit shootHit;
        private bool enemyClicked;
        private float nextFire;
        public Camera cam;

        // Use this for initialization
        void Awake()
        {
            cam = GetComponent<Camera>();
            playerCtrl = GetComponent<PlayerController>();
            //navMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Fire2"))
            {
                Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                destinationPoint.transform.position = mouseWorldPos;
                playerCtrl.InitPathfinding();
            }

            
            if (Input.GetButtonDown("Fire1"))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    destinationPoint.transform.position = hit.point;
                    if (hit.collider.CompareTag("Wall"))
                    {
                        targetedEnemy = hit.transform;
                        enemyClicked = true;
                    }

                    else
                    {
                        enemyClicked = false;

                        destinationPoint.transform.position = hit.point;

                        //navMeshAgent.destination = hit.point;
                        //navMeshAgent.isStopped = false;
                    }
                }
            }

            if (enemyClicked)
            {
                //MoveAndShoot();
            }

            //if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            //{
            //    if (!navMeshAgent.hasPath || Mathf.Abs(navMeshAgent.velocity.sqrMagnitude) < float.Epsilon)
            //        walking = false;
            //}
        }

        //private void MoveAndShoot()
        //{
        //    if (targetedEnemy == null)
        //        return;
        //    navMeshAgent.destination = targetedEnemy.position;
        //    if (navMeshAgent.remainingDistance >= shootDistance)
        //    {

        //        navMeshAgent.isStopped = false;
        //        walking = true;
        //    }

        //    if (navMeshAgent.remainingDistance <= shootDistance)
        //    {

        //        transform.LookAt(targetedEnemy);
        //        Vector3 dirToShoot = targetedEnemy.transform.position - transform.position;
        //        if (Time.time > nextFire)
        //        {
        //            nextFire = Time.time + shootRate;
        //            shootingScript.Shoot(dirToShoot);
        //        }
        //        navMeshAgent.isStopped = true;
        //        walking = false;
        //    }
        //}

    }

}