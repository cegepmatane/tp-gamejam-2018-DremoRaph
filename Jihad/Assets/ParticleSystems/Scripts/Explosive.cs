using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
    public class Explosive : MonoBehaviour
    {
        public Transform explosionPrefab;
        public float detonationImpactVelocity = 10;
        public float sizeMultiplier = 1;
        public bool reset = true;
        public float resetTimeDelay = 10;

        private IEnumerator OnCollisionEnter(Collision col)
        {
            if (enabled)
            {
                if (col.contacts.Length > 0)
                {
                    // compare relative velocity to collision normal - so we don't explode from a fast but gentle glancing collision
                    float velocityAlongCollisionNormal =
                        Vector3.Project(col.relativeVelocity, col.contacts[0].normal).magnitude;

                    if (velocityAlongCollisionNormal > detonationImpactVelocity)
                    {
                        Instantiate(explosionPrefab, col.contacts[0].point,
                                    Quaternion.LookRotation(col.contacts[0].normal));

                        Destroy(this.gameObject);
                    }
                }
            }

            yield return null;
        }


        public void Reset()
        {

        }
    }
}
