namespace Demo.Server.Models
{
    public class IncrementingIdGenerator : IIdGenerator
    {
        private int lastId;

        public string CreateId()
        {
            lastId++;

            return $"Id_{this.lastId.ToString()}";
        }
    }
}
