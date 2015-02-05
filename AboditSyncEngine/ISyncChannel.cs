using System;

namespace AboditSyncEngine
{
    /// <summary>
    /// Represents an external channel that is connected to the Sync engine
    /// </summary>
    public interface ISyncChannel
    {
        string Name {get;}

        /// <summary>
        /// All updates from the sync channel
        /// </summary>
        IObservable<ISyncItem> Updates { get;}
       


    }
}