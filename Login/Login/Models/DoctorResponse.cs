using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Login.Models
{
    public class Subspecialty
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Specialization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Subspecialty> Subspecialties { get; set; }
        public string SubspecialtiesDescription
        {
            get
            {
                return Subspecialties != null ? string.Join(", ", Subspecialties.Select(x => x.Name)) : "";
            }
        }
    }

    public class Facility
    {
        public int Id { get; set; }
        public string Hospital_clinic { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Provice { get; set; }
        public string Rooms { get; set; }
        public string Schedule { get; set; }
        public string Contacts { get; set; }

        public string DisplayAddress
        {
            get
            {
                return Address + " " + City + " " + Provice;
            }
        }
    }

    public class Doctor
    {
        public int Id { get; set; }
        public string First_name { get; set; }
        public string Middle_name { get; set; }
        public string Last_name { get; set; }
        public string Suffix { get; set; }
        public string Title { get; set; }
        public List<Specialization> Specializations { get; set; }
        public List<Facility> Facilities { get; set; }

        public string Name
        {
            get
            {
                return First_name + " " + Middle_name + " " + Last_name + " " + Suffix;
            }
        }

        public string ImgURL {
            get
            {
                return "profile.png";
            }
        }
    }

    public class DoctorResponse
    {
        public List<Doctor> Data { get; set; }
    }
}
