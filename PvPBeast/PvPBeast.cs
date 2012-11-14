using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Styx;
using Styx.Common;
using Styx.CommonBot;
using Styx.CommonBot.POI;
using Styx.CommonBot.Routines;
using Styx.Helpers;
using Styx.WoWInternals;
using Styx.WoWInternals.WoWObjects;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Styx.TreeSharp;
using Action = Styx.TreeSharp.Action;

namespace PvPBeast
{
    internal class PvPBeast : CombatRoutine
    {
        public override WoWClass Class { get { return WoWClass.Hunter; } }

        public static readonly Version Version = new Version(2, 8, 9);

        public override string Name { get { return "PvPBeast " + Version + " TreeSharp Edition"; } }

        private static LocalPlayer Me { get { return StyxWoW.Me; } }

        public delegate WoWUnit UnitSelection(object unit);

        public static string safeName(WoWUnit unit)
        {
            if (unit != null)
            {
                return (unit.Name == Me.Name) ? "Myself" : unit.Name;
            }
            return "No Target";
        }

        #region Log

        public static void standardLog(string msg, params object[] args)
        {
            if (msg != null)
            {
                Logging.Write(LogLevel.Normal, Colors.Aquamarine, msg, args);
            }
        }

        private static void tslog(string msg, params object[] args) //use for troubleshoot logging
        {
            if (msg != null)
            {
                Logging.Write(LogLevel.Quiet, Colors.SeaGreen, msg, args);
            }
        }

        #endregion

        #region Settings

        public override bool WantButton { get { return true; } }

        public override void OnButtonPress()
        {
            standardLog("Configuration opened!");
            new PvPBeastGUI().ShowDialog();
        }

        #endregion

        #region Initialize
        public override void Initialize()
        {
            tslog("Character Faction: {0}", Me.IsHorde ? "Horde" : "Allience");
            tslog("Character Level: {0}", Me.Level);
            tslog("Character Race: {0}", Me.Race);
            standardLog("");
            standardLog("You are using PvPBeast Combat Routine");
            standardLog("Version: " + Version);
            standardLog("Made by FallDown");
            standardLog("For LazyRaider only!");
        }
        #endregion

        #region CastSpell Method

        public static bool FocusCost(string spellName)
        {
            if ((!Me.HasAura("The Beast Within") && Me.CurrentFocus >= SpellManager.Spells[spellName].PowerCost)
                || (Me.HasAura("The Beast Within") && Me.CurrentFocus >= SpellManager.Spells[spellName].PowerCost / 2))
                return true;
            else return false;
        }

        public static bool canCast(string spellName, WoWUnit target)
        {
            if (SpellManager.HasSpell(spellName) && SpellManager.Spells[spellName].CooldownTimeLeft.TotalMilliseconds < 200
                && FocusCost(spellName) && !Me.IsChanneling && (!Me.IsCasting || Me.CurrentCastTimeLeft.TotalMilliseconds < 350)
                && (SpellManager.Spells[spellName].CastTime <= 0 || !Me.IsMoving || (Me.IsMoving && Me.HasAura("Aspect of the Fox") && SpellManager.Spells[spellName].CastTime > 0)))
            {
                return true;
            }
            return false;
        }

        public static Composite castSpell(string spellName, UnitSelection onUnit, CanRunDecoratorDelegate cond, string label)
        {
            return (
                new Decorator(delegate(object a)
                {
                    if (!cond(a))
                        return false;
                    if (!canCast(spellName, onUnit(a)))
                        return false;
                    return onUnit(a) != null;
                },
                    new Sequence(
                        new Action(a => standardLog("[Casting] {0} on {1}", label, safeName(onUnit(a)))),
                        new Action(a => SpellManager.Cast(spellName, onUnit(a))))));
        }

        public static Composite castSpell(string spellName, CanRunDecoratorDelegate cond, string label)
        {
            return castSpell(spellName, ret => Me.CurrentTarget, cond, label);
        }

        public static Composite castOnTarget(string spellName, UnitSelection onUnit, CanRunDecoratorDelegate cond, string label)
        {
            return new Decorator(
                delegate(object a)
                {
                    if (!cond(a))
                        return false;
                    if (!canCast(spellName, onUnit(a)))
                        return false;

                    return onUnit(a) != null;
                },
            new Sequence(
                new Action(a => standardLog("[Casting] {0} on {1}", label, safeName(onUnit(a)))),
                new Action(a => SpellManager.Cast(spellName, onUnit(a)))));
        }

        public static Composite castSelfSpell(string spellName, CanRunDecoratorDelegate cond, string label)
        {
            return castSpell(spellName, ret => Me, cond, label);
        }

        public static Composite castOnUnitLocation(string name, UnitSelection onUnit, CanRunDecoratorDelegate cond, string label)
        {
            return (
                new Decorator(delegate(object a)
                {
                    if (!cond(a))
                        return false;
                    return onUnit != null && canCast(name, onUnit(a));
                },
                    new Sequence(
                    new Action(a => standardLog("[Casting at Location] {0} ", label)),
                    new Action(a => SpellManager.Cast(name)),
                    new Action(ret => SpellManager.ClickRemoteLocation(onUnit(ret).Location)))));
        }

        public static TimeSpan spellCooldown(string spell)
        {
            return SpellManager.HasSpell(spell) ? SpellManager.Spells[spell].CooldownTimeLeft : TimeSpan.MaxValue;
        }

        #endregion

        #region Pet Management

        public static Stopwatch reviveTimer = new Stopwatch();

        public static Composite revivePet(UnitSelection onUnit, CanRunDecoratorDelegate cond, string label)
        {
            return (
                new Decorator(delegate(object a)
                {
                    if (!cond(a))
                        return false;
                    if (!canCast("Revive Pet", onUnit(a)))
                        return false;
                    return onUnit(a) != null;
                },
                    new Sequence(
                        new Action(a => reviveTimer.Start()),
                        new Action(a => standardLog("[Casting] {0} on {1}", label, safeName(onUnit(a)))),
                        new Action(a => SpellManager.Cast("Revive Pet", onUnit(a))))));
        }

        public static Composite revivePet(CanRunDecoratorDelegate cond, string label)
        {
            return revivePet(ret => Me, cond, label);
        }

        public static Composite callPet(string spellName, UnitSelection onUnit, CanRunDecoratorDelegate cond, string label)
        {
            return (
                new Decorator(delegate(object a)
                {
                    if (!cond(a))
                        return false;
                    if (!canCast(spellName, onUnit(a)))
                        return false;
                    return onUnit(a) != null;
                },
                    new Sequence(
                        new Action(a => reviveTimer.Reset()),
                        new Action(a => standardLog("[Casting] {0} on {1}", label, safeName(onUnit(a)))),
                        new Action(a => SpellManager.Cast(spellName, onUnit(a))))));
        }

        public static Composite callPet(string spellName, CanRunDecoratorDelegate cond, string label)
        {
            return callPet(spellName, ret => Me, cond, label);
        }


        #endregion

        #region Use Items

        private static bool CanUseEquippedItem(WoWItem item)
        {
            // Check for engineering tinkers!
            string itemSpell = Lua.GetReturnVal<string>("return GetItemSpell(" + item.Entry + ")", 0);
            if (string.IsNullOrEmpty(itemSpell))
                return false;

            return item.Usable && item.Cooldown <= 0;
        }


        public static Composite UseEquippedItem(uint slot)
        {
            return new PrioritySelector(
                ctx => StyxWoW.Me.Inventory.GetItemBySlot(slot),
                new Decorator(
                    ctx => ctx != null && CanUseEquippedItem((WoWItem)ctx),
                    new Action(ctx => UseItem((WoWItem)ctx))));

        }

