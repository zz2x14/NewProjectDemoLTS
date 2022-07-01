using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkTarget : MonoBehaviour
{
    public void TalkOver()
    {
        GetComponent<ITalk>().TalkOver();
    }

}
