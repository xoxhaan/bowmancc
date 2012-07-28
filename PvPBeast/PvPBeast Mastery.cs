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

namespace PvPBeast
{
    class Classname : CombatRoutine
    {
        public override sealed string Name { get { return "PvPBeast Mastery CC v. 1.0"; } }

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
            Logging.Write(Color.White, "________________________________________");
            Logging.Write(Color.Crimson, "------ PvPBeast Mastery Hunter CC  -------");
            Logging.Write(Color.Crimson, "----------- v. 1.0 by FallDown ------------");
            Logging.Write(Color.Crimson, "---- Credit to ZenLulz for some of the code ----");
            Logging.Write(Color.White, "________________________________________");
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

            PvPBeast.PvPBeastForm f1 = new PvPBeast.PvPBeastForm();
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
            if (!Me.ActiveAuras.ContainsKey("Feign Death"))
                return true;

            else return false;
        }
        #endregion

        #region Invulnerable
        public bool Invulnerable(WoWUnit unit)
        {
            if (unit.HasAura("Cyclone") || unit.HasAura("Dispersion") || unit.HasAura("Ice Block") || unit.HasAura("Deterrence") || unit.HasAura("Divine Shield") ||
                unit.HasAura("Hand of Protection") || (unit.HasAura("Anti-Magic Shell") && unit.HasAura("Icebound Fortitude")))
                return true;

            else return false;
        }
        #endregion

        #region Dumb Bear
        public bool DumbBear(WoWUnit unit)
        {
            if (unit.Class == WoWClass.Druid && unit.Auras.ContainsKey("Bear Form") && unit.Auras.ContainsKey("Frenzied Regeneration") && unit.HealthPercent > 9 && unit.Distance > 9)
                return true;

            else return false;
        }
        #endregion

