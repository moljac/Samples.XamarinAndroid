using System;
using System.Diagnostics;

namespace HolisticWare.Productivity.Utilities
{
    public partial class Trace
    {
        public Trace()
        {
        }


        public static void Setup(string[] args)
        {
            // Compile with define
            //      TRACE
            //      /d:TRACE

            // Define a trace listener to direct trace output from this method
            // to the console.
            ConsoleTraceListener consoleTracer;

            // Check the command line arguments to determine which
            // console stream should be used for trace output.
            if ((args.Length>0)&&(args[0].ToString().ToLower().Equals("/stderr")))
            // Initialize the console trace listener to write
            // trace output to the standard error stream.
            {
                consoleTracer = new ConsoleTraceListener(true);
            }
            else
            {
            // Initialize the console trace listener to write
            // trace output to the standard output stream.
            consoleTracer = new ConsoleTraceListener();
            }
            // Set the name of the trace listener, which helps identify this 
            // particular instance within the trace listener collection.
            consoleTracer.Name = "mainConsoleTracer";

            // Write the initial trace message to the console trace listener.
            consoleTracer.WriteLine
                            (
                                DateTime.Now.ToString()
                                +
                                " ["
                                +consoleTracer.Name
                                +
                                "] - Starting output to trace listener."
                                );

            // Add the new console trace listener to 
            // the collection of trace listeners.
            System.Diagnostics.Trace.Listeners.Clear();
            System.Diagnostics.Trace.Listeners.Add
                                                (
                                                    new System.Diagnostics.TextWriterTraceListener(Console.Out)
                                                );
            System.Diagnostics.Trace.Listeners.Add(consoleTracer);
            System.Diagnostics.Trace.Listeners.Add
                                                (
                                                    new TextWriterTraceListener    
                                                            (
                                                                "TextWriterOutput.log", 
                                                                "TextWritterListener"
                                                            )
                                                );

            // Call a local method, which writes information about the current 
            // execution environment to the configured trace listeners.
            //WriteEnvironmentInfoToTrace();

            // Write the final trace message to the console trace listener.
            consoleTracer.WriteLine
                                (
                                    DateTime.Now.ToString()
                                    +
                                    " ["
                                    +
                                    consoleTracer.Name
                                    +
                                    "] - Ending output to trace listener."
                                );

            // Flush any pending trace messages, remove the 
            // console trace listener from the collection,
            // and close the console trace listener.

            return;
        }

    }
}

