//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenNos.DAL.EF.MySQL.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class partner
    {
        public int PartnerId { get; set; }
        public long CharacterId { get; set; }
        public int owner { get; set; }
        public Nullable<int> Level { get; set; }
        public Nullable<int> isBackpacked { get; set; }
        public Nullable<int> SP { get; set; }
        public Nullable<int> Weapon { get; set; }
        public Nullable<int> Armor { get; set; }
        public Nullable<int> Gloves { get; set; }
        public Nullable<int> Boots { get; set; }
        public Nullable<int> IsTeamed { get; set; }
        public Nullable<int> Hp { get; set; }
        public Nullable<int> Mp { get; set; }
    }
}
