using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PjTexturesScribtable", menuName = "ScriptableObjects/PjTexturesScribtable", order = 1)]

public class PjTexturesScribtable : ScriptableObject
{
    public Sprite[] maleSpritesSkin1;
    public Sprite[] femaleSpritesSkin1;
    public Sprite[] maleSpritesSkin2;
    public Sprite[] femaleSpritesSkin2;
    public Sprite[] maleSpritesSkin3;
    public Sprite[] femaleSpritesSkin3;

    //public List<Sprite[]> maleSprites;
    //public List<Sprite[]> femaleSprites;

    public Sprite[] GetSpritesMaleEspecialidad(int index)
    {
        var maleSprites = new List<Sprite[]>();
        maleSprites.Add(maleSpritesSkin1);
        maleSprites.Add(maleSpritesSkin2);
        maleSprites.Add(maleSpritesSkin3);
        return maleSprites[index];
    }

    public Sprite[] GetSpritesFemaleEspecialidad(int index)
    {
        var femaleSprites = new List<Sprite[]>();
        femaleSprites.Add(femaleSpritesSkin1);
        femaleSprites.Add(femaleSpritesSkin2);
        femaleSprites.Add(femaleSpritesSkin3);
        return femaleSprites[index];
    }

}
