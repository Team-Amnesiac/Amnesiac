using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeperUI : MonoBehaviour
{


    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.Keeper, this);
        UIManager.Instance.hideUI(UIManager.UI.Keeper);
    }
}
