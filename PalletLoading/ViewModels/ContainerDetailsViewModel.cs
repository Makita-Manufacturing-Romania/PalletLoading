using PalletLoading.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalletLoading.ViewModels
{
    public class ContainerDetailsViewModel
    {
        public Container Container { get; set; }
        public Countries Country { get; set; }
        public ContainerType Type { get; set; }
        public List<Pallet> Pallets { get; set; }
        public List<ModelImportDataPccPallets> ModelImportDataPccPallets { get; set; }
        public Pallet Pallet { get; set; }
        public string countryText { get; set; }
        public TotalQuantities TotalQuantities { get; set; }
        public int palletCount { get; set; }

        public List<ClientsData> ClientsData { get; set; }
        public List<PalletForPDFViewModel> PalletsDataForPDF { get; set; }
        public string loadingData { get; set; }
        public string totalClients { get; set; }
        public string truckType { get; set; }
        public string loadingType { get; set; }
        public FormDefinition FormDefinition { get; set;}
        public SecuringLoad SecuringLoad { get; set; }
        public FormData FormData { get; set; }
        public List<UploadModel> UploadModel { get; set; }
    }
}
