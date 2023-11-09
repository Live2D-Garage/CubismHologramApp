using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenSettingMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject MenuButton;
    public bool function = false;
    private bool DisplayingButton = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        if (function == false)
        {
            MenuButton.SetActive(true);
        }
        else
        {
            MenuButton.SetActive(false);
        }
        menu.SetActive(function);
    }

    public void DispMenuButton()
    {
        if (DisplayingButton == false)
        {
            MenuButton.GetComponent<Animator>().SetBool("Disp", true);
            MenuButton.GetComponent<Button>().enabled = true;
            DisplayingButton = true;
            Invoke("CloseButton", 3);
        }
    }
    private void CloseButton()
    {
        DisplayingButton = false;
        MenuButton.GetComponent<Animator>().SetBool("Disp", false);
        MenuButton.GetComponent<Button>().enabled = false;
    }
}
