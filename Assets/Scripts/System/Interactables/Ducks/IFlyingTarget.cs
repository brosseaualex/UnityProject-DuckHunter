﻿using UnityEngine;

public interface IFlyingTarget { 
    public enum State {
        FLYING,
        FLEEING,
        DEAD
    }

    public delegate void DieDelegate();
    public DieDelegate DiedDelegate { get; set; }
    public Vector3 SpanwerPos { set; }
    public Vector3 SpawnSize { set; }
}