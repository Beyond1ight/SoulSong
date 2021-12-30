using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DropHelpText : MonoBehaviour
{
    public TextMeshProUGUI helpReference;
    public TextMeshProUGUI abilitiesMenuHelpReference;
    public bool fireDrop = false;
    public bool iceDrop = false;
    public bool lightningDrop = false;
    public bool waterDrop = false;
    public bool shadowDrop = false;
    public bool holyDrop = false;
    public int partyIndex;

    /* public void DisplayHelp(int dropIndex)
     {
         int index = 0;

         Drops fireDropIndex = Engine.e.fireDrops[dropIndex];
         Drops iceDropIndex = Engine.e.iceDrops[dropIndex];
         Drops lightningDropIndex = Engine.e.lightningDrops[dropIndex];
         Drops waterDropIndex = Engine.e.waterDrops[dropIndex];
         Drops shadowDropIndex = Engine.e.shadowDrops[dropIndex];
         Drops holyDropIndex = Engine.e.holyDrops[dropIndex];


         // In Battle

         if (Engine.e.inBattle)
         {
             if (Engine.e.battleSystem.state == BattleState.CHAR1TURN)
             {
                 index = 0;
             }
             if (Engine.e.battleSystem.state == BattleState.CHAR2TURN)
             {
                 index = 1;
             }
             if (Engine.e.battleSystem.state == BattleState.CHAR3TURN)
             {
                 index = 2;
             }

             Character character = Engine.e.activeParty.activeParty[index].GetComponent<Character>();

             if (fireDrop)
             {
                 if (character.fireDrops[dropIndex] != null)
                 {
                     float fireDropCost = Mathf.Round(fireDropIndex.dropCost - (fireDropIndex.dropCost * character.dropCostReduction / 100) + 0.45f);

                     switch (dropIndex)
                     {
                         case 0:

                             float fireBlastDamage = Mathf.Round(fireDropIndex.dropPower + (fireDropIndex.dropPower * character.fireDropsLevel / 2)
                             + character.fireDropAttackBonus);
                             helpReference.text = "Deals " + fireBlastDamage + " Fire damage. Costs " + fireDropCost + " MP " + "(Current MP: " + character.currentMana + ").";
                             break;
                         case 1:

                             float infernoDamage = Mathf.Round(fireDropIndex.dropPower + (fireDropIndex.dropPower * character.fireDropsLevel / 2)
                             + character.fireDropAttackBonus);
                             helpReference.text = "Deals " + infernoDamage + " Fire damage. Costs " + fireDropCost + " MP " + "(Current MP: " + character.currentMana + ").";
                             break;
                     }
                 }
                 else
                 {
                     helpReference.text = string.Empty;
                 }
             }


             if (iceDrop)
             {
                 if (character.iceDrops[dropIndex] != null)
                 {
                     float iceDropCost = Mathf.Round(iceDropIndex.dropCost - (iceDropIndex.dropCost * character.dropCostReduction / 100) + 0.45f);

                     switch (dropIndex)
                     {
                         case 0:
                             float iceDamage = Mathf.Round(iceDropIndex.dropPower + (iceDropIndex.dropPower * character.iceDropsLevel / 2)
                             + character.iceDropAttackBonus);
                             helpReference.text = "Deals " + iceDamage + " Ice damage. Costs " + iceDropCost + " MP " + "(Current MP: " + character.currentMana + ").";
                             break;
                     }
                 }
                 else
                 {
                     helpReference.text = string.Empty;

                 }
             }

             if (lightningDrop)
             {
                 if (character.lightningDrops[dropIndex] != null)
                 {
                     float lightningDropCost = Mathf.Round(lightningDropIndex.dropCost - (lightningDropIndex.dropCost * character.dropCostReduction / 100) + 0.45f);

                     switch (dropIndex)
                     {
                         case 0:
                             float lightningDamage = Mathf.Round(lightningDropIndex.dropPower + (lightningDropIndex.dropPower * character.lightningDropsLevel / 2)
                             + character.lightningDropAttackBonus);
                             helpReference.text = "Deals " + lightningDamage + " Lightning damage. Costs " + lightningDropCost + " MP " + "(Current MP: " + character.currentMana + ").";
                             break;
                     }
                 }
                 else
                 {
                     helpReference.text = string.Empty;

                 }
             }

             if (waterDrop)
             {
                 if (character.waterDrops[dropIndex] != null)
                 {
                     float waterDropCost = Mathf.Round(waterDropIndex.dropCost - (waterDropIndex.dropCost * character.dropCostReduction / 100) + 0.45f);

                     switch (dropIndex)
                     {
                         case 0:

                             float waterDamage = Mathf.Round(waterDropIndex.dropPower + (waterDropIndex.dropPower * character.waterDropsLevel / 2)
                             + character.waterDropAttackBonus);
                             helpReference.text = "Deals " + waterDamage + " Water damage. Costs " + waterDropCost + " MP " + "(Current MP: " + character.currentMana + ").";
                             break;
                     }
                 }
                 else
                 {
                     helpReference.text = string.Empty;

                 }
             }

             if (shadowDrop)
             {
                 if (character.shadowDrops[dropIndex] != null)
                 {
                     float shadowDropCost = Mathf.Round(shadowDropIndex.dropCost - (shadowDropIndex.dropCost * character.dropCostReduction / 100) + 0.45f);

                     switch (dropIndex)
                     {
                         case 0:

                             float shadowDamage = Mathf.Round(shadowDropIndex.dropPower + (shadowDropIndex.dropPower * character.shadowDropsLevel / 2)
                             + character.shadowDropAttackBonus);
                             helpReference.text = "Deals " + shadowDamage + " Shadow damage. Costs " + shadowDropCost + " MP " + "(Current MP: " + character.currentMana + ").";
                             break;
                         case 1:
                             float poisonDamage = shadowDropIndex.dotDmg + (character.shadowDropsLevel * 6 / 2);
                             helpReference.text = "Inflicts Poison, causing " + poisonDamage + " Shadow damage per turn. Costs " + shadowDropIndex.dropCost + " MP " + "(Current MP: " + character.currentMana + ").";

                             break;
                         case 2:

                             helpReference.text = "Inflicts Sleep. Costs " + shadowDropCost + " MP " + "(Current MP: " + character.currentMana + ").";
                             break;
                         case 3:

                             helpReference.text = "Inflicts Confuse. Costs " + shadowDropCost + " MP " + "(Current MP: " + character.currentMana + ").";
                             break;
                         case 4:

                             helpReference.text = "Inflicts Death. Costs " + shadowDropCost + " MP " + "(Current MP: " + character.currentMana + ").";
                             break;
                     }
                 }
                 else
                 {
                     helpReference.text = string.Empty;

                 }
             }

             if (holyDrop)
             {
                 if (character.holyDrops[dropIndex] != null)
                 {
                     float holyDropCost = Mathf.Round(holyDropIndex.dropCost - (holyDropIndex.dropCost * character.dropCostReduction / 100) + 0.45f);

                     switch (dropIndex)
                     {
                         case 0:

                             float healPower = Mathf.Round(holyDropIndex.dropPower + (holyDropIndex.dropPower * character.holyDropsLevel / 2));
                             helpReference.text = "Heals an ally for " + healPower + " Holy power. Costs " + holyDropCost + " MP " + "(Current MP: " + character.currentMana + ").";
                             break;
                         case 1:

                             float revivePower = Mathf.Round(holyDropIndex.dropPower + (holyDropIndex.dropPower * character.holyDropsLevel / 2));
                             helpReference.text = "Revives a fallen ally, restoring " + revivePower + " HP. Costs " + holyDropCost + " MP " + "(Current MP: " + character.currentMana + ").";
                             break;
                         case 2:
                             if (character.holyDropsLevel >= 20)
                             {
                                 helpReference.text = "Removes all negative status effects. Costs " + holyDropCost + " MP " + "(Current MP: " + character.currentMana + ").";
                             }
                             else
                             {
                                 if (character.holyDropsLevel >= 10 && character.holyDropsLevel < 20)
                                 {
                                     helpReference.text = "Removes Poison, Sleep. Costs " + holyDropCost + " MP " + "(Current MP: " + character.currentMana + ").";
                                 }
                                 else
                                 {
                                     if (character.holyDropsLevel < 10)
                                     {
                                         helpReference.text = "Removes Sleep. Costs " + holyDropCost + " MP " + "(Current MP: " + character.currentMana + ").";
                                     }
                                 }
                             }
                             break;
                         case 3:
                             if (character.holyDropsLevel >= 20)
                             {
                                 helpReference.text = "Removes all positive status effects from an enemy. Costs " + holyDropCost + " MP " + "(Current MP: " + character.currentMana + ").";
                             }
                             else
                             {
                                 if (character.holyDropsLevel >= 10 && character.holyDropsLevel < 20)
                                 {
                                     helpReference.text = "Removes two positive status effects from an enemy. Costs " + holyDropCost + " MP " + "(Current MP: " + character.currentMana + ").";
                                 }
                                 else
                                 {
                                     if (character.holyDropsLevel < 10)
                                     {
                                         helpReference.text = "Removes a positive status effect from an enemy. Costs " + holyDropCost + " MP " + "(Current MP: " + character.currentMana + ").";
                                     }
                                 }
                             }
                             break;
                     }
                 }
                 else
                 {
                     helpReference.text = string.Empty;

                 }
             }
         }

         else
         {

             // Ability Screen
             if (Engine.e.abilityScreenReference.GetComponent<AbilitiesDisplay>().grieveScreen)
             {
                 partyIndex = 0;
             }
             if (Engine.e.abilityScreenReference.GetComponent<AbilitiesDisplay>().macScreen)
             {
                 partyIndex = 1;
             }
             if (Engine.e.abilityScreenReference.GetComponent<AbilitiesDisplay>().fieldScreen)
             {
                 partyIndex = 2;
             }
             if (Engine.e.abilityScreenReference.GetComponent<AbilitiesDisplay>().riggsScreen)
             {
                 partyIndex = 3;
             }

             Character character = Engine.e.party[partyIndex].GetComponent<Character>();

             if (fireDrop)
             {
                 if (fireDropIndex == null)
                 {
                     abilitiesMenuHelpReference.text = string.Empty;
                 }
                 else
                 {
                     if (!fireDropIndex.isKnown)
                     {
                         {
                             abilitiesMenuHelpReference.text = string.Empty;
                         }
                     }
                     else
                     {
                         float fireDropCost = Mathf.Round(fireDropIndex.dropCost - (fireDropIndex.dropCost * character.dropCostReduction / 100) + 0.45f);

                         switch (dropIndex)
                         {

                             case 0:

                                 float fireBlastDamage = Mathf.Round(fireDropIndex.dropPower + (fireDropIndex.dropPower * character.fireDropsLevel / 2)
                                 + character.fireDropAttackBonus);
                                 abilitiesMenuHelpReference.text = "Deals " + fireBlastDamage + " Fire damage. Costs " + fireDropCost + " MP.";
                                 break;
                             case 1:

                                 float infernoDamage = Mathf.Round(fireDropIndex.dropPower + (fireDropIndex.dropPower * character.fireDropsLevel / 2)
                                 + character.fireDropAttackBonus);
                                 abilitiesMenuHelpReference.text = "Deals " + infernoDamage + " Fire damage. Costs " + fireDropCost + " MP.";
                                 break;
                         }
                     }
                 }
             }

             if (iceDrop)
             {
                 if (iceDropIndex == null)
                 {
                     abilitiesMenuHelpReference.text = string.Empty;
                 }
                 else
                 {
                     if (!iceDropIndex.isKnown)
                     {
                         {
                             abilitiesMenuHelpReference.text = string.Empty;
                         }
                     }
                     else
                     {
                         float iceDropCost = Mathf.Round(iceDropIndex.dropCost - (iceDropIndex.dropCost * character.dropCostReduction / 100) + 0.45f);

                         switch (dropIndex)
                         {
                             case 0:
                                 float iceDamage = Mathf.Round(iceDropIndex.dropPower + (iceDropIndex.dropPower * character.iceDropsLevel / 2)
                                 + character.iceDropAttackBonus);
                                 abilitiesMenuHelpReference.text = "Deals " + iceDamage + " Ice damage. Costs " + iceDropCost + " MP.";
                                 break;
                         }
                     }
                 }
             }

             if (lightningDrop)
             {
                 if (lightningDropIndex == null)
                 {
                     abilitiesMenuHelpReference.text = string.Empty;
                 }
                 else
                 {
                     if (!lightningDropIndex.isKnown)
                     {
                         {
                             abilitiesMenuHelpReference.text = string.Empty;
                         }
                     }
                     else
                     {
                         float lightningDropCost = Mathf.Round(lightningDropIndex.dropCost - (lightningDropIndex.dropCost * character.dropCostReduction / 100) + 0.45f);

                         switch (dropIndex)
                         {
                             case 0:
                                 float lightningDamage = Mathf.Round(lightningDropIndex.dropPower + (lightningDropIndex.dropPower * character.lightningDropsLevel / 2)
                                 + character.lightningDropAttackBonus);
                                 abilitiesMenuHelpReference.text = "Deals " + lightningDamage + " Lightning damage. Costs " + lightningDropCost + " MP.";
                                 break;


                         }
                     }
                 }
             }

             if (waterDrop)
             {
                 if (waterDropIndex == null)
                 {
                     helpReference.text = string.Empty;
                 }
                 else
                 {
                     if (!waterDropIndex.isKnown)
                     {
                         {
                             abilitiesMenuHelpReference.text = string.Empty;
                         }
                     }
                     else
                     {
                         float waterDropCost = Mathf.Round(waterDropIndex.dropCost - (waterDropIndex.dropCost * character.dropCostReduction / 100) + 0.45f);

                         switch (dropIndex)
                         {
                             case 0:

                                 float waterDamage = Mathf.Round(waterDropIndex.dropPower + (waterDropIndex.dropPower * character.waterDropsLevel / 2)
                                 + character.waterDropAttackBonus);
                                 abilitiesMenuHelpReference.text = "Deals " + waterDamage + " Water damage. Costs " + waterDropCost + " MP.";
                                 break;
                         }
                     }
                 }
             }

             if (shadowDrop)
             {
                 if (shadowDropIndex == null)
                 {
                     abilitiesMenuHelpReference.text = string.Empty;
                 }
                 else
                 {
                     if (!shadowDropIndex.isKnown)
                     {
                         {
                             abilitiesMenuHelpReference.text = string.Empty;
                         }
                     }
                     else
                     {
                         float shadowDropCost = Mathf.Round(shadowDropIndex.dropCost - (shadowDropIndex.dropCost * character.dropCostReduction / 100) + 0.45f);

                         switch (dropIndex)
                         {
                             case 0:

                                 float shadowDamage = Mathf.Round(shadowDropIndex.dropPower + (shadowDropIndex.dropPower * character.shadowDropsLevel / 2)
                                 + character.shadowDropAttackBonus);
                                 abilitiesMenuHelpReference.text = "Deals " + shadowDamage + " Shadow damage. Costs " + shadowDropCost + " MP.";
                                 break;
                             case 1:
                                 float poisonDamage = shadowDropIndex.dotDmg + (character.shadowDropsLevel * 6 / 2);
                                 abilitiesMenuHelpReference.text = "Inflicts Poison, causing " + poisonDamage + " Shadow damage per turn. Costs " + shadowDropCost + " MP";

                                 break;
                             case 2:

                                 abilitiesMenuHelpReference.text = "Inflicts Sleep. Costs " + shadowDropCost + " MP.";
                                 break;
                             case 3:

                                 abilitiesMenuHelpReference.text = "Inflicts Confuse. Costs " + shadowDropCost + " MP.";
                                 break;
                             case 4:

                                 abilitiesMenuHelpReference.text = "Inflicts Death. Costs " + shadowDropCost + " MP.";
                                 break;
                         }
                     }
                 }
             }

             if (holyDrop)
             {
                 if (holyDropIndex == null)
                 {

                     abilitiesMenuHelpReference.text = string.Empty;
                 }
                 else
                 {
                     if (!holyDropIndex.isKnown)
                     {
                         {
                             abilitiesMenuHelpReference.text = string.Empty;
                         }
                     }
                     else
                     {
                         float holyDropCost = Mathf.Round(holyDropIndex.dropCost - (holyDropIndex.dropCost * character.dropCostReduction / 100) + 0.45f);

                         switch (dropIndex)
                         {
                             case 0:

                                 float healPower = Mathf.Round(holyDropIndex.dropPower + (holyDropIndex.dropPower * character.holyDropsLevel / 2));
                                 abilitiesMenuHelpReference.text = "Heals an ally for " + healPower + " Holy power. Costs " + holyDropCost + " MP.";

                                 break;
                             case 1:

                                 float revivePower = Mathf.Round(holyDropIndex.dropPower + (holyDropIndex.dropPower * character.holyDropsLevel / 2));
                                 abilitiesMenuHelpReference.text = "Revives a fallen ally, restoring " + revivePower + " HP. Costs " + holyDropCost + " MP.";
                                 break;
                             case 2:
                                 if (character.holyDropsLevel >= 20)
                                 {
                                     abilitiesMenuHelpReference.text = "Removes all negative status effects. Costs " + holyDropCost + " MP.";
                                 }
                                 else
                                 {
                                     if (character.holyDropsLevel >= 10 && character.holyDropsLevel < 20)
                                     {
                                         abilitiesMenuHelpReference.text = "Removes Poison, Sleep. Costs " + holyDropCost + " MP.";
                                     }
                                     else
                                     {
                                         if (character.holyDropsLevel < 10)
                                         {
                                             abilitiesMenuHelpReference.text = "Removes Sleep. Costs " + holyDropCost + " MP.";
                                         }
                                     }
                                 }
                                 break;
                             case 3:
                                 if (character.holyDropsLevel >= 20)
                                 {
                                     abilitiesMenuHelpReference.text = "Removes all positive status effects from an enemy. Costs " + holyDropCost + " MP.";
                                 }
                                 else
                                 {
                                     if (character.holyDropsLevel >= 10 && character.holyDropsLevel < 20)
                                     {
                                         abilitiesMenuHelpReference.text = "Removes two positive status effects from an enemy. Costs " + holyDropCost + " MP.";
                                     }
                                     else
                                     {
                                         if (character.holyDropsLevel < 10)
                                         {
                                             abilitiesMenuHelpReference.text = "Removes a positive status effect from an enemy. Costs " + holyDropCost + " MP.";
                                         }
                                     }
                                 }
                                 break;
                         }
                     }
                 }
             }
         }
     }*/
}



