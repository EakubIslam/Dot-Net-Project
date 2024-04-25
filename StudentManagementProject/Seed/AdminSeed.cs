using StudentAttendanceSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFinalProject.Seed
{
    internal class AdminSeed
    {
        public static List<Admin> Admins
        {
            get
            {
                return new List<Admin>()
                {
                    new Admin() {Id=-1, Name = "admin",Username = "admin", Password="123456"},
                    new Admin() {Id=-2, Name = "Eakub",Username = "eakub", Password="123456" }
                };
            }
        }
    }
}
