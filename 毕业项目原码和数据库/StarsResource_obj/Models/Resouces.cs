//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Resouces
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Resouces()
        {
            this.Comment = new HashSet<Comment>();
            this.Collection = new HashSet<Collection>();
        }
    
        public int ResoucesID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> LinkID { get; set; }
        public Nullable<int> PictureID { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> LableID { get; set; }
        public System.DateTime Releasetime { get; set; }
        public string Rname { get; set; }
        public string Rdescribe { get; set; }
        public Nullable<int> Rdemand { get; set; }
        public Nullable<int> Rstate { get; set; }
        public int Reading { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual Lable Lable { get; set; }
        public virtual Link Link { get; set; }
        public virtual Picture Picture { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Collection> Collection { get; set; }
    }
}
