﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldBlockLocator : MonoBehaviour
{
    [Tooltip("DEBUG - Used for rendering block bounds")]
    public LineRenderer DebugLineRenderer;

    [Tooltip("DEBUG - Block delta position relative to the center of the world")]
    public TextMesh DebugBlockDeltaText;

    [Tooltip("DEBUG - Block position in the game map")]
    public TextMesh DebugBlockGameText;
}
