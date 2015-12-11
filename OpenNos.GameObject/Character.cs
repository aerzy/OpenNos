﻿using OpenNos.Core;
using OpenNos.Data;
using OpenNos.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenNos.GameObject
{
    public class Character : INotifyPropertyChanged
    {
        private void OnPropertyChanged()
        {
             ServerManager.GetMap(Map).RegisterCharacter(this);          
        }
        private long characterId;
        public long CharacterId
        {
            get { return characterId; }
            set
            {
                characterId = value;
                OnPropertyChanged();
            }
        }

        public long accountId;
        public long AccountId
        {
            get { return accountId; }
            set
            {
                accountId = value;
                OnPropertyChanged();
            }
        }
        public string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public byte slot;
        public byte Slot
        {
            get { return slot; }
            set
            {
                slot = value;
                OnPropertyChanged();
            }
        }
        public byte gender;
        public byte Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged();
            }
        }

        public byte classobj;
        public byte Class
        {
            get { return classobj; }
            set
            {
                classobj = value;
                OnPropertyChanged();
            }
        }
        public byte hairStyle;
        public byte HairStyle
        {
            get { return hairStyle; }
            set
            {
                hairStyle = value;
                OnPropertyChanged();
            }
        }
        public byte hairColor;
        public byte HairColor
        {
            get { return hairColor; }
            set
            {
                hairColor = value;
                OnPropertyChanged();
            }
        }
        public short map;
        public short Map {
            get { return map; }
            set
            {
                ServerManager.GetMap(Map).UnregisterCharacter(this);
                map = value;
                OnPropertyChanged();
            }
        }
        public short mapX;
        public short MapX {
            get { return mapX; }
            set
            {
                mapX = value;
                OnPropertyChanged();
            }
        }
        public short mapY;
        public short MapY {
            get { return mapY; }
            set
            {
                mapY = value;
                OnPropertyChanged();
            }
        }
        public int hp;
        public int Hp {
            get { return hp; }
            set
            {
                hp = value;
                OnPropertyChanged();
            }
        }
        public int mp;
        public int Mp {
            get { return mp; }
            set
            {
                mp = value;
                OnPropertyChanged();
            }
        }
        public long gold;
        public long Gold {
            get { return gold; }
            set
            {
                gold = value;
                OnPropertyChanged();
            }
        }
        public byte jobLevel;
        public byte JobLevel {
            get { return jobLevel; }
            set
            {
                jobLevel = value;
                OnPropertyChanged();
            }
        }
        public long jobLevelXp;
        public long JobLevelXp {
            get { return jobLevelXp; }
            set
            {
                jobLevelXp = value;
                OnPropertyChanged();
            }
        }
        public byte level;
        public byte Level {
            get { return level; }
            set
            {
                level = value;
                OnPropertyChanged();
            }
        }
        public long levelXp;
        public long LevelXp {
            get { return levelXp; }
            set
            {
                levelXp = value;
                OnPropertyChanged();
            }
        }
        public int reput;
        public int Reput {
            get { return reput; }
            set
            {
                reput = value;
                OnPropertyChanged();
            }
        }
        public int dignite;
        public int Dignite {
            get { return dignite; }
            set
            {
                dignite = value;
                OnPropertyChanged();
            }
        }
        public int lastPulse;
        public int LastPulse {
            get { return lastPulse; }
            set
            {
                lastPulse = value;
                OnPropertyChanged();
            }
        }
        public double lastPortal;
        public double LastPortal
        {
            get { return lastPortal; }
            set
            {
                lastPortal = value;
                OnPropertyChanged();
            }
        }
        public int sp;
        public int Sp {
            get { return sp; }
            set
            {
                sp = value;
                OnPropertyChanged();
            }
        }
        public int authority;
        public int Authority {
            get { return authority; }
            set
            {
                authority = value;
                OnPropertyChanged();
            }
        }
        public int invisible;
        public int Invisible {
            get { return invisible; }
            set
            {
                invisible = value;
                OnPropertyChanged();
            }
        }
        public int speed;
        public int Speed {
            get { return speed; }
            set
            {
                speed = value;
                OnPropertyChanged();
            }
        }
        public int arenaWinner;
        public int ArenaWinner {
            get { return arenaWinner; }
            set
            {
                arenaWinner = value;
                OnPropertyChanged();
            }
        }
        public int spUpgrade;
        public int SpUpgrade {
            get { return spUpgrade; }
            set
            {
                spUpgrade = value;
                OnPropertyChanged();
            }
        }
        public int direction;
        public int Direction {
            get { return direction; }
            set
            {
                direction = value;
                OnPropertyChanged();
            }
        }
        public int rested;
        public int Rested {
            get { return rested; }
            set
            {
                rested = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string GenerateEff(int effectid)
        {
            return String.Format("eff 1 {0} {1}", CharacterId, effectid);
        }
        public List<String> GenerateGp()
        {
            List<String> gpList = new List<String>();
            foreach (Portal portal in ServerManager.GetMap(this.Map).Portals)
                gpList.Add(String.Format("gp {0} {1} {2} {3} {4}", portal.SrcX, portal.SrcY, portal.DestMap, portal.Type, 0));
            return gpList;
        }
        public string GenerateFd()
        {
           return String.Format("fd {0} {1} {2} {3}", Reput, GetReputIco(), Dignite, Math.Abs(GetDigniteIco()));
        }
        public string GenerateMapOut()
        {
            return String.Format("mapout");
        }

        public string GenerateOut()
        {
            return String.Format("out 1 {0}",CharacterId);
        }
        public double HPLoad()
        {
            return ServersData.HPData[Class,Level];
        }

        public double MPLoad()
        {
            return ServersData.MPData[Class,Level];
        }

        public double SPXPLoad()
        {           
            return ServersData.SpXPData[JobLevel - 1];
        }

      
        public double XPLoad()
        {   
            return ServersData.XPData[Level - 1];
        }


        public double JobXPLoad()
        {
            if(Class == (byte)ClassType.Adventurer)
                return ServersData.FirstJobXPData[JobLevel - 1];
            else
                return ServersData.SecondJobXPData[JobLevel - 1];
        }

        public string GenerateLev()
        {
           return String.Format("lev {0} {1} {2} {3} {4} {5} 0 2", Level, LevelXp, JobLevel, JobLevelXp,XPLoad(),JobXPLoad());
        }

        public string GenerateCInfo()
        {
           return String.Format("c_info {0} - {1} {2} - {3} {4} {5} {6} {7} {8} {9} {10} {11} {12} {13} {14} {15} ", Name, -1, -1, CharacterId, Authority, Gender, HairStyle, HairColor, Class, GetReputIco(), 0, Sp, Invisible, 0, SpUpgrade, ArenaWinner);
        }

        public int GetDigniteIco()
        {
            int icoDignite = 1;
            if (Convert.ToInt32(Dignite) <= -100)
                icoDignite = 2;
            if (Convert.ToInt32(Dignite) <= -200)
                icoDignite = 3;
            if (Convert.ToInt32(Dignite) <= -400)
                icoDignite = 4;
            if (Convert.ToInt32(Dignite) <= -600)
                icoDignite = 5;
            if (Convert.ToInt32(Dignite) <= -800)
                icoDignite = 6;

            return icoDignite;

        }

        public int GetReputIco()
        {
            return Convert.ToInt64(Reput) <= 100 ? 1 : Convert.ToInt64(Reput) <= 300 ? 2 : Convert.ToInt64(Reput) <= 500 ? 3 : Convert.ToInt64(Reput) <= 1000 ? 4 : Convert.ToInt64(Reput) <= 1500 ? 5 : Convert.ToInt64(Reput) <= 2000 ? 6 : Convert.ToInt64(Reput) <= 4500 ? 7 :
            Convert.ToInt64(Reput) <= 7000 ? 8 : Convert.ToInt64(Reput) <= 10000 ? 9 : Convert.ToInt64(Reput) <= 19000 ? 10 : Convert.ToInt64(Reput) <= 38000 ? 11 : Convert.ToInt64(Reput) <= 50000 ? 12 : Convert.ToInt64(Reput) <= 80000 ? 13 : Convert.ToInt64(Reput) <= 120000 ? 14 :
                    Convert.ToInt64(Reput) <= 170000 ? 15 : Convert.ToInt64(Reput) <= 230000 ? 16 : Convert.ToInt64(Reput) <= 300000 ? 17 : Convert.ToInt64(Reput) <= 380000 ? 18 : Convert.ToInt64(Reput) <= 470000 ? 19 : Convert.ToInt64(Reput) <= 570000 ? 20 : Convert.ToInt64(Reput) <= 700000 ? 21 :
                    Convert.ToInt64(Reput) <= 1000000 ? 22 : Convert.ToInt64(Reput) <= 3000000 ? 23 : Convert.ToInt64(Reput) <= 5000000 ? 24 : Convert.ToInt64(Reput) <= 7000000 ? 25 : Convert.ToInt64(Reput) <= 10000000 ? 26 : Convert.ToInt64(Reput) <= 50000000 ? 27 : 30;

        }

        public string GenerateTit()
        {
            return String.Format("tit {0} {1}", Language.Instance.GetMessageFromKey(Class == (byte)ClassType.Adventurer ? ClassType.Adventurer.ToString().ToUpper() : Class == (byte)ClassType.Swordman ? ClassType.Swordman.ToString().ToUpper() : Class == (byte)ClassType.Archer ? ClassType.Archer.ToString().ToUpper() : ClassType.Magician.ToString().ToUpper()), Name);
            
        }

        public string GenerateStat()
        {
            //TODO add max HP MP
            return String.Format("stat {0} {1} {2} {3} 0 1024", Hp, HPLoad(), Mp, MPLoad());
           
        }

        public string GenerateAt()
        {
            return String.Format("at {0} {1} {2} {3} 2 0 0 1", CharacterId, Map, MapX, MapY);
           
        }

        public string GenerateCMap()
        {
           return String.Format("c_map 0 {0} 1", Map);
           
        }

        public string GenerateCond()
        {
           return String.Format("cond 1 {0} 0 0 {1}", CharacterId, Speed);

        }

        public string GenerateExts()
        {
           return String.Format("exts 0 48 48 48"); 
        }

        public string GenerateMv(int x,int y )
        {
            return String.Format("mv 1 {0} {1} {2} {3}", CharacterId, x, y, Speed);

        }

        public string GenerateSay(string message,int type)
        {
            return String.Format("say 1 {0} {1} {2}", CharacterId,type, message);
           
        }

        public string GenerateIn()
        {
            return String.Format("in 1 {0} - {1} {2} {3} {4} {5} {6} {7} {8} {9} -1.-1.-1.-1.-1.-1.-1.-1 {10} {11} {12} -1 0 0 0 0 0 0 0 0 -1 - {13} 0 0 0 0 {14} 0 {15} 0 10", Name, CharacterId, MapX, MapY, Direction, (Authority == 2 ? 2 : 0), Gender, HairStyle, HairColor, Class, 100, 100, Sp, (GetDigniteIco() == 1) ? GetReputIco() : -GetDigniteIco(), ArenaWinner,0);
          
        }

        public string GenerateRest()
        {
            return String.Format("rest 1 {0} {1}", CharacterId, Rested);
        }

        public string GenerateDir()
        {
            return String.Format("dir 1 {0} {1}", CharacterId, Direction);
        }
    }
}
