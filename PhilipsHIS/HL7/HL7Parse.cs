﻿using System.Diagnostics;
using NHapi.Base.Parser;
using NHapi.Model.V23.Message;

namespace PhilipsHIS.HL7
{
    public class HL7Parse
    {
        public static void Main(string[] args)
        {
            const string messageString = "MSH|^~\\&|SENDING_APPLICATION|SENDING_FACILITY|RECEIVING_APPLICATION|RECEIVING_FACILITY|20110614075841||ACK|1407511|P|2.3||||||\r\n" +
                                        "MSA|AA|1407511|Success||";

            // instantiate a PipeParser, which handles the "traditional or default encoding"
            var ourPipeParser = new PipeParser();

            try
            {
                // parse the string format message into a Java message object
                var hl7Message = ourPipeParser.Parse(messageString);

                //cast to ACK message to get access to ACK message data
                var ackResponseMessage = hl7Message as ACK;
                if (ackResponseMessage != null)
                {
                    //access message data and display it
                    //note that I am using encode method at the end to convert it back to string for display
                    var mshSegmentMessageData = ackResponseMessage.MSH;
                    Console.WriteLine("Message Type is " + mshSegmentMessageData.MessageType.MessageType);
                    Console.WriteLine("Message Control Id is " + mshSegmentMessageData.MessageControlID);
                    Console.WriteLine("Message Timestamp is " + mshSegmentMessageData.DateTimeOfMessage.TimeOfAnEvent.GetAsDate());
                    Console.WriteLine("Sending Facility is " + mshSegmentMessageData.SendingFacility.NamespaceID.Value);

                    //update message data in MSA segment
                    ackResponseMessage.MSA.AcknowledgementCode.Value = "AR";
                }

                // Display the updated HL7 message using Pipe delimited format
                Console.WriteLine("HL7 Pipe Delimited Message Output:");
                Console.WriteLine(ourPipeParser.Encode(hl7Message));

                // instantiate an XML parser that NHAPI provides
                var ourXmlParser = new DefaultXMLParser();

                // convert from default encoded message into XML format, and send it to standard out for display
                Console.WriteLine("HL7 XML Formatted Message Output:");
                Console.WriteLine(ourXmlParser.Encode(hl7Message));

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occured -> {e.StackTrace}");
            }
        }

        //private static void LogToDebugConsole(string informationToLog)
        //{
        //    Debug.WriteLine(informationToLog);
        //}
    }
}

