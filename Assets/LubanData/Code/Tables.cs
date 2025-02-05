
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
    public TbCardData TbCardData {get; }
    public TbGameData TbGameData {get; }

    public Tables(System.Func<string, JSONNode> loader)
    {
        TbCardData = new TbCardData(loader("tbcarddata"));
        TbGameData = new TbGameData(loader("tbgamedata"));
        ResolveRef();
    }
    
    private void ResolveRef()
    {
        TbCardData.ResolveRef(this);
        TbGameData.ResolveRef(this);
    }
}

}
