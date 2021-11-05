using backend.Model;
using backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services
{
    public class HospitalService
    {
        IHospitalRepository hospitalRepository;

        public HospitalService(IHospitalRepository hospitalRepository) => this.hospitalRepository = hospitalRepository;

        public List<Hospital> GetAll() => hospitalRepository.GetAll();

        public bool Save(Hospital hospital) {
            if (hospitalRepository.Save(hospital)) return true;
            return false;
          }
    }
}
