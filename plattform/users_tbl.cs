//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace plattform
{
    using System;
    using System.Collections.Generic;
    
    public partial class users_tbl
    {
        public int id { get; set; }
        public string vorname { get; set; }
        public string nachname { get; set; }
        public string adresse { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string password { get; set; }
        public string title { get; set; }

        public string  LoginErrorMessage { get; set; }
    }
}
