
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components;

public class GrandmaInteraction : UdonSharpBehaviour
{
    public GameManager manager;
    public GhostScript script;
    public int scriptSize = 10;
    private bool canAdvanceText = false;
    private VRCPlayerApi localPlayer;
    private int scriptCounter = 0;
    private bool playerIsInArea;
    private string[] scriptArrEng = new string[]
    {
        "Well, Hello Again",
        "Don't worry, dearie; you're young. You'll learn in time that I don't live with my body.",
        "You can visit me wherever you please. It's just here where there's nothing to distract you from me",
        "I live in your head, along with everyone else you've met, alive and dead. You just don't have a body to project me onto any longer",
        "I'm very much still me. I just have a little less ... autonomy than I once did",
        "Enough of my blabbering; thank you for humoring me, darling. Let's go around and meet some of the other ghosts here.",
        "No one I'm aware of.",
        "No one truly knows anyone, same for oneself.",
        "The ghosts you see are evoked by their reminders here. They'll be just as real as they ever were, they just, like myself, have a little less autonomy",
        "Ah, there I go being cryptic agai; it's the only thing the dead know how to be.",
        "There's much to learn; let's go!"
    };
    private string[] scriptArrKor = new string[]
    {
        "안녕, 또 만나요.",
        "걱정하지 마, 얘야. 넌 아직 어리잖니. 시간이 지나면 내가 내 몸으로 살지 않는다는 걸 알게 될 거야.",
        "언제든 날 보러 오렴. 나한테서 널 떼어놓을 수 있는 건 아무것도 없는 여기뿐이야",
        "난 네 머릿속에 네가 만났던 다른 모든 사람들과 함께 살아있어. 단지 더 이상 나를 투영할 몸이 없을 뿐이지",
        "나는 여전히 나다. 다만 예전보다 자율성이 조금 떨어졌을 뿐이에요",
        "내 횡설수설은 그만하고 유머러스하게 대해줘서 고마워요, 여보. 이제 돌아가서 다른 유령들을 만나러 가자.",
        "내가 아는 사람은 없어",
        "자기 자신을 제외하고는 누구도 진정으로 아는 사람은 없어.",
        "네가 보는 유령들은 여기서 그들의 기억을 떠올리게 하는 거야. 그들도 나처럼 자율성이 조금 떨어질 뿐이지 예전과 똑같이 실재할 거예요",
        "아, 내가 또 비밀스럽게 굴었구나. 죽은 자만이 아는 유일한 방법이지",
        "배울 게 많으니 가자!"
    };
    private void Start()
    {
        localPlayer = Networking.LocalPlayer;

    }
    private void OnPlayerTriggerEnter(VRC.SDKBase.VRCPlayerApi player)
    {
        if (script != null)
        {
            playerIsInArea = true;
            //if english is true -> english if false -> korean
            if (manager != null) { 
            if (manager.isEnglish)
            {
                script.SendScript(scriptArrEng[0]);
            }
            else
            {
                script.SendScript(scriptArrKor[0]);
            }
        }
            scriptCounter++;
            canAdvanceText = true;
        } else
        {
            Debug.Log("the script for gma is null");
        }
        
    }
    private void Update()
    {
        if ((canAdvanceText && localPlayer != null) && playerIsInArea)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (manager != null)
                {
                    if (manager.isEnglish)
                    {
                        script.SendScript(scriptArrEng[scriptCounter]);
                    }
                    else
                    {
                        script.SendScript(scriptArrKor[scriptCounter]);
                    }
                }
                scriptCounter++;
                if(scriptCounter > scriptSize)
                {
                    canAdvanceText = false;
                }
            }
        }
    }
    private void OnPlayerTriggerExit(VRC.SDKBase.VRCPlayerApi player)
    {
        playerIsInArea = false;
        scriptCounter = 0;
        script.ClearText();
    }
}
