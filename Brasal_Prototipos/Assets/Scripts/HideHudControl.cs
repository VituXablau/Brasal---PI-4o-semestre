using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    Image image;
    Animator anim;
    string currentScene;
    bool disabled;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "MataAtlanticaTutorial")
            disabled = true;

    }

    private bool MouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void Enable()
    {
        disabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!disabled)
        {
            if (MouseOverUI())
            {
                anim.SetBool("hideHud", true);
            }

            if (!MouseOverUI())
            {
                anim.SetBool("hideHud", false);
            }
        }

    }
}
