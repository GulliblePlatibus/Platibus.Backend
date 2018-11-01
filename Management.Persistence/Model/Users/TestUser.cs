using System;
using Dapper.Contrib.Extensions;
using Management.Persistence.Documents;

namespace Management.Persistence.Model
{
    [Table("users")]
    public class TestUser : IEntity
    {
        public string name { get; set; }
        
        [ExplicitKey]
        public string Id { get; set; }//TODO : Skal nok være en guid?
    }
}