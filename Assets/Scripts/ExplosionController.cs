
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    private bool _explosionInvoked = false;
    private string _explosionInvokeTag = "explosionStart";
    public Animator animator;

    public void StartExplosionAnimation()
    {
        if (!_explosionInvoked)
        {
            _explosionInvoked = true;
            Debug.Log("Explosion was started");
            animator.SetBool(_explosionInvokeTag, _explosionInvoked);
        }
    }
}
