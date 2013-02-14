// This source file is part of BoincGuiRpc.Net
//
// Author: 	    Emanuel Jöbstl <emi@eex-dev.net>
// Weblink: 	http://boincguirpc.codeplex.com
//
// Licensed under the MIT License
//
// (c) 2012-2013

using System;
using System.Collections.Generic;
using System.Text;

namespace Boinc
{
    /// <summary>
    /// This class wraps a result of the Boinc application. 
    /// </summary>
    public class Result
    {
        public bool Acknowledged { get; internal set; }
        public bool IsActive { get; internal set; }
        public bool ReadyToReport { get; internal set; }

        public TimeSpan CurrentCpuTime { get; internal set; }
        public TimeSpan ElapsedTime { get; internal set; }
        public TimeSpan EstimatedCpuTimeRemaining { get; internal set; }
        public TimeSpan FinalCpuTime { get; internal set; }
        public TimeSpan FinalElapsedTime { get; internal set; }

        public DateTime ReceivedTime { get; internal set; }
        public DateTime ReportDeadline { get; internal set; }

        public int ExitStatus { get; internal set; }
        public int ProcessId { get; internal set; }
        public int Signal { get; internal set; }
        public int State { get; internal set; }
        public int TaskState { get; internal set; }
        public int VersionNumber { get; internal set; }

        public string Name { get; internal set; }
        public string ProjectUrl { get; internal set; }
        public string Resources { get; internal set; }
        public string WorkUnitName { get; internal set; }

        public double FractionDone { get; internal set; }

        /// <summary>
        /// Creates a new instance of this class. 
        /// </summary>
        internal Result()
        {

        }
    }
}
