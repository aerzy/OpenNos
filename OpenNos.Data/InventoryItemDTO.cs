﻿using OpenNos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenNos.Data
{
    public class InventoryItemDTO
    {
        public long InventoryItemId { get; set; }
        public short DamageMinimum { get; set; }
        public short DamageMaximum { get; set; }
        public short Concentrate { get; set; }
        public short HitRate { get; set; }
        public short CriticalLuckRate { get; set; }
        public short CriticalRate { get; set; }
        public short RangeDefence { get; set; }
        public short DistanceDefence { get; set; }
        public short MagicDefence { get; set; }
        public short Dodge { get; set; }
        public short ElementRate { get; set; }
        public short Upgrade { get; set; }
        public short Rare { get; set; }
        public short Color { get; set; }
        public short Amount { get; set; }
        public short SlElement { get; set; }
        public short SpLevel { get; set; }
        public short SpXp { get; set; }
        public short SlHit { get; set; }
        public short SlDefence { get; set; }
        public short SlHP { get; set; }
        public short DarkElement { get; set; }
        public short LightElement { get; set; }
        public short WaterElement { get; set; }
        public short FireElement { get; set; }
        public short ItemVNum { get; set; }
    }
}
