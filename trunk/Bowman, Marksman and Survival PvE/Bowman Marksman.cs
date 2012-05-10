﻿using System;
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
        public override sealed string Name { get { return "Bowman a Marksmanship CC v4.1.3.2"; } }

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
            Logging.Write(Color.White, "___________________________________________________");
            Logging.Write(Color.Crimson, "----------- Bowman v4.1.3.2 ------------");
			Logging.Write(Color.Crimson, "by FallDown, Shaddar, Venus112 and Jasf10");
            Logging.Write(Color.Crimson, "---  Remember to comment on the forum! ---");
            Logging.Write(Color.Crimson, "--- /like and +rep if you like this CC! ----");
            Logging.Write(Color.Crimson, "----Thank you to Fiftypence for support ----");
            Logging.Write(Color.White, "___________________________________________________");
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

        #region AimedShot
        public bool AimedShot()
        {
            {
                if (MarksmanSettings.Instance.MixedROT)
                {
                    if (Me.ActiveAuras.ContainsKey("Chronohunter") || Me.ActiveAuras.ContainsKey("Ancient Hysteria") || Me.ActiveAuras.ContainsKey("Time Warp") || Me.ActiveAuras.ContainsKey("Bloodlust")
                    || Me.ActiveAuras.ContainsKey("Heroism") || Me.ActiveAuras.ContainsKey("Rapid Fire") || (Me.ActiveAuras.ContainsKey("Improved Steady Shot") && (Me.ActiveAuras.ContainsKey("Hunting Party") 
                    || Me.ActiveAuras.ContainsKey("Improved Icy Talons") || Me.ActiveAuras.ContainsKey("Windfury Totem") || Me.ActiveAuras.ContainsKey("Blessing of Khaz'goroth")
                    || Me.ActiveAuras.ContainsKey("Arrow of Time") || Me.ActiveAuras.ContainsKey("Race Against Death") || Me.ActiveAuras.ContainsKey("Heart's Judgement") || Me.ActiveAuras.ContainsKey("Matrix Restabilizer")
                    || Me.ActiveAuras.ContainsKey("Nefarious Plot") || Me.ActiveAuras.ContainsKey("Velocity") || Me.ActiveAuras.ContainsKey("Devour"))))
                return true;
                }            
            }
            return false;
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
				 if (StyxWoW.IsInWorld && !Me.IsGhost && !Me.Dead && !Me.Mounted && !Me.IsFlying && !Me.IsOnTransport) 
				{
					if (MarksmanSettings.Instance.RP && !Me.GotAlivePet && SpellManager.HasSpell("Revive Pet"))
					{
						if (CastSpell("Revive Pet")) 
						{
						Logging.Write(Color.Aqua, ">> Reviving Pet <<");
						}
						StyxWoW.SleepForLagDuration();
					}			
					if (MarksmanSettings.Instance.CP && Me.Pet == null && !Me.IsCasting )
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
						if ( MarksmanSettings.Instance.PET == 4 && SpellManager.HasSpell("Call Pet 4"))
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
			if (MarksmanSettings.Instance.TL && MarksmanSettings.Instance.TLF && MyDebuffTime("Trap Launcher", Me) < 13 && Me.HasAura("Trap Launcher") && (Me.CurrentTarget == null || addCount() < MarksmanSettings.Instance.Mobs || !Me.CurrentTarget.InLineOfSight ||  Me.CurrentTarget.Distance > 40 || Me.CurrentTarget.Distance < 5 || SpellManager.Spells["Explosive Trap"].CooldownTimeLeft.TotalSeconds > 1))
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
					if (MarksmanSettings.Instance.MP && Me.GotAlivePet && Me.Pet.HealthPercent < 50 && !Me.Pet.ActiveAuras.ContainsKey("Mend Pet"))
					{
						if(CastSpell("Mend Pet"))
						{
						Logging.Write(Color.Aqua, ">> Mend Pet <<");
						}
					}
	///////////////////////////////////////////Close Combat and Defense Mechanisms//////////////////////////////////////////////////////////////////////////////////////
					if (MarksmanSettings.Instance.AGR && Me.CurrentTarget.ThreatInfo.RawPercent > 90 && (Me.CurrentTarget.CurrentTargetGuid == Me.Guid || Me.CurrentTarget.IsCasting))
					{
						if (CastSpell("Feign Death"))
						{
							Logging.Write(Color.Aqua, ">> Aggro'd Feign Death <<");
						}
					}
					if (MarksmanSettings.Instance.INT && MarksmanSettings.Instance.MMSPEC && SpellManager.HasSpell("Silencing Shot") && Me.CurrentTarget.IsCasting && Me.CanInterruptCurrentSpellCast && Me.CurrentTarget.Distance > 5)
					{
						if (CastSpell("Silencing Shot"))
						{
						Logging.Write(Color.Aqua, ">> Silencing Shot <<");
						}
					}
					if (MarksmanSettings.Instance.INT && Me.CurrentTarget.Distance <= 20 && ((MarksmanSettings.Instance.MMSPEC && SpellManager.Spells["Silencing Shot"].CooldownTimeLeft.TotalSeconds > 1) || (MarksmanSettings.Instance.SSPEC)) && Me.CurrentTarget.IsCasting && Me.CanInterruptCurrentSpellCast)
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
					if (MarksmanSettings.Instance.DIS && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && Me.CurrentTarget.Distance < 5 && Me.HealthPercent < 60)
					{
						if (CastSpell("Disengage"))
						{
						Logging.Write(Color.Aqua, ">> Disengage <<");
						}
					}
					if (MarksmanSettings.Instance.DETR &&	Me.HealthPercent < 20 && Me.CurrentTarget.CurrentTargetGuid == Me.Guid && (Me.CurrentTarget.Distance < 10 || Me.CurrentTarget.IsCasting))
					{
						if (CastSpell("Deterrence"))
						{
						Logging.Write(Color.Aqua, ">> Deterrence <<");
						}
					}				
	/////////////////////////////////////////////////////Cooldowns are here/////////////////////////////////////////////////////////////////////////////////////////////////           
					if (MarksmanSettings.Instance.RF && !Me.ActiveAuras.ContainsKey("Rapid Fire") && IsTargetBoss())
					{
						if (CastSpell("Rapid Fire"))
						{
							Logging.Write(Color.Aqua, ">> Rapid Fire <<");
						}
					}
					if (MarksmanSettings.Instance.MMSPEC && MarksmanSettings.Instance.RF && IsTargetBoss() && ((Me.ActiveAuras.ContainsKey("Rapid Fire") && SpellManager.Spells["Rapid Fire"].CooldownTimeLeft.TotalSeconds > 10) || SpellManager.Spells["Rapid Fire"].CooldownTimeLeft.TotalSeconds > 120))
					{
						if (CastSpell("Readiness"))
						{
							Logging.Write(Color.Aqua, ">> Readiness <<");
						}
					}
					if (MarksmanSettings.Instance.LB && IsTargetBoss() && !Me.ActiveAuras.ContainsKey("Rapid Fire"))
					{
						if (CastSpell("Lifeblood"))
						{
							Logging.Write(Color.Aqua, ">> Lifeblood <<");
						}
					}
					if (MarksmanSettings.Instance.GE && IsTargetBoss())
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
				//////////////////////////////////////////////////Racial Skills here/////////////////////////////////////////////////////////////////////////////////////////
					if (MarksmanSettings.Instance.RS && Me.Race == WoWRace.Troll && IsTargetBoss() && !SpellManager.Spells["Berserking"].Cooldown)
					{
						Lua.DoString("RunMacroText('/Cast Berserking');");
					}
					if (MarksmanSettings.Instance.RS && Me.Race == WoWRace.Orc && IsTargetBoss() && !SpellManager.Spells["Blood Fury"].Cooldown)
					{
						Lua.DoString("RunMacroText('/Cast Blood Fury');");
					}        
				}
			//////////////////////////////////////////////////MM Spec Rotations/////////////////////////////////////////////////////////////////////////////////////////
                if (Me.GotTarget && (addCount() < MarksmanSettings.Instance.Mobs || (!MarksmanSettings.Instance.MS && !MarksmanSettings.Instance.TL)) && MarksmanSettings.Instance.MMSPEC && !MarksmanSettings.Instance.ExploROT && Me.CurrentTarget.Distance >= 5 && HaltTrap() && HaltFeign() && Me.CurrentTarget.IsAlive && !Me.Mounted)
                {
					if (MarksmanSettings.Instance.STING && !IsMyAuraActive(Me.CurrentTarget, "Serpent Sting"))
					{
						if (CastSpell("Serpent Sting"))
						{
							Logging.Write(Color.Aqua, ">> Serpent Sting <<");
						}
					}
					if (MyDebuffTime("Serpent Sting", Me.CurrentTarget) >= 1)
					{
						if (CastSpell("Chimera Shot"))
						{
							Logging.Write(Color.Aqua, ">> Chimera Shot <<");
						}
					}
					if (MarksmanSettings.Instance.HM && !Me.CurrentTarget.HasAura("Hunter's Mark") && !Me.CurrentTarget.HasAura("Marked for Death") && (addCount() < 2 || IsTargetBoss()))
					{
						if (CastSpell("Hunter's Mark"))
						{
							Logging.Write(Color.Aqua, ">> Hunter's Mark <<");
						}
					}
					if (SpellManager.Spells["Chimera Shot"].CooldownTimeLeft.TotalMilliseconds > 500 && Me.Auras.ContainsKey("Fire!"))
					{
						Lua.DoString(String.Format("RunMacroText(\"/use Aimed Shot!\")"));
						SpellManager.StopCasting();
						{
							Logging.Write(Color.Aqua, ">> Free Aimed Shot <<");
						}
					}
					if (Me.CurrentTarget.HealthPercent <= 20)
					{
						if (CastSpell("Kill Shot"))
						{
							Logging.Write(Color.Aqua, ">> Kill Shot <<");
						}
					}
	/////////////////////////////////////////////Aimed Shot Rotation////////////////////////////////////////////////////////////////////////////////////////////
					if (MarksmanSettings.Instance.AimedROT && !Me.IsMoving && SpellManager.Spells["Chimera Shot"].CooldownTimeLeft.TotalSeconds > 1)
					{
						if (CastSpell("Aimed Shot"))
						{
							Logging.Write(Color.Aqua, ">> Aimed Shot <<");
						}
					}
	/////////////////////////////////////////////Arcane Shot Rotation///////////////////////////////////////////////////////////////////////////////////////////
					if (MarksmanSettings.Instance.ArcaneROT && !Me.IsMoving && ((Me.CurrentFocus >= 50 && SpellManager.Spells["Chimera Shot"].CooldownTimeLeft.TotalSeconds > 1) || SpellManager.Spells["Chimera Shot"].CooldownTimeLeft.TotalSeconds >= 4))
					{
						if (CastSpell("Arcane Shot"))
						{
							Logging.Write(Color.Aqua, ">> Arcane Shot <<");
						}
					}
	////////////////////////////////Aimed + Arcane shot Rotation/////Aimed shot when haste bonus, else Arcane shot///////////////////////////				
					if (MarksmanSettings.Instance.MixedROT && !Me.IsMoving && (AimedShot() || Me.CurrentTarget.HealthPercent >= 90) && SpellManager.Spells["Chimera Shot"].CooldownTimeLeft.TotalSeconds >= 1)
					{
						if (CastSpell("Aimed Shot"))
						{
							Logging.Write(Color.Aqua, ">> Aimed Shot <<");
						}
					}
					if (MarksmanSettings.Instance.MixedROT && Me.CurrentTarget.HealthPercent < 90 && !Me.IsMoving && !AimedShot() && ((Me.CurrentFocus >= 50 && SpellManager.Spells["Chimera Shot"].CooldownTimeLeft.TotalSeconds > 1) || SpellManager.Spells["Chimera Shot"].CooldownTimeLeft.TotalSeconds >= 4))
					{
						if (CastSpell("Arcane Shot"))
						{
							Logging.Write(Color.Aqua, ">> Arcane Shot <<");
						}
					}
	//////////////////////////////////////////////////MM Spec continued//////////////////////////////////////////////////////////////////////////////////////////
					if (!Me.ActiveAuras.ContainsKey("Fire!") && !Me.IsMoving && Me.CurrentFocus <= MarksmanSettings.Instance.FocusShots && !SpellManager.CanCast("Kill Shot"))
					{
						if (CastSpell("Steady Shot"))
						{
							Logging.Write(Color.Aqua, ">> Steady Shot <<");
						}
					}
                } 
        /////////////////////////////////////////////Survival Spec Rotation///////////////////////////////////////////////////////////////////////////////////////////
            if (Me.GotTarget && (addCount() < MarksmanSettings.Instance.Mobs || (!MarksmanSettings.Instance.MS && !MarksmanSettings.Instance.TL)) && MarksmanSettings.Instance.ExploROT && MarksmanSettings.Instance.SSPEC && Me.CurrentTarget.Distance >= 5 && HaltTrap() && HaltFeign() && Me.CurrentTarget.IsAlive && !Me.Mounted)
            {
				if (MarksmanSettings.Instance.HM && Me.CurrentTarget.HealthPercent > 20 && !Me.CurrentTarget.HasAura("Hunter's Mark"))
				{
					if (CastSpell("Hunter's Mark"))
					{
						Logging.Write(Color.Aqua, ">> Hunter's Mark <<");
					}
				}
				if (!SpellManager.Spells["Explosive Shot"].Cooldown && MyDebuffTime("Explosive Shot", Me.CurrentTarget) <= 1)
				{
					if (CastSpell("Explosive Shot"))                     
					{
						Logging.Write(Color.Aqua, ">> Explosive Shot <<");
					}
				}
				if (Me.CurrentTarget.HealthPercent < 20)
				{
					if (CastSpell("Kill Shot"))
					{
						Logging.Write(Color.Aqua, ">> Kill Shot <<");
					}
				}
				if (MarksmanSettings.Instance.STING && !IsMyAuraActive(Me.CurrentTarget, "Serpent Sting") && Me.CurrentTarget.HealthPercent > 10)
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
				if (Me.CurrentFocus >= 66 && SpellManager.Spells["Explosive Shot"].CooldownTimeLeft.TotalSeconds > 1)
				{
					if (CastSpell("Arcane Shot"))
					{
						Logging.Write(Color.Aqua, ">> Arcane Shot <<");
					}
				}
				if (Me.IsMoving && Me.Auras.ContainsKey("Aspect of the Fox") && !Me.ActiveAuras.ContainsKey("Lock and Load") && Me.CurrentFocus < 66 && !SpellManager.CanCast("Kill Shot"))
				{
					Lua.DoString("RunMacroText('/cast Cobra Shot');");
					{
						Logging.Write(Color.Red, ">> Moving - Cobra Shot <<");
					}
				}
				if (!Me.ActiveAuras.ContainsKey("Lock and Load") && !Me.IsMoving && Me.CurrentFocus < MarksmanSettings.Instance.FocusShots && !Me.IsCasting && !SpellManager.CanCast("Kill Shot"))
				{
					if (CastSpell("Cobra Shot"))
					{
						Logging.Write(Color.Aqua, ">> Cobra Shot <<");
					}
				}
			}
        ///////////////////////////////////////////////Moving Rotation here////////////////////////////////////////////////////////////////////////////////////////////
			if (HaltTrap() && HaltFeign() && Me.CurrentTarget != null && Me.CurrentTarget.IsAlive && !Me.Mounted)
            {
				if (!Me.IsMoving && !Me.Auras.ContainsKey("Aspect of the Hawk") && MarksmanSettings.Instance.AspectSwitching)
				{
					if (CastSpell("Aspect of the Hawk"))
					{
						Logging.Write(Color.Aqua, ">> Not moving - Aspect of the Hawk <<");
					}
				}
				if (Me.IsMoving && Me.Auras.ContainsKey("Aspect of the Hawk") && MarksmanSettings.Instance.AspectSwitching && Me.CurrentFocus < 66)
				{
					if (CastSpell("Aspect of the Fox"))
					{
						Logging.Write(Color.Aqua, ">> Moving - Aspect of the Fox <<");
					}
				}
                
                if (addCount() < MarksmanSettings.Instance.Mobs && MarksmanSettings.Instance.MMSPEC && Me.CurrentTarget.Distance >= 5 && Me.IsMoving)
                {
					if (Me.IsMoving && Me.CurrentFocus >= 50)
					{
						Lua.DoString("RunMacroText('/cast Arcane Shot');");
						{
							Logging.Write(Color.Aqua, ">> Moving - Arcane Shot <<");
						}
					}
					if (addCount() < MarksmanSettings.Instance.Mobs && Me.IsMoving && Me.CurrentTarget.Distance >= 5 && Me.Auras.ContainsKey("Aspect of the Fox") && !SpellManager.CanCast("Kill Shot"))
					{
						Lua.DoString("RunMacroText('/cast Steady Shot');");
						{
							Logging.Write(Color.Red, ">> Moving - Steady Shot <<");
						}
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
				if (!Me.HasAura("Trap Launcher") && MarksmanSettings.Instance.SSPEC && MarksmanSettings.Instance.ExploROT && Me.CurrentTarget.Distance >= 5 && !SpellManager.Spells["Explosive Shot"].Cooldown && MyDebuffTime("Explosive Shot", Me.CurrentTarget) <= 1 && Me.ActiveAuras.ContainsKey("Lock and Load"))
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
				if (!Me.HasAura("Trap Launcher") && MarksmanSettings.Instance.SSPEC && Me.CurrentTarget.Distance >= 5 && Me.CurrentFocus <= 42 && !SpellManager.CanCast("Kill Shot"))
				{
					if (CastSpell("Cobra Shot"))
					{
						Logging.Write(Color.Aqua, ">> Cobra Shot <<");
					}
				}
				if (!Me.HasAura("Trap Launcher") && MarksmanSettings.Instance.MMSPEC && Me.CurrentTarget.Distance >= 5 && Me.CurrentFocus <= 42 && !SpellManager.CanCast("Kill Shot"))
				{
					if (CastSpell("Steady Shot"))
					{
						Logging.Write(Color.Aqua, ">> Steady Shot <<");
					}
				}
            }
        }
        #endregion
    }
}