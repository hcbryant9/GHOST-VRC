
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class magicShrine_Warp : UdonSharpBehaviour
{
    [SerializeField] private GameObject targetAnimator;
    private Animator _animator;


    public FT_TeleportPortal ftTeleportPortal;
    private bool isTeleporting;

    void Start()
    {
        _animator = targetAnimator.GetComponent<Animator>();
        
    }
    

    public override void Interact()
    {
        if (!Networking.IsOwner(Networking.LocalPlayer, this.gameObject))
            Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(OpeningAnimation));
    }

   
     private void FixedUpdate()
     {
         isTeleporting = ftTeleportPortal.isTeleporting;
         if (isTeleporting == true)
        {
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(ClosingAnimation));
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, nameof(ResetAnimationDelaySync));
        }
     }

     public void OpeningAnimation()
    {
        _animator.SetBool("Open", true);
        
    }
    
    public void ClosingAnimation()
    {
        _animator.SetBool("Close", true);
        //Debug.Log("テレポートスタート");
    }

    public void ResetAnimation()
    {
        _animator.SetBool("Open", false);
        _animator.SetBool("Close", false);
    }

    public void ResetAnimationDelaySync()
    {
        SendCustomEventDelayedSeconds(nameof(ResetAnimation), 6f);
    }
    

}
