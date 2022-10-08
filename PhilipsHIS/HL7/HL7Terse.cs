//using System;
//using System.Diagnostics;
//using System.IO;
//using HapiTerserBasicOperations;
//using NHapi.Base.Parser;
//using NHapi.Base.Util;

//namespace PhilipsHIS.HL7
//{
//    public class HL7Terse
//    {
//        private readonly Terser _terser;

//        public OurTerserHelper(Terser terser)
//        {
//            if (terser == null)
//                throw new ArgumentNullException(nameof(terser),
//                    "Terser object must be passed in for data retrieval operation");

//            _terser = terser;
//        }

//        public string GetData(string terserExpression)
//        {

//            if (string.IsNullOrEmpty(terserExpression))
//                throw new ArgumentNullException(nameof(terserExpression),
//                    "Terser expression must be supplied for data retrieval operation");

//            return _terser.Get(terserExpression);
//        }

//        public void SetData(string terserExpression, string value)
//        {

//            if (string.IsNullOrEmpty(terserExpression))
//                throw new ArgumentNullException(nameof(terserExpression),
//                    "Terser expression must be supplied for set operation");

//            if (value == null) //we will let an empty string still go through
//                throw new ArgumentNullException(nameof(value), "Value for set operation must be supplied");

//            _terser.Set(terserExpression, value);
//        }

//        static void Main(string[] args)
//        {
//            try
//            {
//                // see my GitHub page for this file
//                var messageString = ReadHl7MessageFromFileAsString(
//                        "C:\\User\\atom7\\desktop\\HIS\\HL7TestInputFiles\\FileWithObservationResultMessage.txt");

//                // instantiate a PipeParser, which handles the normal HL7 encoding
//                var ourPipeParser = new PipeParser();

//                // parse the message string into a Java message object
//                var orderResultsHl7Message = ourPipeParser.Parse(messageString);

//                // create a terser object instance by wrapping it around the message object
//                var terser = new Terser(orderResultsHl7Message);

//                // now, let us do various operations on the message
//                var terserHelper = new OurTerserHelper(terser);

//                var terserExpression = "MSH-6";
//                var dataRetrieved = terserHelper.GetData(terserExpression);
//                Console.WriteLine($"Field 6 of MSH segment using expression '{terserExpression}' was '{dataRetrieved}'");

//                terserExpression = "/.PID-5-2"; // notice the /. to indicate relative position to root node
//                dataRetrieved = terserHelper.GetData(terserExpression);
//                Console.WriteLine($"Field 5 and Component 2 of the PID segment using expression '{terserExpression}' was {dataRetrieved}'");

//                terserExpression = "/.*ID-5-2";
//                dataRetrieved = terserHelper.GetData(terserExpression);
//                Console.WriteLine($"Field 5 and Component 2 of the PID segment using wildcard-based expression '{terserExpression}' was '{dataRetrieved}'");

//                terserExpression = "/.P?D-5-2";
//                dataRetrieved = terserHelper.GetData(terserExpression);
//                Console.WriteLine($"Field 5 and Component 2 of the PID segment using another wildcard-based expression '{terserExpression}' was '{dataRetrieved}'");

//                terserExpression = "/.PV1-9(1)-1"; // note: field repetitions are zero-indexed
//                dataRetrieved = terserHelper.GetData(terserExpression);
//                Console.WriteLine($"2nd repetition of Field 9 and Component 1 for it in the PV1 segment using expression '{terserExpression}' was '{dataRetrieved}'");

//            }
//            catch (Exception e)
//            {
//                Console.WriteLine($"Error occured while creating HL7 message {e.Message}");
//            }
//        }

//        public static string ReadHl7MessageFromFileAsString(string fileName)
//        {
//            return File.ReadAllText(fileName);
//        }

//        private static void LogToDebugConsole(string informationToLog)
//        {
//            Debug.WriteLine(informationToLog);
//        }

//        public static void Main(string[] args)
//        {
//            try
//            {
//                //see my GitHub page for this file
//                var messageString = ReadHl7MessageFromFileAsString("C:\\HL7TestInputFiles\\FileWithObservationResultMessage.txt");

//                // instantiate a PipeParser, which handles the "traditional or default encoding"
//                var ourPipeParser = new PipeParser();

//                // parse the message string into a Java message object
//                var orderResultsHl7Message = ourPipeParser.Parse(messageString);

//                //create a terser object instance by wrapping it around the message object
//                var terser = new Terser(orderResultsHl7Message);

//                //now, let us do various operations on the message
//                var terserDemonstrator = new OurTerserHelper(terser);

//                //use a HL7 test utility such as HAPI Test Panel Utility as reference
//                //for a visual breakdown of these structures if you need to understand these terser expressions
//                var terserExpression = "/RESPONSE/PATIENT/PID-5-1";
//                var dataRetrieved = terserDemonstrator.GetData(terserExpression);
//                Console.WriteLine($"Terser expression  '{terserExpression}' yielded '{dataRetrieved}'");

//                terserExpression = "/RESPONSE/PATIENT/VISIT/PV1-9-3";
//                dataRetrieved = terserDemonstrator.GetData(terserExpression);
//                Console.WriteLine($"Terser expression '{terserExpression}' yielded '{dataRetrieved}'");

//                terserExpression = "/RESPONSE/ORDER_OBSERVATION(0)/OBSERVATION(1)/OBX-3";
//                dataRetrieved = terserDemonstrator.GetData(terserExpression);
//                Console.WriteLine($"Terser expression '{terserExpression}' yielded '{dataRetrieved}'");

//                terserExpression = "/.ORDER_OBSERVATION(0)/ORC-12-3";
//                dataRetrieved = terserDemonstrator.GetData(terserExpression);
//                Console.WriteLine($"Terser expression '{terserExpression}' yielded '{dataRetrieved}'");

//                //let us now try a set operation using the terser
//                terserExpression = "/.OBSERVATION(0)/NTE-3";
//                terserDemonstrator.SetData(terserExpression, "This is our override value using the setter");
//                Console.WriteLine("Set the data for second repetition of the NTE segment and its Third field..");

//                Console.WriteLine("\nWill display our modified message below \n");
//                Console.WriteLine(ourPipeParser.Encode(orderResultsHl7Message));

//            }
//            catch (Exception e)
//            {
//                Console.WriteLine($"Error occured while creating HL7 message {e.Message}");
//            }
//        }

//        public static string ReadHl7MessageFromFileAsString(string fileName)
//        {
//            return File.ReadAllText(fileName);
//        }

//        private static void LogToDebugConsole(string informationToLog)
//        {
//            Debug.WriteLine(informationToLog);
//        }
//    }
//}
