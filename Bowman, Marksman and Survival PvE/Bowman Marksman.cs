using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.IO;
using System.Drawing;
using Styx;
using Styx.Combat.CombatRoutine;
using Styx.Helpers;
using Styx.Logic;
using Styx.Logic.Combat;
using Styx.Logic.Pathing;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using TreeSharp;

namespace Marksman
{
    class Classname : CombatRoutine
    {
        public override sealed string Name { get { return "Bowman PvE CC v4.3.0.0"; } }

        public override WoWClass Class { get { return WoWClass.Hunter; } }


        private static LocalPlayer Me { get { return ObjectManager.Me; } }


        #region Log
        private void slog(string format, params object[] args) //use for slogging
        {
            Logging.Write(format, args);
        }
        #endregion


        #region Initialize
        public override void Initialize()
        {
            Logging.Write(Color.White, "___________________________________________");
            Logging.Write(Color.Crimson, "----------- Bowman PvE CC v4.3.0.0 ------------");
            Logging.Write(Color.Crimson, "by FallDown, Shaddar, Venus112 and Jasf10");
            Logging.Write(Color.Crimson, "---  Remember to comment on the forum! ---");
            Logging.Write(Color.Crimson, "--- /like and +rep if you like this CC! ----");
            Logging.Write(Color.Crimson, "----Thank you to Fiftypence for support ----");
            Logging.Write(Color.White, "___________________________________________");
        }
        #endregion



        #region Settings

        public override bool WantButton
        {
            get
            {
                return true;
            }
        }

        public override void OnButtonPress()
        {
            Marksman.MarksForm1 f1 = new Marksman.MarksForm1();
            f1.ShowDialog();
        }
        #endregion

        #region Halt on Trap Launcher
        public bool HaltTrap()
        {
            if (!Me.HasAura("Trap Launcher"))
                return true;
            else return false;
        }
        #endregion

        #region Halt on Feign Death
        public bool HaltFeign()
        {
            {
                if (!Me.ActiveAuras.ContainsKey("Feign Death"))
                    return true;
            }
            return false;
        }
        #endregion

        #region Dragon Soul

        public bool DebuffByID(int spellId)
        {
            if (Me.HasAura(spellId) && StyxWoW.Me.GetAuraById(spellId).TimeLeft.TotalMilliseconds <= 2000)
                return true;
            else return false;
        }

