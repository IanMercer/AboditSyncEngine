using System;

namespace AboditSyncEngine
{
    public interface ISyncItem
    {
        string Id {get; set;}

        DateTimeOffset LastModified { get; set;}

        int Priorty {get; set;}
    }
}