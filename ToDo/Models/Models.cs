﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ToDo.Models
{
    #region Enums
    //Enums
    public enum Category { Charity, Community, Entertainment, Family, Music, Outdoor, Sporting, Theatre, Other }
    public enum ActivityCategory { Adventure, Culture, Drink, Family, Food, Historical, Shop }
    public enum FileType { EventImage = 1, Photo }
    #endregion

    //Music Genre Class
    public class MusicGenre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int MusicGenreID { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Genre")]
        public string MusicGenreName { get; set; }

        public virtual ICollection<Band> GenreBands { get; set; }
    }

    //Event Category Class
    public class EventCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int EventCategoryID { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Event Type")]
        public string EventCategoryName { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }

    //Venue Type Class
    public class Venue_Type
    {
        //Note: if I change the name of the properties in this class, make the changes in the VenuesTablePartialView mehtod and the _VenuesTable view
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int Venue_TypeID { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Venue Type")]
        public string VenueTypeName { get; set; }

        public virtual ICollection<Venue> Venues { get; set; }
    }

    //Town Class
    public class Town
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int TownID { get; set; }

        [Required(ErrorMessage = "You must town name")]
        [DataType(DataType.Text)]
        [Display(Name = "City/ Town")]
        public string TownName { get; set; }

        [Required(ErrorMessage = "You must county")]
        [DataType(DataType.Text)]
        [Display(Name = "County")]
        public string County { get; set; }

        public virtual ICollection<Venue> Venues { get; set; }
    }

    //Event Class
    public class Event
    {
        //ID
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventID { get; set; }

        //User ID Foregin Key
        public string OwnerID { get; set; }

        //Foreign Key for Club
        public int VenueID { get; set; }
        [ForeignKey("VenueID")]
        public virtual Venue Venue { get; set; }

        //Title
        [Required(ErrorMessage = "You must enter a title")]
        [DataType(DataType.Text)]
        [Display(Name = "Title*")]
        public string EventTitle { get; set; }

        //Date
        [Required(ErrorMessage = "You must enter a date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date*")]
        public DateTime EventDate { get; set; }

        //Time
        [Required(ErrorMessage = "You must enter a start time")]
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [Display(Name = "Time*")]
        public DateTime EventTime { get; set; }

        //End Time
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [Display(Name = "End Time*")]
        public DateTime EventEndTime { get; set; }

        //Description
        [Required(ErrorMessage = "Give your event a brief description")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description*")]
        public string EventDescription { get; set; }

        //Event Category
        [Required(ErrorMessage = "You must select a category from the list")]
        [Display(Name = "Category*")]
        public Category EventCategory { get; set; }

        //Youtube Link     
        [Display(Name = "YouTube")]
        public string EventYouTube { get; set; }

        //SoundCloud Link
        [Display(Name = "SoundCloud")]
        public string EventSoundCloud { get; set; }

        //Facebook Link
        [Display(Name = "Facebook")]
        public string EventFacebook { get; set; }

        //Twitter Link
        [Display(Name = "Twitter")]
        public string EventTwitter { get; set; }

        //Instagram Link
        [Display(Name = "Instagram")]
        public string EventInstagram { get; set; }

        //Official Website Link
        [Display(Name = "Website")]
        [DataType(DataType.Url, ErrorMessage = "This is not a valid Url")]
        public string EventWebsite { get; set; }

        //Ticket Price
        [Display(Name = "Price")]
        public double? EventTicketPrice { get; set; }

        //Ticket Shop Link/ Location
        [Display(Name = "Vendor")]
        public string EventTicketStore { get; set; }

        //Image File 
        public virtual ICollection<File> Files { get; set; }

        //Event Active
        [Display(Name = "Event Active")]
        public bool EventActive { get; set; }

        //Event Category Foreign ID
        [Display(Name = "Category")]
        [Required(ErrorMessage = "You must select a category")]
        public int EventCatID { get; set; }

        //Event Category
        public virtual EventCategory EventCat { get; set; }

        //Event View Counter
        [Display(Name = "Total Views:")]
        public int EventViewCounter { get; set; }

        //Event variable for daily views counter
        public DateTime EventViewCounterReset { get; set; }

        //EventDaily  View Counter
        [Display(Name = "Todays Views:")]
        public int EventDailyViewCounter { get; set; }

        //Foreign Key for Band
        public int? BandID { get; set; }
    }

    //Image File class
    //http://www.mikesdotnetting.com/article/259/asp-net-mvc-5-with-ef-6-working-with-files
    public class File
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FileId { get; set; }

        [StringLength(255)]
        public string FileName { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        public byte[] Content { get; set; }

        public FileType FileType { get; set; }

        public int EventID { get; set; }

        public virtual Event Event { get; set; }
    }

    //Venue Files
    public class VenueFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VenueFileId { get; set; }

        [StringLength(255)]
        public string VenueFileName { get; set; }

        [StringLength(100)]
        public string VenueContentType { get; set; }

        public byte[] VenueContent { get; set; }

        public FileType VenueFileType { get; set; }

        public int VenueID { get; set; }

        public virtual Venue Venue { get; set; }
    }

    //Band Files
    public class BandFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BandFileId { get; set; }

        [StringLength(255)]
        public string BandFileName { get; set; }

        [StringLength(100)]
        public string BandContentType { get; set; }

        public byte[] BandContent { get; set; }

        public FileType BandFileType { get; set; }

        public int BandID { get; set; }

        public virtual Band Band { get; set; }
    }

    //Venue class
    public class Venue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VenueID { get; set; }

        //Id for the user that created this venue
        public string OwnerId { get; set; }

        //List of events for this venue
        public List<Event> VenueEvents { get; set; }

        //Name
        [Required(ErrorMessage = "You must enter a name")]
        [DataType(DataType.Text)]
        [Display(Name = "Name*")]
        public string VenueName { get; set; }

        //Address
        [Required(ErrorMessage = "You must enter a street")]
        [DataType(DataType.Text)]
        [Display(Name = "Street*")]
        public string VenueAddress { get; set; }

        //Description
        [Required(ErrorMessage = "Give your venue a brief description")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Details*")]
        public string VenueDescription { get; set; }

        //Contact Email
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "This is not a valid email address")]
        public string VenueEmail { get; set; }

        //Contact Number
        [Display(Name = "Telephone")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "This is not a valid phone number")]
        public string VenuePhoneNumber { get; set; }

        //Image File 
        public virtual ICollection<VenueFile> VenueFiles { get; set; }

        //Venue Active
        [Display(Name = "Venue Active")]
        public bool VenueActive { get; set; }

        //Venue Town Foreign
        [Display(Name = "Town*")]
        [Required(ErrorMessage = "You must select a town")]
        public int VenueTownID { get; set; }

        //venue Town
        public virtual Town VenueTown { get; set; }

        //Venue Catergory Foreign
        [Display(Name = "Type*")]
        [Required(ErrorMessage = "You must select a venue type")]
        public int VenueTypeID { get; set; }

        //venue Category
        [Display(Name = "Type*")]
        public virtual Venue_Type VenueType { get; set; }

        //Venue Facebook
        [Display(Name = "Facebook")]
        public string VenueFacebook { get; set; }

        //Venue View Counter
        [Display(Name = "Total Views:")]
        public int VenueViewCounter { get; set; }

        //Event variable for daily views counter
        public DateTime VenueViewCounterReset { get; set; }

        //EventDaily  View Counter
        [Display(Name = "Todays Views:")]
        public int VenueDailyViewCounter { get; set; }

        //Boolean to flag venue for deletition
        [Display(Name = "Delete Flag")]
        public bool VenueDeleteFlag { get; set; }
    }

    //Venue Mailing List class
    public class VenueMailingList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VenueMailingListId { get; set; }

        public int VenueID { get; set; }

        public string User_ID { get; set; }

        public string UserEmail { get; set; }
    }

    //Band Mailing List class
    public class BandMailingList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BandMailingListId { get; set; }

        public int BandID { get; set; }

        public string User_ID { get; set; }

        public string UserEmail { get; set; }
    }

    //Band Class
    public class Band
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BandID { get; set; }

        //Id for the owner of this band
        public string OwnerId { get; set; }

        //Name
        [Required(ErrorMessage = "You must enter a name")]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string BandName { get; set; }

        //Description
        [Required(ErrorMessage = "Give your venue a brief description")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string BandDescription { get; set; }

        //Contact Number
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "This is not a valid phone number")]
        public int? BandContactNumber { get; set; }

        //Contact Email
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "This is not a valid email address")]
        public string BandEmail { get; set; }

        //Facebook
        [Display(Name = "Facebook")]
        public string BandFacebook { get; set; }

        //Band Active
        [Display(Name = "Band Active")]
        public bool BandActive { get; set; }

        //Music Genre Foreign ID
        [Required(ErrorMessage = "You must select a genre for your band")]
        [Display(Name = "Genre")]
        public int BandGenreID { get; set; }

        //Band Genre
        [Display(Name = "Genre")]
        public virtual MusicGenre BandGenre { get; set; }

        //Image File 
        public virtual ICollection<BandFile> BandFiles { get; set; }

        //Youtube Link      
        [Display(Name = "YouTube")]
        public string BandYouTube { get; set; }

        //SoundCloud Link
        [Display(Name = "SoundCloud")]
        public string BandSoundCloud { get; set; }

        //Manager Name
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string BandManagerName { get; set; }

        //Manager Email
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "This is not a valid email address")]
        public string BandManagerEmail { get; set; }

        //Press Contact
        [Display(Name = "Email")]
        [DataType(DataType.Text)]
        public string BandPressContact { get; set; }

        //Record Label
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string BandRecordLabel { get; set; }

        //Booking Agent
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string BandBookingAgentName { get; set; }

        //Manager Email
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "This is not a valid email address")]
        public string BandBookingAgentEmail { get; set; }

        //List of events for this band
        public List<Event> BandEvents { get; set; }

        public Band()
        {
            BandEvents = new List<Event>();
        }
    }

    //Admin settings class
    public class AdminSettings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdminSettingsID { get; set; }

        //Events
        [Display(Name = "Top Event")]
        public int TopFeaturedEvent { get; set; }

        [Display(Name = "Featured Event 1")]
        public int FeaturedEvent1 { get; set; }

        [Display(Name = "Featured Event 2")]
        public int FeaturedEvent2 { get; set; }

        [Display(Name = "Featured Event 3")]
        public int FeaturedEvent3 { get; set; }

        [Display(Name = "Featured Event 4")]
        public int FeaturedEvent4 { get; set; }

        //Venues
        [Display(Name = "Top Venue")]
        public int TopFeaturedVenue { get; set; }

        [Display(Name = "Featured Venue 1")]
        public int FeaturedVenue1 { get; set; }

        [Display(Name = "Featured Venue 2")]
        public int FeaturedVenue2 { get; set; }

        [Display(Name = "Featured Venue 3")]
        public int FeaturedVenue3 { get; set; }

        [Display(Name = "Featured Venue 4")]
        public int FeaturedVenue4 { get; set; }
    }

    //Gmail SMTP class
    public class GMailer
    {
        public static string GmailUsername { get; set; }
        public static string GmailPassword { get; set; }
        public static string GmailHost { get; set; }
        public static int GmailPort { get; set; }
        public static bool GmailSSL { get; set; }

        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }

        static GMailer()
        {
            GmailHost = "smtp.gmail.com";
            GmailPort = 587; // Gmail can use ports 25, 465 & 587; but must be 25 for medium trust environment.
            GmailSSL = true;
        }

        public void Send()
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = GmailHost;
            smtp.Port = GmailPort;
            smtp.EnableSsl = GmailSSL;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(GmailUsername, GmailPassword);

            using (var message = new MailMessage(GmailUsername, ToEmail))
            {
                message.Subject = Subject;
                message.Body = Body;
                message.IsBodyHtml = IsHtml;
                smtp.Send(message);
            }
        }
    }
}
