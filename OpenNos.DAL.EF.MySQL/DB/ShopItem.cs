//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenNos.DAL.EF.MySQL.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShopItem
    {
        public int ShopItemId { get; set; }
        public short Type { get; set; }
        public short Slot { get; set; }
        public short ItemVNum { get; set; }
        public short Upgrade { get; set; }
        public short Rare { get; set; }
        public short Color { get; set; }
        public int ShopId { get; set; }
    
        public virtual Item item { get; set; }
        public virtual Shop shop { get; set; }
    }
}
