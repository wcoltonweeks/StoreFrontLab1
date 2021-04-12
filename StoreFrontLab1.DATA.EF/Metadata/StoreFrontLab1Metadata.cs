using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFrontLab1.DATA.EF//.Metadata
{
    class StoreFrontLab1Metadata
    {
        [MetadataType(typeof(PaintingMetadata))]
        public partial class Painting { }

        public class PaintingMetadata
        {
            public int PaintingID { get; set; }
            [Required(ErrorMessage = "* Painting Title is required")]
            [StringLength(15, ErrorMessage = "* Max 14 characters")]
            public string PaintingTitle { get; set; }
            [Required(ErrorMessage = "* Size is required")]
            public int SizeID { get; set; }     
            [Required(ErrorMessage = "* Status is required")]
            public int StatusID { get; set; }
            [StringLength(120, ErrorMessage = "* Description must not exceed 120 characters.")]
            public string Description { get; set; }
            public string PaintingImg { get; set; }
            [Required(ErrorMessage = "* Price is required")]
            [DisplayFormat(DataFormatString = "{0:c}")]
            public decimal Price { get; set; }

        }

        [MetadataType(typeof(PaintingCopyMetadata))]
        public partial class PaintingCopy { }

        public class PaintingCopyMetadata
        {
            public int CopyID { get; set; }
            [Required(ErrorMessage = ("* PaintingId is required"))]
            public int PaintingID { get; set; }
            [Required(ErrorMessage ="* Price is required")]
            [DisplayFormat(DataFormatString = "{0:c}")]
            public decimal Price { get; set; }
            [Required(ErrorMessage ="* Status is required")]
            [Display(Name = "Status")]
            public int StatusID { get; set; }
            [Required(ErrorMessage = "* Size is required")]
            [Display(Name = "Size")]
            public int SizeID { get; set; }
        }

        [MetadataType(typeof(SizeMetadata))]
        public partial class Size { }

        public class SizeMetadata
        {
            public int SizeID { get; set; }
            [Required(ErrorMessage = "* Size is required")]
            [Display(Name ="Size")]
            public string Size1 { get; set; }
        }

        [MetadataType(typeof(StatusMetadata))]
        public partial class Status { }

        public class StatusMetadata
        {
            public int StatusID { get; set; }
            [Required(ErrorMessage = "* Status is required")]
            [Display(Name ="Status")]
            public string StatusName { get; set; }
        }
    }
}
