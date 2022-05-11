using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Entity
{
    [Serializable]
    [Table("Okpd2")]
    public class Okpd2
    {
        //public Okpd2Element? Parent { get; }
        //public readonly List<Okpd2Element> Okpd2s = new List<Okpd2Element>();

        [Key]
        [Column("Code")]
        public string Code { get; } = "";

        [Column("Name")]
        public string Name { get; } = "";

        [Column("Parent")]
        public string? Parent { get; set; }

        public Okpd2()
        {

        }

        public Okpd2(string code, string name)
        {
            this.Code = code;
            Name = name;
        }

        public Okpd2(string code, string name, string? parent) : this(code, name)
        {
            Parent = parent;
        }

        //public Okpd2Element(Okpd2Element? parent, string number, string name) : this(number, name)
        //{
        //    Parent = parent;
        //}

        //public bool HasParent { get => Parent != null; }
        //public bool HasRoot { get => Okpd2s != null; }
    }
}
