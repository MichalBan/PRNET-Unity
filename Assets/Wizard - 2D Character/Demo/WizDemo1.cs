using UnityEngine;

namespace Assets.Wizard___2D_Character.Demo
{
    public class WizDemo1 : MonoBehaviour
    {

        Animator _anim;
        // Start is called before the first frame update
        void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        void ResetAnimation()
        {
            _anim.SetBool("isLookUp", false);
            _anim.SetBool("isRun", false);
            _anim.SetBool("isJump", false);
        }
        public void Idle()
        {
            ResetAnimation();
            _anim.SetTrigger("idle");
        }
        public void Attack()
        {
            ResetAnimation();
            _anim.SetTrigger("attack");
        }
        public void TripOver()
        {
            ResetAnimation();
            _anim.SetTrigger("tripOver");
        }
        public void Hurt()
        {
            ResetAnimation();
            _anim.SetTrigger("hurt");
        }
        public void Die()
        {
            ResetAnimation();
            _anim.SetTrigger("die");
        }
        public void LookUp()
        {
            ResetAnimation();
            _anim.SetBool("isLookUp", true);
        }
        public void Run()
        {
            ResetAnimation();
            _anim.SetBool("isRun", true);

        }
        public void Jump()
        {
            ResetAnimation();
            _anim.SetBool("isJump", true);

        }
    }
}
