
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class GwarosaInteraction : UdonSharpBehaviour
{
    //player data
    private VRCPlayerApi localPlayer;
    public GameManager manager;
    public GhostScript script;

    public ParticleColorChanger particleColorChanger;

    private bool canAdvanceText = false;
    private int scriptCounter = 0;
    private int length = 6;

    private string[] scriptArrEngGwarosa = new string[]
    {
        "In the heart of Seoul, a midst the neon lights and endless hustle, I was just another face in the crowd, chasing dreams that seemed always just out of reach.",
        "Every morning, as the city awoke, I was already at my desk, drowning in a sea of deadlines and expectations.",
        "The weight of my family's hopes, society's standards, and my own ambitions pressed down on me, turning days into nights and nights into blurry dawns.",
        "Friends often joked that I was married to my job, but behind the laughter, there was a haunting truth",
        "The relentless pursuit of success, the 'ppali ppali' (hurry, hurry) culture, consumed me.",
        " I yearned for a moment of standstill, or a breath of fresh air, but the city's pace allowed for neither. ",
        "And so, in the prime of my youth, I found myself lost, a weary soul in a city that never sleeps."



    };
    private string[] scriptArrKorGwarosa = new string[]
    {
      "서울 한복판의 네온사인과 끝없는 번잡함 속에서 저는 늘 닿을 듯 닿지 않을 것 같은 꿈을 쫓는 군중 속의 한 얼굴에 불과했습니다.",
      "매일 아침 도시가 깨어나면, 저는 이미 책상에 앉아 마감과 기대의 바다에 허우적댔어요. ",
      "가족의 희망, 사회의 기준, 제 자신의 야망의 무게가 저를 짓누르며 낮이 밤으로, 밤이 흐릿한 새벽으로 바뀌었습니다.",
      "친구들은 종종 제가 일과 결혼했다는 농담을 했지만, 웃음 뒤에는 가슴 아픈 진실이 있기 마련이죠.",
      "성공에 대한 끊임없는 추구, '빨리빨리' 문화가 저를 집어삼켰습니다. ",
      "저는 잠깐의 휴식과 신선한 공기를 마시고 싶었지만, 도시의 속도에 쫓기다 보니 그럴 여유가 없었습니다.",
      "그래서 한창 젊었을 때 저는 잠들지 않는 도시에서 지친 영혼이 되어 길을 잃은 자신을 발견했습니다."
    };
    void Start()
    {
        localPlayer = Networking.LocalPlayer;
    }
    private void OnPlayerTriggerEnter(VRC.SDKBase.VRCPlayerApi player)

    {
        Debug.Log("Player has entered the interaction zone for gwarosa");
        if (script != null)
        {

            //if english is true -> english if false -> korean
            if (manager != null)
            {
                if (manager.isEnglish)
                {
                    script.SendScript(scriptArrEngGwarosa[0]);
                }
                else
                {
                    script.SendScript(scriptArrKorGwarosa[0]);
                }
            }
            scriptCounter++;
            canAdvanceText = true;
            if(particleColorChanger!= null)
            {
                particleColorChanger.ChangeParticleColorPresent(4);
            }
        }
        else
        {
            Debug.Log("the script for gwarosa is null");
        }
    }
    private void OnPlayerTriggerExit(VRC.SDKBase.VRCPlayerApi player)
    {
        script.ClearText();
        scriptCounter = 0;
        canAdvanceText = false;
    }
    private void Update()
    {
        if (canAdvanceText)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (manager != null)
                {
                    if (manager.isEnglish)
                    {
                        script.SendScript(scriptArrEngGwarosa[scriptCounter]);
                    }
                    else
                    {
                        script.SendScript(scriptArrKorGwarosa[scriptCounter]);
                    }
                }
                scriptCounter++;
                if (scriptCounter > length)
                {
                    canAdvanceText = false;
                    
                }
            }
        }
    }
}
