
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class trashGhostInteraction : UdonSharpBehaviour
{
    //player data
    private VRCPlayerApi localPlayer;
    public GameManager manager;
    public GhostScript script;

    //rotating variables
    public float turnSpeed = 10f;
    private Vector3 rotationAxis = Vector3.up;

    //script variables
    private bool canAdvanceText = false;
    private int scriptCounter = 0;
    private int length = 6;

    private string[] scriptArrEngTrash = new string[]
    {
        "Greetings, traveler.",
        "I am the spirit of a forsaken ancestor, languishing among the refuse that surrounds my resting place. ",
        "Once the land of my kin echoed with their laughter, but now they are all sold and forgotten.",
        "The weight of their absence fills me with anger and sorrow.",
        "If you have the heart, I implore you to help me clear away the debris that defiles my grave, and in doing so, perhaps I can ease the pain of their abandonment.",
        "Please clean my grave."
    };
    private string[] scriptArrKorTrash = new string[]
    {
      "안녕하세요. 여행자님.",
      "저는 버림받은 조상의 혼령으로, 제 안식처를 둘러싸고 있는 쓰레기들 속에서 시들어가고 있습니다.",
      "한때 선산 땅에는 친족들의 웃음소리가 울려 퍼졌지만, 지금은 그들이 모두 팔아 먹어버렸죠.",
      "그렇게 잊혀진 채 저 홀로 남았습니다.",
      "그 외면의 무게가 저를 분노와 슬픔으로 가득 채우고 있어요. 마음만 있다면 제 무덤을 더럽히는 잔해들을 치우는 데 도움을 주시길 간청합니다. ",
      "그러면 그들의 버림으로 인한 고통을 조금이나마 덜을 수 있을 것 같아요."
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
                    script.SendScript(scriptArrEngTrash[0]);
                }
                else
                {
                    script.SendScript(scriptArrKorTrash[0]);
                }
            }
            scriptCounter++;
            canAdvanceText = true;
        }
        else
        {
            Debug.Log("the script for trash is null");
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
                        script.SendScript(scriptArrEngTrash[scriptCounter]);
                    }
                    else
                    {
                        script.SendScript(scriptArrKorTrash[scriptCounter]);
                    }
                }
                scriptCounter++;
                if (scriptCounter > length)
                {
                    canAdvanceText = false;
                    Rotate();
                }
            }
        }
    }
    void Rotate()
    {
        transform.Rotate(rotationAxis, turnSpeed * Time.deltaTime);
    }
}
