﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Styx;
using Styx.Combat.CombatRoutine;
using Styx.Common;
using Styx.CommonBot;
using Styx.CommonBot.POI;
using Styx.CommonBot.Routines;
using Styx.Helpers;
using Styx.Pathing;
using Styx.Plugins;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;

namespace TheBeastMaster
{
    internal class BeastMaster : CombatRoutine
    {
        public override sealed string Name { get { return "The Beast Master PvE CC 1.4"; } }

        public override WoWClass Class { get { return WoWClass.Hunter; } }

        private static LocalPlayer Me { get { return ObjectManager.Me; } }


        #region Log
        private static void slog(string format, params object[] args) //use for slogging
        {
            Logging.Write(format, args);
        }

        #endregion


        #region Initialize
        public override void Initialize()
        {
            Logging.Write(Colors.Crimson, "The Beast Master 1.4");
            Logging.Write(Colors.Crimson, "A Beast Mastery Hunter Routine");
            Logging.Write(Colors.Crimson, "Made By FallDown");
            Logging.Write(Colors.Crimson, "For LazyRaider Only!");
        }
        #endregion



        #region Settings

        public override bool WantButton { get { return true; } }

        #endregion

        public override void OnButtonPress()
        {
            slog("Config opened!");
            new BeastForm1().ShowDialog();
        }

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

        #region SelfControl
        public bool SelfControl(WoWUnit unit)
        {
            if (Me.GotTarget && (unit.HasAura("Freezing Trap") || unit.HasAura("Wyvern Sting") || unit.HasAura("Scatter Shot") || unit.HasAura("Bad Manner")))
                return true;

            else return false;
        }
        #endregion

        #region Dragon Soul

        public bool DebuffByID(int spellId)
        {
            if (Me.HasAura(spellId) && StyxWoW.Me.GetAuraById(spellId).TimeLeft.TotalMilliseconds <= 2500)
                return true;
            else return false;
        }

        public bool Ultra()
        {
            if (BeastMasterSettings.Instance.DSNOR || BeastMasterSettings.Instance.DSLFR)
            {
                if (!Me.ActiveAuras.ContainsKey("Deterrence"))
                {
                    foreach (WoWUnit u in ObjectManager.GetObjectsOfType<WoWUnit>(true, true))
                    {
                        if (u.IsAlive
                            && u.Guid != Me.Guid
                            && u.IsHostile
                            && u.IsCasting
                            && (u.CastingSpell.Id == 106174
                                || u.CastingSpell.Id == 106389
                                || u.CastingSpell.Id == 103327)
                            && u.CurrentCastTimeLeft.TotalMilliseconds <= 1000)
                            return true;
                    }
                }
            }
            return false;
        }

        public bool UltraFL()
        {
            if (DebuffByID(109200) || DebuffByID(105926) || DebuffByID(105925))
                return true;

            else return false;
        }

        public bool DW()
        {
            if (DebuffByID(106791) || DebuffByID(106794))
                return true;

            else return false;
        }
        #endregion

        #region Boss Check

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
        public static bool CastSpell(string spellName, WoWUnit target)
        {
            if (SpellManager.CanCast(spellName, target))
            {
                if (SpellManager.HasSpell(spellName) && SpellManager.Spells[spellName].CooldownTimeLeft.TotalMilliseconds < 300)
                {
                    SpellManager.Cast(spellName, target);
                    return true;
                }
            }
            return false;
        }

        public static bool CastSpell(string spellName)
        {
            if (SpellManager.CanCast(spellName))
            {
                if (SpellManager.HasSpell(spellName) && SpellManager.Spells[spellName].CooldownTimeLeft.TotalMilliseconds < 300)
                {
                    SpellManager.Cast(spellName);
                    return true;
                }
            }
            return false;
        }

        #endregion


        #region Controlled

         private static List<WoWSpellMechanic> controlMechanic = new List<WoWSpellMechanic>()
        {
            WoWSpellMechanic.Charmed,
            WoWSpellMechanic.Disoriented,
            WoWSpellMechanic.Fleeing,
            WoWSpellMechanic.Frozen,
            WoWSpellMechanic.Incapacitated,
            WoWSpellMechanic.Polymorphed,
            WoWSpellMechanic.Sapped
        };

