using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepManagement.Models
{
    public class Production
    {
        public Guid Id { get; set; }
        [Required]
        public string SerialNum { get; set; }
        public Guid MatTypeId { get; set; }
        public Guid StyleTypeId { get; set; }
        public string Color { get; set; }
        public double? Size { get; set; }
        public Guid MouldId { get; set; }
        public double? ProcessCost { get; set; }
        public Guid MainVendorId { get; set; }
        public double? MainCost { get; set; }
        public Guid DecorateVendorId { get; set; }
        public double? DecorateCost { get; set; }
        public Guid SweatbandVendorId { get; set; }
        public double? SweatbandCost { get; set; }
        public double? PacketCost { get; set; }
        public double? TransportCost { get; set; }
        [NotMapped]
        public double TotalCost
        {
            get
            {
                return (ProcessCost.HasValue ? ProcessCost.Value : 0.0)
                    + (MainCost.HasValue ? MainCost.Value : 0.0)
                    + (DecorateCost.HasValue ? DecorateCost.Value : 0.0)
                    + (SweatbandCost.HasValue ? SweatbandCost.Value : 0.0)
                    + (PacketCost.HasValue ? PacketCost.Value : 0.0)
                    + (TransportCost.HasValue ? TransportCost.Value : 0.0);
            }
        }
        public Guid CustomerId { get; set; }
    }
}
