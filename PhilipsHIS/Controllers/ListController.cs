using Microsoft.AspNetCore.Mvc;
using PhilipsHIS.Data;
using PhilipsHIS.Models;
using PhilipsHIS.HL7;
using NHapi.Base.Parser;
using NHapi.Model.V23.Message;
//using System.Diagnostics;
//using NHapi.Model.CustomZSegments;
//using NHapi.Model.V23;

namespace PhilipsHIS.Controllers
{
    public class ListController : Controller
    {
        public readonly ApplicationDbContext _db;
        public readonly HL7Create _hL7Create;

        public ListController(ApplicationDbContext db, HL7Create hL7Create)
        {
            _db = db;
            _hL7Create = hL7Create;
        }

        public IActionResult ListIndex()
        {
            IEnumerable<List> objListIndexList = _db.Lists;

            const string messageString = "MSH|^~\\&|SENDING_APPLICATION|SENDING_FACILITY|RECEIVING_APPLICATION|RECEIVING_FACILITY|20221002160641||ACK|1407511|P|2.3||||||\r\n" +
                                            "MSA|AA|1407511|Success||";
            var result = HL7Parser(messageString);
            var message_type = result.MessageType.MessageType; //เก็บค่า
            Console.WriteLine("Message Type is " + message_type);
            Console.WriteLine("Message Control Id is " + result.MessageControlID);
            Console.WriteLine("Message Timestamp is " + result.DateTimeOfMessage.TimeOfAnEvent.GetAsDate());
            Console.WriteLine("Sending Application is " + result.SendingApplication.NamespaceID.Value);
            Console.WriteLine("Sending Facility is " + result.SendingFacility.NamespaceID.Value);

            //_hL7Create.CreateMessage();

            return View(objListIndexList);
        }

        //GET
        public IActionResult Admit()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Admit(List obj)
        {
            if (ModelState.IsValid)
            {
                _db.Lists.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Admit new patient successfully";
                return RedirectToAction("ListIndex");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(string? hn)
        {
            if (hn == null)
            {
                return NotFound();
            }
            var listFromDb = _db.Lists.Find(hn);
            if (listFromDb == null)
            {
                return NotFound();
            }
            return View(listFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(List obj)
        {
            if (ModelState.IsValid)
            {
                _db.Lists.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Update patient information successfully";
                return RedirectToAction("ListIndex");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(string? hn)
        {
            if (hn == null)
            {
                return NotFound();
            }
            var listFromDb = _db.Lists.Find(hn);
            if (listFromDb == null)
            {
                return NotFound();
            }
            return View(listFromDb);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(string? hn)
        {
            var obj = _db.Lists.Find(hn);
            if (hn == null)
            {
                return NotFound();
            }
                _db.Lists.Remove(obj);
                _db.SaveChanges();
                TempData["success"] = "Delete patient successfully";
                return RedirectToAction("ListIndex");
        }

        //GET
        public IActionResult ViewDetail(string? hn)
        {
            if (hn == null)
            {
                return NotFound();
            }
            var listFromDb = _db.Lists.Find(hn);
            if (listFromDb == null)
            {
                return NotFound();
            }
            return View(listFromDb);
        }

        public IActionResult VSIndex()
        {
            IEnumerable<VitalSignsModel> vsVSIndexList = _db.VSTable;
            return View(vsVSIndexList);
        }

        //GET
        public IActionResult VitalSigns()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VitalSigns(VitalSignsModel vs)
        {
            if (ModelState.IsValid)
            {
                _db.VSTable.Add(vs);
                _db.SaveChanges();
                //TempData["success"] = "Admit new patient successfully";
                return RedirectToAction("VSIndex");
            }
            return View(vs);
        }

        public NHapi.Model.V23.Segment.MSH HL7Parser(String input)
        {
            // instantiate a PipeParser, which handles the "traditional or default encoding"
            var ourPipeParser = new PipeParser();

            try
            {
                // parse the string format message into a Java message object
                var hl7Message = ourPipeParser.Parse(input);

                //cast to ACK message to get access to ACK message data
                var ackResponseMessage = hl7Message as ACK;
                if (ackResponseMessage != null)
                {
                    //access message data and display it
                    //note that I am using encode method at the end to convert it back to string for display
                    var mshSegmentMessageData = ackResponseMessage.MSH;

                    return mshSegmentMessageData;
                }
                return null;
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
                return null;
            }
        }

        //public NHapi.Model.V23.Segment.MSH HL7Parser2(string customsegmentbasedhl7message)
        //{
        //    const string customsegmentbasedhl7message = "msh|^~\\&|suns1|ovi02|azis|cmd|200606221348||adt^a01|1049691900|p|2.3\r"
        //                                                    + "evn|a01|200803051509||||200803031508\r"
        //                                                    + "pid|||5520255^^^pk^pk~zzzzzz83m64z148r^^^cf^cf~zzzzzz83m64z148r^^^ssn^ssn^^20070103^99991231~^^^^team||zzz^zzz||19830824|f||||||||||||||||||||||n\r"
        //                                                    + "zpv|some custom notes|additional custom description of the visit goes here";

        //    var parser = new PipeParser();

        //    var parsedmessage = parser.Parse(customsegmentbasedhl7message, Constants.VERSION);

        //    Console.WriteLine("type: " + parsedmessage.GetType());

        //    //cast this to the custom message that we have overridden
        //    var zdta01 = parsedmessage as NHapi.Model.Customzsegments.message.adt_a01;
        //    if (zdta01 != null)
        //    {
        //        Console.WriteLine(zdta01.zpv.customnotes.value);
        //        Console.WriteLine(zdta01.zpv.customdescription.value);
        //    }
        //}
    }

}
