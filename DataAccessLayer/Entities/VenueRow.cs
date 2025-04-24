namespace DataAccessLayer.Entities
{
    public class VenueRow : BaseEntity
    {
        public VenueRow()
        {
            VenueSeats = new HashSet<VenueSeat>();
        }

        public int SectionId { get; set; }

        public string Name { get; set; }

        public virtual VenueSection Section { get; set; }

        public virtual ICollection<VenueSeat> VenueSeats { get; set; }
    }
}
