using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TitleAnimController : MonoBehaviour
{

    public CanvasGroup menuGroup;
    public Animator[] listAnim;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("startButtonsAnim", 1.0f);
    }

    public void startButtonsAnim() {
        foreach (Animator a in listAnim)
        {
            a.SetBool("StartAnimation", true);
        }
        Invoke("ActivateButtons", 1.0f);
    }

    public void ActivateButtons() {
        menuGroup.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