        private static bool CanUseItem(WoWItem item)
        {
            return item != null && item.Usable && item.Cooldown <= 0;
        }


        private static void UseItem(WoWItem item)
        {
            if (item != null)
            {
                standardLog("Using item: " + item.Name);
                item.Use();
            }
        }

        public static Composite UseItem(uint id)
        {
            return new PrioritySelector(
                       ctx => ObjectManager.GetObjectsOfType<WoWItem>().FirstOrDefault(item => item.Entry == id),
                       new Decorator(
                           ctx => ctx != null && CanUseItem((WoWItem)ctx),
                           new Action(ctx => UseItem((WoWItem)ctx))));
        }

        public static Composite UseBagItem(string name, CanRunDecoratorDelegate cond, string label)
        {
            WoWItem item = null;
            return new Decorator(
                delegate(object a)
                {
                    if (!cond(a))
                        return false;
                    item = Me.BagItems.FirstOrDefault(x => x.Name == name && x.Usable && x.Cooldown <= 0);
                    return item != null;
                },
            new Sequence(
                new Action(a => standardLog(" [BagItem] {0} ", label)),
                new Action(a => item.UseContainerItem())));
        }

        #endregion

        #region Generic Auras
        //Used for checking how the time left on "my" debuff
        private int MyDebuffTime(String SpellName, WoWUnit unit)
        {
            {
                if (unit.HasAura(SpellName))
                {
                    var auras = unit.GetAllAuras();
                    foreach (var a in auras)
                    {
                        if (a.Name == SpellName && a.CreatorGuid == Me.Guid)
                        {
                            return a.TimeLeft.Seconds;
                        }
                    }
                }
            }
            return 0;
        }

        //Used for checking debuff timers
        private int DebuffTime(String SpellName, WoWUnit unit)
        {
            {
                if (unit.HasAura(SpellName))
                {
                    var auras = unit.GetAllAuras();
                    foreach (var b in auras)
                    {
                        if (b.Name == SpellName)
                        {
                            return b.TimeLeft.Seconds;
                        }
                    }
                }
            }
            return 0;
        }

        //Used for checking auras that has no time
        private bool IsMyAuraActive(WoWUnit Who, String What)
        {
            {
                return Who.GetAllAuras().Where(p => p.CreatorGuid == Me.Guid && p.Name == What).FirstOrDefault() != null;
            }
        }

        public static double spellCD(string spell)
        {
            return SpellManager.Spells[spell].CooldownTimeLeft.TotalSeconds;
        }

        #endregion

        #region Specific Auras

        // Halt on own Crowd Control
        public bool SelfControl(WoWUnit unit)
        {
            if (Me.GotTarget && (unit.HasAura("Freezing Trap") || unit.HasAura("Wyvern Sting") || unit.HasAura("Scatter Shot") || unit.HasAura("Bad Manner") || unit.HasAura("Narrow Escape")))
                return true;

            else return false;
        }

        // Halt on Feign Death
        public bool HaltFeign()
        {
            {
                if (!Me.HasAura("Feign Death"))
                    return true;
            }
            return false;
        }

        // Invulnerable
        public bool Invulnerable(WoWUnit unit)
        {
            if (unit.HasAura("Cyclone") || unit.HasAura("Dispersion") || unit.HasAura("Ice Block") || unit.HasAura("Deterrence") || unit.HasAura("Divine Shield") ||
                unit.HasAura("Hand of Protection") || (unit.HasAura("Anti-Magic Shell") && unit.HasAura("Icebound Fortitude")))
                return true;

            else return false;
        }

        // Dumb Bear
        public bool DumbBear(WoWUnit unit)
        {
            if (unit.Class == WoWClass.Druid && unit.HasAura("Bear Form") && unit.HasAura("Frenzied Regeneration") && unit.HealthPercent > 5 && unit.Distance > 8)
                return true;

            else return false;
        }

        // TranqAuras

        public bool CanBeTranqed(WoWUnit unit, int time)
        {
            if (DebuffTime("Power Word: Shield", unit) > time || DebuffTime("Icy Veins", unit) > time || DebuffTime("Ice Barrier", unit) > time
            || DebuffTime("Berserker Rage", unit) > time || DebuffTime("Hand of Sacrifice", unit) > time || DebuffTime("Hand of Freedom", unit) > time
            || DebuffTime("Master's Call", unit) > time || DebuffTime("Hand of Protection", unit) > time)
                return true;
            else
                return false;
        }

        #endregion

        #region Class type
        public bool MeleeClass(WoWUnit unit)
        {
            if (Me.GotTarget && (unit.Class == WoWClass.Rogue || unit.Class == WoWClass.Monk || unit.Class == WoWClass.Warrior || unit.Class == WoWClass.DeathKnight ||
                (unit.Class == WoWClass.Paladin && unit.MaxMana < 90000) ||
                (unit.Class == WoWClass.Druid && (unit.HasAura("Cat Form") || unit.HasAura("Bear Form")))))
                return true;

            else return false;
        }
        public bool RangedClass(WoWUnit unit)
        {
            if (Me.GotTarget && (unit.Class == WoWClass.Hunter || unit.Class == WoWClass.Shaman || unit.Class == WoWClass.Priest ||
                unit.Class == WoWClass.Mage || unit.Class == WoWClass.Warlock || (unit.Class == WoWClass.Paladin && unit.MaxMana >= 90000) ||
                (unit.Class == WoWClass.Druid && !unit.HasAura("Cat Form") && !unit.HasAura("Bear Form"))))
                return true;

            else return false;
        }
        public bool HealerClass(WoWUnit unit)
        {
            if (Me.GotTarget && (unit.Class == WoWClass.Shaman || (unit.Class == WoWClass.Priest && !unit.HasAura("Shadow Form")) ||
                (unit.Class == WoWClass.Paladin && unit.MaxMana >= 90000) || (unit.Class == WoWClass.Druid && !unit.HasAura("Cat Form") && !unit.HasAura("Bear Form") && !unit.HasAura("Boomkin Form"))))
                return true;

            else return false;
        }

        #endregion

        #region Target Checks
        public bool HostilePlayer(WoWUnit unit)
        {
            if (Me.GotTarget && unit.IsPlayer && unit.IsHostile && unit.IsAlive)
                return true;

            else return false;
        }

        public bool HostileNPC(WoWUnit unit)
        {
            if (unit.IsHostile && unit.IsAlive && !unit.IsPlayer)
                return true;

            else return false;
        }

        private bool validTarget(WoWUnit unit)
        {
            if (Me.GotTarget && unit.IsAlive && unit.Attackable)
                return true;
            else return false;
        }

