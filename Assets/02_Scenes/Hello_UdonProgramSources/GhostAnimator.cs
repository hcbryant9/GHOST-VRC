
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class GhostAnimator : UdonSharpBehaviour
{
    public GameManager manager;
    private Animator anim;
    public GhostScript script;
    private int scriptCounter = 0;
    private int length = 6;
    private bool canAdvanceText = false;
    private string[] scriptArrEngIntro = new string[]
    {
        "Honey?",

        "Oh, sorry",
        "I once walked the earth, just like you, with dreams and hopes that danced in my heart.",
        "Every evening, I'd stand by the old lighthouse, waiting for the ship that carried the man I loved.",
        "He promised he'd return, and I believed him.",

        "Seasons changed, years rolled by, and still, I waited.",
        "The townsfolk said he was lost at sea, but my heart refused to let go. ",
        "One fateful night, a storm took me, and I became one with the wind and waves. ",
        "Now, I linger here, a specter of eternal hope, still waiting for a ship that will never dock, and a love that will never return.",
        
    };
    private string[] scriptArrKorIntro = new string[]
    {
        //honey?
        "안녕, 또 만나요.",
        //oh, sorry
        "걱정하지 마, 얘야. 넌 아직 어리잖니. 시간이 지나면 내가 내 몸으로 살지 않는다는 걸 알게 될 거야.",

        "나도 한때 당신처럼 꿈과 희망을 가슴에 품고 지구를 걸었습니다.",

        "매일 저녁 낡은 등대 옆에 서서 사랑하는 남자를 태운 배를 기다리곤 했죠.",

        "계절이 바뀌고 세월이 흘렀지만 여전히 저는 기다렸습니다. ",

        "마을 사람들은 그가 바다에서 길을 잃었다고 말했지만 제 마음은 그를 놓아주지 않았어요.",

        "운명적인 어느 날 밤, 폭풍이 저를 덮쳤고 저는 바람과 파도와 하나가 되었습니다.",

        "이제 저는 영원한 희망의 유령이 되어 정박하지 못할 배와 돌아오지 않을 사랑을 기다리며 이곳에 머물러 있습니다.",
        
    };
    void Start()
    {
        anim = GetComponent<Animator>();
        StopAnimation();
    }

    public void PlayAnimation()
    {
        anim.enabled = true;

        //sending the script over
        if (script != null)
        {
            
            //if english is true -> english if false -> korean
            if (manager != null)
            {
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
        }
        else
        {
            Debug.Log("the script for lover is null");
        }
    }
    public void StopAnimation()
    {
        
        anim.enabled = false;
    }
    private void Update()
    {
        //might need to change this to be in the area too because its just checking clicks
        if (canAdvanceText)
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
                if (scriptCounter > length)
                {
                    //this variable says -> should grandma follow us?

                    manager.shouldFollow = true;

                    //since we've gone through the first script , we can say that the first interaction is complete

                    manager.loverInteraction = true;
                    canAdvanceText = false;

                }
            }
        }
    }
}