        #region Class type
        public bool MeleeClass(WoWUnit unit)
        {
            if (Me.GotTarget && (unit.Class == WoWClass.Rogue || unit.Class == WoWClass.Warrior || unit.Class == WoWClass.DeathKnight ||
                (unit.Class == WoWClass.Paladin && Me.CurrentTarget.MaxMana < 90000) || 
                (unit.Class == WoWClass.Druid && (Me.CurrentTarget.Auras.ContainsKey("Cat Form") || Me.CurrentTarget.Auras.ContainsKey("Bear Form")))))
                return true;

            else return false;
        }
        public bool RangedClass(WoWUnit unit)
        {
            if (Me.GotTarget && (unit.Class == WoWClass.Hunter || unit.Class == WoWClass.Shaman || unit.Class == WoWClass.Priest ||
                unit.Class == WoWClass.Mage || unit.Class == WoWClass.Warlock || (unit.Class == WoWClass.Paladin && Me.CurrentTarget.MaxMana >= 90000) ||
                (unit.Class == WoWClass.Druid && !Me.CurrentTarget.Auras.ContainsKey("Cat Form") && !Me.CurrentTarget.Auras.ContainsKey("Bear Form"))))
                return true;

            else return false;
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

        #region Alive Hostile Enemy
        public bool HostilePlayer(WoWUnit unit)
        {
            if (Me.GotTarget && unit.IsPlayer && unit.IsHostile && unit.IsAlive)
                return true;

            else return false;
        }

        public bool HostileNPC(WoWUnit unit)
        {
            if (unit.IsHostile && unit.IsAlive)
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
                if (u.IsAlive
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
        #endregion

        #region CastSpell Method
        // Credit to Apoc for the below CastSpell code
        // Used for calling CastSpell in the Combat Rotation
        //Credit to Wulf!
        public bool CastSpell(string spellName)
        {
            if (SpellManager.CanCast(spellName))
            {
                SpellManager.Cast(spellName);
                // We managed to cast the spell, so return true, saying we were able to cast it.
                return true;
            }
            // Can't cast the spell right now, so return false.
            return false;
        }
        #endregion

        #region MyDebuffTime
        //Used for checking how the time left on "my" debuff
        private int MyDebuffTime(String spellName, WoWUnit unit)
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
            return 0;
        }
        #endregion

        #region DebuffTime
        //Used for checking debuff timers
        private int DebuffTime(String spellName, WoWUnit unit)
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
            return 0;
        }
        #endregion

        #region IsMyAuraActive
        //Used for checking auras that has no time
        private bool IsMyAuraActive(WoWUnit Who, String What)
        {
            return Who.GetAllAuras().Where(p => p.CreatorGuid == Me.Guid && p.Name == What).FirstOrDefault() != null;
        }
        #endregion

        // Big thanks and credit to ZenLulz for all the movement imparement related code.
        #region Movement Imparement
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
        private static List<WoWSpellMechanic> slowMechanic = new List<WoWSpellMechanic>()
        {
            WoWSpellMechanic.Dazed,
            WoWSpellMechanic.Shackled,
            WoWSpellMechanic.Slowed,
            WoWSpellMechanic.Snared
        };
        private static List<WoWSpellMechanic> rootMechanic = new List<WoWSpellMechanic>()
        {
            WoWSpellMechanic.Rooted
        };
        private static List<WoWSpellMechanic> stunMechanic = new List<WoWSpellMechanic>()
        {
            WoWSpellMechanic.Stunned,
            WoWSpellMechanic.Frozen,
            WoWSpellMechanic.Fleeing,
            WoWSpellMechanic.Horrified
        };
        private static List<WoWSpellMechanic> forsakenMechanic = new List<WoWSpellMechanic>()
        {
            WoWSpellMechanic.Asleep,
            WoWSpellMechanic.Horrified,
            WoWSpellMechanic.Fleeing,
            WoWSpellMechanic.Charmed
        };

        public static TimeSpan isForsaken(WoWUnit unit)
        {
            TimeSpan tsTimeRemaining = new TimeSpan(0, 0, 0);
            foreach (WoWAura aura in unit.Auras.Values)
            {
                if (forsakenMechanic.Contains(WoWSpell.FromId(aura.SpellId).Mechanic))
                {
                    if (tsTimeRemaining < aura.TimeLeft)
                        tsTimeRemaining = aura.TimeLeft;
                }
            }
            return tsTimeRemaining;
        }

        public static TimeSpan isStunned(WoWUnit unit)
        {
            TimeSpan tsTimeRemaining = new TimeSpan(0, 0, 0);
            foreach (WoWAura aura in unit.Auras.Values)
            {
                if (stunMechanic.Contains(WoWSpell.FromId(aura.SpellId).Mechanic))
                {
                    if (tsTimeRemaining < aura.TimeLeft)
                        tsTimeRemaining = aura.TimeLeft;
                }
            }
            return tsTimeRemaining;
        }

        public static bool isSlowed(WoWUnit unit)
        {
            if (unit.MovementInfo.RunSpeed < 4.5)
                return true;
            else
                return false;
        }

        public static TimeSpan isControlled(WoWUnit unit)
        {
            TimeSpan tsTimeRemaining = new TimeSpan(0, 0, 0);
            foreach (WoWAura aura in unit.Auras.Values)
            {
                if (controlMechanic.Contains(WoWSpell.FromId(aura.SpellId).Mechanic) || aura.SpellId == 19503)
                {
                    if (tsTimeRemaining < aura.TimeLeft)
                        tsTimeRemaining = aura.TimeLeft;
                }
            }
            return tsTimeRemaining;
        }

        public static TimeSpan isRooted(WoWUnit unit)
        {
            TimeSpan tsTimeRemaining = new TimeSpan(0, 0, 0);
            foreach (WoWAura aura in unit.Auras.Values)
            {
                if (rootMechanic.Contains(WoWSpell.FromId(aura.SpellId).Mechanic) || isStunned(Me).Milliseconds > 0)
                {
                    if (tsTimeRemaining < aura.TimeLeft)
                        tsTimeRemaining = aura.TimeLeft;
                }
            }
            return tsTimeRemaining;
        }
        #endregion

        // Following region is also from ZenLulz, if you're reading this and see him on the forums, +rep him!
        #region Snare Check

        public static bool isValid(WoWUnit unit)
        {
            return unit != null && unit.IsValid && !unit.Dead && ((unit.IsPet && !unit.OwnedByUnit.IsPlayer) || !unit.IsPet);
        }

        public static bool NeedSnare(WoWUnit unit)
        {
            if (isValid(unit))
            {
                if (isSlowed(unit))
                    return false;
                else if (unit.HasAura("Divine Shield"))
                    return false;
                else if (unit.HasAura("Hand Of Freedom"))
                    return false;
                else if (unit.HasAura("Hand Of Protection"))
                    return false;
                else if (unit.HasAura("Master's Call"))
                    return false;
                else if (unit.HasAura("Pillar Of Frost"))
                    return false;
                else if (unit.IsWithinMeleeRange && isControlled(unit).TotalMilliseconds > 0)
                    return false;

                return true;
            }

            return false;
        }
        #endregion

        #region rest
        public override bool NeedRest
        {
            get
            {
                if (StyxWoW.IsInWorld && !Me.IsGhost && !Me.GotAlivePet && !Me.Dead && !Me.Mounted)
                {
                    if (PvPBeastSettings.Instance.RP && !Me.GotAlivePet && SpellManager.HasSpell("Revive Pet"))
                    {
                        if (CastSpell("Revive Pet"))
                            StyxWoW.SleepForLagDuration();
                    }
                }
                if (PvPBeastSettings.Instance.CP && Me.Pet == null && !Me.IsCasting)
                {
                    if (PvPBeastSettings.Instance.PET == 1 && SpellManager.HasSpell("Call Pet 1"))
                    {
                        SpellManager.Cast("Call Pet 1");
                        StyxWoW.SleepForLagDuration();
                    }
                    if (PvPBeastSettings.Instance.PET == 2 && SpellManager.HasSpell("Call Pet 2"))
                    {
                        SpellManager.Cast("Call Pet 2");
                        StyxWoW.SleepForLagDuration();
                    }
                    if (PvPBeastSettings.Instance.PET == 3 && SpellManager.HasSpell("Call Pet 3"))
                    {
                        SpellManager.Cast("Call Pet 3");
                        StyxWoW.SleepForLagDuration();
                    }
                    if (PvPBeastSettings.Instance.PET == 4 && SpellManager.HasSpell("Call Pet 4"))
                    {
                        SpellManager.Cast("Call Pet 4");
                        StyxWoW.SleepForLagDuration();
                    }
                    if (PvPBeastSettings.Instance.PET == 5 && SpellManager.HasSpell("Call Pet 5"))
                    {
                        SpellManager.Cast("Call Pet 5");
                        StyxWoW.SleepForLagDuration();
                    }
                    StyxWoW.SleepForLagDuration();
                }
                return true;
            }
        }
        #endregion

        #region CombatStart

        private void AutoAttack()
        {
            if (Me.GotTarget && Me.Pet.CurrentTargetGuid != Me.CurrentTargetGuid && Me.GotAlivePet && !SelfControl(Me.CurrentTarget))
            {
                Lua.DoString("PetAttack()");
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
                Logging.Write(Color.Aqua, ">> Stop Everything! <<");
            }
            SpellManager.StopCasting();
            {
                Logging.Write(Color.Aqua, ">> Stop Everything! <<");
            }
        }
        if (!SelfControl(Me.CurrentTarget))
        {
            if (Me.GotTarget && Me.GotAlivePet && Me.Pet.CurrentTargetGuid != Me.CurrentTargetGuid && !SelfControl(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget))
            {
                Lua.DoString("PetAttack()");
            }
            if (PvPBeastSettings.Instance.TLF && Me.HasAura("Trap Launcher") && MyDebuffTime("Trap Launcher", Me) < 13 && (Me.CurrentTarget == null || !Me.CurrentTarget.InLineOfSight || Me.CurrentTarget.Distance > 40 || Me.CurrentTarget.Distance < 8))
            {
                if (PvPBeastSettings.Instance.TL && SpellManager.Spells["Ice Trap"].CooldownTimeLeft.TotalSeconds > 1)
                {
                    Lua.DoString("RunMacroText('/cancelaura Trap Launcher');");
                }
                else if (PvPBeastSettings.Instance.TL2 && SpellManager.Spells["Snake Trap"].CooldownTimeLeft.TotalSeconds > 1)
                {
                    Lua.DoString("RunMacroText('/cancelaura Trap Launcher');");
                }
                else if (PvPBeastSettings.Instance.TL3 && SpellManager.Spells["Freezing Trap"].CooldownTimeLeft.TotalSeconds > 1)
                {
                    Lua.DoString("RunMacroText('/cancelaura Trap Launcher');");
                }
                else if (PvPBeastSettings.Instance.TL4 && SpellManager.Spells["Explosive Trap"].CooldownTimeLeft.TotalSeconds > 1)
                {
                    Lua.DoString("RunMacroText('/cancelaura Trap Launcher');");
                }
            }
            if (PvPBeastSettings.Instance.FT && !Me.Mounted && !Me.Dead)
            {
                if (Me.GotTarget && Me.CurrentTarget.IsAlive && !Me.CurrentTarget.IsPet && Me.FocusedUnit != Me.CurrentTarget)
                {
                    Me.SetFocus(Me.CurrentTarget);
                }
                if (!Me.GotTarget && Me.FocusedUnit != Me.CurrentTarget && Me.FocusedUnit.Distance < 40 && Me.FocusedUnit.InLineOfSight)
                {
                    Me.FocusedUnit.Target();
                }
            }
            if (Me.GotTarget && Me.CurrentTarget.IsAlive && !Me.Mounted && !Me.Dead && HaltTrap() && HaltFeign())
            {
                if (PvPBeastSettings.Instance.MP && Me.GotAlivePet && !Me.Pet.ActiveAuras.ContainsKey("Mend Pet") && (isStunned(Me.Pet).TotalSeconds > 0 || isForsaken(Me.Pet).TotalSeconds > 0 || isRooted(Me.Pet).TotalMilliseconds > 0 || isSlowed(Me.Pet) || isControlled(Me.Pet).TotalSeconds > 0 || Me.Pet.HealthPercent < PvPBeastSettings.Instance.MendHealth))
                {
                    if (CastSpell("Mend Pet"))
                    {
                        Logging.Write(Color.Aqua, ">> Mend Pet <<");
                    }
                }
                if (PvPBeastSettings.Instance.RS && Me.Race == WoWRace.Draenei && Me.HealthPercent < 30 && !SpellManager.Spells["Gift of the Naaru"].Cooldown)
                {
                    if (CastSpell("Gift of the Naaru"))
                    {
                        Logging.Write(Color.Aqua, ">> Gift of the Naaru <<");
                    }
                }
                //////////////////////////////// Trinkets ///////////////////////////////////////
                if (PvPBeastSettings.Instance.T1MOB && Me.Inventory.Equipped.Trinket1 != null && Me.Inventory.Equipped.Trinket1.Cooldown <= 0 && (isStunned(Me).TotalSeconds > 2 || isControlled(Me).TotalSeconds > 2))
                {
                    Lua.DoString("RunMacroText('/use 13');");
                }
                if (PvPBeastSettings.Instance.T1DMG && !Invulnerable(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget) && HostilePlayer(Me.CurrentTarget) && Me.CurrentTarget.HealthPercent > 15 && Me.CurrentTarget.Distance > 6 && Me.Inventory.Equipped.Trinket1 != null && Me.Inventory.Equipped.Trinket1.Cooldown <= 0)
                {
                    Lua.DoString("RunMacroText('/use 13');");
                }
                if (PvPBeastSettings.Instance.T1DEF && !Invulnerable(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget) && HostilePlayer(Me.CurrentTarget) && Me.HealthPercent < 50 && Me.Inventory.Equipped.Trinket1 != null && Me.Inventory.Equipped.Trinket1.Cooldown <= 0 && (Me.CurrentTarget.Distance < 20 || Me.CurrentTarget.IsCasting))
                {
                    Lua.DoString("RunMacroText('/use 13');");
                }
                if (PvPBeastSettings.Instance.T2MOB && Me.Inventory.Equipped.Trinket2 != null && Me.Inventory.Equipped.Trinket2.Cooldown <= 0 && (isStunned(Me).TotalSeconds > 2 || isControlled(Me).TotalSeconds > 2))
                {
                    Lua.DoString("RunMacroText('/use 14');");
                }
                if (PvPBeastSettings.Instance.T2DMG && !Invulnerable(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget) && HostilePlayer(Me.CurrentTarget) && Me.CurrentTarget.HealthPercent > 15 && Me.CurrentTarget.Distance > 9 && Me.Inventory.Equipped.Trinket2 != null && Me.Inventory.Equipped.Trinket2.Cooldown <= 0)
                {
                    Lua.DoString("RunMacroText('/use 14');");
                }
                if (PvPBeastSettings.Instance.T2DEF && !Invulnerable(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget) && HostilePlayer(Me.CurrentTarget) && Me.HealthPercent < 50 && Me.Inventory.Equipped.Trinket2 != null && Me.Inventory.Equipped.Trinket2.Cooldown <= 0 && (Me.CurrentTarget.Distance < 20 || Me.CurrentTarget.IsCasting))
                {
                    Lua.DoString("RunMacroText('/use 14');");
                }
                /////////////////////////////Close Combat and Defense Mechanisms////////////////////////////////
                if (isSlowed(Me) || isRooted(Me).TotalMilliseconds > 0)
                {
                    if (CastSpell("Master's Call"))
                    {
                        Logging.Write(Color.Aqua, ">> Master's Call <<");
                    }
                }
                if (PvPBeastSettings.Instance.IntimidateBox == "1. Interrupt" && Me.GotAlivePet && Me.Pet.Location.Distance(Me.CurrentTarget.Location) < 9 && HostilePlayer(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget) && Me.CurrentTarget.IsCasting && Me.CanInterruptCurrentSpellCast)
                {
                    if (CastSpell("Intimidation"))
                    {
                        Logging.Write(Color.Aqua, ">> Intimidation Interrupt <<");
                    }
                }
                if (PvPBeastSettings.Instance.IntimidateBox == "2. Low Health" && Me.GotAlivePet && Me.Pet.Location.Distance(Me.CurrentTarget.Location) < 9 && HostilePlayer(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget) && Me.CurrentTarget.HealthPercent <= PvPBeastSettings.Instance.TargetHealth)
                {
                    if (CastSpell("Intimidation"))
                    {
                        Logging.Write(Color.Aqua, ">> Intimidation Target Low on Health <<");
                    }
                }
                if (PvPBeastSettings.Instance.IntimidateBox == "3. Protection" && Me.GotAlivePet && Me.Pet.Location.Distance(Me.CurrentTarget.Location) < 9 && HostilePlayer(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget) && Me.CurrentTarget.Distance < 7 && MeleeClass(Me.CurrentTarget))
                {
                    if (CastSpell("Intimidation"))
                    {
                        Logging.Write(Color.Aqua, ">> Intimidation Stranger Danger <<");
                    }
                }
                if (PvPBeastSettings.Instance.IntimidateBox == "1 + 2" && Me.GotAlivePet && Me.Pet.Location.Distance(Me.CurrentTarget.Location) < 9 && HostilePlayer(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget) && ((Me.CurrentTarget.IsCasting && Me.CanInterruptCurrentSpellCast) || Me.CurrentTarget.HealthPercent <= PvPBeastSettings.Instance.TargetHealth))
                {
                    if (CastSpell("Intimidation"))
                    {
                        Logging.Write(Color.Aqua, ">> Intimidation Interrupt <<");
                    }
                }
                if (PvPBeastSettings.Instance.IntimidateBox == "1 + 3" && Me.GotAlivePet && Me.Pet.Location.Distance(Me.CurrentTarget.Location) < 9 && HostilePlayer(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget) && ((Me.CurrentTarget.IsCasting && Me.CanInterruptCurrentSpellCast) || (Me.CurrentTarget.Distance < 7 && MeleeClass(Me.CurrentTarget))))
                {
                    if (CastSpell("Intimidation"))
                    {
                        Logging.Write(Color.Aqua, ">> Intimidation Interrupt <<");
                    }
                }
                if (PvPBeastSettings.Instance.IntimidateBox == "2 + 3" && Me.GotAlivePet && Me.Pet.Location.Distance(Me.CurrentTarget.Location) < 9 && HostilePlayer(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget) && (Me.CurrentTarget.HealthPercent <= PvPBeastSettings.Instance.TargetHealth || (Me.CurrentTarget.Distance < 7 && MeleeClass(Me.CurrentTarget))))
                {
                    if (CastSpell("Intimidation"))
                    {
                        Logging.Write(Color.Aqua, ">> Intimidation Interrupt <<");
                    }
                }
                if (PvPBeastSettings.Instance.IntimidateBox == "1 + 2 + 3" && Me.GotAlivePet && Me.Pet.Location.Distance(Me.CurrentTarget.Location) < 9 && HostilePlayer(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget) && ((Me.CurrentTarget.IsCasting && Me.CanInterruptCurrentSpellCast) || (Me.CurrentTarget.Distance < 7 && MeleeClass(Me.CurrentTarget)) || (Me.CurrentTarget.HealthPercent <= PvPBeastSettings.Instance.TargetHealth)))
                {
                    if (CastSpell("Intimidation"))
                    {
                        Logging.Write(Color.Aqua, ">> Intimidation Interrupt <<");
                    }
                }
                if (PvPBeastSettings.Instance.RS && Me.Race == WoWRace.Human && !SpellManager.Spells["Every Man for Himself"].Cooldown && !PvPBeastSettings.Instance.T2MOB && !PvPBeastSettings.Instance.T1MOB && (isStunned(Me).TotalSeconds > 2 || isControlled(Me).TotalSeconds > 2))
                {
                    if (CastSpell("Every Man for Himself"))
                    {
                        Logging.Write(Color.Aqua, ">> Every Man for Himself <<");
                    }
                }
                if (PvPBeastSettings.Instance.RS && Me.Race == WoWRace.Gnome && !SpellManager.Spells["Escape Artist"].Cooldown && isSlowed(Me) || isRooted(Me).TotalMilliseconds > 0)
                {
                    if (CastSpell("Escape Artist"))
                    {
                        Logging.Write(Color.Aqua, ">> Escape Artist <<");
                    }
                }
                if (PvPBeastSettings.Instance.RS && Me.Race == WoWRace.Undead && !SpellManager.Spells["Will of The Forsaken"].Cooldown && isForsaken(Me).TotalMilliseconds > 0)
                {
                    if (CastSpell("Will of The Forsaken"))
                    {
                        Logging.Write(Color.Aqua, ">> Will of The Forsaken <<");
                    }
                }
                if (PvPBeastSettings.Instance.RS && Me.Race == WoWRace.Dwarf && !SpellManager.Spells["Stoneform"].Cooldown && StyxWoW.Me.GetAllAuras().Any(a => a.Spell.Mechanic == WoWSpellMechanic.Bleeding || a.Spell.DispelType == WoWDispelType.Disease || a.Spell.DispelType == WoWDispelType.Poison))
                {
                    if (CastSpell("Stoneform"))
                    {
                        Logging.Write(Color.Aqua, ">> Stoneform <<");
                    }
                }
                if (PvPBeastSettings.Instance.INT && Me.CurrentTarget.Distance <= 20 && !Invulnerable(Me.CurrentTarget) && Me.CurrentTarget.IsCasting && Me.CanInterruptCurrentSpellCast)
                {
                    if (CastSpell("Scatter Shot"))
                    {
                        Logging.Write(Color.Aqua, ">> Scatter Shot, Interrupt <<");
                    }
                }
                if (PvPBeastSettings.Instance.INT && Me.Race == WoWRace.Tauren && PvPBeastSettings.Instance.RS && Me.CurrentTarget.Distance < 8 && !Invulnerable(Me.CurrentTarget) && Me.CurrentTarget.IsCasting && Me.CanInterruptCurrentSpellCast && (SpellManager.Spells["Silencing Shot"].CooldownTimeLeft.TotalSeconds > 1 || Me.CurrentTarget.Distance < 5))
                {
                    if (CastSpell("War Stomp"))
                    {
                        Logging.Write(Color.Aqua, ">> War Stomp, Interrupt <<");
                    }
                }
                if (PvPBeastSettings.Instance.SCAT && Me.CurrentTarget.Distance < 10 && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && !Invulnerable(Me.CurrentTarget))
                {
                    if (CastSpell("Scatter Shot"))
                    {
                        Logging.Write(Color.Aqua, ">> Scatter Shot, Evade <<");
                    }
                }
                if (PvPBeastSettings.Instance.RS && Me.Race == WoWRace.Tauren && (Me.CurrentTarget.Distance < 8 || Me.CurrentTarget.Pet.Distance < 8) && (Me.CurrentTarget.CurrentTargetGuid == Me.Guid || Me.CurrentTarget.Pet.CurrentTargetGuid == Me.Guid) && (NeedSnare(Me.CurrentTarget) || NeedSnare(Me.CurrentTarget.Pet)) && (!Invulnerable(Me.CurrentTarget) || Me.CurrentTarget.Pet.Distance < 8))
                {
                    if (CastSpell("War Stomp"))
                    {
                        Logging.Write(Color.Aqua, ">> War Stomp, Evade <<");
                    }
                }
                if (PvPBeastSettings.Instance.INT && PvPBeastSettings.Instance.RS && Me.CurrentTarget.IsPlayer && Me.CurrentTarget.IsCasting && Me.CanInterruptCurrentSpellCast && Me.CurrentTarget.Distance >= 5)
                {
                    if (CastSpell("Arcane Torrent"))
                    {
                        Logging.Write(Color.Aqua, ">> Arcane Torrent <<");
                    }
                }
                if (PvPBeastSettings.Instance.SMend && Me.CurrentHealth < Me.MaxHealth - 9000 && !WoWSpell.FromId(90361).Cooldown)
                {
                    Lua.DoString("RunMacroText(\"/cast [@" + Me.Name + "] Spirit Mend\")");
                    {
                        Logging.Write(Color.Crimson, ">> Pet: Spirit Mend <<");
                    }
                }
                if (PvPBeastSettings.Instance.WEB && NeedSnare(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget) && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && !WoWSpell.FromId(54706).Cooldown && Me.CurrentTarget.Distance <= 30)
                {
                    Lua.DoString("RunMacroText('/cast Venom Web Spray');");
                    {
                        Logging.Write(Color.Crimson, ">> Pet: Silithid Web <<");
                    }
                }
                if (PvPBeastSettings.Instance.CONC && NeedSnare(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget) && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && DebuffTime("Wing Clip", Me.CurrentTarget) <= 1 && DebuffTime("Concussive Shot", Me.CurrentTarget) <= 1 && DebuffTime("Concussive Barrage", Me.CurrentTarget) <= 1 && Me.CurrentTarget.Distance <= 40 && Me.CurrentTarget.Distance >= 5)
                {
                    if (CastSpell("Concussive Shot"))
                    {
                        Logging.Write(Color.Aqua, ">> Concussive Shot <<");
                    }
                }
                if (PvPBeastSettings.Instance.CONC && NeedSnare(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget) && DebuffTime("Wing Clip", Me.CurrentTarget) <= 1 && DebuffTime("Concussive Shot", Me.CurrentTarget) <= 1 && DebuffTime("Concussive Barrage", Me.CurrentTarget) <= 1 && Me.CurrentTarget.Distance <= 40 && Me.CurrentTarget.Distance >= 5 && Me.CurrentTarget.IsMoving)
                {
                    if (CastSpell("Concussive Shot"))
                    {
                        Logging.Write(Color.Aqua, ">> Concussive Shot <<");
                    }
                }
                if (PvPBeastSettings.Instance.MLE && NeedSnare(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget) && !Me.IsCasting && DebuffTime("Wing Clip", Me.CurrentTarget) < 2 && Me.CurrentTarget.Distance < 5)
                {
                    if (CastSpell("Wing Clip"))
                    {
                        Logging.Write(Color.Aqua, ">> Wing Clip<<");
                    }
                }
                if (PvPBeastSettings.Instance.MLE && !Invulnerable(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget) && Me.CurrentTarget.Distance < 5 && !Me.IsCasting)
                {
                    if (CastSpell("Raptor Strike"))
                    {
                        Logging.Write(Color.Aqua, ">> Raptor Strike <<");
                    }
                }
                if (PvPBeastSettings.Instance.FDCBox == "1. Pet Near" && Me.CurrentTarget.GotAlivePet && Me.CurrentTarget.Pet.CurrentTargetGuid == Me.Guid && Me.CurrentTarget.Pet.Distance <= 5)
                {
                    if (CastSpell("Feign Death"))
                    {
                        System.Threading.Thread.Sleep(1000);
                        Logging.Write(Color.Aqua, ">> Feign Death Enemy Pet Near <<");
                    }
                }
                if (PvPBeastSettings.Instance.FDCBox == "2. Target Casting" && Me.CurrentTarget.IsPlayer && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && Me.CurrentTarget.IsCasting && Me.CanInterruptCurrentSpellCast && WoWSpell.FromId(Me.CurrentTarget.CastingSpellId).SpellEffect1.EffectType != WoWSpellEffectType.Heal)
                {
                    if (CastSpell("Feign Death"))
                    {
                        System.Threading.Thread.Sleep(1000);
                        Logging.Write(Color.Aqua, ">> Feign Death Target Is Casting <<");
                    }
                }
                if (PvPBeastSettings.Instance.FDCBox == "1 + 2" && ((Me.CurrentTarget.GotAlivePet && Me.CurrentTarget.Pet.CurrentTargetGuid == Me.Guid && Me.CurrentTarget.Pet.Distance <= 5) || (Me.CurrentTarget.IsPlayer && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && Me.CurrentTarget.IsCasting && Me.CanInterruptCurrentSpellCast && WoWSpell.FromId(Me.CurrentTarget.CastingSpellId).SpellEffect1.EffectType != WoWSpellEffectType.Heal)))
                {
                    if (CastSpell("Feign Death"))
                    {
                        System.Threading.Thread.Sleep(1000);
                        Logging.Write(Color.Aqua, ">> Feign Death Enemy Pet Near or Target is Casting <<");
                    }
                }
                if (PvPBeastSettings.Instance.FDCBox == "3. Low Health" && Me.HealthPercent < 10)
                {
                    if (CastSpell("Feign Death"))
                    {
                        System.Threading.Thread.Sleep(1000);
                        Logging.Write(Color.Aqua, ">> Feign Death: Low Health <<");
                    }
                }
                if (PvPBeastSettings.Instance.FDCBox == "1 + 2 + 3" && ((Me.CurrentTarget.GotAlivePet && Me.CurrentTarget.Pet.CurrentTargetGuid == Me.Guid && Me.CurrentTarget.Pet.Distance <= 5) || (Me.CurrentTarget.IsPlayer && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && Me.CurrentTarget.IsCasting && Me.CanInterruptCurrentSpellCast && WoWSpell.FromId(Me.CurrentTarget.CastingSpellId).SpellEffect1.EffectType != WoWSpellEffectType.Heal) || Me.HealthPercent < 10))
                {
                    if (CastSpell("Feign Death"))
                    {
                        System.Threading.Thread.Sleep(1000);
                        Logging.Write(Color.Aqua, ">> Feign Death: Enemy pet is near us or Targeting is casting a harmful spell or We're low on health.<<");
                    }
                }
                if (PvPBeastSettings.Instance.FDCBox == "1 + 3" && ((Me.CurrentTarget.GotAlivePet && Me.CurrentTarget.Pet.CurrentTargetGuid == Me.Guid && Me.CurrentTarget.Pet.Distance <= 5) || Me.HealthPercent < 10))
                {
                    if (CastSpell("Feign Death"))
                    {
                        System.Threading.Thread.Sleep(1000);
                        Logging.Write(Color.Aqua, ">> Feign Death: Enemy pet is near us or Targeting is casting a harmful spell or We're low on health.<<");
                    }
                }
                if (PvPBeastSettings.Instance.FDCBox == "2 + 3" && ((Me.CurrentTarget.IsPlayer && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && Me.CurrentTarget.IsCasting && Me.CanInterruptCurrentSpellCast && WoWSpell.FromId(Me.CurrentTarget.CastingSpellId).SpellEffect1.EffectType != WoWSpellEffectType.Heal) || Me.HealthPercent < 10))
                {
                    if (CastSpell("Feign Death"))
                    {
                        System.Threading.Thread.Sleep(1000);
                        Logging.Write(Color.Aqua, ">> Feign Death: Enemy pet is near us or Targeting is casting a harmful spell or We're low on health.<<");
                    }
                }
                if (PvPBeastSettings.Instance.DIS && !Invulnerable(Me.CurrentTarget) && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && Me.CurrentTarget.Distance <= 5 && Me.HealthPercent < 60)
                {
                    if (CastSpell("Disengage"))
                    {
                        Logging.Write(Color.Aqua, ">> Disengage <<");
                    }
                }
                if (PvPBeastSettings.Instance.DETR && Me.HealthPercent < 20 && Me.CurrentTarget.CurrentTargetGuid == Me.Guid)
                {
                    if (CastSpell("Deterrence"))
                    {
                        Logging.Write(Color.Aqua, ">> Deterrence <<");
                    }
                }
                if (PvPBeastSettings.Instance.ICET && !Invulnerable(Me.CurrentTarget) && HostilePlayer(Me.CurrentTarget) && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && Me.CurrentTarget.Distance < 5)
                {
                    if (CastSpell("Ice Trap"))
                    {
                        Logging.Write(Color.Aqua, ">> Ice Trap<<");
                    }
                }
                if (PvPBeastSettings.Instance.SNAT && !Invulnerable(Me.CurrentTarget) && HostilePlayer(Me.CurrentTarget) && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && Me.CurrentTarget.Distance < 5)
                {
                    if (CastSpell("Snake Trap"))
                    {
                        Logging.Write(Color.Aqua, ">> Snake Trap<<");
                    }
                }
                if (PvPBeastSettings.Instance.EXPT && !Invulnerable(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget) && HostilePlayer(Me.CurrentTarget) && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && Me.CurrentTarget.Distance < 5)
                {
                    if (CastSpell("Explosive Trap"))
                    {
                        Logging.Write(Color.Aqua, ">> Explosive Trap<<");
                    }
                }
                if (PvPBeastSettings.Instance.FRET && !Invulnerable(Me.CurrentTarget) && HostilePlayer(Me.CurrentTarget) && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && Me.CurrentTarget.Distance < 5)
                {
                    if (CastSpell("Freezing Trap"))
                    {
                        Logging.Write(Color.Aqua, ">> Freezing Trap<<");
                    }
                }
                /////////////////////////////////////////////////////Cooldowns are here/////////////////////////////////////////////////////////////////////////////////////////////////           
                if (PvPBeastSettings.Instance.RF && !Me.ActiveAuras.ContainsKey("Rapid Fire") && HostilePlayer(Me.CurrentTarget) && Me.CurrentTarget.HealthPercent > 20 && Me.CurrentTarget.Distance > 9 && !Invulnerable(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget))
                {
                    if (CastSpell("Rapid Fire"))
                    {
                        Logging.Write(Color.Aqua, ">> Rapid Fire <<");
                    }
                }
                if (PvPBeastSettings.Instance.BW && Me.GotAlivePet && HostilePlayer(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget) && Me.CurrentTarget.HealthPercent > 5 && Me.Pet.Location.Distance(Me.CurrentTarget.Location) < 9 && !Invulnerable(Me.CurrentTarget) && !Invulnerable(Me.Pet.CurrentTarget))
                {
                    if (CastSpell("Bestial Wrath"))
                    {
                        Logging.Write(Color.Aqua, ">> Bestial Wrath <<");
                    }
                }
                if (PvPBeastSettings.Instance.FervorBox == "Low Focus" && Me.CurrentFocus < 40 && HostilePlayer(Me.CurrentTarget) && Me.Pet.Location.Distance(Me.CurrentTarget.Location) < 9 && !Invulnerable(Me.CurrentTarget) && !Invulnerable(Me.Pet.CurrentTarget))
                {
                    if (CastSpell("Fervor"))
                    {
                        Logging.Write(Color.Aqua, ">> Fervor: Low Focus <<");
                    }
                }
                if (PvPBeastSettings.Instance.FervorBox == "Target Low Health" && Me.CurrentFocus < 40 && Me.CurrentTarget.HealthPercent < PvPBeastSettings.Instance.TargetHealth && HostilePlayer(Me.CurrentTarget) && Me.Pet.Location.Distance(Me.CurrentTarget.Location) < 9 && !Invulnerable(Me.CurrentTarget) && !Invulnerable(Me.Pet.CurrentTarget))
                {
                    if (CastSpell("Fervor"))
                    {
                        Logging.Write(Color.Aqua, ">> Fervor: Target Low on Health <<");
                    }
                }
                if (PvPBeastSettings.Instance.ROS && Me.GotAlivePet && !WoWSpell.FromId(53434).Cooldown && HostilePlayer(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget))
                {
                    Lua.DoString("RunMacroText('/cast Call of the Wild');");
                    {
                        Logging.Write(Color.Crimson, ">> Pet: Call of the Wild <<");
                    }
                }
                if (PvPBeastSettings.Instance.LB && HostilePlayer(Me.CurrentTarget) && Me.CurrentTarget.HealthPercent > 10 && !Invulnerable(Me.CurrentTarget))
                {
                    if (CastSpell("Lifeblood"))
                    {
                        Logging.Write(Color.Aqua, ">> Lifeblood <<");
                    }
                }
                if (PvPBeastSettings.Instance.GE && !Invulnerable(Me.CurrentTarget) && HostilePlayer(Me.CurrentTarget) && Me.Inventory.Equipped.Hands != null && Me.Inventory.Equipped.Hands.Usable && Me.Inventory.Equipped.Hands.CooldownTimeLeft.TotalSeconds == 0 && Me.CurrentTarget.HealthPercent > 10)
                {
                    Lua.DoString("RunMacroText('/use 10');");
                }
                //////////////////////////////////////////////////Racial Skills here/////////////////////////////////////////////////////////////////////////////////////////
                if (PvPBeastSettings.Instance.RS && !Invulnerable(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget) && Me.Race == WoWRace.Troll && HostilePlayer(Me.CurrentTarget) && Me.CurrentTarget.HealthPercent > 20 && !SpellManager.Spells["Berserking"].Cooldown)
                {
                    Lua.DoString("RunMacroText('/Cast Berserking');");
                }
                if (PvPBeastSettings.Instance.RS && !Invulnerable(Me.CurrentTarget) && Me.Race == WoWRace.Orc && HostilePlayer(Me.CurrentTarget) && Me.CurrentTarget.HealthPercent > 20 && !SpellManager.Spells["Blood Fury"].Cooldown)
                {
                    Lua.DoString("RunMacroText('/Cast Blood Fury');");
                }
            }
            ////////////////////////////////////////////////// Trap Launchers /////////////////////////////////////////////////////////////////////////
            if (PvPBeastSettings.Instance.TL4 && HostilePlayer(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget) && Me.CurrentTarget.InLineOfSight && !Me.CurrentTarget.IsMoving && !SpellManager.Spells["Explosive Trap"].Cooldown && !Me.HasAura("Trap Launcher") && Me.CurrentTarget.Distance >= 20 && Me.CurrentTarget.Distance < 40 && Me.CurrentTarget.HealthPercent > 20)
            {
                if (CastSpell("Trap Launcher"))
                {
                    Logging.Write(Color.Red, ">> Trap Launcher Activated! <<");
                }
            }
            if (Me.HasAura("Trap Launcher") && PvPBeastSettings.Instance.TL4)
            {
                Lua.DoString("CastSpellByName('Explosive Trap');");
                {
                    LegacySpellManager.ClickRemoteLocation(StyxWoW.Me.CurrentTarget.Location);
                }
            }
            if (PvPBeastSettings.Instance.TL && MeleeClass(Me.CurrentTarget) && HostilePlayer(Me.CurrentTarget) && Me.CurrentTarget.InLineOfSight && !Me.CurrentTarget.IsMoving && !SpellManager.Spells["Ice Trap"].Cooldown && !Me.HasAura("Trap Launcher") && Me.CurrentTarget.Distance >= 20 && Me.CurrentTarget.Distance < 40 && Me.CurrentTarget.HealthPercent > 20)
            {
                if (CastSpell("Trap Launcher"))
                {
                    Logging.Write(Color.Red, ">> Trap Launcher Activated! <<");
                }
            }
            if (Me.HasAura("Trap Launcher") && PvPBeastSettings.Instance.TL && MeleeClass(Me.CurrentTarget))
            {
                Lua.DoString("CastSpellByName('Ice Trap');");
                {
                    LegacySpellManager.ClickRemoteLocation(StyxWoW.Me.CurrentTarget.Location);
                }
            }
            if (PvPBeastSettings.Instance.TL2 && MeleeClass(Me.CurrentTarget) && HostilePlayer(Me.CurrentTarget) && Me.CurrentTarget.InLineOfSight && !Me.CurrentTarget.IsMoving && !SpellManager.Spells["Snake Trap"].Cooldown && !Me.HasAura("Trap Launcher") && Me.CurrentTarget.Distance >= 20 && Me.CurrentTarget.Distance < 40 && Me.CurrentTarget.HealthPercent > 20)
            {
                if (CastSpell("Trap Launcher"))
                {
                    Logging.Write(Color.Red, ">> Trap Launcher Activated! <<");
                }
            }
            if (Me.HasAura("Trap Launcher") && PvPBeastSettings.Instance.TL2 && MeleeClass(Me.CurrentTarget))
            {
                Lua.DoString("CastSpellByName('Snake Trap');");
                {
                    LegacySpellManager.ClickRemoteLocation(StyxWoW.Me.CurrentTarget.Location);
                }
            }
            if (PvPBeastSettings.Instance.TL3 && RangedClass(Me.CurrentTarget) && HostilePlayer(Me.CurrentTarget) && Me.CurrentTarget.InLineOfSight && !Me.CurrentTarget.IsMoving && !SpellManager.Spells["Freezing Trap"].Cooldown && !Me.HasAura("Trap Launcher") && Me.CurrentTarget.Distance >= 20 && Me.CurrentTarget.Distance < 40 && Me.CurrentTarget.HealthPercent > 20)
            {
                if (CastSpell("Trap Launcher"))
                {
                    Logging.Write(Color.Red, ">> Trap Launcher Activated! <<");
                }
            }
            if (Me.HasAura("Trap Launcher") && PvPBeastSettings.Instance.TL3 && RangedClass(Me.CurrentTarget))
            {
                Lua.DoString("CastSpellByName('Freezing Trap');");
                {
                    LegacySpellManager.ClickRemoteLocation(StyxWoW.Me.CurrentTarget.Location);
                }
            }
            ////////////////////////////////////////////////// Beast Mastery Rotation /////////////////////////////////////////////////////////////////////////////////////////
            if (Me.GotTarget && Me.CurrentTarget.Distance >= 5 && HaltTrap() && HaltFeign() && !Invulnerable(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget) && Me.CurrentTarget.IsAlive && !Me.Mounted && !Me.Dead)
            {
                if (PvPBeastSettings.Instance.HM && !Me.CurrentTarget.HasAura("Hunter's Mark"))
                {
                    if (CastSpell("Hunter's Mark"))
                    {
                        Logging.Write(Color.Aqua, ">> Hunter's Mark <<");
                    }
                }
                if (PvPBeastSettings.Instance.STING && !IsMyAuraActive(Me.CurrentTarget, "Serpent Sting"))
                {
                    if (CastSpell("Serpent Sting"))
                    {
                        Logging.Write(Color.Aqua, ">> Serpent Sting <<");
                    }
                }
                if (Me.CurrentTarget.HealthPercent <= 8 && Me.IsCasting && Me.CastingSpell.Name == "Steady Shot" && Me.CurrentCastTimeLeft.TotalMilliseconds > 500 && !SpellManager.Spells["Kill Shot"].Cooldown)
                {
                    SpellManager.StopCasting();
                    {
                        Logging.Write(Color.Aqua, ">> Kill Shot Time, Stop Casting <<");
                    }
                }
                if (Me.CurrentTarget.HealthPercent <= 20)
                {
                    if (CastSpell("Kill Shot"))
                    {
                        Logging.Write(Color.Aqua, ">> Kill Shot <<");
                    }
                }
                if (PvPBeastSettings.Instance.WVE && Me.CurrentFocus > 52 && HostilePlayer(Me.CurrentTarget) && SpellManager.Spells["Kill Command"].CooldownTimeLeft.TotalSeconds > 1 && (!IsMyAuraActive(Me.CurrentTarget, "Widow Venom") || MyDebuffTime("Widow Venom", Me.CurrentTarget) <= 1))
                {
                    if (CastSpell("Widow Venom"))
                    {
                        Logging.Write(Color.Aqua, ">> Widow Venom <<");
                    }
                }
                if (Me.GotAlivePet && Me.CurrentFocus >= 37 && Me.Pet.Location.Distance(Me.CurrentTarget.Location) < 9)
                {
                    if (CastSpell("Kill Command"))
                    {
                        Logging.Write(Color.Aqua, ">> Kill Command <<");
                    }
                }
                if (Me.GotAlivePet && Me.Pet.Auras.ContainsKey("Frenzy Effect") && Me.Pet.Auras["Frenzy Effect"].StackCount >= 5)
                {
                    if (CastSpell("Focus Fire"))
                    {
                        Logging.Write(Color.Aqua, ">> Focus Fire <<");
                    }
                }
                if ((Me.GotAlivePet && Me.CurrentFocus >= 40 && SpellManager.Spells["Kill Command"].CooldownTimeLeft.TotalSeconds > 2) || (Me.GotAlivePet && Me.CurrentFocus > 65 && SpellManager.Spells["Kill Command"].CooldownTimeLeft.TotalSeconds <= 2 && SpellManager.Spells["Kill Command"].Cooldown) || (Me.Pet == null && Me.CurrentFocus >= 22))
                {
                    if (CastSpell("Arcane Shot"))
                    {
                        Logging.Write(Color.Aqua, ">> Arcane Shot <<");
                    }
                }
                if (!Me.IsMoving && !Me.IsCasting && Me.CurrentFocus <= PvPBeastSettings.Instance.FocusShots && Me.CurrentTarget.Distance >= 5 && !SpellManager.CanCast("Kill Shot") && (SpellManager.Spells["Kill Shot"].CooldownTimeLeft.TotalSeconds > 1 || Me.CurrentTarget.HealthPercent > 20))
                {
                    if (CastSpell("Cobra Shot"))
                    {
                        Logging.Write(Color.Aqua, ">> Cobra Shot <<");
                    }
                }
            }
            /////////////////////////////////////////////// Moving Rotation here ////////////////////////////////////////////////////////////////////////////////////////////
            if (HaltTrap() && HaltFeign() && Me.GotTarget && Me.CurrentTarget.IsAlive && !Me.Mounted)
            {
                if (!Me.Auras.ContainsKey("Aspect of the Hawk") && PvPBeastSettings.Instance.AspectSwitching && (!Me.IsMoving || Me.CurrentFocus >= 60))
                {
                    if (CastSpell("Aspect of the Hawk"))
                    {
                        Logging.Write(Color.Aqua, ">> Not moving - Aspect of the Hawk <<");
                    }
                }
                if (PvPBeastSettings.Instance.AspectSwitching && Me.IsMoving && !Me.Auras.ContainsKey("Aspect of the Fox") && Me.CurrentFocus < 50)
                {
                    if (CastSpell("Aspect of the Fox"))
                    {
                        Logging.Write(Color.Aqua, ">> Moving Below 60 Focus - Asp. of the Fox <<");
                    }
                }
                if (Me.IsMoving && !Me.IsCasting && Me.Auras.ContainsKey("Aspect of the Fox") && !DumbBear(Me.CurrentTarget) && Me.CurrentFocus <= PvPBeastSettings.Instance.FocusShots && Me.CurrentTarget.Distance >= 5 && !SpellManager.CanCast("Kill Shot") && (SpellManager.Spells["Kill Shot"].CooldownTimeLeft.TotalSeconds > 1 || Me.CurrentTarget.HealthPercent > 20))
                {
                    Lua.DoString("RunMacroText('/cast Cobra Shot');");
                    {
                        Logging.Write(Color.Red, ">> Moving - Cobra Shot <<");
                    }
                }
            }
        }
        }
        #endregion
    }
}