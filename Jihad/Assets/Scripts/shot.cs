

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerShooting : MonoBehaviour
//{
//    public LineRenderer gunLine;

//    private bool enemyClicked;
//    private Transform targetedEnemy;

//    // Use this for initialization
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//        if (Input.GetButtonDown("Fire1"))
//        {
//            Ray mseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

//            Plane playerPlane = new Plane(Vector3.back, transform.position);
//            float d;
//            if (playerPlane.Raycast(mseRay, out d))
//            {
//                Vector3 hitPt = mseRay.GetPoint(d);

//                Debug.DrawRay(transform.position, hitPt - transform.position, Color.red, 1f);
//                RaycastHit2D hit = Physics2D.Raycast(transform.position, hitPt - transform.position);
//                if (hit)
//                {
//                    gunLine.SetPosition(0, transform.position);
//                    gunLine.SetPosition(1, hitPt);

//                    if (hit.collider.CompareTag("Enemy"))
//                    {
//                        targetedEnemy = hit.transform;
//                        Debug.Log("Enemy");
//                        enemyClicked = true;
//                    }

//                    else
//                    {
//                        enemyClicked = false;
//                    }

//                    if (hit.collider.CompareTag("Wall"))
//                    {
//                        Debug.Log("Wall");
//                    }
//                }
//            }
//        }


//        //if (enemyClicked)
//        //{
//        //    targetedEnemy.
//        //}

//    }
//}
