using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClass : Character
{

    public void SetClassSoldier(int i)
    {
        Engine.e.playableCharacters[i].characterClass[0] = true;
        Engine.e.playableCharacters[i].currentClass = string.Empty;
        Engine.e.playableCharacters[i].currentClass = "Soldier";

    }
    public void SetClassShaman(int i)
    {
        Engine.e.playableCharacters[i].characterClass[1] = true;
        Engine.e.playableCharacters[i].currentClass = string.Empty;
        Engine.e.playableCharacters[i].currentClass = "Shaman";
    }
    public void SetClassThief(int i)
    {
        Engine.e.playableCharacters[i].characterClass[2] = true;
        Engine.e.playableCharacters[i].currentClass = string.Empty;
        Engine.e.playableCharacters[i].currentClass = "Thief";

    }
    public void SetClassMage(int i)
    {
        Engine.e.playableCharacters[i].characterClass[3] = true;
        Engine.e.playableCharacters[i].currentClass = string.Empty;
        Engine.e.playableCharacters[i].currentClass = "Mage";
    }
    public void SetClassAssassin(int i)
    {
        Engine.e.playableCharacters[i].characterClass[4] = true;
        Engine.e.playableCharacters[i].currentClass = string.Empty;
        Engine.e.playableCharacters[i].currentClass = "Assassin";
    }
    public void SetClassRonin(int i)
    {
        Engine.e.playableCharacters[i].characterClass[5] = true;
        Engine.e.playableCharacters[i].currentClass = string.Empty;
        Engine.e.playableCharacters[i].currentClass = "Ronin";
    }
    public void SetClassMonk(int i)
    {
        Engine.e.playableCharacters[i].characterClass[6] = true;
        Engine.e.playableCharacters[i].currentClass = string.Empty;
        Engine.e.playableCharacters[i].currentClass = "Monk";
    }
    public void SetClassWatcher(int i)
    {
        Engine.e.playableCharacters[i].characterClass[7] = true;
        Engine.e.playableCharacters[i].currentClass = string.Empty;
        Engine.e.playableCharacters[i].currentClass = "Watcher";
    }
    public void SetClassQuickpocket(int i)
    {
        Engine.e.playableCharacters[i].characterClass[8] = true;
        Engine.e.playableCharacters[i].currentClass = string.Empty;
        Engine.e.playableCharacters[i].currentClass = "Quickpocket";
    }
    public void SetClassEvoker(int i)
    {
        Engine.e.playableCharacters[i].characterClass[9] = true;
        Engine.e.playableCharacters[i].currentClass = string.Empty;
        Engine.e.playableCharacters[i].currentClass = "Evoker";
    }
    public void SetClassShinobi(int i)
    {
        Engine.e.playableCharacters[i].characterClass[10] = true;
        Engine.e.playableCharacters[i].currentClass = string.Empty;
        Engine.e.playableCharacters[i].currentClass = "Shinobi";
    }
    public void SetClassBushi(int i)
    {
        Engine.e.playableCharacters[i].characterClass[11] = true;
        Engine.e.playableCharacters[i].currentClass = string.Empty;
        Engine.e.playableCharacters[i].currentClass = "Bushi";
    }
}
