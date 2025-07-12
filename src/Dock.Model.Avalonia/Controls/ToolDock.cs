﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Avalonia;
using Dock.Model.Avalonia.Core;
using Dock.Model.Controls;
using Dock.Model.Core;

namespace Dock.Model.Avalonia.Controls;

/// <summary>
/// Tool dock.
/// </summary>
[DataContract(IsReference = true)]
public class ToolDock : DockBase, IToolDock
{
    /// <summary>
    /// Defines the <see cref="Alignment"/> property.
    /// </summary>
    public static readonly DirectProperty<ToolDock, Alignment> AlignmentProperty =
        AvaloniaProperty.RegisterDirect<ToolDock, Alignment>(nameof(Alignment), o => o.Alignment, (o, v) => o.Alignment = v, Alignment.Unset);

    /// <summary>
    /// Defines the <see cref="IsExpanded"/> property.
    /// </summary>
    public static readonly DirectProperty<ToolDock, bool> IsExpandedProperty =
        AvaloniaProperty.RegisterDirect<ToolDock, bool>(nameof(IsExpanded), o => o.IsExpanded, (o, v) => o.IsExpanded = v);

    /// <summary>
    /// Defines the <see cref="AutoHide"/> property.
    /// </summary>
    public static readonly DirectProperty<ToolDock, bool> AutoHideProperty =
        AvaloniaProperty.RegisterDirect<ToolDock, bool>(nameof(AutoHide), o => o.AutoHide, (o, v) => o.AutoHide = v, true);

    /// <summary>
    /// Defines the <see cref="GripMode"/> property.
    /// </summary>
    public static readonly DirectProperty<ToolDock, GripMode> GripModeProperty =
        AvaloniaProperty.RegisterDirect<ToolDock, GripMode>(nameof(GripMode), o => o.GripMode, (o, v) => o.GripMode = v);

    private Alignment _alignment = Alignment.Unset;
    private bool _isExpanded;
    private bool _autoHide = true;
    private GripMode _gripMode = GripMode.Visible;

    /// <summary>
    /// Initializes new instance of the <see cref="ToolDock"/> class.
    /// </summary>
    public ToolDock()
    {
    }

    /// <inheritdoc/>
    [DataMember(IsRequired = false, EmitDefaultValue = true)]
    [JsonPropertyName("Alignment")]
    public Alignment Alignment
    {
        get => _alignment;
        set => SetAndRaise(AlignmentProperty, ref _alignment, value);
    }

    /// <inheritdoc/>
    [DataMember(IsRequired = false, EmitDefaultValue = true)]
    [JsonPropertyName("IsExpanded")]
    public bool IsExpanded
    {
        get => _isExpanded;
        set => SetAndRaise(IsExpandedProperty, ref _isExpanded, value);
    }

    /// <inheritdoc/>
    [DataMember(IsRequired = false, EmitDefaultValue = true)]
    [JsonPropertyName("AutoHide")]
    public bool AutoHide
    {
        get => _autoHide;
        set => SetAndRaise(AutoHideProperty, ref _autoHide, value);
    }

    /// <inheritdoc/>
    [DataMember(IsRequired = false, EmitDefaultValue = true)]
    [JsonPropertyName("GripMode")]
    public GripMode GripMode
    {
        get => _gripMode;
        set => SetAndRaise(GripModeProperty, ref _gripMode, value);
    }

    /// <summary>
    /// Adds the specified tool to this dock and makes it active and focused.
    /// </summary>
    /// <param name="tool">The tool to add.</param>
    public virtual void AddTool(IDockable tool)
    {
        Factory?.AddDockable(this, tool);
        Factory?.SetActiveDockable(tool);
        Factory?.SetFocusedDockable(this, tool);
    }

    /// <summary>
    /// Starts flashing the specified dockable tab.
    /// </summary>
    /// <param name="dockable">The dockable to flash.</param>
    public void FlashDockable(IDockable dockable)
    {
        Factory?.FlashDockable(dockable);
    }

    /// <summary>
    /// Stops flashing the specified dockable tab.
    /// </summary>
    /// <param name="dockable">The dockable to stop flashing.</param>
    public void StopFlashingDockable(IDockable dockable)
    {
        Factory?.StopFlashingDockable(dockable);
    }
}
