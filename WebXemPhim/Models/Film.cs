//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebXemPhim.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Film
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Film()
        {
            this.Comments = new HashSet<Comment>();
        }
    
        public int FilmID { get; set; }
        public string Filmname { get; set; }
        public int AuthorID { get; set; }
        public int CategoryID { get; set; }
        public int GenreID { get; set; }
        public Nullable<double> Length { get; set; }
        public Nullable<System.DateTime> ReleaseDay { get; set; }
        public Nullable<double> Rating { get; set; }
        public Nullable<int> CurrnentEpisodes { get; set; }
        public Nullable<int> Episodes { get; set; }
        public int CountryID { get; set; }
        public Nullable<bool> Status { get; set; }
        public string FilmPath { get; set; }
        public byte[] Poster { get; set; }
    
        public virtual Author Author { get; set; }
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual Country Country { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