        public bool Ultra()
        {
            using (new FrameLock())
            {
                if (MarksmanSettings.Instance.DSNOR || MarksmanSettings.Instance.DSLFR)
                {
                    if (!Me.ActiveAuras.ContainsKey("Deterrence"))
                    {
                        foreach (WoWUnit u in ObjectManager.GetObjectsOfType<WoWUnit>(true, true))
                        {
                            if (u.IsAlive
                                && u.Guid != Me.Guid
                                && u.IsHostile
                                && u.IsCasting
                                && (u.CastingSpell.Id == 109417
                                    || u.CastingSpell.Id == 109416
                                    || u.CastingSpell.Id == 109415
                                    || u.CastingSpell.Id == 106371)
                                && u.CurrentCastTimeLeft.TotalMilliseconds <= 800)
                                return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool UltraFL()
        {
            if (DebuffByID(110079)
                || DebuffByID(110080)
                || DebuffByID(110070)
                || DebuffByID(110069)
                || DebuffByID(109075)
                || DebuffByID(110068)
                || DebuffByID(105925)
                || DebuffByID(110078))
                return true;

            else return false;
        }

        public bool DW()
        {
            if (DebuffByID(110139)
                || DebuffByID(110140)
                || DebuffByID(110141)
                || DebuffByID(106791)
                || DebuffByID(109599)
                || DebuffByID(106794)
                || DebuffByID(109597)
                || DebuffByID(109598))
                return true;

            else return false;
        }
        #endregion

        #region Add Detection
        private int addCount()
        {
            int count = 0;
            foreach (WoWUnit u in ObjectManager.GetObjectsOfType<WoWUnit>(true, true))
            {
                if (Me.GotTarget
                    && u.IsAlive
                    && u.Guid != Me.Guid
                    && u.IsHostile
                    && !u.IsCritter
                    && (u.Location.Distance(Me.CurrentTarget.Location) <= 12 || u.Location.Distance2D(Me.CurrentTarget.Location) <= 12)
                    && (u.IsTargetingMyPartyMember || u.IsTargetingMyRaidMember || u.IsTargetingMeOrPet || u.IsTargetingAnyMinion)
                    && !u.IsFriendly)
                {
                    count++;
                }
            }
            return count;
        }
        private bool IsTargetBoss()
        {
            if (Me.CurrentTarget.CreatureRank == WoWUnitClassificationType.WorldBoss ||
               (Me.CurrentTarget.Level >= 85 && Me.CurrentTarget.Elite && Me.CurrentTarget.MaxHealth > 3500000))
                return true;

            else return false;
        }
        private bool IsTargetEasyBoss()
        {
            if (Me.CurrentTarget.CreatureRank == WoWUnitClassificationType.WorldBoss ||
               (Me.CurrentTarget.Level >= 85 && Me.CurrentTarget.Elite && Me.CurrentTarget.MaxHealth > 300000))
                return true;

            else return false;
        }
        #endregion

        #region CastSpell Method
        // Credit to Apoc for the below CastSpell code
        // Used for calling CastSpell in the Combat Rotation
        //Credit to Wulf!
        public bool CastSpell(string spellName)
        {
            {
                if (SpellManager.CanCast(spellName))
                {
                    SpellManager.Cast(spellName);
                    // We managed to cast the spell, so return true, saying we were able to cast it.
                    return true;
                }
            }
            // Can't cast the spell right now, so return false.
            return false;
        }
        #endregion

        #region MyDebuffTime
        //Used for checking how the time left on "my" debuff
        private int MyDebuffTime(String spellName, WoWUnit unit)
        {
            {
                if (unit.HasAura(spellName))
                {
                    var auras = unit.GetAllAuras();
                    foreach (var a in auras)
                    {
                        if (a.Name == spellName && a.CreatorGuid == Me.Guid)
                        {
                            return a.TimeLeft.Seconds;
                        }
                    }
                }
            }
            return 0;
        }
        #endregion

        #region DebuffTime
        //Used for checking debuff timers
        private int DebuffTime(String spellName, WoWUnit unit)
        {
            {
                if (unit.HasAura(spellName))
                {
                    var auras = unit.GetAllAuras();
                    foreach (var b in auras)
                    {
                        if (b.Name == spellName)
                        {
                            return b.TimeLeft.Seconds;
                        }
                    }
                }
            }
            return 0;
        }
        #endregion

        #region IsMyAuraActive
        //Used for checking auras that has no time
        private bool IsMyAuraActive(WoWUnit Who, String What)
        {
            {
                return Who.GetAllAuras().Where(p => p.CreatorGuid == Me.Guid && p.Name == What).FirstOrDefault() != null;
            }
        }
        #endregion

        #region rest
        public override bool NeedRest
        {
            get
            {
                if (HaltFeign() && StyxWoW.IsInWorld && !Me.IsGhost && !Me.Dead && !Me.Mounted && !Me.IsFlying && !Me.IsOnTransport)
                {
                    if (MarksmanSettings.Instance.RP && !Me.GotAlivePet && SpellManager.HasSpell("Revive Pet"))
                    {
                        if (CastSpell("Revive Pet"))
                        {
                            Logging.Write(Color.Aqua, ">> Reviving Pet <<");
                        }
                        StyxWoW.SleepForLagDuration();
                    }
                    if (MarksmanSettings.Instance.CP && Me.Pet == null && !Me.IsCasting)
                    {
                        if (MarksmanSettings.Instance.PET == 1 && SpellManager.HasSpell("Call Pet 1"))
                        {
                            SpellManager.Cast("Call Pet 1");
                            StyxWoW.SleepForLagDuration();
                        }
                        if (MarksmanSettings.Instance.PET == 2 && SpellManager.HasSpell("Call Pet 2"))
                        {
                            SpellManager.Cast("Call Pet 2");
                            StyxWoW.SleepForLagDuration();
                        }
                        if (MarksmanSettings.Instance.PET == 3 && SpellManager.HasSpell("Call Pet 3"))
                        {
                            SpellManager.Cast("Call Pet 3");
                            StyxWoW.SleepForLagDuration();
                        }
                        if (MarksmanSettings.Instance.PET == 4 && SpellManager.HasSpell("Call Pet 4"))
                        {
                            SpellManager.Cast("Call Pet 4");
                            StyxWoW.SleepForLagDuration();
                        }
                        if (MarksmanSettings.Instance.PET == 5 && SpellManager.HasSpell("Call Pet 5"))
                        {
                            SpellManager.Cast("Call Pet 5");
                            StyxWoW.SleepForLagDuration();
                        }
                        StyxWoW.SleepForLagDuration();
                    }
                }
                return true;
            }
        }
        #endregion

        #region CombatStart

        private void AutoAttack()
        {
            if (!Me.IsAutoAttacking && HaltTrap() && HaltFeign())
            {
                Lua.DoString("StartAttack()");
            }

        }
        #endregion

        #region Combat

        public override void Combat()
        {
            if (MarksmanSettings.Instance.TL && MarksmanSettings.Instance.TLF && Me.HasAura("Trap Launcher") && MyDebuffTime("Trap Launcher", Me) < 13 && (Me.CurrentTarget == null || addCount() < MarksmanSettings.Instance.Mobs || !Me.CurrentTarget.InLineOfSight || Me.CurrentTarget.Distance > 40 || SpellManager.Spells["Explosive Trap"].CooldownTimeLeft.TotalSeconds > 1))
            {
                Lua.DoString("RunMacroText('/cancelaura Trap Launcher');");
                {
                    Logging.Write(Color.Crimson, ">> Cancel Trap Launcher <<");
                }
            }
            if (Me.GotTarget && Me.CurrentTarget.IsAlive && !Me.Mounted && HaltTrap() && HaltFeign())
            {
                if (Ultra())
                {
                    Lua.DoString("RunMacroText('/click ExtraActionButton1');");
                    SpellManager.StopCasting();
                    {
                        Logging.Write(Color.Aqua, ">> Heroic Will! <<");
                    }
                }
                if (UltraFL())
                {
                    Lua.DoString("RunMacroText('/click ExtraActionButton1');");
                    SpellManager.StopCasting();
                    {
                        Logging.Write(Color.Aqua, ">> Heroic Will! <<");
                    }
                }
                if (DW())
                {
                    Lua.DoString("RunMacroText('/click ExtraActionButton1');");
                    SpellManager.StopCasting();
                    {
                        Logging.Write(Color.Aqua, ">> Enter the dream! <<");
                    }
                }
                if (MarksmanSettings.Instance.MP && Me.GotAlivePet && Me.Pet.HealthPercent <= MarksmanSettings.Instance.MendHealth && !Me.Pet.ActiveAuras.ContainsKey("Mend Pet"))
                {
                    if (CastSpell("Mend Pet"))
                    {
                        Logging.Write(Color.Aqua, ">> Mend Pet <<");
                    }
                }
                if (MarksmanSettings.Instance.MDPet && Me.GotAlivePet && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && !Me.ActiveAuras.ContainsKey("Misdirection") && !WoWSpell.FromId(34477).Cooldown && !SpellManager.Spells["Misdirection"].Cooldown)
                {
                    Lua.DoString("RunMacroText('/cast [@pet,exists] Misdirection');");
                    {
                        Logging.Write(Color.Crimson, ">> Misdirection on Pet <<");
                    }
                }
                ///////////////////////////////////////////Close Combat and Defense Mechanisms//////////////////////////////////////////////////////////////////////////////////////
                if (MarksmanSettings.Instance.FDCBox == "High Threat" && Me.CurrentTarget.ThreatInfo.RawPercent > 90)
                {
                    if (CastSpell("Feign Death"))
                    {
                        Logging.Write(Color.Aqua, ">> High Aggro, Feign Death <<");
                    }
                }
                if (MarksmanSettings.Instance.FDCBox == "On Aggro" && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && (Me.CurrentTarget.IsCasting || Me.CurrentTarget.Distance < 10))
                {
                    if (CastSpell("Feign Death"))
                    {
                        Logging.Write(Color.Aqua, ">> Aggro'ed, Feign Death <<");
                    }
                }
                if (MarksmanSettings.Instance.FDCBox == "Low Health" && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && Me.HealthPercent < 15)
                {
                    if (CastSpell("Feign Death"))
                    {
                        Logging.Write(Color.Aqua, ">> Low Health, Feign Death <<");
                    }
                }
                if (MarksmanSettings.Instance.INT && SpellManager.HasSpell("Wyvern Sting") && Me.CurrentTarget.IsCasting && Me.CanInterruptCurrentSpellCast && Me.CurrentTarget.Distance > 5)
                {
                    if (CastSpell("Wyvern"))
                    {
                        Logging.Write(Color.Aqua, ">> Wyvern Sting, Interrupt <<");
                    }
                }
                if (MarksmanSettings.Instance.INT && Me.CurrentTarget.Distance <= 20 && Me.CurrentTarget.IsCasting && Me.CanInterruptCurrentSpellCast)
                {
                    if (CastSpell("Scatter Shot"))
                    {
                        Logging.Write(Color.Aqua, ">> Scatter Shot, Interrupt <<");
                    }
                }
                if (MarksmanSettings.Instance.SCAT && Me.CurrentTarget.Distance <= 20 && Me.CurrentTarget.CurrentTargetGuid == Me.Guid)
                {
                    if (CastSpell("Scatter Shot"))
                    {
                        Logging.Write(Color.Aqua, ">> Scatter Shot, Evade <<");
                    }
                }
                if (MarksmanSettings.Instance.CONC && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && !Me.CurrentTarget.HasAura("Wing Clip") && !Me.CurrentTarget.HasAura("Concussive Shot") && Me.CurrentTarget.Distance < 35 && Me.CurrentTarget.Distance > 5 && Me.CurrentTarget.IsMoving)
                {
                    if (CastSpell("Concussive Shot"))
                    {
                        Logging.Write(Color.Aqua, ">> Concussive Shot <<");
                    }
                }
                if (MarksmanSettings.Instance.MLE && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && !Me.IsCasting && !Me.CurrentTarget.HasAura("Wing Clip") && !Me.CurrentTarget.HasAura("Concussive Shot") && Me.CurrentTarget.Distance < 5)
                {
                    if (CastSpell("Wing Clip"))
                    {
                        Logging.Write(Color.Aqua, ">> Wing Clip<<");
                    }
                }
                if (MarksmanSettings.Instance.MLE && Me.CurrentTarget.Distance < 5 && !Me.IsCasting)
                {
                    if (CastSpell("Raptor Strike"))
                    {
                        Logging.Write(Color.Aqua, ">> Raptor Strike <<");
                    }
                }
                if (MarksmanSettings.Instance.DIS && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && Me.CurrentTarget.Distance < 7 && Me.HealthPercent < 60)
                {
                    if (CastSpell("Disengage"))
                    {
                        Logging.Write(Color.Aqua, ">> Disengage <<");
                    }
                }
                if (MarksmanSettings.Instance.DETR && Me.HealthPercent < 20 && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && (Me.CurrentTarget.Distance < 10 || Me.CurrentTarget.IsCasting))
                {
                    if (CastSpell("Deterrence"))
                    {
                        Logging.Write(Color.Aqua, ">> Deterrence <<");
                    }
                }
                /////////////////////////////////////////////////////Cooldowns/////////////////////////////////////////////////////////////////////////////////////////////////           
                if (MarksmanSettings.Instance.RF && !Me.ActiveAuras.ContainsKey("Rapid Fire") && IsTargetBoss())
                {
                    if (CastSpell("Rapid Fire"))
                    {
                        Logging.Write(Color.Aqua, ">> Rapid Fire <<");
                    }
                }
                if (MarksmanSettings.Instance.LB && IsTargetBoss() && !Me.ActiveAuras.ContainsKey("Rapid Fire"))
                {
                    if (CastSpell("Lifeblood"))
                    {
                        Logging.Write(Color.Aqua, ">> Lifeblood <<");
                    }
                }
                if (MarksmanSettings.Instance.CW && Me.GotAlivePet && !WoWSpell.FromId(53434).Cooldown && !Me.ActiveAuras.ContainsKey("Rapid Fire") && IsTargetBoss())
                {
                    Lua.DoString("RunMacroText('/cast Call of the Wild');");
                    {
                        Logging.Write(Color.Crimson, ">> Pet: Call of the Wild <<");
                    }
                }
                if (MarksmanSettings.Instance.GE && IsTargetBoss() && StyxWoW.Me.Inventory.Equipped.Hands != null && StyxWoW.Me.Inventory.Equipped.Hands.Cooldown <= 0)
                {
                    Lua.DoString("RunMacroText('/use 10');");
                }
                if (MarksmanSettings.Instance.T1 && IsTargetBoss() && StyxWoW.Me.Inventory.Equipped.Trinket1 != null && StyxWoW.Me.Inventory.Equipped.Trinket1.Cooldown <= 0)
                {
                    Lua.DoString("RunMacroText('/use 13');");
                }
                if (MarksmanSettings.Instance.T2 && IsTargetBoss() && StyxWoW.Me.Inventory.Equipped.Trinket2 != null && StyxWoW.Me.Inventory.Equipped.Trinket2.Cooldown <= 0)
                {
                    Lua.DoString("RunMacroText('/use 14');");
                }
                //////////////////////////////////////////////////Racial Skills/////////////////////////////////////////////////////////////////////////////////////////
                if (MarksmanSettings.Instance.RS && Me.Race == WoWRace.Troll && IsTargetBoss() && !SpellManager.Spells["Berserking"].Cooldown)
                {
                    Lua.DoString("RunMacroText('/Cast Berserking');");
                }
                if (MarksmanSettings.Instance.RS && Me.Race == WoWRace.Orc && IsTargetBoss() && !SpellManager.Spells["Blood Fury"].Cooldown)
                {
                    Lua.DoString("RunMacroText('/Cast Blood Fury');");
                }
            }
            /////////////////////////////////////////////Survival Rotation///////////////////////////////////////////////////////////////////////////////////////////
            if (Me.GotTarget && (addCount() < MarksmanSettings.Instance.Mobs || (!MarksmanSettings.Instance.MS && !MarksmanSettings.Instance.TL)) && Me.CurrentTarget.Distance >= 5 && HaltTrap() && HaltFeign() && Me.CurrentTarget.IsAlive && !Me.Mounted)
            {
                if (MarksmanSettings.Instance.HM && Me.CurrentTarget.HealthPercent > 25 && !Me.CurrentTarget.HasAura("Hunter's Mark") && IsTargetEasyBoss())
                {
                    if (CastSpell("Hunter's Mark"))
                    {
                        Logging.Write(Color.Aqua, ">> Hunter's Mark <<");
                    }
                }
                if (Me.CurrentTarget.HealthPercent < 20)
                {
                    if (CastSpell("Kill Shot"))
                    {
                        Logging.Write(Color.Aqua, ">> Kill Shot <<");
                    }
                }
                if (MarksmanSettings.Instance.STING && !IsMyAuraActive(Me.CurrentTarget, "Serpent Sting") && Me.CurrentTarget.HealthPercent > 10 && IsTargetEasyBoss())
                {
                    if (CastSpell("Serpent Sting"))
                    {
                        Logging.Write(Color.Aqua, ">> Serpent Sting <<");
                    }
                }
                if (MarksmanSettings.Instance.BAR && !IsMyAuraActive(Me.CurrentTarget, "Black Arrow") && !SpellManager.Spells["Black Arrow"].Cooldown && Me.CurrentTarget.HealthPercent > 5)
                {
                    if (CastSpell("Black Arrow"))
                    {
                        Logging.Write(Color.Aqua, ">> Black Arrow <<");
                    }
                }
                if (!SpellManager.Spells["Explosive Shot"].Cooldown && MyDebuffTime("Explosive Shot", Me.CurrentTarget) <= 1)
                {
                    if (CastSpell("Explosive Shot"))
                    {
                        Logging.Write(Color.Aqua, ">> Explosive Shot <<");
                    }
                }
                if (Me.CurrentFocus >= 66 && (SpellManager.Spells["Explosive Shot"].CooldownTimeLeft.TotalSeconds > 1 || Me.CurrentFocus > 89) && SpellManager.Spells["Black Arrow"].CooldownTimeLeft.TotalSeconds > 1)
                {
                    if (CastSpell("Arcane Shot"))
                    {
                        Logging.Write(Color.Aqua, ">> Arcane Shot <<");
                    }
                }
                if (Me.CurrentFocus >= 66 && IsMyAuraActive(Me.CurrentTarget, "Explosive Shot"))
                {
                    if (CastSpell("Arcane Shot"))
                    {
                        Logging.Write(Color.Aqua, ">> Arcane Shot <<");
                    }
                }
                if (Me.CurrentFocus > 90 && Me.IsCasting && Me.CastingSpell.Name == "Cobra Shot" && Me.CurrentCastTimeLeft.TotalMilliseconds > 700)
                {
                    SpellManager.StopCasting();
                    Logging.Write(Color.Red, ">> Stop Cobra Shot <<");
                }
                if (Me.IsMoving && Me.Auras.ContainsKey("Aspect of the Fox") && !Me.ActiveAuras.ContainsKey("Lock and Load") && Me.CurrentFocus < MarksmanSettings.Instance.FocusShots && !SpellManager.CanCast("Kill Shot") && !SpellManager.GlobalCooldown)
                {
                    Lua.DoString("RunMacroText('/cast Cobra Shot');");
                    {
                        Logging.Write(Color.Red, ">> Moving - Cobra Shot <<");
                    }
                }
                if (!Me.ActiveAuras.ContainsKey("Lock and Load") && !Me.IsMoving && !SpellManager.GlobalCooldown && !SpellManager.CanCast("Kill Shot") && !SpellManager.CanCast("Black Arrow"))
                {
                    if (Me.IsCasting && Me.CastingSpell.Name == "Cobra Shot" && Me.CurrentFocus + 20 < MarksmanSettings.Instance.FocusShots)
                    {
                        if (CastSpell("Cobra Shot"))
                        {
                            Logging.Write(Color.Aqua, ">> 2nd Cobra Shot <<");
                        }
                    }
                    else if (!Me.IsCasting && Me.CurrentFocus < MarksmanSettings.Instance.FocusShots)
                    {
                        if (CastSpell("Cobra Shot"))
                        {
                            Logging.Write(Color.Aqua, ">> Cobra Shot <<");
                        }
                    }
                }
            }
            ///////////////////////////////////////////////Aspect Switching////////////////////////////////////////////////////////////////////////////////////////////
            if (MarksmanSettings.Instance.AspectSwitching && HaltTrap() && HaltFeign() && Me.CurrentTarget != null && Me.CurrentTarget.IsAlive && !Me.Mounted)
            {
                if (!Me.IsMoving && !Me.Auras.ContainsKey("Aspect of the Hawk") )
                {
                    if (CastSpell("Aspect of the Hawk"))
                    {
                        Logging.Write(Color.Aqua, ">> Not moving - Aspect of the Hawk <<");
                    }
                }
                if (Me.IsMoving && Me.Auras.ContainsKey("Aspect of the Hawk") && Me.CurrentFocus < 66)
                {
                    if (CastSpell("Aspect of the Fox"))
                    {
                        Logging.Write(Color.Aqua, ">> Moving - Aspect of the Fox <<");
                    }
                }
            }
            //////////////////////////////////////////////AoE Rotation here/////////////////////////////////////////////////////////////////////////////////////////////////
            if (addCount() >= MarksmanSettings.Instance.Mobs && HaltFeign() && Me.GotTarget && Me.CurrentTarget.IsAlive && !Me.Mounted && (MarksmanSettings.Instance.MS || MarksmanSettings.Instance.TL))
            {
                if (MarksmanSettings.Instance.TL && SpellManager.Spells["Explosive Trap"].CooldownTimeLeft.TotalSeconds < 1 && !Me.HasAura("Trap Launcher") && Me.CurrentTarget.InLineOfSight && Me.CurrentTarget.Distance <= 40 && Me.CurrentTarget.Distance >= 5)
                {
                    if (CastSpell("Trap Launcher"))
                    {
                        Logging.Write(Color.Red, ">> Trap Launcher Activated! <<");
                    }
                }
                else if (MarksmanSettings.Instance.TL && SpellManager.Spells["Explosive Trap"].CooldownTimeLeft.TotalSeconds < 1 && !Me.HasAura("Trap Launcher") && Me.CurrentTarget.Distance < 5)
                {
                    if (CastSpell("Explosive Trap"))
                    {
                        Logging.Write(Color.Red, ">> Dropping Explosive Trap <<");
                    }
                }
                if (Me.HasAura("Trap Launcher") && MarksmanSettings.Instance.TL)
                {
                    Lua.DoString("CastSpellByName('Explosive Trap');");
                    {
                        LegacySpellManager.ClickRemoteLocation(StyxWoW.Me.CurrentTarget.Location);
                    }
                }
                if (!Me.HasAura("Trap Launcher") && Me.CurrentTarget.Distance >= 5 && !SpellManager.Spells["Explosive Shot"].Cooldown && MyDebuffTime("Explosive Shot", Me.CurrentTarget) <= 1 && Me.ActiveAuras.ContainsKey("Lock and Load"))
                {
                    if (CastSpell("Explosive Shot"))
                    {
                        Logging.Write(Color.Aqua, ">> Explosive Shot <<");
                    }
                }
                if (!Me.HasAura("Trap Launcher") && Me.CurrentTarget.Distance >= 5 && MarksmanSettings.Instance.MS)
                {
                    if (CastSpell("Multi-Shot"))
                    {
                        Logging.Write(Color.Aqua, ">> Multi-Shot <<");
                    }
                }
                if (!Me.HasAura("Trap Launcher") && Me.CurrentTarget.Distance >= 5 && Me.CurrentFocus <= 42 && !SpellManager.CanCast("Kill Shot") && !SpellManager.GlobalCooldown)
                {
                    if (CastSpell("Cobra Shot"))
                    {
                        Logging.Write(Color.Aqua, ">> Cobra Shot <<");
                    }
                }
            }
        }
        #endregion
    }
}