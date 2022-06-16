using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null) _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _i;
        }
    }

    public Sprite s_Dagger;
    public Sprite s_Katana;
    public Sprite s_GreatSword;
    public Sprite s_Tomahawk;
    public Sprite s_Zweihander;
}
