using EduHomeBackEnd.Models;
using System.Collections.Generic;

namespace EduHomeBackEnd.ViewModels
{
    public class EventVM
    {
        public Event Event { get; set; }
        public List<Tag> Tags { get; set; }
        public List<EventSpeaker> EventSpeakers { get; set; }
        public List<Speaker> Speakers { get; set; }
    }
}
