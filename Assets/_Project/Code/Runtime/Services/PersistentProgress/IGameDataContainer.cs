﻿using Code.Runtime.Data;
using Code.Runtime.Data.PlayerData;

namespace Code.Runtime.Services.PersistentProgress
{
    public interface 
        IGameDataContainer
    {
        PlayerData PlayerData { get; set; }
        AudioData AudioData { get; set; }
    }
}