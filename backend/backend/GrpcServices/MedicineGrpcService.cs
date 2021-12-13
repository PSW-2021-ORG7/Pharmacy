﻿using backend.DAL;
using backend.Model;
using backend.Protos;
using backend.Repositories;
using backend.Repositories.Interfaces;
using backend.Services;
using Grpc.Core;
using System.Threading.Tasks;

namespace backend.GrpcServices
{
    public class MedicineGrpcService : NetGrpcService.NetGrpcServiceBase
    {
        private MedicineService _medicineService = new MedicineService(new MedicineRepository(new DrugStoreContext()), new MedicineInventoryRepository(new DrugStoreContext()));
        public override Task<MedicineQuantityCheckResponse> CheckIfAvailable(MedicineQuantityCheckRequest request, ServerCallContext context)
        {
            MedicineQuantityCheckResponse response = new MedicineQuantityCheckResponse();
            response.Response = _medicineService.CheckMedicineQuantity(new DTO.MedicineQuantityCheck { Name = request.Name, DosageInMg = request.DosageInMg, Quantity = request.Quantity });
            return Task.FromResult(response);
        }


    }
}
