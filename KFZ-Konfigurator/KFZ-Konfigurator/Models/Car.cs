//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KFZ_Konfigurator.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public double BasePrice { get; set; }
        public int Year { get; set; }
        public CarBrandKind Brand { get; set; }
        public WheelDriveKind WheelDrive { get; set; }
        public BodyKind BodyType { get; set; }
    }
}