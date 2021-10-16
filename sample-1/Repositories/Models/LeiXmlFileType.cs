using LinqToDB.Mapping;

namespace Nsd.Repository.Ef.Model.Entities
{
    public enum LeiXmlFileType
    {
        [MapValue("F")]
        Full,
        [MapValue("D")]
        Delta,
        [MapValue("U")]
        Public,
        [MapValue("P")]
        Private,
        [MapValue("N")]
        NonPublic,
        [MapValue("R")]
        RepEx
    }
}
