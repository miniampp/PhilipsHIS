using System.Diagnostics;
using System.Globalization;
using NHapi.Base.Model;
using NHapi.Base.Parser;
using NHapi.Model.V23.Message;

namespace PhilipsHIS.HL7
{
    public class HL7Create
    {
        public void CreateMessage()
        {
            try
            {
                // create the HL7 message
                // this AdtMessageFactory class is not from NHAPI but my own wrapper
                Console.WriteLine("Creating ADT A01 message...");
                var adtMessage = AdtMessageFactory.CreateMessage("A01");

                // create these parsers for the file encoding operations
                var pipeParser = new PipeParser();
                var xmlParser = new DefaultXMLParser();

                // print out the message that we constructed
                Console.WriteLine("Message was constructed successfully..." + "\n");

                // serialize the message to pipe delimited output file
                WriteMessageFile(pipeParser, adtMessage, "C:\\User\\atom7\\desktop\\HIS\\HL7TestOutputs", "testPipeDelimitedOutputFile.txt");

                // serialize the message to XML format output file
                WriteMessageFile(xmlParser, adtMessage, "C:\\User\\atom7\\desktop\\HIS\\HL7TestOutputs", "testXmlOutputFile.xml");

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occured while creating HL7 message {e.Message}");
            }
        }

        private static void WriteMessageFile(ParserBase parser, IMessage hl7Message, string outputDirectory, string outputFileName)
        {
            if (!Directory.Exists(outputDirectory))
                Directory.CreateDirectory(outputDirectory);

            var fileName = Path.Combine(outputDirectory, outputFileName);

            Console.WriteLine("Writing data to file...");

            if (File.Exists(fileName))
                File.Delete(fileName);
            File.WriteAllText(fileName, parser.Encode(hl7Message));
            Console.WriteLine($"Wrote data to file {fileName} successfully...");
        }

        private static void LogToDebugConsole(string informationToLog)
        {
            Debug.WriteLine(informationToLog);
        }
    }

    public class AdtMessageFactory
    {
        public static IMessage CreateMessage(string messageType)
        {
            //This patterns enables you to build other message types
            if (messageType.Equals("A01"))
            {
                return new OurAdtA01MessageBuilder().Build();
            }

            //if other types of ADT messages are needed, then implement your builders here
            throw new ArgumentException($"'{messageType}' is not supported yet. Extend this if you need to");
        }
    }

    internal class OurAdtA01MessageBuilder
    {
        private ADT_A01 _adtMessage;

        /*You can pass in a domain or data transfer object as a parameter
        when integrating with data from your application here
        I will leave that to you to explore on your own
        Using fictional data here for illustration*/

        public ADT_A01 Build()
        {
            var currentDateTimeString = GetCurrentTimeStamp();
            _adtMessage = new ADT_A01();

            CreateMshSegment(currentDateTimeString);
            CreateEvnSegment(currentDateTimeString);
            CreatePidSegment();
            CreatePv1Segment();
            return _adtMessage;
        }

        private void CreateMshSegment(string currentDateTimeString)
        {
            var mshSegment = _adtMessage.MSH;
            mshSegment.FieldSeparator.Value = "|";
            mshSegment.EncodingCharacters.Value = "^~\\&";
            mshSegment.SendingApplication.NamespaceID.Value = "Our System";
            mshSegment.SendingFacility.NamespaceID.Value = "Our Facility";
            mshSegment.ReceivingApplication.NamespaceID.Value = "Their Remote System";
            mshSegment.ReceivingFacility.NamespaceID.Value = "Their Remote Facility";
            mshSegment.DateTimeOfMessage.TimeOfAnEvent.Value = currentDateTimeString;
            mshSegment.MessageControlID.Value = GetSequenceNumber();
            mshSegment.MessageType.MessageType.Value = "ADT";
            mshSegment.MessageType.TriggerEvent.Value = "A01";
            mshSegment.VersionID.Value = "2.3";
            mshSegment.ProcessingID.ProcessingID.Value = "P";
        }

        private void CreateEvnSegment(string currentDateTimeString)
        {
            var evn = _adtMessage.EVN;
            evn.EventTypeCode.Value = "A01";
            evn.RecordedDateTime.TimeOfAnEvent.Value = currentDateTimeString;
        }

        private void CreatePidSegment()
        {
            var pid = _adtMessage.PID;
            var patientName = pid.GetPatientName(0);
            patientName.FamilyName.Value = "Mouse";
            patientName.GivenName.Value = "Mickey";
            pid.SetIDPatientID.Value = "378785433211";
            var patientAddress = pid.GetPatientAddress(0);
            patientAddress.StreetAddress.Value = "123 Main Street";
            patientAddress.City.Value = "Lake Buena Vista";
            patientAddress.StateOrProvince.Value = "FL";
            patientAddress.Country.Value = "USA";
        }

        private void CreatePv1Segment()
        {
            var pv1 = _adtMessage.PV1;
            pv1.PatientClass.Value = "O"; // to represent an 'Outpatient'
            var assignedPatientLocation = pv1.AssignedPatientLocation;
            assignedPatientLocation.Facility.NamespaceID.Value = "Some Treatment Facility";
            assignedPatientLocation.PointOfCare.Value = "Some Point of Care";
            pv1.AdmissionType.Value = "ALERT";
            var referringDoctor = pv1.GetReferringDoctor(0);
            referringDoctor.IDNumber.Value = "99999999";
            referringDoctor.FamilyName.Value = "Smith";
            referringDoctor.GivenName.Value = "Jack";
            referringDoctor.IdentifierTypeCode.Value = "456789";
            pv1.AdmitDateTime.TimeOfAnEvent.Value = GetCurrentTimeStamp();
        }

        private static string GetCurrentTimeStamp()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
        }

        private static string GetSequenceNumber()
        {
            const string facilityNumberPrefix = "1234"; // some arbitrary prefix for the facility
            return facilityNumberPrefix + GetCurrentTimeStamp();
        }
    }
}