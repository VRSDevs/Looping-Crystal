using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLs : MonoBehaviour
{
    public void goTo(int user) {
        switch (user)
        {
            case 0:
                Application.OpenURL("https://blinx24.itch.io/");
                break;
            case 1:
                Application.OpenURL("https://requenisima21.itch.io/");
                break;
            case 2:
                Application.OpenURL("https://nachete07.itch.io/");
                break;
            case 3:
                Application.OpenURL("https://mikimanx.itch.io/");
                break;
            case 4:
                Application.OpenURL("https://rox06io.itch.io/");
                break;
            case 5:
                Application.OpenURL("https://vrsdevs.itch.io/");
                break;
        }
    }
}
