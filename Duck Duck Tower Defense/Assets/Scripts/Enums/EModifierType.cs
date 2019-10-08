using System;

[Flags]
public enum EModifierType
{
    None = 0b_0000_0001,
    Stats = 0b_0000_0010,
    Visual = 0b_0000_0100,
    Action = 0b_0000_1000
}