        public bool isControlled(WoWUnit u)
        {
            foreach (WoWAura aura in u.Auras.Values)
            {
                if (controlMechanic.Contains(WoWSpell.FromId(aura.SpellId).Mechanic))
                    return true;
                else
                    return false;
            }
            return true;
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
                    && !u.IsFriendly
                    && u.IsHostile
                    && u.Attackable
                    && !u.IsTotem
                    && !u.IsCritter
                    && !u.IsNonCombatPet
                    && (u.Location.Distance(Me.CurrentTarget.Location) <= 10 || u.Location.Distance2D(Me.CurrentTarget.Location) <= 10))
                {
                    count++;
                }
            }
            return count;
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
                if (HaltFeign() && StyxWoW.IsInWorld && !Me.IsGhost && Me.IsAlive && !Me.Mounted && !Me.IsFlying && !Me.IsOnTransport)
                {
                    if (BeastMasterSettings.Instance.RP && !Me.GotAlivePet && SpellManager.HasSpell("Revive Pet"))
                    {
                        if (CastSpell("Revive Pet"))
                        {
                            Logging.Write(Colors.Aqua, ">> Reviving Pet <<");
                        }
                        StyxWoW.SleepForLagDuration();
                    }
                    if (BeastMasterSettings.Instance.CP && Me.Pet == null && !Me.IsCasting)
                    {
                        if (BeastMasterSettings.Instance.PET == 1 && SpellManager.HasSpell("Call Pet 1"))
                        {
                            SpellManager.Cast("Call Pet 1");
                            StyxWoW.SleepForLagDuration();
                        }
                        if (BeastMasterSettings.Instance.PET == 2 && SpellManager.HasSpell("Call Pet 2"))
                        {
                            SpellManager.Cast("Call Pet 2");
                            StyxWoW.SleepForLagDuration();
                        }
                        if (BeastMasterSettings.Instance.PET == 3 && SpellManager.HasSpell("Call Pet 3"))
                        {
                            SpellManager.Cast("Call Pet 3");
                            StyxWoW.SleepForLagDuration();
                        }
                        if (BeastMasterSettings.Instance.PET == 4 && SpellManager.HasSpell("Call Pet 4"))
                        {
                            SpellManager.Cast("Call Pet 4");
                            StyxWoW.SleepForLagDuration();
                        }
                        if (BeastMasterSettings.Instance.PET == 5 && SpellManager.HasSpell("Call Pet 5"))
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
            if (!Me.IsAutoAttacking && Me.GotTarget && HaltFeign() && !SelfControl(Me.CurrentTarget))
            {
                Lua.DoString("StartAttack()");  
            }

        }
        #endregion

        #region Combat

        public override void Combat()
        {
            if (SelfControl(Me.CurrentTarget))
            {
                Lua.DoString("StopAttack()");
                {
                    Logging.Write(Colors.Aqua, ">> Stop Everything! <<");
                }
                SpellManager.StopCasting();
                {
                    Logging.Write(Colors.Aqua, ">> Stop Everything! <<");
                }
            }
            if (Me.GotTarget && Me.CurrentTarget.IsAlive && !Me.Mounted && HaltFeign())
            {
                if (Ultra())
                {
                    Lua.DoString("RunMacroText('/click ExtraActionButton1');");
                    SpellManager.StopCasting();
                    {
                        Logging.Write(Colors.Aqua, ">> Heroic Will! <<");
                    }
                } 
                if (UltraFL())
                {
                    Lua.DoString("RunMacroText('/click ExtraActionButton1');");
                    SpellManager.StopCasting();
                    {
                        Logging.Write(Colors.Aqua, ">> Heroic Will! <<");
                    }
                }
                if (DW())
                {
                    Lua.DoString("RunMacroText('/click ExtraActionButton1');");
                    SpellManager.StopCasting();
                    {
                        Logging.Write(Colors.Aqua, ">> Enter the dream! <<");
                    }
                }
                if (BeastMasterSettings.Instance.MP && Me.GotAlivePet && Me.Pet.HealthPercent <= BeastMasterSettings.Instance.MendHealth && !Me.Pet.ActiveAuras.ContainsKey("Mend Pet"))
                {
                    if (CastSpell("Mend Pet"))
                    {
                        Logging.Write(Colors.Aqua, ">> Mend Pet <<");
                    }
                }
                if (BeastMasterSettings.Instance.TL2_EXH && (Me.HealthPercent < 70 
                    || (Me.Pet.HealthPercent < 10 && SpellManager.HasSpell("Heart of the Phoenix") && SpellManager.Spells["Heart of the Phoenix"].CooldownTimeLeft.TotalSeconds > 5)
                    || (Me.Pet.HealthPercent < 10 && !SpellManager.HasSpell("Heart of the Phoenix"))))
                {
                    if (CastSpell("Exhilaration"))
                    {
                        Logging.Write(Colors.Aqua, ">> Exhilaration <<");
                    }
                }
                if (BeastMasterSettings.Instance.MDPet && Me.GotAlivePet && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && !Me.ActiveAuras.ContainsKey("Misdirection") 
                    && !WoWSpell.FromId(34477).Cooldown && !SpellManager.Spells["Misdirection"].Cooldown)
                {
                    Lua.DoString("RunMacroText('/cast [@pet,exists] Misdirection');");
                    {
                        Logging.Write(Colors.Aqua, ">> Misdirection on Pet <<");
                    }
                }
                if (BeastMasterSettings.Instance.MDF && Me.FocusedUnit != null && !Me.ActiveAuras.ContainsKey("Misdirection") 
                    && !WoWSpell.FromId(34477).Cooldown && !SpellManager.Spells["Misdirection"].Cooldown)
                {
                    Lua.DoString("RunMacroText('/cast [@focus,exists] Misdirection');");
                    {
                        Logging.Write(Colors.Aqua, ">> Misdirection on Focus <<");
                    }
                }
                ///////////////////////////////////////////Close Combat and Defense Mechanisms//////////////////////////////////////////////////////////////////////////////////////
                if (BeastMasterSettings.Instance.SMend && Me.CurrentHealth < Me.MaxHealth - 20000 && !WoWSpell.FromId(90361).Cooldown)
                {
                    Lua.DoString("RunMacroText(\"/cast [@" + Me.Name + "] Spirit Mend\")");
                    {
                        Logging.Write(Colors.Aqua, ">> Pet: Spirit Mend <<");
                    }
                }
                if (BeastMasterSettings.Instance.FDCBox == "1. High Threat" && Me.CurrentTarget.ThreatInfo.RawPercent > 90)
                {
                    if (CastSpell("Feign Death"))
                    {
                        Logging.Write(Colors.Aqua, ">> High Aggro, Feign Death <<");
                    }
                }
                if (BeastMasterSettings.Instance.FDCBox == "2. On Aggro" && Me.CurrentTarget.ThreatInfo.RawPercent > 90 
                    && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && (Me.CurrentTarget.IsCasting || Me.CurrentTarget.Distance < 10))
                {
                    if (CastSpell("Feign Death"))
                    {
                        Logging.Write(Colors.Aqua, ">> Aggro'ed, Feign Death <<");
                    }
                }
                if (BeastMasterSettings.Instance.FDCBox == "3. Low Health" && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && Me.HealthPercent < 15)
                {
                    if (CastSpell("Feign Death"))
                    {
                        Logging.Write(Colors.Aqua, ">> Low Health, Feign Death <<");
                    }
                }
                if (BeastMasterSettings.Instance.FDCBox == "1 + 3" && Me.CurrentTarget.ThreatInfo.RawPercent > 90 && Me.HealthPercent < 15)
                {
                    if (CastSpell("Feign Death"))
                    {
                        Logging.Write(Colors.Aqua, ">> Thread + Low HP, Feign Death <<");
                    }
                }
                if (BeastMasterSettings.Instance.FDCBox == "2 + 3" && Me.CurrentTarget.ThreatInfo.RawPercent > 90 && Me.HealthPercent < 15 
                    && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && (Me.CurrentTarget.IsCasting || Me.CurrentTarget.Distance < 10))
                {
                    if (CastSpell("Feign Death"))
                    {
                        Logging.Write(Colors.Aqua, ">> Aggro + Low HP, Feign Death <<");
                    }
                }
                if (BeastMasterSettings.Instance.DETR && Me.HealthPercent < 20 && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && (Me.CurrentTarget.Distance < 10 || Me.CurrentTarget.IsCasting))
                {
                    if (CastSpell("Deterrence"))
                    {
                        Logging.Write(Colors.Aqua, ">> Deterrence <<");
                    }
                }
            }
            if (Me.GotTarget && Me.CurrentTarget.IsAlive && !Me.Mounted && HaltFeign() && !SelfControl(Me.CurrentTarget))
            {
                if (BeastMasterSettings.Instance.ScatterBox == "1. Interrupt" && Me.CurrentTarget.Distance <= 20 && Me.CurrentTarget.IsCasting && Me.CanInterruptCurrentSpellCast)
                {
                    if (CastSpell("Scatter Shot"))
                    {
                        Logging.Write(Colors.Aqua, ">> Scatter Shot, Interrupt <<");
                    }
                }
                if (BeastMasterSettings.Instance.ScatterBox == "2. Defense" && Me.CurrentTarget.Distance <= 20 && Me.CurrentTarget.CurrentTargetGuid == Me.Guid)
                {
                    if (CastSpell("Scatter Shot"))
                    {
                        Logging.Write(Colors.Aqua, ">> Scatter Shot, Evade <<");
                    }
                }
                if (BeastMasterSettings.Instance.TL1_SS && Me.CurrentTarget.IsCasting && Me.CanInterruptCurrentSpellCast)
                {
                    if (CastSpell("Silencing Shot"))
                    {
                        Logging.Write(Colors.Aqua, ">> Silencing Shot <<");
                    }
                }
                if (BeastMasterSettings.Instance.TL1_WS && Me.CurrentTarget.IsCasting && Me.CanInterruptCurrentSpellCast)
                {
                    if (CastSpell("Wyvern Sting"))
                    {
                        Logging.Write(Colors.Aqua, ">> Wyvern Sting, Interrupt <<");
                    }
                }
                if (BeastMasterSettings.Instance.TL1_BS && addCount() >= BeastMasterSettings.Instance.Mobs)
                {
                    if (CastSpell("Binding Shot"))
                    {
                        SpellManager.ClickRemoteLocation(StyxWoW.Me.CurrentTarget.Location);
                        Logging.Write(Colors.Aqua, ">> Binding Shot Launched! <<");
                    }
                }
                if (BeastMasterSettings.Instance.ScatterBox == "1 + 2" && ((Me.CurrentTarget.Distance <= 20 && Me.CurrentTarget.CurrentTargetGuid == Me.Guid) 
                    || (Me.CurrentTarget.IsCasting && Me.CanInterruptCurrentSpellCast)))
                {
                    if (CastSpell("Scatter Shot"))
                    {
                        Logging.Write(Colors.Aqua, ">> Scatter Shot <<");
                    }
                }
                if (BeastMasterSettings.Instance.CONC && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && !Me.CurrentTarget.HasAura("Concussive Shot") && Me.CurrentTarget.Distance < 25)
                {
                    if (CastSpell("Concussive Shot"))
                    {
                        Logging.Write(Colors.Aqua, ">> Concussive Shot <<");
                    }
                }
                if (BeastMasterSettings.Instance.IntimidateBox == "1. Interrupt" && Me.GotAlivePet && Me.Pet.Location.Distance(Me.CurrentTarget.Location) < 9 
                    && Me.CurrentTarget.IsCasting && Me.CanInterruptCurrentSpellCast && !SpellManager.Spells["Intimidation"].Cooldown)
                {
                    if (CastSpell("Intimidation"))
                    {
                        Logging.Write(Colors.Aqua, ">> Intimidation, Interrupt <<");
                    }
                }
                if (BeastMasterSettings.Instance.IntimidateBox == "2. Defense" && Me.GotAlivePet && Me.Pet.Location.Distance(Me.CurrentTarget.Location) < 9 
                    && Me.CurrentTarget.Distance <= 20 && Me.CurrentTarget.CurrentTargetGuid == Me.Guid)
                {
                    if (CastSpell("Intimidation"))
                    {
                        Logging.Write(Colors.Aqua, ">> Intimidation, Defense <<");
                    }
                }
                if (BeastMasterSettings.Instance.IntimidateBox == "1 + 2" && ((Me.CurrentTarget.Distance <= 20 && Me.CurrentTarget.CurrentTargetGuid == Me.Guid) 
                    || (Me.CurrentTarget.IsCasting && Me.CanInterruptCurrentSpellCast && !SpellManager.Spells["Intimidation"].Cooldown)))
                {
                    if (CastSpell("Intimidation"))
                    {
                        Logging.Write(Colors.Aqua, ">> Intimidation Stranger Danger! <<");
                    }
                }
                
                /////////////////////////////////////////////////////Cooldowns/////////////////////////////////////////////////////////////////////////////////////////////////           
                if (BeastMasterSettings.Instance.RF && !Me.ActiveAuras.ContainsKey("Rapid Fire") && !Me.ActiveAuras.ContainsKey("The Beast Within") 
                    && !Me.ActiveAuras.ContainsKey("Bloodlust") && !Me.ActiveAuras.ContainsKey("Heroism") && !Me.ActiveAuras.ContainsKey("Ancient Hysteria") 
                    && !Me.ActiveAuras.ContainsKey("Time Warp") && (IsTargetBoss() || Me.CurrentTarget.Name == "Training Dummy"))
                {
                    if (CastSpell("Rapid Fire"))
                    {
                        Logging.Write(Colors.Aqua, ">> Rapid Fire <<");
                    }
                }
                if (Me.GotAlivePet && BeastMasterSettings.Instance.BWR && Me.Pet.Location.Distance(Me.CurrentTarget.Location) <= 25 && !Me.ActiveAuras.ContainsKey("Rapid Fire") 
                    && !Me.ActiveAuras.ContainsKey("The Beast Within") && !Me.ActiveAuras.ContainsKey("Bloodlust") && !Me.ActiveAuras.ContainsKey("Heroism") 
                    && !Me.ActiveAuras.ContainsKey("Ancient Hysteria") && !Me.ActiveAuras.ContainsKey("Time Warp") && (Me.CurrentTarget.MaxHealth > 200000 || Me.CurrentTarget.Name == "Training Dummy"))
                {
                    if (CastSpell("Bestial Wrath"))
                    {
                        Logging.Write(Colors.Aqua, ">> Bestial Wrath <<");
                    }
                }
                if (BeastMasterSettings.Instance.RDN 
                    && SpellManager.Spells["Rapid Fire"].CooldownTimeLeft.TotalSeconds > 1 
                    && SpellManager.Spells["Bestial Wrath"].CooldownTimeLeft.TotalSeconds > 1
                    && (SpellManager.Spells["Kill Command"].CooldownTimeLeft.TotalSeconds > 1 || !BeastMasterSettings.Instance.KCO)
                    && (SpellManager.HasSpell("Lynx Rush") && SpellManager.Spells["Lynx Rush"].CooldownTimeLeft.TotalSeconds > 1 || !BeastMasterSettings.Instance.TL4_LR)
                    && (SpellManager.HasSpell("Fervor") && SpellManager.Spells["Fervor"].CooldownTimeLeft.TotalSeconds > 1 || !BeastMasterSettings.Instance.TL3_FV)
                    && (SpellManager.HasSpell("A Murder of Crows") && SpellManager.Spells["A Murder of Crows"].CooldownTimeLeft.TotalSeconds > 1 || !BeastMasterSettings.Instance.TL4_AMOC)
                    && (SpellManager.HasSpell("Blink Strike") && SpellManager.Spells["Blink Strike"].CooldownTimeLeft.TotalSeconds > 1 || !BeastMasterSettings.Instance.TL4_BSTRK)
                    && (SpellManager.HasSpell("Dire Beast") && SpellManager.Spells["Dire Beast"].CooldownTimeLeft.TotalSeconds > 1 || !BeastMasterSettings.Instance.TL3_DB))
                {
                    if (CastSpell("Readiness"))
                    {
                        Logging.Write(Colors.Aqua, ">> Readiness <<");
                    }
                }
                if (BeastMasterSettings.Instance.LB && IsTargetBoss())
                {
                    if (CastSpell("Lifeblood"))
                    {
                        Logging.Write(Colors.Aqua, ">> Lifeblood <<");
                    }
                }
                if (BeastMasterSettings.Instance.CW && Me.GotAlivePet && !WoWSpell.FromId(53434).Cooldown && !Me.ActiveAuras.ContainsKey("Rapid Fire") && IsTargetBoss())
                {
                    Lua.DoString("RunMacroText('/cast Call of the Wild');");
                    {
                        Logging.Write(Colors.Aqua, ">> Pet: Call of the Wild <<");
                    }
                }
                if (BeastMasterSettings.Instance.GE && IsTargetBoss() && StyxWoW.Me.Inventory.Equipped.Hands != null && StyxWoW.Me.Inventory.Equipped.Hands.Cooldown <= 0)
                {
                    Lua.DoString("RunMacroText('/use 10');");
                }
                if (BeastMasterSettings.Instance.T1 && IsTargetBoss() && StyxWoW.Me.Inventory.Equipped.Trinket1 != null && StyxWoW.Me.Inventory.Equipped.Trinket1.Cooldown <= 0)
                {
                    Lua.DoString("RunMacroText('/use 13');");
                }
                if (BeastMasterSettings.Instance.T2 && IsTargetBoss() && StyxWoW.Me.Inventory.Equipped.Trinket2 != null && StyxWoW.Me.Inventory.Equipped.Trinket2.Cooldown <= 0)
                {
                    Lua.DoString("RunMacroText('/use 14');");
                }
                //////////////////////////////////////////////////Racial Skills/////////////////////////////////////////////////////////////////////////////////////////
                if (BeastMasterSettings.Instance.RS && Me.Race == WoWRace.Troll && IsTargetBoss() && !SpellManager.Spells["Berserking"].Cooldown)
                {
                    Lua.DoString("RunMacroText('/Cast Berserking');");
                }
                if (BeastMasterSettings.Instance.RS && Me.Race == WoWRace.Orc && IsTargetBoss() && !SpellManager.Spells["Blood Fury"].Cooldown)
                {
                    Lua.DoString("RunMacroText('/Cast Blood Fury');");
                }
            }
            ///////////////////////////////////////////////Aspect Switching////////////////////////////////////////////////////////////////////////////////////////////
            if (BeastMasterSettings.Instance.AspectSwitching && HaltFeign() && Me.CurrentTarget != null && Me.CurrentTarget.IsAlive && !Me.Mounted)
            {
                if (BeastMasterSettings.Instance.TL2_AOTIH)
                {
                    if (!Me.IsMoving && !Me.Auras.ContainsKey("Aspect of the Iron Hawk"))
                    {
                        if (CastSpell("Aspect of the Iron Hawk"))
                        {
                            Logging.Write(Colors.Aqua, ">> Not moving - Aspect of the Iron Hawk <<");
                        }
                    }
                    if (Me.IsMoving && Me.Auras.ContainsKey("Aspect of the Iron Hawk") && Me.CurrentFocus < 60)
                    {
                        if (CastSpell("Aspect of the Fox"))
                        {
                            Logging.Write(Colors.Aqua, ">> Moving - Aspect of the Fox <<");
                        }
                    }
                }
                else if (!BeastMasterSettings.Instance.TL2_AOTIH)
                {
                    if (!Me.IsMoving && !Me.Auras.ContainsKey("Aspect of the Hawk"))
                    {
                        if (CastSpell("Aspect of the Hawk"))
                        {
                            Logging.Write(Colors.Aqua, ">> Not moving - Aspect of the Hawk <<");
                        }
                    }
                    if (Me.IsMoving && Me.Auras.ContainsKey("Aspect of the Hawk") && Me.CurrentFocus < 60)
                    {
                        if (CastSpell("Aspect of the Fox"))
                        {
                            Logging.Write(Colors.Aqua, ">> Moving - Aspect of the Fox <<");
                        }
                    }
                }

            }
            /////////////////////////////////////////////Beastmastery Rotation///////////////////////////////////////////////////////////////////////////////////////////
            if (Me.GotTarget && (addCount() < BeastMasterSettings.Instance.Mobs || (!BeastMasterSettings.Instance.MS && !BeastMasterSettings.Instance.TL))
                && HaltFeign() && Me.CurrentTarget.IsAlive && !Me.Mounted && !SelfControl(Me.CurrentTarget))
            {
                if (BeastMasterSettings.Instance.HM && Me.CurrentTarget.HealthPercent > 25 && !Me.CurrentTarget.HasAura("Hunter's Mark") && IsTargetEasyBoss())
                {
                    if (CastSpell("Hunter's Mark"))
                    {
                        Logging.Write(Colors.Aqua, ">> Hunter's Mark <<");
                    }
                }
                if (BeastMasterSettings.Instance.KSH && Me.CurrentTarget.HealthPercent <= 20)
                {
                    if (CastSpell("Kill Shot"))
                    {
                        Logging.Write(Colors.Aqua, ">> Kill Shot <<");
                    }
                }
                if (BeastMasterSettings.Instance.SerpentBox == "Always" && (!IsMyAuraActive(Me.CurrentTarget, "Serpent Sting") || MyDebuffTime("Serpent Sting", Me.CurrentTarget) < 1))
                {
                    if (CastSpell("Serpent Sting"))
                    {
                        Logging.Write(Colors.Aqua, ">> Serpent Sting <<");
                    }
                }
                if (BeastMasterSettings.Instance.SerpentBox == "Sometimes" && !IsMyAuraActive(Me.CurrentTarget, "Serpent Sting") 
                    && Me.CurrentTarget.MaxHealth > Me.MaxHealth * 2 && Me.CurrentTarget.HealthPercent > 10)
                {
                    if (CastSpell("Serpent Sting"))
                    {
                        Logging.Write(Colors.Aqua, ">> Serpent Sting <<");
                    }
                }
                if (BeastMasterSettings.Instance.TL3_DB && ((Me.CurrentTarget.Level >= Me.Level && Me.CurrentTarget.CurrentHealth > Me.MaxHealth * 0.3) 
                    || Me.CurrentFocus < 20) || Me.CurrentTarget.Name == "Training Dummy")
                {
                    if (CastSpell("Dire Beast"))
                    {
                        Logging.Write(Colors.Aqua, ">> Dire Beast <<");
                    }
                }
                if (BeastMasterSettings.Instance.TL3_FV && Me.CurrentFocus < 40)
                {
                    if (CastSpell("Fervor"))
                    {
                        Logging.Write(Colors.Aqua, ">> Fervor <<");
                    }
                }
                if (BeastMasterSettings.Instance.TL4_BSTRK && Me.GotAlivePet && Me.Pet.Location.Distance(Me.CurrentTarget.Location) <= 40)
                {
                    if (CastSpell("Blink Strike"))
                    {
                        Logging.Write(Colors.Aqua, ">> Blink Strike <<");
                    }
                }
                if (BeastMasterSettings.Instance.KCO && Me.GotAlivePet && Me.CurrentFocus >= 39 && Me.Pet.Location.Distance(Me.CurrentTarget.Location) <= 25)
                {
                    if (CastSpell("Kill Command"))
                    {
                        Logging.Write(Colors.Aqua, ">> Kill Command <<");
                    }
                }
                if (BeastMasterSettings.Instance.TL4_LR && ((Me.CurrentTarget.MaxHealth > 250000 && Me.CurrentTarget.CurrentHealth > 75000) || Me.CurrentTarget.Name == "Training Dummy"))
                {
                    if (CastSpell("Lynx Rush"))
                    {
                        Logging.Write(Colors.Aqua, ">> Lynx Rush <<");
                    }
                }
                if (BeastMasterSettings.Instance.TL4_AMOC && ((Me.CurrentTarget.MaxHealth > Me.MaxHealth * 2 && Me.CurrentTarget.HealthPercent > 20) 
                    || (Me.CurrentTarget.MaxHealth > Me.MaxHealth && Me.CurrentTarget.HealthPercent <= 20) 
                    || Me.CurrentTarget.Name == "Training Dummy"))
                {
                    if (CastSpell("A Murder of Crows"))
                    {
                        Logging.Write(Colors.Aqua, ">> A Murder of Crows <<");
                    }
                }
                if (BeastMasterSettings.Instance.FF && Me.GotAlivePet && Me.Pet.Auras.ContainsKey("Frenzy") && !Me.ActiveAuras.ContainsKey("The Beast Within") 
                    && ((SpellManager.Spells["Bestial Wrath"].Cooldown && SpellManager.Spells["Bestial Wrath"].CooldownTimeLeft.TotalSeconds > 9) 
                    || (!SpellManager.Spells["Bestial Wrath"].Cooldown && (Me.CurrentTarget.MaxHealth <= 200000 || Me.ActiveAuras.ContainsKey("Rapid Fire")))))
                {
                    if (BeastMasterSettings.Instance.FFS == 5 && Me.Pet.Auras["Frenzy"].StackCount >= 5)
                    {
                        if (CastSpell("Focus Fire"))
                        {
                            Logging.Write(Colors.Aqua, ">> Focus Fire: 5 Stacks <<");
                        }
                    }
                    if (BeastMasterSettings.Instance.FFS == 4 && Me.Pet.Auras["Frenzy"].StackCount >= 4)
                    {
                        if (CastSpell("Focus Fire"))
                        {
                            Logging.Write(Colors.Aqua, ">> Focus Fire: 4 Stacks <<");
                        }
                    }
                    if (BeastMasterSettings.Instance.FFS == 3 && Me.Pet.Auras["Frenzy"].StackCount >= 3)
                    {
                        if (CastSpell("Focus Fire"))
                        {
                            Logging.Write(Colors.Aqua, ">> Focus Fire: 3 Stacks <<");
                        }
                    }
                    if (BeastMasterSettings.Instance.FFS == 2 && Me.Pet.Auras["Frenzy"].StackCount >= 2)
                    {
                        if (CastSpell("Focus Fire"))
                        {
                            Logging.Write(Colors.Aqua, ">> Focus Fire: 2 Stacks <<");
                        }
                    }
                    if (BeastMasterSettings.Instance.FFS == 1 && Me.Pet.Auras["Frenzy"].StackCount >= 1)
                    {
                        if (CastSpell("Focus Fire"))
                        {
                            Logging.Write(Colors.Aqua, ">> Focus Fire: 1 Stack <<");
                        }
                    }
                }
                if (BeastMasterSettings.Instance.FF && Me.Pet.Auras.ContainsKey("Frenzy") && Me.Pet.Auras["Frenzy"].StackCount >= 1 && DebuffTime("Frenzy", Me.Pet) < 2)
                {
                    if (CastSpell("Focus Fire"))
                    {
                        Logging.Write(Colors.Aqua, ">> Focus Fire: Running out of time <<");
                    }
                }
                if (BeastMasterSettings.Instance.ARC)
                {
                    if (BeastMasterSettings.Instance.KCO)
                    {
                        if (!Me.ActiveAuras.ContainsKey("Thrill of the Hunt"))
                        {
                            if (Me.GotAlivePet && SpellManager.Spells["Kill Command"].Cooldown && SpellManager.Spells["Kill Command"].CooldownTimeLeft.TotalMilliseconds > 700)
                            {
                                if (!Me.ActiveAuras.ContainsKey("The Beast Within"))
                                {
                                    if ((Me.CurrentFocus > 60 && SpellManager.Spells["Kill Command"].CooldownTimeLeft.TotalSeconds <= 2) 
                                        || (Me.CurrentFocus > 40 && SpellManager.Spells["Kill Command"].CooldownTimeLeft.TotalSeconds > 2))
                                    {
                                        if (CastSpell("Arcane Shot"))
                                        {
                                            Logging.Write(Colors.Aqua, ">> Arcane Shot <<");
                                        }
                                    }
                                }
                                else if (Me.ActiveAuras.ContainsKey("The Beast Within"))
                                {
                                    if ((Me.CurrentFocus > 50 && SpellManager.Spells["Kill Command"].CooldownTimeLeft.TotalSeconds <= 2)
                                        || (Me.CurrentFocus > 30 && SpellManager.Spells["Kill Command"].CooldownTimeLeft.TotalSeconds > 2))
                                    {
                                        if (CastSpell("Arcane Shot"))
                                        {
                                            Logging.Write(Colors.Aqua, ">> Arcane Shot <<");
                                        }
                                    }
                                }
                            }
                            else if (!Me.GotAlivePet)
                            {
                                if (!Me.ActiveAuras.ContainsKey("The Beast Within") && Me.CurrentFocus > 20)
                                {
                                    if (CastSpell("Arcane Shot"))
                                    {
                                        Logging.Write(Colors.Aqua, ">> Arcane Shot <<");
                                    }
                                }
                                else if (Me.ActiveAuras.ContainsKey("The Beast Within") && Me.CurrentFocus > 10)
                                {
                                    if (CastSpell("Arcane Shot"))
                                    {
                                        Logging.Write(Colors.Aqua, ">> Arcane Shot <<");
                                    }
                                }
                            }
                        }
                        else if (Me.ActiveAuras.ContainsKey("Thrill of the Hunt"))
                        {
                            if (Me.GotAlivePet && SpellManager.Spells["Kill Command"].Cooldown && SpellManager.Spells["Kill Command"].CooldownTimeLeft.TotalMilliseconds > 500)
                            { 
                                if (CastSpell("Arcane Shot"))
                                {
                                    Logging.Write(Colors.Aqua, ">> Arcane Shot <<");
                                } 
                            }
                            else if (!Me.GotAlivePet)
                            {
                                if (CastSpell("Arcane Shot"))
                                {
                                    Logging.Write(Colors.Aqua, ">> Arcane Shot <<");
                                }
                            }
                        }
                    }
                    else if (!BeastMasterSettings.Instance.KCO)
                    {
                        if (!Me.ActiveAuras.ContainsKey("Thrill of the Hunt"))
                        {
                            if (!Me.ActiveAuras.ContainsKey("The Beast Within") && Me.CurrentFocus > 20)
                            {
                                if (CastSpell("Arcane Shot"))
                                {
                                    Logging.Write(Colors.Aqua, ">> Arcane Shot <<");
                                }
                            }
                            else if (Me.ActiveAuras.ContainsKey("The Beast Within") && Me.CurrentFocus > 10)
                            {
                                if (CastSpell("Arcane Shot"))
                                {
                                    Logging.Write(Colors.Aqua, ">> Arcane Shot <<");
                                }
                            }
                        }
                        else if (Me.ActiveAuras.ContainsKey("Thrill of the Hunt"))
                        {
                            if (CastSpell("Arcane Shot"))
                            {
                                Logging.Write(Colors.Aqua, ">> Arcane Shot <<");
                            }
                        }
                    }
                }
                if (Me.CurrentFocus > 100 && Me.IsCasting && (Me.CastingSpell.Name == "Cobra Shot" || Me.CastingSpell.Name == "Steady Shot") && Me.CurrentCastTimeLeft.TotalMilliseconds > 700)
                {
                    SpellManager.StopCasting();
                    Logging.Write(Colors.Red, ">> Stop Cobra Shot <<");
                }
                
                if (!Me.IsCasting && ((!SpellManager.CanCast("Kill Shot") && SpellManager.Spells["Kill Shot"].CooldownTimeLeft.TotalSeconds > 1) || Me.CurrentTarget.HealthPercent > 20) 
                    && ((!Me.ActiveAuras.ContainsKey("The Beast Within") && (Me.CurrentFocus >= 40 && SpellManager.Spells["Kill Command"].CooldownTimeLeft.TotalSeconds > 1) || Me.CurrentFocus < 39)
                    || (Me.ActiveAuras.ContainsKey("The Beast Within") && (Me.CurrentFocus >= 20 && SpellManager.Spells["Kill Command"].CooldownTimeLeft.TotalSeconds > 1) || Me.CurrentFocus < 19)))
                {
                    if (Me.Level >= 81)
                    {
                        if ((Me.CurrentFocus < BeastMasterSettings.Instance.FocusShots || (Me.ActiveAuras.ContainsKey("The Beast Within") && Me.CurrentFocus < BeastMasterSettings.Instance.FocusShots * 0.5))
                            || (MyDebuffTime("Serpent Sting", Me.CurrentTarget) < 9 && Me.CurrentFocus < 90))
                        {
                            Lua.DoString("RunMacroText('/cast Cobra Shot');");
                            {
                                Logging.Write(Colors.Aqua, ">> Cobra Shot <<");
                            }
                        }
                    }
                    else if (Me.Level < 81)
                    {
                        if ((Me.CurrentFocus < BeastMasterSettings.Instance.FocusShots || (Me.ActiveAuras.ContainsKey("The Beast Within") && Me.CurrentFocus < BeastMasterSettings.Instance.FocusShots * 0.5)))
                        {
                            Lua.DoString("RunMacroText('/cast Steady Shot');");
                            {
                                Logging.Write(Colors.Aqua, ">> Steady Shot <<");
                            }
                        }
                    }
                }       
            }
            //////////////////////////////////////////////AoE Rotation here/////////////////////////////////////////////////////////////////////////////////////////////////
            if (addCount() >= BeastMasterSettings.Instance.Mobs && HaltFeign() && Me.GotTarget && Me.CurrentTarget.IsAlive && !Me.Mounted && !SelfControl(Me.CurrentTarget) && (BeastMasterSettings.Instance.MS || BeastMasterSettings.Instance.TL))
            {
                if (Me.CurrentTarget.Distance >= 5)
                {
                    if (BeastMasterSettings.Instance.TL && SpellManager.Spells["Explosive Trap"].CooldownTimeLeft.TotalSeconds < 1 && Me.CurrentTarget.InLineOfSight)
                    {
                        if (!Me.HasAura("Trap Launcher"))
                        {
                            if (CastSpell("Trap Launcher"))
                            {
                            Logging.Write(Colors.Crimson, ">> Trap Launcher Activated! <<");                    
                            }
                        }
                        else if (Me.HasAura("Trap Launcher"))
                        {
                            Lua.DoString("CastSpellByName('Explosive Trap');");
                            {
                                SpellManager.ClickRemoteLocation(StyxWoW.Me.CurrentTarget.Location);
                                Logging.Write(Colors.Crimson, ">> Explosive Trap Launched! <<");
                            }
                        }
                    }
                }
                else if (Me.CurrentTarget.Distance < 5)
                {
                    if (BeastMasterSettings.Instance.TL && SpellManager.Spells["Explosive Trap"].CooldownTimeLeft.TotalSeconds < 1)
                    {
                        if (Me.HasAura("Trap Launcher"))
                        {
                            if (CastSpell("Trap Launcher"))
                            {
                                Logging.Write(Colors.Crimson, ">> Trap Launcher Deactivated! <<");
                            }
                        }

                        else if (!Me.HasAura("Trap Launcher"))
                        {
                            if (CastSpell("Explosive Trap"))
                            {
                                Logging.Write(Colors.Crimson, ">> Dropping Explosive Trap <<");
                            }
                        }
                    }              
                }
                if (BeastMasterSettings.Instance.FF && Me.GotAlivePet && Me.Pet.Auras.ContainsKey("Frenzy") && !Me.ActiveAuras.ContainsKey("The Beast Within")
                    && ((SpellManager.Spells["Bestial Wrath"].Cooldown && SpellManager.Spells["Bestial Wrath"].CooldownTimeLeft.TotalSeconds > 9)
                    || (!SpellManager.Spells["Bestial Wrath"].Cooldown && (Me.CurrentTarget.MaxHealth <= 200000 || Me.ActiveAuras.ContainsKey("Rapid Fire")))))
                {
                    if (BeastMasterSettings.Instance.FFS == 5 && Me.Pet.Auras["Frenzy"].StackCount >= 5)
                    {
                        if (CastSpell("Focus Fire"))
                        {
                            Logging.Write(Colors.Crimson, ">> Focus Fire: 5 Stacks <<");
                        }
                    }
                    if (BeastMasterSettings.Instance.FFS == 4 && Me.Pet.Auras["Frenzy"].StackCount >= 4)
                    {
                        if (CastSpell("Focus Fire"))
                        {
                            Logging.Write(Colors.Crimson, ">> Focus Fire: 4 Stacks <<");
                        }
                    }
                    if (BeastMasterSettings.Instance.FFS == 3 && Me.Pet.Auras["Frenzy"].StackCount >= 3)
                    {
                        if (CastSpell("Focus Fire"))
                        {
                            Logging.Write(Colors.Crimson, ">> Focus Fire: 3 Stacks <<");
                        }
                    }
                    if (BeastMasterSettings.Instance.FFS == 2 && Me.Pet.Auras["Frenzy"].StackCount >= 2)
                    {
                        if (CastSpell("Focus Fire"))
                        {
                            Logging.Write(Colors.Crimson, ">> Focus Fire: 2 Stacks <<");
                        }
                    }
                    if (BeastMasterSettings.Instance.FFS == 1 && Me.Pet.Auras["Frenzy"].StackCount >= 1)
                    {
                        if (CastSpell("Focus Fire"))
                        {
                            Logging.Write(Colors.Crimson, ">> Focus Fire: 1 Stack <<");
                        }
                    }
                }
                if (BeastMasterSettings.Instance.FF && Me.Pet.Auras.ContainsKey("Frenzy") && Me.Pet.Auras["Frenzy"].StackCount >= 1 && DebuffTime("Frenzy", Me.Pet) < 2)
                {
                    if (CastSpell("Focus Fire"))
                    {
                        Logging.Write(Colors.Crimson, ">> Focus Fire: Running out of time <<");
                    }
                }
                if (Me.GotAlivePet && BeastMasterSettings.Instance.BWR && Me.Pet.Location.Distance(Me.CurrentTarget.Location) <= 25 && !Me.ActiveAuras.ContainsKey("The Beast Within"))
                {
                    if (CastSpell("Bestial Wrath"))
                    {
                        Logging.Write(Colors.Crimson, ">> Bestial Wrath, AoE <<");
                    }
                }
                if (BeastMasterSettings.Instance.KSH && Me.CurrentTarget.HealthPercent < 20 && (Me.CurrentFocus < 40 || (Me.ActiveAuras.ContainsKey("The Beast Within") && Me.CurrentFocus < 20)))
                {
                    if (CastSpell("Kill Shot"))
                    {
                        Logging.Write(Colors.Crimson, ">> Kill Shot <<");
                    }
                }
                if (Me.CurrentPendingCursorSpell == null && BeastMasterSettings.Instance.MS && (!Me.Pet.HasAura("Beast Cleave") || DebuffTime("Beast Cleave", Me.Pet) < 1 
                    || Me.CurrentFocus > 70 || Me.ActiveAuras.ContainsKey("The Beast Within") || Me.ActiveAuras.ContainsKey("Thrill of the Hunt")))
                {
                    if (CastSpell("Multi-Shot"))
                    {
                        Logging.Write(Colors.Crimson, ">> Multi-Shot <<");
                    }
                }
                if (BeastMasterSettings.Instance.TL3_DB && BeastMasterSettings.Instance.AOEDB && Me.CurrentFocus < 40 && !Me.ActiveAuras.ContainsKey("The Beast Within"))
                {
                    if (CastSpell("Dire Beast"))
                    {
                        Logging.Write(Colors.Crimson, ">> Dire Beast, AoE <<");
                    }
                }
                if (BeastMasterSettings.Instance.TL3_FV && Me.CurrentFocus < 50)
                {
                    if (CastSpell("Fervor"))
                    {
                        Logging.Write(Colors.Crimson, ">> AoE Fervor <<");
                    }
                }
                if (BeastMasterSettings.Instance.AOELR && Me.CurrentFocus < 40 && !Me.ActiveAuras.ContainsKey("The Beast Within") 
                    && ((Me.CurrentTarget.MaxHealth > 250000 && Me.CurrentTarget.CurrentHealth > 75000) || Me.CurrentTarget.Name == "Training Dummy"))
                {
                    if (CastSpell("Lynx Rush"))
                    {
                        Logging.Write(Colors.Crimson, ">> Lynx Rush, AoE <<");
                    }
                }
                if (BeastMasterSettings.Instance.BRA && !WoWSpell.FromId(93433).Cooldown && Me.Pet.Location.Distance(Me.CurrentTarget.Location) < 5)
                {
                    Lua.DoString("RunMacroText('/cast Burrow Attack');");
                    {
                        Logging.Write(Colors.Crimson, ">> Pet AoE: Burrow Attack <<");
                    }
                }
                if (!SpellManager.CanCast("Kill Shot") && (Me.CurrentFocus < 40 || (Me.ActiveAuras.ContainsKey("The Beast Within") && Me.CurrentFocus < 20)
                    || (!Me.ActiveAuras.ContainsKey("The Beast Within") && !Me.ActiveAuras.ContainsKey("Thrill of the Hunt") && Me.CurrentFocus <= 68 && Me.Pet.HasAura("Beast Cleave") && DebuffTime("Beast Cleave", Me.Pet) > 1)))
                {
                    if (Me.Level >= 81)
                    {
                        Lua.DoString("RunMacroText('/cast Cobra Shot');");
                        {
                            Logging.Write(Colors.Crimson, ">> AoE Cobra Shot <<");
                        }
                    }
                    else if (Me.Level < 81)
                    {
                        Lua.DoString("RunMacroText('/cast Steady Shot');");
                        {
                            Logging.Write(Colors.Crimson, ">> AoE Steady Shot <<");
                        }
                    }
                }
            }
        }
        #endregion
    }
}