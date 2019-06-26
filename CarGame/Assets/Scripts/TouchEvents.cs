using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{

    public class TouchEvents : MonoBehaviour
    {
        public bool canDrift;
        [Space(10)]
        public Animator carAnim;
        public TrailRenderer skidLeft;
        public TrailRenderer skidRight;
        public MeshRenderer theRenderer;
        public MeshCollider theCollider;
        public float timer = 0.0f;
        public PlayerStats stats;

        public bool canPlay = false;



        // Start is called before the first frame update
        void Start()
        {
            GetComponent<MeshRenderer>().enabled = false;
            carAnim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            //While animation is playing dont allow player damage
            if (this.carAnim.GetCurrentAnimatorStateInfo(0).IsName("carSpinRight"))
            {
                GetComponentInParent<PlayerStats>().canTakeDamage = false;
            }
            else
            {
                if(stats.isBoosting == false)
                {
                    GetComponentInParent<PlayerStats>().canTakeDamage = true;
                }
            }

            //While animation is playing create skid marks
            if (this.carAnim.GetCurrentAnimatorStateInfo(0).IsName("carDriftRight") ||
                this.carAnim.GetCurrentAnimatorStateInfo(0).IsName("carDriftLeft") ||
                this.carAnim.GetCurrentAnimatorStateInfo(0).IsName("carSpinRight"))
            {
                skidLeft.emitting = true;
                skidRight.emitting = true;
            }
            else
            {
                skidLeft.emitting = false;
                skidRight.emitting = false;
            }
        }

        public void StartGame()
        {
            if (!canPlay)
            {
                canPlay = true;
                //timer = 0.0f;
                carAnim.Play("carStartForward");
                //StartCoroutine(Blink(3.0f));
                //StartCoroutine(EnableCollider(3.0f));
                theRenderer.enabled = true;
                theCollider.enabled = true;
                //theCollider.enabled = false;
            }
        }

        public void SpinRight()
        {
            if (canPlay == true)
            {
                
            }
        }


        public bool canReturnRight = false;
        public bool canReturnLeft = false;

        public void GoRight()
        {
            if(canPlay == true && canReturnLeft == false && canReturnRight == false)//only way to go right is if returnright is false
            {
                carAnim.Play("carTurnRight");
                canReturnLeft = true;//
            }
        }
        public void LeftReturn()
        {
            if (canPlay == true && canReturnLeft == true)
            {
                carAnim.Play("carReturnLeft");
                canReturnLeft = false;
            }
        }



        public void GoLeft()
        {
            if (canPlay == true && canReturnRight == false && canReturnLeft == false)//only way to go left is if returnleft is false
            {
                carAnim.Play("carTurnLeft");
                canReturnRight = true;//
            }
        }
        public void RightReturn()
        {
            if (canPlay == true && canReturnRight == true)
            {
                carAnim.Play("carReturnRight");
                canReturnRight = false;
            }
        }




        public void DriftRight()
        {
            if (canPlay == true && canDrift == true)
            {
                carAnim.SetTrigger("DriftRight");
            }
        }
        public void DriftLeft()
        {
            if (canPlay == true && canDrift == true)
            {
                carAnim.SetTrigger("DriftLeft");
            }
        }

        private IEnumerator EnableCollider(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            theCollider.enabled = true;
        }
        
        private IEnumerator Blink(float waitTime)
        {
            float endTime = timer + waitTime;
            while (timer < waitTime)
            {
                theRenderer.enabled = false;
                yield return new WaitForSeconds(0.15f);
                theRenderer.enabled = true;
                yield return new WaitForSeconds(0.15f);

            }
        }
    }

}
