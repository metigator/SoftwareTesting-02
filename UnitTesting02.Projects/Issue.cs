using System;

namespace UnitTesting02.Projects
{
    public class Issue
    {
        public string Key { get; private set; } // "HW-2022-U-1F7531A6", "SW-2019-L-405B417E"
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; } // 0001-01-01
        public Priority Priority { get; private set; }
        public Category Category { get; private set; }


        public Issue (string description, Priority priority, Category category, 
            DateTime? createdAt = null)
        {

            if (string.IsNullOrWhiteSpace(description))
                throw new InvalidIssueDescriptionException();

            Description = description;

            this.Priority = priority;

            this.Category = category;

            this.CreatedAt = createdAt is null ? DateTime.Now : createdAt.Value;

            this.Key = GenerateKey();
           
        }

        private string GenerateKey()
        {
            var categorySegment = Category is Category.Hardware ? "HW" :
            Category is Category.Software ? "SW" : "NA";

            var prioritySegment =
                Priority is Priority.Low ? "L" :
                   Priority is Priority.Medium ? "M" :
                         Priority is Priority.High ? "H" : "U";

            var yearSegment = CreatedAt.Year.ToString(); // YYYY

            var uniqueId = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();

            return $"{ categorySegment }-{yearSegment}-{prioritySegment}-{uniqueId}";
        }

        public override string ToString()
        {
            return $"[{Key}] {Description}";
        }
    }
}
