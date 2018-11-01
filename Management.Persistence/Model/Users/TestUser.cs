using Dapper.Contrib.Extensions;

namespace Management.Persistence.Model
{
    [Table("users")]
    public class TestUser 
    {
        public string name { get; set; }
        
        [ExplicitKey]
        public string id { get; set; }
    }
}