﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using LeagueSharp.Common;
using LeagueSharp;

namespace DisableSpells
{
    internal class Program
    {
        public struct SpellStruct
        {
            public string ChampionName;
            public SpellSlot AvailableSpell;

        }
        public static List<SpellStruct> Spells = new List<SpellStruct>();
        public static Menu Config;

        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        private static void Game_OnGameLoad(EventArgs args)
        {
            Spells.Add(new SpellStruct
            {
                ChampionName = "Rumble",
                AvailableSpell = SpellSlot.W
            });
            
            Spells.Add(new SpellStruct
            {
                ChampionName = "Vladimir",
                AvailableSpell = SpellSlot.W
            });

            Spells.Add(new SpellStruct
            {
                ChampionName = "Sion",
                AvailableSpell = SpellSlot.W
            });
            
            Spells.Add(new SpellStruct
            {
                ChampionName = "Lee Sin",
                AvailableSpell = SpellSlot.Q
            });
            
            Spells.Add(new SpellStruct
            {
                ChampionName = "Riven",
                AvailableSpell = SpellSlot.R
            });
            
             Spells.Add(new SpellStruct
            {
                ChampionName = "Lux",
                AvailableSpell = SpellSlot.R
            });
            
             Spells.Add(new SpellStruct
            {
                ChampionName = "Nasus",
                AvailableSpell = SpellSlot.Q
            });
            
             Spells.Add(new SpellStruct
            {
                ChampionName = "Twitch",
                AvailableSpell = SpellSlot.W
            });
            
             Spells.Add(new SpellStruct
            {
                ChampionName = "Thresh",
                AvailableSpell = SpellSlot.E
            });

            Spells.Add(new SpellStruct
            {
                ChampionName = "TwistedFate",
                AvailableSpell = SpellSlot.W
            });

            Spells.Add(new SpellStruct
            {
                ChampionName = "Jax",
                AvailableSpell = SpellSlot.E
            });

            Spells.Add(new SpellStruct
            {
                ChampionName = "MasterYi",
                AvailableSpell = SpellSlot.R
            });
            
            Spells.Add(new SpellStruct
            {
                ChampionName = "Taric",
                AvailableSpell = SpellSlot.Q
            });
            
            Spells.Add(new SpellStruct
            {
                ChampionName = "Fiddlesticks",
                AvailableSpell = SpellSlot.E
            });
            
            Spells.Add(new SpellStruct
            {
                ChampionName = "Sivir",
                AvailableSpell = SpellSlot.Q
            });
            
            Spells.Add(new SpellStruct
            {
                ChampionName = "Syndra",
                AvailableSpell = SpellSlot.Q
            });
            
            
            Spells.Add(new SpellStruct
            {
                ChampionName = "Lulu",
                AvailableSpell = SpellSlot.Q
            });
            
            Spells.Add(new SpellStruct
            {
                ChampionName = "Lulu",
                AvailableSpell = SpellSlot.E
            });

            Spells.Add(new SpellStruct
            {
                ChampionName = "Annie",
                AvailableSpell = SpellSlot.E
            });

            Spells.Add(new SpellStruct
            {
                ChampionName = "Singed",
                AvailableSpell = SpellSlot.R
            });

            Spells.Add(new SpellStruct
            {
                ChampionName = "Vayne",
                AvailableSpell = SpellSlot.R
            });

            Spells.Add(new SpellStruct
            {
                ChampionName = "Tryndamere",
                AvailableSpell = SpellSlot.R
            });
            
            Spells.Add(new SpellStruct
            {
                ChampionName = "Kog'Maw",
                AvailableSpell = SpellSlot.W
            });
            
            Spells.Add(new SpellStruct
            {
                            ChampionName = "Lucian",
                AvailableSpell = SpellSlot.R
            });

            Spells.Add(new SpellStruct
            {
                ChampionName = "Teemo",
                AvailableSpell = SpellSlot.W
            });
            
            Spells.Add(new SpellStruct
            {
                            ChampionName = "Malphite",
                AvailableSpell = SpellSlot.Q
            });

            Spells.Add(new SpellStruct
            {
                ChampionName = "Blitzcrank",
                AvailableSpell = SpellSlot.W
            });

            Spells.Add(new SpellStruct
            {
                ChampionName = "Ashe",
                AvailableSpell = SpellSlot.Q
            });

            Spells.Add(new SpellStruct
            {
                ChampionName = "Zilean",
                AvailableSpell = SpellSlot.W
            });

            /*
             * Ashe:
             * -> Fiora: OnAttack: Instant ultimate / no duration limit / less damage / can be attacked
             * -> Twitch: OnAttack: Cast's W without CD except of AA
             * -> TwistedFate: OnAttack: Always shoots with red card
             * -> Ezreal: OnAttack: E particle, ways less damage, ways less attackspeed
             * -> Lucian: OnAttack: R particle, goes throguh enemys, ways less damage, ways less attackspeed
             * -> Brand: OnAttack: Ultimate
             * -> Pantheon: Weird shit.
             * -> Gragas: OnAttack: Ultimate with a cd of 10-15sec
             * -> Varus: Uses the area Damage on attack
             * -> Jax: Possible to stun everyone
             * -> Lulu: OnAttack: Lulu AA becomes her Q and Pix also CS
             */

            Config = new Menu("Exploit", "Exploit", true);
            Config.AddSubMenu(new Menu("Disable", "Disable"));
            foreach (var hero in ObjectManager.Get<Obj_AI_Hero>().Where(hero => !hero.IsMe))
            {
                Config.SubMenu("Disable")
                    .AddItem(
                        new MenuItem(hero.ChampionName, "Disable on " + hero.ChampionName).SetValue(false));
                Config.Item(hero.ChampionName).SetValue(false);
            }
            Config.AddToMainMenu();

            Game.PrintChat("Exploit loaded!");
            Game.OnGameUpdate += Game_OnGameUpdate;
        }

        private static void Game_OnGameUpdate(EventArgs args)
        {
            foreach (var spell in Spells)
            {
                if (spell.ChampionName == ObjectManager.Player.ChampionName)
                {
                    foreach (var hero in from hero in ObjectManager.Get<Obj_AI_Hero>().Where(hero => !hero.IsMe)
                        let isEnabled = Config.Item(hero.ChampionName).GetValue<bool>()
                        let championName = Config.Item(hero.ChampionName).Name
                        where hero.ChampionName == championName & isEnabled && !hero.IsDead
                        select hero)
                    {
                        Packet.C2S.Cast.Encoded(new Packet.C2S.Cast.Struct(hero.NetworkId, spell.AvailableSpell)).Send();
                    }   
                }
            }
        }
    }
}
