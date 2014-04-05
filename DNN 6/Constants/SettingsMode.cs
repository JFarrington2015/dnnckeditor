﻿/*
 * CKEditor Html Editor Provider for DotNetNuke
 * ========
 * http://dnnckeditor.codeplex.com/
 * Copyright (C) Ingo Herbote
 *
 * The software, this file and its contents are subject to the CKEditor Provider
 * License. Please read the license.txt file before using, installing, copying,
 * modifying or distribute this file or part of its contents. The contents of
 * this file is part of the Source Code of the CKEditor Provider.
 */

namespace WatchersNET.CKEditor.Constants
{
    /// <summary>
    /// Settings Mode
    /// </summary>
    public enum SettingsMode
    {
        /// <summary>
        /// The default settings mode.
        /// </summary>
        Default = -1,

        /// <summary>
        /// The portal settings mode.
        /// </summary>
        Portal = 0,

        /// <summary>
        /// The page settings mode.
        /// </summary>
        Page = 1,

        /// <summary>
        /// The module instance settings mode.
        /// </summary>
        ModuleInstance = 2
    }
}