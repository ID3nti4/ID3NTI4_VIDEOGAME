using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

using UnityEngine;
using UnityEngine.UI;

using DialogueTree;

public class NPCDialogue : SignalEmitterSource {
    #region Singleton
    public static NPCDialogue instance;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }
    #endregion

    private Dialogue dia;
    
    private GameObject dialogueWindow;
    public bool dialogoInit = false;
    public GameObject nodeText;
    public GameObject option1, option2, option3;/*, option4, next*/
    private string nodeNum;
    private int selectedOption = -2;
    private int tiempo = 1;
    //public string DialogueDataFilePath;
    public TextAsset dialogueXMLFile;
    public int thoughts = 19;
    public GameObject DialogueWindowPrefab;
    public GameObject canvas;
    public Transform diaWinParent;

    InventoryUI inventoryUI;
    bool hasFoodCan;
    public bool chooseTime;
    List<Button> choiceButtons = new List<Button>();

    bool skipLine = false;
    public bool canSkipLines;

    SignalInfo info;


    void Start() {
        info = new SignalInfo();
        info.intValue = -1;
        // Here goes the path where the dialogue.xml is
        dia = loadDialogue(dialogueXMLFile.text);
        dialogueWindow = Instantiate<GameObject>(DialogueWindowPrefab, canvas.transform);

        inventoryUI = FindObjectOfType<InventoryUI>();

        RectTransform diaWindowTransform = (RectTransform)dialogueWindow.transform;

        nodeText = GameObject.Find("Conversation");
        option1 = GameObject.Find("RightButton");
        option2 = GameObject.Find("LeftButton");
        option3 = GameObject.Find("SupButton");

        dialogueWindow.SetActive(false);
    }

    void Update() {

        if (chooseTime)
        {
            if (Input.GetButton("Option1") && hasFoodCan)
            {
                choiceButtons[0].onClick.Invoke();
            } 
            else if (Input.GetButton("Option2"))
            {
                choiceButtons[choiceButtons.Count - 2].onClick.Invoke();
            } 
            else if (Input.GetButton("Option3"))
            {
                choiceButtons[choiceButtons.Count - 1].onClick.Invoke();
            }
        }
    }

    public void RunDialogue(int Inicial = 0) {
        dialogoInit = true;
        StartCoroutine(run(Inicial));
    }

    public void SetSelectedOption(int option, int uibutton = -1) {
        selectedOption = option;
        if (uibutton != -1)
        {
            info.intValue = uibutton;
        }
    }

    public void SetCanSkipLines(bool canSkipLines)
    {
        this.canSkipLines = canSkipLines;
    }

    public void SkipLine()
    {
        if (!canSkipLines) return;
        skipLine = true;
        Debug.Log("ActiveSkip");
    }

    IEnumerator CancellableWaitForSeconds(float Delay)
    {
        float elapsed = 0.0f;
        while(elapsed < Delay)
        {
            yield return new WaitForEndOfFrame();
            elapsed += Time.deltaTime;
            if(skipLine)
            {
                skipLine = false;
                elapsed = Delay;
                Debug.Log("Skip!");
            }
        }
    }

    public IEnumerator run(int Inicial) {
        dialogueWindow.SetActive(true);

        // Create an Indexer, set it to 0 - the dialogues start node
        int nodeID = Inicial;

        // While the next node isn't an exit node, traverse the dialogue tree based on the user input
        while (nodeID != -1) {
            displayNode(dia.Nodes[nodeID]);

            new WaitForSeconds(0.5f);

            selectedOption = -2;
            while (selectedOption == -2) {
                yield return new WaitForSeconds(0.25f);
            }

            nodeID = selectedOption;
        }

        dialogueWindow.SetActive(false);
        
        //EmitSignalToNext?.Invoke(info);
    }

    private void displayNode(DialogueNode node) {
        //Debug.Log("Node ID: " + node.NodeID + "  Node Options: " + node.Options.Count + "  Node Text: " + node.Text);

        nodeText.GetComponentInChildren<Text>().text = node.Name + ": " + node.Text.Substring(0, node.Text.Length - 1);
        nodeNum = node.Text.Substring(node.Text.Length - 1);
        option1.SetActive(false);
        option2.SetActive(false);
        option3.SetActive(false);
        //option4.SetActive(false);
        //next.SetActive(false);
        int r = 1;
        try {
            r = int.Parse(nodeNum);
        }
        catch {

        }
        tiempo = r;
        Debug.Log(tiempo);
        for (int i = 0; i < node.Options.Count; i++) {
            if (node.Options[i].Text == "Continue") {
                StartCoroutine(setContinueOption(node.Options[i]));
            }

            if (node.Options[i].Text == "Exit") {
                StartCoroutine(setExitOption(node.Options[i]));
            }

            if (node.Options[i].Text != "Continue" && node.Options[i].Text != "Exit") {
                //GameManager.instance.SetCanOpenMenu(false);
                switch (i)
                {
                    case 0: // right
                        // quick and dirty hack to check for the food can; this is the only npc dialogue in the VS
                        /*foreach (InventorySlot itemSlot in inventoryUI.slotsPool)
                        {
                            if (itemSlot.itemName.text == "Food Can")
                            {
                                hasFoodCan = true;
                                FindObjectOfType<MenusManager>().SetObjectCollected(itemSlot.gameObject);
                            }
                        }*/
                        if (hasFoodCan) setOptionButton(option1, node.Options[i], 0);
                        break;
                    case 1: // left
                        setOptionButton(option2, node.Options[i], 1);
                        break;
                    case 2: // top
                        setOptionButton(option3, node.Options[i], 2);
                        break;
                    /*case 3:
                        setOptionButton(option4, node.Options[i]);
                        break;*/                        
                }
                if(i == node.Options.Count - 1)
                {
                    chooseTime = true;
                    Debug.Log("Time to choose: " + chooseTime);
                }
            }
        }
    }

    private void setOptionButton(GameObject button, DialogueOption opt, int uibutton) {
        button.SetActive(true);
        button.GetComponentInChildren<Text>().text = opt.Text;
        if (button == option1)
        {
            button.GetComponent<Button>().onClick.AddListener(delegate
            {
                dialogueWindow.SetActive(false);
                //inventoryUI.CanOfferFood = true;
                //MenusManager.instance.needToGive = true;
                //MenusManager.instance.OnCloseAction = delegate
                {
                    dialogueWindow.SetActive(true);
                    SetSelectedOption(opt.DestinationNodeID, uibutton);
                };
                //GameManager.instance.InventoryPause();
                ChooseOption();
            });
            choiceButtons.Add(button.GetComponent<Button>());
        }
        else
        {
            button.GetComponent<Button>().onClick.AddListener(delegate
            {
                SetSelectedOption(opt.DestinationNodeID, uibutton);
                ChooseOption();
            });
            choiceButtons.Add(button.GetComponent<Button>());
        }
    }

    private IEnumerator setContinueOption(DialogueOption opt) {
        yield return StartCoroutine(CancellableWaitForSeconds(tiempo));
        SetSelectedOption(opt.DestinationNodeID);
    }

    private IEnumerator setExitOption(DialogueOption opt) {
        yield return StartCoroutine(CancellableWaitForSeconds(tiempo));
        SetSelectedOption(opt.DestinationNodeID);
        dialogoInit = false;
    }

    private static Dialogue loadDialogue(string contents) {
        XmlSerializer serz = new XmlSerializer(typeof(Dialogue));
        MemoryStream memStr = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(contents));
        StreamReader reader = new StreamReader(memStr);
   

        Dialogue dia = (Dialogue)serz.Deserialize(reader);

        return dia;
    }

    public void ChooseOption()
    {
        chooseTime = false;
        //GameManager.instance.SetCanOpenMenu(true);
    }
}
