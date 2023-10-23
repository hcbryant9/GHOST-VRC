
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

    private bool canAdvanceText = false;
    private int scriptCounter = 0;
    private int length = 4;

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
    void Start()
    {
        localPlayer = Networking.LocalPlayer;
    }
    private void OnPlayerTriggerEnter(VRC.SDKBase.VRCPlayerApi player)
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
        }
        else
        {
            Debug.Log("the script for lover is null");
        }
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

                }
            }
        }
    }
}
