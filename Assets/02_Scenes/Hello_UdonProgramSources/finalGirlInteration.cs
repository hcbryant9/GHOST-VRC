
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class finalGirlInteration : UdonSharpBehaviour
{
    //player data
    private VRCPlayerApi localPlayer;
    public GameManager manager;
    public GhostScript script;

    //star spawning object
    public StarsSpawn spawner;

    private bool canAdvanceText = false;
    private int scriptCounter = 0;
    private int length = 4;
    private int length2 = 5;
    private bool finishedTalking = false;
    private bool rising = false;
    //sun
    public Sun sun;
    //particles
    public ParticleColorChanger particleColorChanger;

    private string[] scriptArrEngFinal = new string[]
    {
        "I am the soul of a young girl trapped in an eternal night, terrified of the devouring silence and darkness.",
        " I desperately need your help. ",
        "There are special objects around my grave that once brought light and comfort to my life, and if you can find them, they will illuminate my existence once again.",
        "In return, I promise to share with you precious memories of my family and the stories of darkness and light that have shaped my ethereal journey.",
        "Give me the light to find peace around my grave."
    };
    private string[] scriptArrKorFinal = new string[]
    {
       "나는 영원한 밤에 갇힌 영혼이에요. 삼키는 침묵과 어둠이 두렵습니다.",
       "당신의 도움이 절실히 필요해요.",
       "제 무덤 주변에는 한때 제 삶에 빛과 다정함을 주었던 특별한 물건들이 있습니다. ",
       "그 물건들을 찾아주신다면, 제 존재를 다시 한 번 밝혀줄 수 있을 거예요. 그 대가로 제 가족에 대한 소중한 추억, 그리고 저의 미묘한 여정을 형성한 어둠과 빛에 대한 이야기를 당신에게 들려드릴게요. ",
       "약속해요!무덤 주변에서 평안을 찾아줄 빛을 저에게 가져다 주세요 "
    };



    private string[] scriptArrEngFinal2 = new string[]
    {
        "It's been a long time coming, I've wanted these things for so long, and you've helped me get them. Thank you.",
        "My family was so harmonious, every day was fun, my mom and dad were having so much fun...",
        "then one day, all of a sudden, soldiers came into our house and said we had to evacuate. I was so young, I don't even remember.",
        "In return, I promise to share with you precious memories of my family and the stories of darkness and light that have shaped my ethereal journey.",
        "We had to evacuate, and eventually my mom and dad got scattered, and I never found them again. I didn't know where they were.",
        "Well, the sun is rising over there, I guess it's time for us to part ways. It was great meeting you, and I hope you'll come back again!"
    };
    private string[] scriptArrKorFinal2 = new string[]
    {
       "드디어 밝아졌네요. 이 밝음을 맞이하기까지 너무 오랜 시간이 지났어요. 정말 오랫동안 이 물건들을 원했는데, 당신이 도와줬군요. 감사해요.",
       "제 가족은 정말 화목했어요. 매일매일이 즐거웠죠. 엄마와도 아빠와도 정말 재밌게 지냈어요.",
       "그러던 어느 날 갑자기 군인들이 집에 들어왔어요. 대피해야 한대요. 저는 그때 너무 어려서 기억도 잘 안 나요. ",
       "대피를 하다가 결국 엄마도, 아빠도 다 흩어졌어요. 그리고 다시는 못 찾았어요",
       "엄마 아빠도 어디에선가 잘 지내고 있겠죠? 그래도 제 물건들을 되찾았으니 조금은 편안히 잘 수 있을 것 같아요.",
       "저기 해가 뜨고 있으니 이제 헤어져야 할 시간이네요. 만나서 반가웠고, 다시 오셨으면 좋겠어요!"
    };


    void Start()
    {
        localPlayer = Networking.LocalPlayer;
    }
    private void OnPlayerTriggerEnter(VRC.SDKBase.VRCPlayerApi player)

    {
        if (!finishedTalking)
        {
            if (script != null)
            {

                //if english is true -> english if false -> korean
                if (manager != null)
                {
                    if (manager.isEnglish)
                    {
                        script.SendScript(scriptArrEngFinal[0]);
                    }
                    else
                    {
                        script.SendScript(scriptArrKorFinal[0]);
                    }
                }
                scriptCounter++;
                canAdvanceText = true;
                if (particleColorChanger != null)
                {
                    particleColorChanger.ChangeParticleColorPresent(6);
                }
            }
            else
            {
                Debug.Log("the script for lover is null");
            }
        } else if (rising)
        //second part of speech
        {
            if (script != null)
            {

                //if english is true -> english if false -> korean
                if (manager != null)
                {
                    if (manager.isEnglish)
                    {
                        script.SendScript(scriptArrEngFinal2[0]);
                    }
                    else
                    {
                        script.SendScript(scriptArrKorFinal2[0]);
                    }
                }
                scriptCounter++;
                canAdvanceText = true;
                if (particleColorChanger != null)
                {
                    particleColorChanger.ChangeParticleColorPresent(6);
                }
            }
            else
            {
                Debug.Log("the script for lover is null");
            }
        }
        {

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
                        script.SendScript(scriptArrEngFinal[scriptCounter]);
                    }
                    else
                    {
                        script.SendScript(scriptArrKorFinal[scriptCounter]);
                    }
                }
                scriptCounter++;
                if (scriptCounter > length)
                { 
                    canAdvanceText = false;
                    spawner.SpawnObject();
                    
                    finishedTalking = true;
                }
            }
        }
        if (finishedTalking)
        {
            if(manager.startCount > 2)
            {
                particleColorChanger.ChangeParticleColorPresent(7);
                rising = true;
                sun.PlayDirectorAndShowRenderer();
                
            }
        }
    }
    
}
