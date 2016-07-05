using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NTAF.PlugInFramework {
    /// <summary>
    /// Standard operationg modes for the editors
    /// </summary>
    public enum EditorMode {
        /// <summary>
        /// the object is new and being edited
        /// </summary>
        New,
        /// <summary>
        /// object is old and being changed
        /// </summary>
        Edit,
        /// <summary>
        /// currently onl viewing the object
        /// </summary>
        View,
        /// <summary>
        /// object cannot be changed
        /// </summary>
        ReadOnly
    }

    /// <summary>
    /// standard exit messages
    /// </summary>
    public enum EditorExitCode {
        /// <summary>
        /// Ended on a good note
        /// </summary>
        OK,
        /// <summary>
        /// Ended on a not good note
        /// </summary>
        Cancel
    }

    /// <summary>
    /// defines what standard field to look in when searching
    /// </summary>
    public enum SearchField {
        /// <summary>
        /// Search by name field
        /// </summary>
        Name,
        /// <summary>
        /// search by ID field
        /// </summary>
        ID
    }
}
