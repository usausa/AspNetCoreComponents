namespace Example.Models.Entity
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public class DataEntity
    {
        public int Id { get; set; }

        [AllowNull]
        public string Name { get; set; }

        public bool Flag { get; set; }

        public DateTime DateTime { get; set; }
    }
}
