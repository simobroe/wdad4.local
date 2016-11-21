using System;
using System.Collections.Generic;
using BnbGo.Models.Security;

namespace BnbGo.Models
{
    public class ImageType : BaseEntity<Int32>
    {
       // link with images
       public List<Image> Images { get; set; }
    }
}