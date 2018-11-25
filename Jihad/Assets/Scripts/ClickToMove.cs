using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace CompleteProject
{

    public class ClickToMove : MonoBehaviour
    {
        public GameObject destinationPoint;
        public MapGrid grid;

        private PlayerController playerCtrl;

        private float nextFire;
        
        // Use this for initialization
        void Awake()
        {
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
        }
    }
}