namespace Demo.Server.Models
{
    public class Puppy : IIdentifiable
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Breed { get; set; }
    }
}
