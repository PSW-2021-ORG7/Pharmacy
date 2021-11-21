using backend.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services
{
    public class MedicineCombinationService
    {
        private IMedicineCombinationRepository medicineCombinationRepository;

        public MedicineCombinationService(IMedicineCombinationRepository medicineCombinationRepository)
        {
            this.medicineCombinationRepository = medicineCombinationRepository;
        }

        public bool Save(int id, int m)
        {
            return medicineCombinationRepository.Save(new Model.MedicineCombination(id, m));
        }
    }
}
