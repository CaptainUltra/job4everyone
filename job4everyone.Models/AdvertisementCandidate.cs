using System;
using System.Collections.Generic;
using System.Text;

namespace job4everyone.Models
{
    public class AdvertisementCandidate
    {
        public int AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }

        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
    }
}
