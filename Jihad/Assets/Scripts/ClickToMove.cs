using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace CompleteProject
{

    public class ClickToMove : MonoBehaviour
    {

        public float shootDistance = 10f;
        public float shootRate = .5f;
        public GameObject destinationPoint;
        public MapGrid grid;
        public Camera cam;

        private PlayerController playerCtrl;
        private Transform targetedEnemy;
        private Ray shootRay;
        private RaycastHit shootHit;
        private bool enemyClicked;
        private float nextFire;
        
        // Use this for initialization
        void Awake()
        {
            cam = GetComponent<Camera>();
            playerCtrl = GetComponent<PlayerController>();
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
                Ray mseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                Plane playerPlane = new Plane(Vector3.back, transform.position);
                float d;
                if (playerPlane.Raycast(mseRay, out d))
                {
                    Vector3 hitPt = mseRay.GetPoint(d);

                    Debug.DrawRay(transform.position, hitPt - transform.position, Color.red, 1f);
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, hitPt - transform.position);
                    if ( hit )
                    {
                        if (hit.collider.CompareTag("Enemy"))
                        {
                            targetedEnemy = hit.transform;
                            Debug.Log("Enemy");
                            enemyClicked = true;
                        }

                        else
                        {
                            enemyClicked = false;

                            destinationPoint.transform.position = hit.point;
                        }

                        if (hit.collider.CompareTag("Wall"))
                        {
                            Debug.Log("Wall");
                        }
                    }
                }
            }

            //if (enemyClicked)
            //{
            //    targetedEnemy.
            //}

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