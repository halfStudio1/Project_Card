
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;
using SimpleJSON;

namespace cfg
{
public partial class Tables
{
    public card.TbCards TbCards {get; }
    public buff.TbBuff TbBuff {get; }

    public Tables(System.Func<string, JSONNode> loader)
    {
        TbCards = new card.TbCards(loader("card_tbcards"));
        TbBuff = new buff.TbBuff(loader("buff_tbbuff"));
        ResolveRef();
    }
    
    private void ResolveRef()
    {
        TbCards.ResolveRef(this);
        TbBuff.ResolveRef(this);
    }
}

}
