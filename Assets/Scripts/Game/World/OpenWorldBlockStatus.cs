using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Defines different states open world block can be in
/// </summary>
public enum OpenWorldBlockStatus
{
    // Undefined state, shouldn't be used
    UNKNOWN,

    // Block was created but doesn't have anything loaded
    CREATED,

    // Data loaded
    LOADED,

    // Block expired and can be re-used -> loaded
    EXPIRED
}
