
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components;

public class GrandmaInteraction : UdonSharpBehaviour
{
    public GameManager manager;
    public GhostScript script;
    public int scriptSizeIntro = 10;
    public int scriptSizeGen = 7;
    private bool canAdvanceText = false;
    private VRCPlayerApi localPlayer;
    private int scriptCounter = 0;
    private bool playerIsInArea;
    private string[] scriptArrEngIntro = new string[]
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
    private string[] scriptArrKorIntro = new string[]
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
    
    //script when talking to general (english)
    private string[] scriptArrEngGen = new string[]
    {
        "Don't bother, he won't talk to you. He never looked his constinuents in the eye. He certainly won't now",
        "People have this idea that the seductive nature of power over others comes from the comfort it provides in life.",
        "That's certainly true, but very short term.",
        "Greed is a manifestation of the mind's carnal fight against death; to hoard as much for itself as possible while it still has control over its image",
        "The more control one has in life, the more control one has over their death.",
        "Anyone can spin heroic tales of their own significance in the history of things, but only the powerful can make people believe it.",
        "Look at him up there, all high and mighty, as if he's any less dead than the rest of us.",
        "Prick."
    };
    private string[] scriptArrKorGen = new string[]
    {
        "신경 쓰지 마세요. 그는 당신에게 말하지 않을 거예요. 그는 유권자들의 눈을 똑바로 쳐다보지 않았어요. 앞으로도 그럴 겁니다. ",
        "사람들은 타인을 지배하는 권력의 매혹적인 본질이 삶의 편안함에서 비롯된다는 생각을 가지고 있습니다.",
        "그것은 확실히 사실이지만 매우 단기적입니다. ",
        "탐욕은 죽음에 맞서 싸우는 마음의 육체적 싸움의 표현이며, 자신의 이미지를 통제할 수 있는 동안 가능한 한 많은 것을 쌓아두려는 것입니다.",
        "삶에서 더 많은 통제권을 가질수록 죽음에 대해서도 더 많은 통제권을 갖게 됩니다.",
        "누구나 사물의 역사에서 자신의 중요성에 대한 영웅적인 이야기를 만들 수 있지만, 권력자만이 사람들이 그것을 믿게 만들 수 있습니다",
        "저 위에 있는 그를 보세요. 마치 그가 우리보다 덜 죽은 것처럼요.",
        "개자식."
    };




    private void Start()
    {
        localPlayer = Networking.LocalPlayer;

    }
    private void OnPlayerTriggerEnter(VRC.SDKBase.VRCPlayerApi player)
    {
        //for first interaction
        if (script != null && (!(manager.firstInteraction)) )
        {
            playerIsInArea = true;
            //if english is true -> english if false -> korean
            if (manager != null) { 
            if (manager.isEnglish)
            {
                script.SendScript(scriptArrEngIntro[0]);
            }
            else
            {
                script.SendScript(scriptArrKorIntro[0]);
            }
        }
            scriptCounter++;
            canAdvanceText = true;
        } else
        {
            Debug.Log("the script for gma is null");
        }

        //for general interaction
        if (manager.generalInteraction && manager.firstInteraction) {
            if (script != null)
            {
                playerIsInArea = true;
                //if english is true -> english if false -> korean
                if (manager != null)
                {
                    if (manager.isEnglish)
                    {
                        script.SendScript(scriptArrEngGen[0]);
                    }
                    else
                    {
                        script.SendScript(scriptArrKorGen[0]);
                    }
                }
                scriptCounter++;
                canAdvanceText = true;
            }
            else
            {
                Debug.Log("the script for gma is null");
            }
        }
    }
    private void Update()
    {
        //first interaction true -> interaction is complete
        if (!(manager.firstInteraction))
        {
            if ((canAdvanceText && localPlayer != null) && playerIsInArea)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (manager != null)
                    {
                        if (manager.isEnglish)
                        {
                            script.SendScript(scriptArrEngIntro[scriptCounter]);
                        }
                        else
                        {
                            script.SendScript(scriptArrKorIntro[scriptCounter]);
                        }
                    }
                    scriptCounter++;
                    if (scriptCounter > scriptSizeIntro)
                    {
                        //this variable says -> should grandma follow us?

                        manager.shouldFollow = true;

                        //since we've gone through the first script , we can say that the first interaction is complete
                       
                        manager.firstInteraction = true;
                        canAdvanceText = false;
                        
                    }
                }
            }
        }

        if ( manager.generalInteraction && manager.firstInteraction)
        {
            if ((canAdvanceText && localPlayer != null) && playerIsInArea)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (manager != null)
                    {
                        if (manager.isEnglish)
                        {
                            script.SendScript(scriptArrEngGen[scriptCounter]);
                        }
                        else
                        {
                            script.SendScript(scriptArrKorGen[scriptCounter]);
                        }
                    }
                    scriptCounter++;
                    if (scriptCounter > scriptSizeGen)
                    {
                        //this variable says -> should grandma follow us?

                        manager.shouldFollow = true;

                        //we've talked to the general now
                        manager.generalInteraction = false;
                        canAdvanceText = false;
                    }
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
