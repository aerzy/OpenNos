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
    
    public partial class GeneralLog
    {
        public long LogId { get; set; }
        public long AccountId { get; set; }
        public string IpAddress { get; set; }
        public System.DateTime Timestamp { get; set; }
        public string LogType { get; set; }
        public string LogData { get; set; }
        public Nullable<long> CharacterId { get; set; }
    
        public virtual Account account { get; set; }
        public virtual Character character { get; set; }
    }
}
