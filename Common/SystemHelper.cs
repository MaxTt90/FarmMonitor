

using System;
using System.IO;
using System.Collections;
using System.Text;
using System.Threading;
using System.Diagnostics;

    /// <summary>
    /// General Helper methods that assist in running of system functions
    /// </summary>
    public class SystemHelper : IDisposable
    {
        private bool _Monitoring = false;
        private Hashtable ProcessWatch = new Hashtable();
        private Hashtable WatchedProcesses = new Hashtable();
        public ArrayList ProcInfo = new ArrayList();
        private StreamWriter ProcMonitoringWriter;
        //private Thread monitorThread;

        public bool Monitoring { get { return _Monitoring; } }

        /// <summary>
        /// Runs an Executable and returns the output
        /// </summary>
        /// <param name="ExeName">The full path and name of the executable</param>
        /// <param name="Params">The parameters for the exe</param>
        /// <returns>string containing the STDOUT of the process</returns>
        public static ProcessExitInfo RunExecutable(string ExeName, string Params)
        {

            RunProcess myProc = new RunProcess(ExeName, Params);
            return myProc.Run();

        }

        public static ProcessExitInfo RunExecutable(string ExeName, string Params, string WorkingDirectory)
        {

            RunProcess myProc = new RunProcess(ExeName, Params, WorkingDirectory);
            return myProc.Run();

        }

        public static int RunExecutable(string ExeName, string Params, Stream StandardOut, Stream StandardError)
        {
            RunProcess myProc = new RunProcess(ExeName, Params, StandardOut, StandardError);
            return myProc.RunStreams();
        }

        #region Multithreaded Watch For Processes

        public void WatchForProcesses(string[] ExeNames, string OutputFileName)
        {
            foreach (string ExeName in ExeNames)
            {
                if (!ProcessWatch.ContainsKey(ExeName))
                {
                    ProcessWatch.Add(ExeName, null);
                }
            }

            if (!_Monitoring)
            {
                //set it all up
                _Monitoring = true;
                ProcMonitoringWriter = new StreamWriter(OutputFileName);
                ThreadPool.QueueUserWorkItem(new WaitCallback(Monitor));
            }

        }

        private void Monitor(object input)
        {
            Debug.WriteLine("Watching for processes");
            bool exit = false;
            Process[] myProcs;

            while (!exit)
            {
                lock (this)
                {
                    if (!_Monitoring)
                    {
                        exit = true;
                    }
                }

                foreach (string ProcName in ProcessWatch.Keys)
                {
                    myProcs = Process.GetProcessesByName(ProcName);

                    if (myProcs.Length > 0)
                    {
                        for (int i = 0; i < myProcs.Length; i++)
                        {
                            if (!WatchedProcesses.ContainsKey(myProcs[i].ProcessName + "(" + myProcs[i].Id + ")"))
                            {
                                Debug.WriteLine("New process found, firing monitor thread");
                                WatchedProcesses.Add(myProcs[i].ProcessName + "(" + myProcs[i].Id + ")", null);
                                ThreadPool.QueueUserWorkItem(new WaitCallback(ProcMonitor), myProcs[i]);
                            }
                        }
                    }

                    myProcs = null;
                }
                Thread.Sleep(200);
            }

            Console.WriteLine("Exiting");
        }

        /// <summary>
        /// Stops the current threads from watching
        /// </summary>
        public void StopWatching()
        {
            if (_Monitoring)
            {
                //Shut it all down.
                lock (this)
                {
                    _Monitoring = false;
                    ProcMonitoringWriter.Close();
                }
            }
        }

        /// <summary>
        /// Flushes the current process stats to disk
        /// </summary>
        private void FlushMonitorStream()
        {
            foreach (ProcessStats ps in ProcInfo)
            {
                ProcMonitoringWriter.WriteLine(ps.Name + ":");
                ProcMonitoringWriter.WriteLine("\tStart Time: " + ps.StartTime);
                ProcMonitoringWriter.WriteLine("\tEnd Time:   " + ps.EndTime);
                ProcMonitoringWriter.WriteLine("\tPID:        " + ps.PID);
                ProcMonitoringWriter.WriteLine("\tPeak Threads:    " + ps.PeakThreads);
                ProcMonitoringWriter.WriteLine("\tWorking Set:" + ps.WorkingSet);
                ProcMonitoringWriter.WriteLine("\tPeak Working Set: " + ps.PeakWorkingSet);
                //ProcMonitoringWriter.WriteLine("\tVirtualMemorySize: " + ps.VirtualMemorySize);
                ProcMonitoringWriter.WriteLine("\tHandleCount: " + ps.HandleCount);
                //ProcMonitoringWriter.WriteLine("\tNonpagedSystemMemorySize: " + ps.NonpagedSystemMemorySize);
                //ProcMonitoringWriter.WriteLine("\tPagedMemorySize: " + ps.PagedMemorySize);
                //ProcMonitoringWriter.WriteLine("\tPagedSystemMemorySize: " + ps.PagedSystemMemorySize);
                ProcMonitoringWriter.WriteLine("\tPeakPagedMemorySize: " + ps.PeakPagedMemorySize);
                ProcMonitoringWriter.WriteLine("\tPeakVirtualMemorySize: " + ps.PeakVirtualMemorySize);
                ProcMonitoringWriter.WriteLine("\tPeakPrivateMemorySize: " + ps.PeakPrivateMemorySize);
            }

            ProcMonitoringWriter.Flush();

            ProcInfo.Clear();
        }

        private void ProcMonitor(object myProc)
        {
            Process Proc = (Process)myProc;
            ProcessStats procStats = new ProcessStats(Proc);
            string HashProcName = Proc.ProcessName + "(" + Proc.Id + ")";

            Console.WriteLine("\tMonitoring...");
            while (!Proc.HasExited)
            {
                lock (this)
                {
                    if (!this.Monitoring)
                    {
                        Proc = null;
                        return;
                    }
                }
                procStats.UpdateStats(Proc);
                Thread.Sleep(500);
            }

            lock (this)
            {
                Debug.WriteLine("Closing Thread");
                this.RemoveWatched(HashProcName);
                this.ProcInfo.Add(procStats);
                this.FlushMonitorStream();
                Proc.Close();
                Proc.Dispose();
            }
        }

        private void RemoveWatched(string procToRemove)
        {
            if (WatchedProcesses.ContainsKey(procToRemove))
                WatchedProcesses.Remove(procToRemove);
        }


        private class ProcessStats
        {
            private long _WorkingSet = 0;
            private int _PeakThreads = 0;
            private DateTime _startTime;
            private DateTime _endTime;
            private string _Name = "";
            private int _Pid = 0;
            private TimeSpan _TotalProcessorTime;
            private TimeSpan _UserProcessorTime;
            //private int _VirtualMemorySize;
            private int _HandleCount;
            //private int _NonpagedSystemMemorySize;
            //private int _PagedMemorySize;
            //private int _PagedSystemMemorySize;
            private long _PeakPagedMemorySize;
            private long _PeakVirtualMemorySize;
            private long _PeakWorkingSet;
            private long _PeakPrivateMemorySize;

            public long WorkingSet { get { return _WorkingSet; } }
            public int PeakThreads { get { return _PeakThreads; } }
            public DateTime StartTime { get { return _startTime; } }
            public DateTime EndTime { get { return _endTime; } }
            public string Name { get { return _Name; } }
            public int PID { get { return _Pid; } }
            //public int VirtualMemorySize{get{return _VirtualMemorySize;}}
            public int HandleCount { get { return _HandleCount; } }
            //public int NonpagedSystemMemorySize{get{return _NonpagedSystemMemorySize;}}
            //public int PagedMemorySize{get{return _PagedMemorySize;}}
            //public int PagedSystemMemorySize{get{return _PagedSystemMemorySize;}}
            public long PeakPagedMemorySize { get { return _PeakPagedMemorySize; } }
            public long PeakVirtualMemorySize { get { return _PeakVirtualMemorySize; } }
            public long PeakWorkingSet { get { return _PeakWorkingSet; } }
            public long PeakPrivateMemorySize { get { return _PeakPrivateMemorySize; } }


            public ProcessStats(Process proc)
            {
                _WorkingSet = proc.WorkingSet64;
                Debug.WriteLine("Threads: " + proc.Threads.Count.ToString());
                _PeakThreads = proc.Threads.Count;
                _startTime = proc.StartTime;
                _endTime = DateTime.Now;
                //_endTime = proc.ExitTime; //If you didn't start the process this isn't valid
                _Name = proc.ProcessName;
                _Pid = proc.Id;
                _TotalProcessorTime = proc.TotalProcessorTime;
                _UserProcessorTime = proc.UserProcessorTime;
                //_VirtualMemorySize = proc.VirtualMemorySize;
                _HandleCount = proc.HandleCount;
                //_NonpagedSystemMemorySize = proc.NonpagedSystemMemorySize;
                //_PagedMemorySize = proc.PagedMemorySize;
                //_PagedSystemMemorySize = proc.PagedSystemMemorySize;
                _PeakPagedMemorySize = proc.PeakPagedMemorySize64;
                _PeakVirtualMemorySize = proc.PeakVirtualMemorySize64;
                _PeakWorkingSet = proc.PeakWorkingSet64;
                _PeakPrivateMemorySize = proc.PrivateMemorySize64;
            }

            public void UpdateStats(Process proc)
            {
                proc.Refresh();
                Debug.WriteLine("Threads: " + proc.Threads.Count.ToString());
                if (proc.Threads.Count > _PeakThreads)
                    _PeakThreads = proc.Threads.Count;
                if (proc.HandleCount > _HandleCount)
                    _HandleCount = proc.HandleCount;
                if (proc.PrivateMemorySize64 > _PeakPrivateMemorySize)
                    _PeakPrivateMemorySize = proc.PrivateMemorySize64;

                _endTime = DateTime.Now;
                _TotalProcessorTime = proc.TotalProcessorTime;
                _UserProcessorTime = proc.UserProcessorTime;
                _PeakPagedMemorySize = proc.PeakPagedMemorySize64;
                _PeakVirtualMemorySize = proc.PeakVirtualMemorySize64;
                _PeakWorkingSet = proc.PeakWorkingSet64;
            }
        }


        #endregion

        /// <summary>
        /// This class is so that you can run a process.  Some processes hang without 
        /// actively reading from stdout and stderror so this class fixes that issue
        /// </summary>
        private class RunProcess
        {

            Object readStdOutLock = new object();
            Object readStdErrLock = new object();
            ProcessStartInfo myProcessInfo;
            Process myProc;
            StreamWriter StandardOut;
            StreamWriter StandardError;

            StringBuilder STdout = new StringBuilder(100000);
            StringBuilder STderr = new StringBuilder(100000);
            //			string STdout = "";
            //			string STderr = "";

            public RunProcess(string ExeName, string Params, string WorkingDirectory)
            {
                myProcessInfo = new ProcessStartInfo(ExeName, Params);
                myProcessInfo.WorkingDirectory = WorkingDirectory;
                myProcessInfo.RedirectStandardInput = true;
                myProcessInfo.RedirectStandardOutput = true;
                myProcessInfo.RedirectStandardError = true;
                myProcessInfo.UseShellExecute = false;
            }

            public RunProcess(string ExeName, string Params, string WorkingDirectory, Stream standardOut, Stream standardError)
            {
                myProcessInfo = new ProcessStartInfo(ExeName, Params);
                myProcessInfo.WorkingDirectory = WorkingDirectory;
                myProcessInfo.RedirectStandardOutput = true;
                myProcessInfo.RedirectStandardError = true;
                myProcessInfo.UseShellExecute = false;
                StandardOut = new StreamWriter(standardOut);
                StandardError = new StreamWriter(standardError);
            }

            public RunProcess(string ExeName, string Params)
            {
                myProcessInfo = new ProcessStartInfo(ExeName, Params);
                myProcessInfo.RedirectStandardOutput = true;
                myProcessInfo.RedirectStandardError = true;
                myProcessInfo.UseShellExecute = false;
            }

            public RunProcess(string ExeName, string Params, Stream standardOut, Stream standardError)
            {
                myProcessInfo = new ProcessStartInfo(ExeName, Params);
                myProcessInfo.RedirectStandardOutput = true;
                myProcessInfo.RedirectStandardError = true;
                myProcessInfo.UseShellExecute = false;
                StandardOut = new StreamWriter(standardOut);
                StandardError = new StreamWriter(standardError);
            }

            public int RunStreams()
            {
                Thread stdOut = new Thread(new ThreadStart(this.GetSTDoutStream));
                Thread stdErr = new Thread(new ThreadStart(this.GetSTDerrStream));
                int exitCode = 0;

                myProc = Process.Start(myProcessInfo);

                stdOut.Start();
                stdErr.Start();

                myProc.WaitForExit();

                while (stdOut.IsAlive || stdErr.IsAlive)
                {
                    Thread.Sleep(100);
                }

                stdOut = null;
                stdErr = null;
                exitCode = myProc.ExitCode;
                myProc.Close();
                myProc.Dispose();

                return exitCode;

            }

            public ProcessExitInfo Run()
            {
                Thread stdOut = new Thread(new ThreadStart(this.GetSTDout));
                Thread stdErr = new Thread(new ThreadStart(this.GetSTDerr));
                int exitCode = 0;

                myProc = Process.Start(myProcessInfo);


                stdOut.Start();
                stdErr.Start();

                myProc.WaitForExit();

                while (stdOut.IsAlive || stdErr.IsAlive)
                {
                    Thread.Sleep(100);
                }

                stdOut = null;
                stdErr = null;
                exitCode = myProc.ExitCode;
                myProc.Close();
                myProc.Dispose();

                return new ProcessExitInfo(STdout.ToString(), STderr.ToString(), exitCode);
            }

            private void GetSTDout()
            {
                while (!myProc.HasExited)
                {
                  
                    string outputSTR = string.Empty;
                    lock (this.readStdOutLock)
                    {
                       outputSTR = myProc.StandardOutput.ReadLine();
                    }
                   
                    if (!string.IsNullOrEmpty(outputSTR))
                    {
                        STdout.Append(outputSTR + "\r\n");
                    }
                    else
                    {
                        STdout.Append("\r\n");
                    }
                       
                }
            }

            private void GetSTDoutStream()
            {
                string lineout;
                while (!myProc.HasExited)
                {
                    Thread.Sleep(200);
                    while ((lineout = myProc.StandardOutput.ReadLine()) != null)
                    {
                        StandardOut.WriteLine(lineout);
                        StandardOut.Flush();
                    }
                }

                //don't close the underlying streams since we are allowing the caller of this while thing handle what they 
                //want to do with the stream afterwards
            }

            private void GetSTDerr()
            {

                while (!myProc.HasExited)
                {
                    string outputSTR = string.Empty;
                    lock (this.readStdErrLock)
                    {
                        outputSTR = myProc.StandardError.ReadLine();
                    }

                    if (!string.IsNullOrEmpty(outputSTR))
                    {
                        this.STderr.Append(outputSTR + "\r\n");
                    }
                    else
                    {
                        this.STderr.Append("\r\n");
                    }

                      
                   
                }
            }

            private void GetSTDerrStream()
            {
                string lineout;
                while (!myProc.HasExited)
                {
                    Thread.Sleep(200);
                    while ((lineout = myProc.StandardError.ReadLine()) != null)
                    {
                        StandardError.WriteLine(lineout);
                        StandardError.Flush();
                    }
                }

                //don't close the underlying streams since we are allowing the caller of this while thing handle what they 
                //want to do with the stream afterwards
            }
        }


        /// <summary>
        /// Contains information about a process exit.  Console ouput, and exit code
        /// </summary>
        public class ProcessExitInfo
        {
            private string _StandardOutput = "";
            private string _StandardError = "";
            private int _ExitCode = 0;

            /// <summary>
            /// Ouput from Standard Out
            /// </summary>
            public string Output { get { return _StandardOutput; } }

            /// <summary>
            /// STDERROR from the process
            /// </summary>
            public string Error { get { return _StandardError; } }

            /// <summary>
            /// The Return Code provided by the process
            /// </summary>
            public int ReturnCode { get { return _ExitCode; } }

            public ProcessExitInfo(string StandardOutput, string StandardError, int ExitCode)
            {
                if (!StandardError.Equals("\r\n"))
                {
                    _StandardError = StandardError;
                }

                if (!StandardOutput.Equals("\r\n"))
                {
                    _StandardOutput = StandardOutput;
                }

                _ExitCode = ExitCode;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (this.ProcMonitoringWriter != null)
            {
                this.ProcMonitoringWriter.Dispose();
            }
        }

        #endregion
    }

