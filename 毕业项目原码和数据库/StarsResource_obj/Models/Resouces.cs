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
            this.Category = new HashSet<Category>();
            this.UserInfo = new HashSet<UserInfo>();
        }
    
        public int ResID { get; set; }
        public int RuserID { get; set; }
        public Nullable<int> LinkID { get; set; }
        public System.DateTime Releasetime { get; set; }
        public string Rname { get; set; }
        public string Rpicture { get; set; }
        public string Rdescribe { get; set; }
        public string Rstatement { get; set; }
        public Nullable<int> Rdemand { get; set; }
        public Nullable<int> Rstate { get; set; }
        public Nullable<int> ComID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Category> Category { get; set; }
        public virtual Comment Comment { get; set; }
        public virtual Link Link { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserInfo> UserInfo { get; set; }
    }
}