        private bool validFocus()
        {
            if (Me.FocusedUnit != null && Me.FocusedUnit.IsAlive && !Me.FocusedUnit.IsFriendly && Me.FocusedUnit.Attackable)
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
                    && !u.IsFriendly
                    && u.IsHostile
                    && u.Attackable
                    && !u.IsTotem
                    && !u.IsCritter
                    && !u.IsNonCombatPet
                    && u.GotTarget
                    && (u.Location.Distance(Me.CurrentTarget.Location) <= 10 || u.Location.Distance2D(Me.CurrentTarget.Location) <= 10))
                {
                    count++;
                }
            }
            return count;
        }
        #endregion

        // Big thanks and credit to ZenLulz for all the movement imparement related code.
        #region Movement Imparement

        public static bool isCrowdControlled(WoWUnit unit)
        {
            if (unit != null)
            {
                Dictionary<string, WoWAura>.ValueCollection auras = unit.Auras.Values;
                return auras.Any(a => a.Spell.Mechanic == WoWSpellMechanic.Banished || a.Spell.Mechanic == WoWSpellMechanic.Disoriented || a.Spell.Mechanic == WoWSpellMechanic.Charmed || a.Spell.Mechanic == WoWSpellMechanic.Horrified || a.Spell.Mechanic == WoWSpellMechanic.Incapacitated || a.Spell.Mechanic == WoWSpellMechanic.Polymorphed || a.Spell.Mechanic == WoWSpellMechanic.Sapped || a.Spell.Mechanic == WoWSpellMechanic.Shackled || a.Spell.Mechanic == WoWSpellMechanic.Asleep || a.Spell.Mechanic == WoWSpellMechanic.Frozen || a.Spell.Mechanic == WoWSpellMechanic.Invulnerable || a.Spell.Mechanic == WoWSpellMechanic.Invulnerable2 || a.Spell.Mechanic == WoWSpellMechanic.Turned || a.Spell.Mechanic == WoWSpellMechanic.Fleeing || a.Spell.Name == "Hex");
            }
            return false;
        }

        private static List<WoWSpellMechanic> controlMechanic = new List<WoWSpellMechanic>()
        {
            WoWSpellMechanic.Charmed,
            WoWSpellMechanic.Disoriented,
            WoWSpellMechanic.Fleeing,
            WoWSpellMechanic.Frozen,
            WoWSpellMechanic.Incapacitated,
            WoWSpellMechanic.Polymorphed,
            WoWSpellMechanic.Sapped,
            WoWSpellMechanic.Asleep
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
            return unit != null && unit.IsValid && !unit.IsDead && ((unit.IsPet && !unit.OwnedByUnit.IsPlayer) || !unit.IsPet);
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

        public override Composite RestBehavior
        {
            get
            {
                return (
                    new Decorator(ret => HaltFeign() && StyxWoW.IsInWorld && !Me.IsGhost && Me.IsAlive && !Me.Mounted && !Me.IsFlying && !Me.IsOnTransport,
                        new PrioritySelector(
                            new Decorator(ret => reviveTimer.ElapsedMilliseconds < 100,
                                revivePet(ret => PvPBeastSettings.Instance.RP && !Me.GotAlivePet && SpellManager.HasSpell("Revive Pet"), "Reviving Pet")),
                            new Decorator(ret => PvPBeastSettings.Instance.CP && Me.Pet == null && !Me.IsCasting,
                                new PrioritySelector(
                                    callPet("Call Pet 1", ret => PvPBeastSettings.Instance.PET == 1 && SpellManager.HasSpell("Call Pet 1"), "Calling Pet 1"),
                                    callPet("Call Pet 2", ret => PvPBeastSettings.Instance.PET == 2 && SpellManager.HasSpell("Call Pet 2"), "Calling Pet 2"),
                                    callPet("Call Pet 3", ret => PvPBeastSettings.Instance.PET == 3 && SpellManager.HasSpell("Call Pet 3"), "Calling Pet 3"),
                                    callPet("Call Pet 4", ret => PvPBeastSettings.Instance.PET == 4 && SpellManager.HasSpell("Call Pet 4"), "Calling Pet 4"),
                                    callPet("Call Pet 5", ret => PvPBeastSettings.Instance.PET == 5 && SpellManager.HasSpell("Call Pet 5"), "Calling Pet 5")
                                    )))));
            }
        }

        #endregion

        #region Combat

        public override Composite CombatBehavior
        {
            get
            {
                return (
                    new PrioritySelector(
                        new Decorator(ret => SelfControl(Me.CurrentTarget),
                                new Action(delegate
                                    {
                                        Lua.DoString("StopAttack()");
                                        SpellManager.StopCasting();
                                        standardLog("Stop everything!");
                                        return RunStatus.Failure;
                                    }
                                    )),
                        new Decorator(ret => validTarget(Me.CurrentTarget) && Me.GotAlivePet && Me.Pet.CurrentTargetGuid != Me.CurrentTargetGuid && !Invulnerable(Me.CurrentTarget) && !SelfControl(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget),
                                new Action(delegate
                                    {
                                         Lua.DoString("PetAttack()");
                                        standardLog("Send pet on my current Target");
                                        return RunStatus.Failure;
                                    }
                                    )),

                                castSelfSpell("Mend Pet", ret => PvPBeastSettings.Instance.MP && Me.GotAlivePet && !Me.Pet.HasAura("Mend Pet") && Me.Pet.HealthPercent < PvPBeastSettings.Instance.MendHealth, "Mend Pet"),
                                /*  (isStunned(Me.Pet).TotalSeconds > 0 || isForsaken(Me.Pet).TotalSeconds > 0 || isRooted(Me.Pet).TotalMilliseconds > 0 || isSlowed(Me.Pet) || isControlled(Me.Pet).TotalSeconds > 0 ||  <- Mend Pet code for use with Glyph */
                                castSelfSpell("Exhilaration", ret => PvPBeastSettings.Instance.TL2_EXH && (Me.HealthPercent < 67 || (Me.GotAlivePet && Me.Pet.HealthPercent < 15)
                                || (Me.GotAlivePet && Me.Pet.HealthPercent < 15 && !SpellManager.HasSpell("Heart of the Phoenix"))), "Exhilaration"),

                                castSelfSpell("Gift of the Naaru", ret => PvPBeastSettings.Instance.RS && Me.Race == WoWRace.Draenei && Me.HealthPercent < 30 && !SpellManager.Spells["Gift of the Naaru"].Cooldown, "Gift of the Naaru"),

                                castSelfSpell("Master's Call", ret => isSlowed(Me) || isRooted(Me).TotalMilliseconds > 0, "Master's Call"),

                                new Decorator(ret => PvPBeastSettings.Instance.SpiritMendBox != "Never" && Me.GotAlivePet && !WoWSpell.FromId(90361).Cooldown,
                                new Action(delegate
                                {
                                    if ((PvPBeastSettings.Instance.SpiritMendBox == "1. Me" || PvPBeastSettings.Instance.SpiritMendBox == "1 + 2") && Me.HealthPercent < PvPBeastSettings.Instance.SpiritHealth_Me)
                                    {
                                        Lua.DoString("RunMacroText(\"/cast [@" + Me.Name + "] Spirit Mend\")");
                                        {
                                            Logging.Write(Colors.Aquamarine, "Spirit Mend on Me");
                                        }
                                    }
                                    if ((PvPBeastSettings.Instance.SpiritMendBox == "2. Focus" || PvPBeastSettings.Instance.SpiritMendBox == "1 + 2") && Me.FocusedUnit != null && Me.FocusedUnit.IsFriendly && Me.FocusedUnit.HealthPercent < PvPBeastSettings.Instance.SpiritHealth_Focus)
                                    {
                                        Lua.DoString("RunMacroText(\"/cast [@Focus] Spirit Mend\")");
                                        {
                                            Logging.Write(Colors.Aquamarine, "Spirit Mend on Focus");
                                        }
                                    }
                                    return RunStatus.Failure;
                                }
                                )),

                        new Decorator(ret => validTarget(Me.CurrentTarget) && !Me.Mounted && HaltFeign(),
                            new PrioritySelector(
                             //////////////////////////////// Trinkets ///////////////////////////////////////  
                                new Decorator(ret => Me.Inventory.Equipped.Trinket1 != null && Me.Inventory.Equipped.Trinket1.Usable && Me.Inventory.Equipped.Trinket1.Cooldown <= 0,
                                    new PrioritySelector(
                                        new Decorator(ret => PvPBeastSettings.Instance.T1MOB && (isStunned(Me).TotalSeconds > 2 || isControlled(Me).TotalSeconds > 2),
                                        new Action(delegate
                                            {
                                                Lua.DoString("RunMacroText('/use 13');");
                                                {
                                                    Logging.Write(Colors.Aquamarine, "Trinket 1 Mobility");
                                                }
                                                return RunStatus.Failure;
                                            }
                                        )),
                                        new Decorator(ret => PvPBeastSettings.Instance.T1DMG && !Invulnerable(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget) && HostilePlayer(Me.CurrentTarget),
                                        new Action(delegate
                                            {
                                                Lua.DoString("RunMacroText('/use 13');");
                                                {
                                                    Logging.Write(Colors.Aquamarine, "Trinket 1 Damage");
                                                }
                                                return RunStatus.Failure;
                                            }
                                        )),
                                        new Decorator(ret => PvPBeastSettings.Instance.T1DEF && !Invulnerable(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget) && HostilePlayer(Me.CurrentTarget) && Me.HealthPercent < 50 && (Me.CurrentTarget.Distance < 20 || Me.CurrentTarget.IsCasting),
                                        new Action(delegate
                                            {
                                                Lua.DoString("RunMacroText('/use 13');");
                                                {
                                                    Logging.Write(Colors.Aquamarine, "Trinket 1 Defense");
                                                }
                                                return RunStatus.Failure;
                                            }
                                        ))
                                    )),
                                new Decorator(ret => Me.Inventory.Equipped.Trinket2 != null && Me.Inventory.Equipped.Trinket2.Usable && Me.Inventory.Equipped.Trinket2.Cooldown <= 0,
                                    new PrioritySelector(
                                        new Decorator(ret => PvPBeastSettings.Instance.T2MOB && (isStunned(Me).TotalSeconds > 2 || isControlled(Me).TotalSeconds > 2),
                                        new Action(delegate
                                            {
                                                Lua.DoString("RunMacroText('/use 14');");
                                                {
                                                    Logging.Write(Colors.Aquamarine, "Trinket 2 Mobility");
                                                }
                                                return RunStatus.Failure;
                                            }
                                        )),
                                        new Decorator(ret => PvPBeastSettings.Instance.T2DMG && !Invulnerable(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget) && HostilePlayer(Me.CurrentTarget),
                                        new Action(delegate
                                            {
                                                Lua.DoString("RunMacroText('/use 14');");
                                                {
                                                    Logging.Write(Colors.Aquamarine, "Trinket 2 Damage");
                                                }
                                                return RunStatus.Failure;
                                            }
                                        )),
                                        new Decorator(ret => PvPBeastSettings.Instance.T2DEF && !Invulnerable(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget) && HostilePlayer(Me.CurrentTarget) && Me.HealthPercent < 50 && (Me.CurrentTarget.Distance < 20 || Me.CurrentTarget.IsCasting),
                                        new Action(delegate
                                            {
                                                Lua.DoString("RunMacroText('/use 14');");
                                                {
                                                    Logging.Write(Colors.Aquamarine, "Trinket 2 Defense");
                                                }
                                                return RunStatus.Failure;
                                            }
                                        ))
                                    ))
                            )),
            new Decorator(ret => !SelfControl(Me.CurrentTarget),
                new PrioritySelector(
                        new Decorator(ret => validTarget(Me.CurrentTarget) && !Me.Mounted && HaltFeign(),
                            new PrioritySelector(

                                new Decorator(ret => PvPBeastSettings.Instance.FFZT && validFocus() && !SpellManager.Spells["Freezing Trap"].Cooldown && Me.FocusedUnit.Distance < 40 && (!Me.FocusedUnit.IsMoving || Me.FocusedUnit.HasAura("Scatter Shot") || Me.FocusedUnit.HasAura("Wyvern Sting") || Me.FocusedUnit.HasAura("Binding Shot")),
                                    new PrioritySelector(
                                        castSelfSpell("Trap Launcher", ret => !Me.HasAura("Trap Launcher"), "Trap Launcher Activated"),
                                        castOnUnitLocation("Freezing Trap", ret => Me.FocusedUnit, ret => Me.HasAura("Trap Launcher"), "Freezing Trap Launched")
                                    )
                                ),

                                castOnUnitLocation("Binding Shot", ret => Me.FocusedUnit, ret => PvPBeastSettings.Instance.FBS && validFocus() && !SpellManager.Spells["Binding Shot"].Cooldown && Me.FocusedUnit.Distance <= 30, "Binding Shot Launched"),

                                castOnTarget("Wyvern Sting", ret => Me.FocusedUnit, ret => PvPBeastSettings.Instance.FWVS && validFocus() && !Invulnerable(Me.FocusedUnit) && !Me.FocusedUnit.HasAura("Freezing Trap") && Me.FocusedUnit.Distance <= 35, "Wyvern Sting"),

                                castOnTarget("Scatter Shot", ret => Me.FocusedUnit, ret => PvPBeastSettings.Instance.FSCA && validFocus() && !SelfControl(Me.FocusedUnit) && Me.FocusedUnit.Distance <= 20 && !Invulnerable(Me.FocusedUnit), "Scatter Shot"),

                                castOnTarget("Silencing Shot", ret => Me.FocusedUnit, ret => PvPBeastSettings.Instance.FSS && validFocus() && Me.FocusedUnit.IsCasting && Me.FocusedUnit.CanInterruptCurrentSpellCast && WoWSpell.FromId(Me.FocusedUnit.CastingSpellId).SpellEffect1.EffectType == WoWSpellEffectType.Heal, "Silencing Shot"),
                                
                                castOnTarget("Concussive Shot", ret => Me.FocusedUnit, ret => PvPBeastSettings.Instance.FCONC && validFocus() && !SelfControl(Me.FocusedUnit) && NeedSnare(Me.FocusedUnit) && !Invulnerable(Me.FocusedUnit) && MyDebuffTime("Concussive Shot", Me.FocusedUnit) <= 1 && Me.FocusedUnit.Distance <= 40, "Concussive Shot"),

                                castOnTarget("Tranquilizing Shot", ret => Me.FocusedUnit, ret => PvPBeastSettings.Instance.FTRQS && validFocus() && CanBeTranqed(Me.CurrentTarget, 2) && (Me.CurrentFocus > 60 || Me.HasAura("The Beast Within")) && !SelfControl(Me.FocusedUnit) && !Invulnerable(Me.FocusedUnit) && !DumbBear(Me.FocusedUnit) && Me.FocusedUnit.Distance <= 40, "Tranquilizing Shot"),

                                castSpell("Tranquilizing Shot", ret => PvPBeastSettings.Instance.TRQS && CanBeTranqed(Me.CurrentTarget, 2) && (Me.CurrentFocus > 60 || Me.HasAura("The Beast Within")) && !Invulnerable(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget) && Me.CurrentTarget.Distance <= 40, "Tranquilizing Shot"),

                                castSelfSpell("Intimidation", ret => PvPBeastSettings.Instance.IntimidateBox == "1. Interrupt" && Me.GotAlivePet && Me.Pet.Location.Distance(Me.CurrentTarget.Location) < 9 && HostilePlayer(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget) && Me.CurrentTarget.IsCasting && Me.CurrentTarget.CanInterruptCurrentSpellCast, "Intimidation"),

                                castSelfSpell("Intimidation", ret => PvPBeastSettings.Instance.IntimidateBox == "2. Low Health" && Me.GotAlivePet && Me.Pet.Location.Distance(Me.CurrentTarget.Location) < 9 && HostilePlayer(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget) && Me.CurrentTarget.HealthPercent <= PvPBeastSettings.Instance.TargetHealth, "Intimidation"),

                                castSelfSpell("Intimidation", ret => PvPBeastSettings.Instance.IntimidateBox == "3. Protection" && Me.GotAlivePet && Me.Pet.Location.Distance(Me.CurrentTarget.Location) < 9 && HostilePlayer(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget) && Me.CurrentTarget.Distance < 7 && MeleeClass(Me.CurrentTarget), "Intimidation"),

                                castSelfSpell("Intimidation", ret => PvPBeastSettings.Instance.IntimidateBox == "1 + 2" && Me.GotAlivePet && Me.Pet.Location.Distance(Me.CurrentTarget.Location) < 9 && HostilePlayer(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget) && ((Me.CurrentTarget.IsCasting && Me.CurrentTarget.CanInterruptCurrentSpellCast) || Me.CurrentTarget.HealthPercent <= PvPBeastSettings.Instance.TargetHealth), "Intimidation"),

                                castSelfSpell("Intimidation", ret => PvPBeastSettings.Instance.IntimidateBox == "1 + 3" && Me.GotAlivePet && Me.Pet.Location.Distance(Me.CurrentTarget.Location) < 9 && HostilePlayer(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget) && ((Me.CurrentTarget.IsCasting && Me.CurrentTarget.CanInterruptCurrentSpellCast) || (Me.CurrentTarget.Distance < 7 && MeleeClass(Me.CurrentTarget))), "Intimidation"),

                                castSelfSpell("Intimidation", ret => PvPBeastSettings.Instance.IntimidateBox == "2 + 3" && Me.GotAlivePet && Me.Pet.Location.Distance(Me.CurrentTarget.Location) < 9 && HostilePlayer(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget) && (Me.CurrentTarget.HealthPercent <= PvPBeastSettings.Instance.TargetHealth || (Me.CurrentTarget.Distance < 7 && MeleeClass(Me.CurrentTarget))), "Intimidation"),

                                castSelfSpell("Intimidation", ret => PvPBeastSettings.Instance.IntimidateBox == "1 + 2 + 3" && Me.GotAlivePet && Me.Pet.Location.Distance(Me.CurrentTarget.Location) < 9 && HostilePlayer(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget) && ((Me.CurrentTarget.IsCasting && Me.CurrentTarget.CanInterruptCurrentSpellCast) || (Me.CurrentTarget.Distance < 7 && MeleeClass(Me.CurrentTarget)) || (Me.CurrentTarget.HealthPercent <= PvPBeastSettings.Instance.TargetHealth)), "Intimidation"),

                                castSelfSpell("Every Man for Himself", ret => PvPBeastSettings.Instance.RS && Me.Race == WoWRace.Human && !SpellManager.Spells["Every Man for Himself"].Cooldown && !PvPBeastSettings.Instance.T2MOB && !PvPBeastSettings.Instance.T1MOB && (isStunned(Me).TotalSeconds > 2 || isControlled(Me).TotalSeconds > 2), "Every Man for Himself"),

                                castSelfSpell("Escape Artist", ret => PvPBeastSettings.Instance.RS && Me.Race == WoWRace.Gnome && !SpellManager.Spells["Escape Artist"].Cooldown && isSlowed(Me) || isRooted(Me).TotalMilliseconds > 0, "Escape Artist"),

                                castSelfSpell("Will of The Forsaken", ret => PvPBeastSettings.Instance.RS && Me.Race == WoWRace.Undead && !SpellManager.Spells["Will of The Forsaken"].Cooldown && isForsaken(Me).TotalMilliseconds > 0, "Will of The Forsaken"),

                                castSelfSpell("Stoneform", ret => PvPBeastSettings.Instance.RS && Me.Race == WoWRace.Dwarf && !SpellManager.Spells["Stoneform"].Cooldown && StyxWoW.Me.GetAllAuras().Any(a => a.Spell.Mechanic == WoWSpellMechanic.Bleeding || a.Spell.DispelType == WoWDispelType.Disease || a.Spell.DispelType == WoWDispelType.Poison), "Stoneform"),

                                castSelfSpell("War Stomp", ret => PvPBeastSettings.Instance.INT && Me.Race == WoWRace.Tauren && PvPBeastSettings.Instance.RS && Me.CurrentTarget.Distance < 8 && !Invulnerable(Me.CurrentTarget) && Me.CurrentTarget.IsCasting && Me.CurrentTarget.CanInterruptCurrentSpellCast && (SpellManager.Spells["Silencing Shot"].CooldownTimeLeft.TotalSeconds > 1 || Me.CurrentTarget.Distance < 5), "War Stomp"),

                                castSelfSpell("War Stomp", ret => PvPBeastSettings.Instance.RS && Me.Race == WoWRace.Tauren && (Me.CurrentTarget.Distance < 8 || Me.CurrentTarget.Pet.Distance < 8) && (Me.CurrentTarget.CurrentTargetGuid == Me.Guid || Me.CurrentTarget.Pet.CurrentTargetGuid == Me.Guid) && (NeedSnare(Me.CurrentTarget) || NeedSnare(Me.CurrentTarget.Pet)) && (!Invulnerable(Me.CurrentTarget) || Me.CurrentTarget.Pet.Distance < 8), "War Stomp"),

                                castSpell("Arcane Torrent", ret => PvPBeastSettings.Instance.INT && Me.Race == WoWRace.BloodElf && PvPBeastSettings.Instance.RS && Me.CurrentTarget.IsPlayer && Me.CurrentTarget.IsCasting && Me.CurrentTarget.CanInterruptCurrentSpellCast && Me.CurrentTarget.Distance >= 5, "Arcane Torrent"),

                                new Decorator(ret => PvPBeastSettings.Instance.WEB && NeedSnare(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget) && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && !WoWSpell.FromId(54706).Cooldown && Me.CurrentTarget.Distance <= 30,
                                new Action(delegate
                                    {
                                        Lua.DoString("RunMacroText('/cast Venom Web Spray');");
                                        Logging.Write(Colors.Aquamarine, ">> Pet: Silithid Web <<");
                                        return RunStatus.Failure;
                                    }
                                )),

                                UseBagItem("Healthstone", ret => Me.HealthPercent < PvPBeastSettings.Instance.HealthStone && Me.IsAlive, "Healthstone"),

                                UseBagItem("Virmen's Bite", ret => PvPBeastSettings.Instance.VSB && Me.CurrentTarget.IsPlayer && Me.HealthPercent > 40 && Me.CurrentTarget.HealthPercent <= PvPBeastSettings.Instance.VirmenHealth 
                                    && !Me.HasAura("Virmen's Bite") && ((Me.CurrentFocus > 70 && Me.HasAura("The Beast Within")) || PvPBeastSettings.Instance.VBBP), "Virmen's Bite"),

                                new Decorator(ret => Me.HealthPercent < PvPBeastSettings.Instance.ItemsHealth,
                                    new PrioritySelector(
                                        UseBagItem("Life Spirit", ret => PvPBeastSettings.Instance.LIFES && Me.IsAlive, "Life Spirit"),

                                        UseBagItem("Alchemist's Rejuvenation", ret => PvPBeastSettings.Instance.ALCR && Me.IsAlive, "Alchemist's Rejuvenation"),

                                        UseBagItem("Master Healing Potion", ret => PvPBeastSettings.Instance.HEALP && Me.IsAlive, "Master Healing Potion")
                                )),

                                castSelfSpell("Feign Death", ret => PvPBeastSettings.Instance.FDCBox == "1. Pet Near" && Me.CurrentTarget.GotAlivePet && Me.CurrentTarget.Pet.CurrentTargetGuid == Me.Guid && Me.CurrentTarget.Pet.Distance <= 5, "Feign Death"),

                                castSelfSpell("Feign Death", ret => PvPBeastSettings.Instance.FDCBox == "2. Target Casting" && Me.CurrentTarget.IsPlayer && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && Me.CurrentTarget.IsCasting && Me.CurrentTarget.CanInterruptCurrentSpellCast && WoWSpell.FromId(Me.CurrentTarget.CastingSpellId).SpellEffect1.EffectType != WoWSpellEffectType.Heal, "Feign Death"),

                                castSelfSpell("Feign Death", ret => PvPBeastSettings.Instance.FDCBox == "3. Low Health" && Me.HealthPercent < 10, "Feign Death"),

                                castSelfSpell("Feign Death", ret => PvPBeastSettings.Instance.FDCBox == "1 + 2" && ((Me.CurrentTarget.GotAlivePet && Me.CurrentTarget.Pet.CurrentTargetGuid == Me.Guid && Me.CurrentTarget.Pet.Distance <= 5) || (Me.CurrentTarget.IsPlayer && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && Me.CurrentTarget.IsCasting && Me.CurrentTarget.CanInterruptCurrentSpellCast && WoWSpell.FromId(Me.CurrentTarget.CastingSpellId).SpellEffect1.EffectType != WoWSpellEffectType.Heal)), "Feign Death"),

                                castSelfSpell("Feign Death", ret => PvPBeastSettings.Instance.FDCBox == "1 + 3" && ((Me.CurrentTarget.GotAlivePet && Me.CurrentTarget.Pet.CurrentTargetGuid == Me.Guid && Me.CurrentTarget.Pet.Distance <= 5) || Me.HealthPercent < 10), "Feign Death"),

                                castSelfSpell("Feign Death", ret => PvPBeastSettings.Instance.FDCBox == "2 + 3" && ((Me.CurrentTarget.IsPlayer && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && Me.CurrentTarget.IsCasting && Me.CurrentTarget.CanInterruptCurrentSpellCast && WoWSpell.FromId(Me.CurrentTarget.CastingSpellId).SpellEffect1.EffectType != WoWSpellEffectType.Heal) || Me.HealthPercent < 10), "Feign Death"),

                                castSelfSpell("Feign Death", ret => PvPBeastSettings.Instance.FDCBox == "1 + 2 + 3" && ((Me.CurrentTarget.GotAlivePet && Me.CurrentTarget.Pet.CurrentTargetGuid == Me.Guid && Me.CurrentTarget.Pet.Distance <= 5) || (Me.CurrentTarget.IsPlayer && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && Me.CurrentTarget.IsCasting && Me.CurrentTarget.CanInterruptCurrentSpellCast && WoWSpell.FromId(Me.CurrentTarget.CastingSpellId).SpellEffect1.EffectType != WoWSpellEffectType.Heal) || Me.HealthPercent < 10), "Feign Death"),

                                castSelfSpell("Deterrence", ret => PvPBeastSettings.Instance.DETR && Me.HealthPercent < 15 && Me.CurrentTarget.CurrentTargetGuid == Me.Guid, "Deterrence"),

                                castSpell("Scatter Shot", ret => PvPBeastSettings.Instance.ScatterBox == "1. Interrupt" && Me.CurrentTarget.Distance <= 20 && Me.CurrentTarget.IsCasting && Me.CurrentTarget.CanInterruptCurrentSpellCast && !Invulnerable(Me.CurrentTarget), "Scatter Shot"),

                                castSpell("Scatter Shot", ret => PvPBeastSettings.Instance.ScatterBox == "2. Defense" && Me.CurrentTarget.Distance <= 20 && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && !Invulnerable(Me.CurrentTarget), "Scatter Shot"),

                                castSpell("Scatter Shot", ret => PvPBeastSettings.Instance.ScatterBox == "1 + 2" && ((Me.CurrentTarget.Distance <= 20 && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && !Invulnerable(Me.CurrentTarget))
                                || (Me.CurrentTarget.IsCasting && Me.CurrentTarget.CanInterruptCurrentSpellCast)), "Scatter Shot"),

                                castSpell("Silencing Shot", ret => PvPBeastSettings.Instance.TL1_SS && Me.CurrentTarget.IsCasting && Me.CurrentTarget.CanInterruptCurrentSpellCast && (WoWSpell.FromId(Me.CurrentTarget.CastingSpellId).SpellEffect1.EffectType == WoWSpellEffectType.Heal || Me.HealthPercent < 50), "Silencing Shot"),

                                castOnUnitLocation("Binding Shot", ret => Me.CurrentTarget, ret => PvPBeastSettings.Instance.TL1_BS && Me.CurrentTarget.Distance < 15 && MeleeClass(Me.CurrentTarget), "Binding Shot"),

                                castSpell("Concussive Shot", ret => PvPBeastSettings.Instance.CONC && NeedSnare(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget) && MyDebuffTime("Concussive Shot", Me.CurrentTarget) <= 1 && Me.CurrentTarget.Distance <= 40, "Concussive Shot"),

                                castSpell("Fervor", ret => PvPBeastSettings.Instance.TL3_FV && (Me.CurrentFocus < 60 || (Me.HasAura("The Beast within") && Me.CurrentFocus < 40)), "Fervor"),

                                castSpell("Rapid Fire", ret => PvPBeastSettings.Instance.RF && !Me.HasAura("Rapid Fire") && Me.CurrentTarget.CurrentHealth > 25000 && HostilePlayer(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget), "Rapid Fire"),

                                castSpell("Stampede", ret => PvPBeastSettings.Instance.STAM && !Invulnerable(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget) && Me.CurrentTarget.Distance <= 30, "Stampede"),

                                castSelfSpell("Bestial Wrath", ret => Me.GotAlivePet && PvPBeastSettings.Instance.BWR && (!PvPBeastSettings.Instance.TL4_LR || SpellManager.Spells["Lynx Rush"].CooldownTimeLeft.TotalSeconds > 5
                                || SpellManager.Spells["Lynx Rush"].CooldownTimeLeft.TotalMilliseconds < 1500) && (!PvPBeastSettings.Instance.TL4_AMOC || SpellManager.Spells["A Murder of Crows"].CooldownTimeLeft.TotalSeconds > 5
                                || SpellManager.Spells["A Murder Of Crows"].CooldownTimeLeft.TotalMilliseconds < 1500) && Me.Pet.Location.Distance(Me.CurrentTarget.Location) <= 25 && !Me.HasAura("The Beast Within") && HostilePlayer(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget), "Bestial Wrath"),

                                castSelfSpell("Readiness", ret => PvPBeastSettings.Instance.RDN
                                && SpellManager.Spells["Rapid Fire"].CooldownTimeLeft.TotalSeconds > 10
                                && SpellManager.Spells["Bestial Wrath"].CooldownTimeLeft.TotalSeconds > 8
                                && (!PvPBeastSettings.Instance.KCO || SpellManager.Spells["Kill Command"].CooldownTimeLeft.TotalSeconds > 2)
                                && (!PvPBeastSettings.Instance.ARN|| SpellManager.Spells["Disengage"].CooldownTimeLeft.TotalSeconds > 2)
                                && (!PvPBeastSettings.Instance.TL1_SS || !PvPBeastSettings.Instance.ARN || SpellManager.Spells["Silencing Shot"].CooldownTimeLeft.TotalSeconds > 2)
                                && (!PvPBeastSettings.Instance.TL1_WS || !PvPBeastSettings.Instance.ARN || SpellManager.Spells["Wyvern Sting"].CooldownTimeLeft.TotalSeconds > 2)
                                && (!PvPBeastSettings.Instance.TL1_BS || !PvPBeastSettings.Instance.ARN || SpellManager.Spells["Binding Shot"].CooldownTimeLeft.TotalSeconds > 2)
                                && (!PvPBeastSettings.Instance.TL2_EXH || SpellManager.Spells["Exhilaration"].CooldownTimeLeft.TotalSeconds > 2)
                                && (!PvPBeastSettings.Instance.TL3_DB || SpellManager.Spells["Dire Beast"].CooldownTimeLeft.TotalSeconds > 2)
                                && (!PvPBeastSettings.Instance.TL3_FV || SpellManager.Spells["Fervor"].CooldownTimeLeft.TotalSeconds > 4)
                                && (!PvPBeastSettings.Instance.TL4_AMOC || SpellManager.Spells["A Murder of Crows"].CooldownTimeLeft.TotalSeconds > 10)
                                && (!PvPBeastSettings.Instance.TL4_BSTRK || SpellManager.Spells["Blink Strike"].CooldownTimeLeft.TotalSeconds > 2)
                                && (!PvPBeastSettings.Instance.TL4_LR || SpellManager.Spells["Lynx Rush"].CooldownTimeLeft.TotalSeconds > 10)
                                && (!PvPBeastSettings.Instance.TL5_GLV || SpellManager.Spells["Glaive Toss"].CooldownTimeLeft.TotalSeconds > 2)
                                && (!PvPBeastSettings.Instance.TL5_PWR || SpellManager.Spells["Powershot"].CooldownTimeLeft.TotalSeconds > 2)
                                && (!PvPBeastSettings.Instance.TL5_BRG || SpellManager.Spells["Barrage"].CooldownTimeLeft.TotalSeconds > 2)
                                && (PvPBeastSettings.Instance.IntimidateBox == "Never" || !PvPBeastSettings.Instance.ARN || SpellManager.Spells["Intimidation"].CooldownTimeLeft.TotalSeconds > 2), "Readiness"),

                                castSelfSpell("Lifeblood", ret => PvPBeastSettings.Instance.LB && SpellManager.HasSpell("Lifeblood") && !SpellManager.Spells["Lifeblood"].Cooldown && Me.HealthPercent < 99, "Lifeblood"),

                                new Decorator(ret => PvPBeastSettings.Instance.GE && !Invulnerable(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget) && HostilePlayer(Me.CurrentTarget) && Me.Inventory.Equipped.Hands != null && Me.Inventory.Equipped.Hands.Cooldown <= 0 && Me.Inventory.Equipped.Hands.Usable,
                                new Action(delegate
                                 {
                                     Lua.DoString("RunMacroText('/use 10');");
                                     {
                                         Logging.Write(Colors.Aquamarine, "Using Gloves");
                                     }
                                     return RunStatus.Failure;
                                 }
                                )),
                             
                                /////////////////////////////////////////Racial Skills///////////////////////////////////////////
                                new Decorator(ret => PvPBeastSettings.Instance.RS && !Invulnerable(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget) && Me.Race == WoWRace.Troll && HostilePlayer(Me.CurrentTarget) && (Me.CurrentTarget.HealthPercent > 15 || PvPBeastSettings.Instance.ARN) && !SpellManager.Spells["Berserking"].Cooldown,
                                new Action(delegate
                                {
                                    Lua.DoString("RunMacroText('/Cast Berserking');");
                                    return RunStatus.Failure;
                                }
                                )),

                                new Decorator(ret => PvPBeastSettings.Instance.RS && !Invulnerable(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget) && Me.Race == WoWRace.Orc && HostilePlayer(Me.CurrentTarget) && (Me.CurrentTarget.HealthPercent > 15 || PvPBeastSettings.Instance.ARN) && !SpellManager.Spells["Blood Fury"].Cooldown,
                                new Action(delegate
                                {
                                    Lua.DoString("RunMacroText('/Cast Blood Fury');");
                                    return RunStatus.Failure;
                                }
                                )),
                        
                                new Decorator(ret => Me.CurrentTarget.Distance >= 5 && Me.CurrentTarget.Distance < 40 && HostilePlayer(Me.CurrentTarget) && !Invulnerable(Me.CurrentTarget) && Me.CurrentTarget.InLineOfSpellSight && !Me.CurrentTarget.IsMoving,
                                    new PrioritySelector(
                                        castSelfSpell("Trap Launcher", ret => !Me.HasAura("Trap Launcher"), "Trap Launcher Activated"),
                                        new Decorator(ret => Me.HasAura("Trap Launcher"),
                                            new PrioritySelector(
                                                castOnUnitLocation("Ice Trap", ret => Me.CurrentTarget, ret => PvPBeastSettings.Instance.TL && MeleeClass(Me.CurrentTarget) && !SpellManager.Spells["Ice Trap"].Cooldown, "Ice Trap Launched"),
                                                castOnUnitLocation("Snake Trap", ret => Me.CurrentTarget, ret => PvPBeastSettings.Instance.TL2 && MeleeClass(Me.CurrentTarget) && !SpellManager.Spells["Snake Trap"].Cooldown && !DumbBear(Me.CurrentTarget), "Snake Trap Launched"),
                                                castOnUnitLocation("Freezing Trap", ret => Me.CurrentTarget, ret => PvPBeastSettings.Instance.TL3 && RangedClass(Me.CurrentTarget) && !SpellManager.Spells["Freezing Trap"].Cooldown, "Freezing Trap Launched"),
                                                castOnUnitLocation("Explosive Trap", ret => Me.CurrentTarget, ret => PvPBeastSettings.Instance.TL4 && !SpellManager.Spells["Explosive Trap"].Cooldown && !DumbBear(Me.CurrentTarget), "Explosive Trap Launched")
                                            )
                                        )
                                    )
                                ),
                                new Decorator(ret => Me.CurrentTarget.Distance < 5 && !Invulnerable(Me.CurrentTarget) && HostilePlayer(Me.CurrentTarget) && Me.CurrentTarget.CurrentTargetGuid == Me.Guid,
                                    new PrioritySelector(
                                        castSelfSpell("Trap Launcher", ret => Me.HasAura("Trap Launcher"), "Trap Launcher Deactivated"),
                                        new Decorator(ret => Me.HasAura("Trap Launcher"),
                                            new PrioritySelector(
                                                castSelfSpell("Ice Trap", ret => PvPBeastSettings.Instance.ICET && MeleeClass(Me.CurrentTarget) && !SpellManager.Spells["Ice Trap"].Cooldown, "Ice Trap Launched"),
                                                castSelfSpell("Snake Trap", ret => PvPBeastSettings.Instance.SNAT && !SpellManager.Spells["Snake Trap"].Cooldown && !DumbBear(Me.CurrentTarget), "Snake Trap Launched"),
                                                castSelfSpell("Freezing Trap", ret => PvPBeastSettings.Instance.FRET && RangedClass(Me.CurrentTarget) && !SpellManager.Spells["Freezing Trap"].Cooldown, "Freezing Trap Launched"),
                                                castSelfSpell("Explosive Trap", ret => PvPBeastSettings.Instance.EXPT && !SpellManager.Spells["Explosive Trap"].Cooldown && !DumbBear(Me.CurrentTarget), "Explosive Trap Launched")
                                            )
                                        )
                                    )
                                ),

                        ////////////////////////////// ASPECT SWITCHING /////////////////////////////////
                        new Decorator(ret => PvPBeastSettings.Instance.AspectSwitching,
                            new PrioritySelector(
                                castSelfSpell("Aspect of the Hawk", ret => !Me.IsMoving && !Me.HasAura("Aspect of the Iron Hawk") && !Me.HasAura("Aspect of the Hawk"), "Aspect of the Hawk"),
                                castSelfSpell("Aspect of the Fox", ret => Me.IsMoving && !Me.HasAura("Aspect of the Fox") && Me.CurrentFocus < 50, "Aspect of the Fox")   
                            )
                        ),

                        //////////////////////////////// SINGLE TARGET ROTATION ////////////////////////////////////
                        new Decorator(ret => !Invulnerable(Me.CurrentTarget) && !DumbBear(Me.CurrentTarget),
                            new PrioritySelector(
                                castSpell("Hunter's Mark", ret => PvPBeastSettings.Instance.HM && !Me.CurrentTarget.HasAura("Hunter's Mark"), "Hunter's Mark"),

                                castSpell("Kill Shot", ret => PvPBeastSettings.Instance.KSH && Me.CurrentTarget.HealthPercent <= 20, "Kill Shot"),

                                castSpell("Lynx Rush", ret => PvPBeastSettings.Instance.TL4_LR && Me.GotAlivePet && Me.Pet.Location.Distance(Me.CurrentTarget.Location) < 10, "Lynx Rush"),

                                castSpell("Kill Command", ret => PvPBeastSettings.Instance.KCO && Me.GotAlivePet && Me.Pet.Location.Distance(Me.CurrentTarget.Location) <= 25, "Kill Command"),

                                castSpell("Serpent Sting", ret => PvPBeastSettings.Instance.SerpentBox == "Always" && (!IsMyAuraActive(Me.CurrentTarget, "Serpent Sting") || MyDebuffTime("Serpent Sting", Me.CurrentTarget) < 1), "Serpent Sting"),

                                castSpell("Serpent Sting", ret => PvPBeastSettings.Instance.SerpentBox == "Sometimes" && (!IsMyAuraActive(Me.CurrentTarget, "Serpent Sting") || MyDebuffTime("Serpent Sting", Me.CurrentTarget) < 1) && Me.CurrentTarget.HealthPercent > 50, "Serpent Sting"),

                                castSpell("Dire Beast", ret => PvPBeastSettings.Instance.TL3_DB, "Dire Beast"),

                                castSpell("A Murder of Crows", ret => PvPBeastSettings.Instance.TL4_AMOC && !IsMyAuraActive(Me.CurrentTarget, "A Murder of Crows") && (!PvPBeastSettings.Instance.PVP || Me.CurrentTarget.HealthPercent > 20), "A Murder of Crows"),

                                castSpell("Widow Venom", ret => PvPBeastSettings.Instance.WVE && Me.CurrentFocus >= 55 && HostilePlayer(Me.CurrentTarget)
                                && (!PvPBeastSettings.Instance.KCO || spellCD("Kill Command") > 2 || !SpellManager.GlobalCooldown) 
                                && (!Me.CurrentTarget.HasAura("Widow Venom") || MyDebuffTime("Widow Venom", Me.CurrentTarget) <= 1), "Widow Venom"),

                                castSpell("Blink Strike", ret => PvPBeastSettings.Instance.TL4_BSTRK && Me.GotAlivePet && Me.Pet.Location.Distance(Me.CurrentTarget.Location) <= 40, "Blink Strike"),

                                castSpell("Glaive Toss", ret => PvPBeastSettings.Instance.TL5_GLV && (!PvPBeastSettings.Instance.ARN || !validFocus() || !SelfControl(Me.FocusedUnit) || Me.CurrentTarget.Location.Distance(Me.FocusedUnit.Location) > 5), "Glaive Toss"),

                                castSpell("Powershot", ret => PvPBeastSettings.Instance.TL5_PWR, "Powershot"),

                                castSpell("Barrage", ret => PvPBeastSettings.Instance.TL5_BRG, "Barrage"),
                                

                                new Decorator(ret => PvPBeastSettings.Instance.FF && Me.HasAura("Frenzy") && !Me.HasAura("The Beast Within") && SpellManager.Spells["Bestial Wrath"].CooldownTimeLeft.TotalSeconds >= 10,
                                    new PrioritySelector(
                                        castSpell("Focus Fire", ret => Me.Auras["Frenzy"].StackCount >= 5, "Focus Fire: 5 Stacks")
                                    )
                                ),

                                castSpell("Focus Fire", ret => PvPBeastSettings.Instance.FF && Me.HasAura("Frenzy") && Me.Auras["Frenzy"].StackCount >= 1 && DebuffTime("Frenzy", Me) < 2, " Focus Fire: Running out of time"),

                                new Decorator(ret => PvPBeastSettings.Instance.ARC
                                    && (!PvPBeastSettings.Instance.TL5_GLV || spellCD("Glaive Toss") > 2 || !SpellManager.GlobalCooldown)
                                    && (!PvPBeastSettings.Instance.TL4_BSTRK || spellCD("Blink Strike") > 2 || !SpellManager.GlobalCooldown),
                                    new PrioritySelector(
                                        new Decorator(ret => PvPBeastSettings.Instance.KCO,
                                            new PrioritySelector(
                                                new Decorator(ret => !Me.HasAura("Thrill of the Hunt"),
                                                    new PrioritySelector(
                                                        new Decorator(ret => Me.GotAlivePet && SpellManager.Spells["Kill Command"].CooldownTimeLeft.TotalMilliseconds > 300,
                                                            new PrioritySelector(
                                                                new Decorator(ret => !Me.HasAura("The Beast Within"),
                                                                    new PrioritySelector(
                                                                        castSpell("Arcane Shot", ret => (Me.CurrentFocus > 60 && SpellManager.Spells["Kill Command"].CooldownTimeLeft.TotalSeconds <= 2) 
                                                                        || (Me.CurrentFocus > 40 && SpellManager.Spells["Kill Command"].CooldownTimeLeft.TotalSeconds > 2), "Arcane Shot")
                                                                    )
                                                                ),
                                                                new Decorator(ret => Me.HasAura("The Beast Within"),
                                                                    new PrioritySelector(
                                                                        castSpell("Arcane Shot", ret => (Me.CurrentFocus > 50 && SpellManager.Spells["Kill Command"].CooldownTimeLeft.TotalSeconds <= 2) 
                                                                        || (Me.CurrentFocus > 30 && SpellManager.Spells["Kill Command"].CooldownTimeLeft.TotalSeconds > 2), "Arcane Shot")
                                                                    )
                                                                )
                                                            )
                                                        )
                                                    )
                                                ),
                                                new Decorator(ret => Me.HasAura("Thrill of the Hunt"),
                                                    new PrioritySelector(
                                                        castSpell("Arcane Shot", ret => Me.GotAlivePet && (!PvPBeastSettings.Instance.KCO || spellCD("Kill Command") > 2 || !SpellManager.GlobalCooldown), "Arcane Shot")
                                                    )
                                                )
                                            )
                                        ),
                                        castSpell("Arcane Shot", ret => !PvPBeastSettings.Instance.KCO || !Me.GotAlivePet, "Arcane Shot")
                                    )
                                ),

                                new Decorator(ret => Me.CurrentFocus > 105 && Me.IsCasting && (Me.CastingSpell.Name == "Cobra Shot" || Me.CastingSpell.Name == "Steady Shot") && Me.CurrentCastTimeLeft.TotalMilliseconds > 700,
                                new Action(delegate
                                {
                                    SpellManager.StopCasting();
                                    Logging.Write(Colors.Red, "Stopping Cobra Shot");
                                    return RunStatus.Failure;
                                }
                                )),

                                new Decorator(ret => ((!Me.IsCasting && !SpellManager.GlobalCooldown && spellCD("Kill Command") > 1.5) || Me.CurrentFocus < 18 || (!Me.HasAura("The Beast Within") && Me.CurrentFocus < 38))
                                    && (!PvPBeastSettings.Instance.TL3_FV || (SpellManager.Spells["Fervor"].CooldownTimeLeft.TotalMilliseconds > 750 && spellCD("Fervor") < 29)),
                                    new PrioritySelector(
                                        castSpell("Steady Shot", ret => Me.CurrentFocus < PvPBeastSettings.Instance.FocusShots || (Me.HasAura("The Beast Within") && Me.CurrentFocus < PvPBeastSettings.Instance.FocusShots / 2), "Cobra Shot")
                                    )
                                )  
                            ))
                        ))
                    ))
                ));
            }
        }
        #endregion
    }
}

