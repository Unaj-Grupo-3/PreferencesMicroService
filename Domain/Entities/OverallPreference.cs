﻿namespace Domain.Entities
{
    public class OverallPreference
    {
        public int OverallPreferenceId { get; set; }
        public Guid UserId { get; set; } //FK
        public int SinceAge { get; set; }
        public int UntilAge { get; set; }
        public int Distance { get; set; }
    }
}
