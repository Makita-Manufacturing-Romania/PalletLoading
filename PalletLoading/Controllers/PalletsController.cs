using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using PalletLoading.Data;
using PalletLoading.Models;
using PalletLoading.ViewModels;

namespace PalletLoading.Controllers
{
    public class PalletsController : MainController
    {

        public PalletsController(PalletLoadingContext context) : base(null, context, null)
        {
        }

        // GET: Pallets
        public async Task<IActionResult> Index()
        {
            List<Pallet> pallets = _context.Pallets.ToList();
            List<PalletType> palletTypes = _context.PalletTypes.ToList();

            var viewModel = new PalletViewModel
            {
                Pallet = pallets,
                PalletType = palletTypes
            };
            return View(viewModel);
        }

        public async Task<IActionResult> CountryList(int id)
        {
            var countryList = _context.ContainerATs.Include(c => c.Country).Where(c => c.ContainerId == id).ToList();
            AddClientsModelView acmv = new AddClientsModelView { ContainerATs = countryList, idContainer = id };
            return View(acmv);
        }

        public IActionResult AddContainer(int id)
        {
            ViewData["CountryId"] = new SelectList(_context.Countries.Select(c => new { Id = c.Id, Name = (c.Name + " - " + c.Abbreviation) }).OrderBy(x => x.Name), "Id", "Name");

            var tempContainerAt = new ContainerAT
            {
                ContainerId = id
            };
            return View(tempContainerAt);
        }

        public IActionResult SaveContainer(ContainerAT containerAt)
        {
            ViewData["CountryId"] = new SelectList(_context.Countries.OrderBy(x => x.Name), "Id", "Name");
            _context.Add(containerAt);
            _context.SaveChanges();
            return RedirectToAction("CountryList", new { id = containerAt.ContainerId });
        }


        // GET: Pallets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pallet = await _context.Pallets
                .FirstOrDefaultAsync(m => m.Id == id);

            PalletType palletType = _context.PalletTypes.First(x => x.Id == pallet.TypeId);

            var viewModel = new PalletDetailsViewModel()
            {
                Pallet = pallet,
                PalletType = palletType
            };
            if (pallet == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: Pallets/Create
        public IActionResult Create(int? id)
        {
            ViewData["PalletId"] = new SelectList(_context.PalletTypes, "Id", "Name");
            ViewData["CountryId"] = new SelectList(_context.Countries.OrderBy(x => x.Name), "Id", "Name");
            TempData["containerId"] = id.ToString();

            var container = _context.Containers.Include(c => c.Country).First(x => x.Id == id);
            List<Pallet> pallets = _context.Pallets.Include(c => c.PalletImportData).Include(c => c.PalletImportDataHistory).Where(x => x.Container2Id == container.Id).ToList();
            List<ModelImportDataPccPallets> PccPallets = _context.Pallets.Include(c => c.PalletImportData).Include(c => c.PalletImportDataHistory).Where(x => x.Container2Id == container.Id)
                .GroupJoin(_context.PartCenterPallets, c => c.PalletImportDataId, c => c.ImportDataId, (c, d) => new { pallet = c, PartCenterPallets = d })
                .SelectMany(c => c.PartCenterPallets.DefaultIfEmpty(), (c, d) => new ModelImportDataPccPallets { pallet = c.pallet, PartCenterPallets = d })
                .GroupJoin(_context.PartCenterPallets, c => c.pallet.PalletImportDataHistoryId, c => c.ImportDataHistoryId, (c, d) => new { pallet = c.pallet, PartCenterPalletsHistory = d, PartCenterPallets = c.PartCenterPallets })
                .SelectMany(c => c.PartCenterPalletsHistory.DefaultIfEmpty(), (c, d) => new ModelImportDataPccPallets { pallet = c.pallet, PartCenterPalletsHistory = d, PartCenterPallets = c.PartCenterPallets })
                .ToList();
            ContainerType type = _context.ContainerTypes.First(x => x.Id == container.TypeId);
            List<SwitchedPallet> switchedPallets = _context.SwitchedPallets.Where(x => x.IdContainer == container.Id).ToList();
            var pickedPallets = _context.ImportData.Where(x => x.container_no == container.Name).Count();
            ViewData["pickedPallets"] = pickedPallets;
            List<ContainerAT> containerATs = _context.ContainerATs.Include(c => c.Country).Where(x => x.ContainerId == container.Id).ToList();
            //List<Countries> countries = containerATs.Select(c => c.Country).ToList();
            //foreach(var containerAT in containerATs)
            //{
            //    var country = _context.Countries.First(x => x.Id == containerAT.CountryId);
            //    countries.Add(country);
            //}



            if (pallets.Count == 0)
            {
                var fillRule = _context.ContainerFillRules
                    .Where(l => l.Id == container.ContainerFillRule)
                    .Select(l => l.Name)
                    .FirstOrDefault();
                switch (fillRule)
                {
                    case "Manual":
                        //No Auto Fill
                        break;
                    case "Auto 2x2":
                        for (int i = 0; i < container.NoOfColumns; i++)
                        {
                            AddPallet(id.ToString(), "0," + i, null);
                            AddPallet(id.ToString(), "0," + i, null);
                            AddPallet(id.ToString(), "1," + i, null);
                            AddPallet(id.ToString(), "1," + i, null);
                        }
                        return RedirectToAction("Create", "Pallets", new { id = id });
                    case "X":
                        break;
                }
            }


            try
            {
                using (var cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandText = "sp_execPalletPLJob";


                    cmd.CommandType = CommandType.StoredProcedure;

                    if (cmd.Connection.State != ConnectionState.Open)
                        cmd.Connection.Open();
                    cmd.CommandTimeout = 120;
                    cmd.ExecuteReader();


                    cmd.Connection.Close();
                }
            }
            catch (Exception e)
            {

            }
            var listOfCountryAbv = containerATs.Select(c => c.Country.Abbreviation.Trim()).ToList();
            //removed bad code ['with' error]
            List<ImportDataPalletsLP> idplpList = _context.ImportDataPalletsLP.AsEnumerable().Where(c => listOfCountryAbv.Contains(c.CustomerCode180P.Trim()) ||  listOfCountryAbv.Contains(c.CustomerCode250P.Trim())).ToList();
            

            //ImportDataPalletsLP idplp = _context.ImportDataPalletsLP.Where(c => c.CustomerCode180P.Equals(container.Country.Abbreviation) || c.CustomerCode250P.Equals(container.Country.Abbreviation)).FirstOrDefault();

            var viewModel = new AddContainerViewModel
            {
                Container = container,
                Pallets = pallets,
                Type = type,
                SwitchedPallets = switchedPallets,
                //Countries = countries,
                ModelImportDataPccPallets = PccPallets,
                ContainerAT = containerATs,
                idplp = idplpList
            };

            return View(viewModel);
        }
        public IActionResult GetLP(int containerId)
        {

            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "sp_execPalletPLJob";


                cmd.CommandType = CommandType.StoredProcedure;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                cmd.CommandTimeout = 120;
                cmd.ExecuteReader();


                cmd.Connection.Close();
            }

            var listOfCountryAbv = _context.ContainerATs.Where(c => c.ContainerId == containerId).Select(c => c.Country.Abbreviation.Trim()).ToList();

            //var importLP = _context.ImportDataPalletsLP.Where(c => c.CustomerCode180P.Equals(country) || c.CustomerCode250P.Equals(country)).FirstOrDefault();
            List<ImportDataPalletsLP> idplpList = _context.ImportDataPalletsLP.Where(c => listOfCountryAbv.Contains(c.CustomerCode180P.Trim()) || listOfCountryAbv.Contains(c.CustomerCode250P.Trim())).ToList();

            if (idplpList == null)
            {
                idplpList.Add(new ImportDataPalletsLP { CustomerCode180P = "", CustomerCode250P = "", LOADED180 = 0, LOADED250 = 0, PICKED180 = 0, PICKED250 = 0 });
            }

            return Json(new { importLP = idplpList });

        }
        public IActionResult GetPallets(int containerId)
        {

            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "sp_execPalletJob";


                cmd.CommandType = CommandType.StoredProcedure;

                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();
                cmd.CommandTimeout = 120;

                try
                {
                    cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    string errorMessage = Convert.ToString(ex);
                    //return Json(new { ex = errorMessage });
                }


                cmd.Connection.Close();
            }


            var dateContainer = _context.Containers.Any(c => c.CreatedDate == DateTime.Today && c.Id == containerId);
            var containerAt = _context.ContainerATs.Include(c => c.Country).Where(c => c.ContainerId == containerId).ToList();

            var listPalletsMap = _context.ImportData.AsEnumerable().Where(c => containerAt.Select(c => c.Country.Abbreviation).ToList().Contains(c.consignee_code) && containerAt.Select(c => c.ContainerName).ToList().Contains(c.container_no)).OrderBy(c => c.loading_time).ToList();
            var switches = _context.SwitchedPallets.Where(c => c.IdContainer == containerId).ToList();
            decimal volumOcupat = 0.000000M;
            if (dateContainer && listPalletsMap.Count != 0)
            {

                var listPalletsApp = _context.Pallets.Include(x => x.PalletImportData).Where(c => c.Container2Id == containerId).OrderBy(c => c.OrderNo).ToList();

                var mvcp = new List<ModelViewCreatePallet>();
                int i = 0;
                decimal tempVolume = 0;

                for (; i < listPalletsApp.Count && i < listPalletsMap.Count; i++)
                {

                    listPalletsApp[i].PalletImportDataId = listPalletsMap[i].id;

                    if (listPalletsMap[i].serial_from == 0)
                    {
                        if (_context.PartCenterPallets.Any(c => c.Destination == listPalletsMap[i].consignee_code  && c.Pallet_number == listPalletsMap[i].pallet_no && c.Status == false))
                        {
                            var tempPcPallet = _context.PartCenterPallets.Where(c => c.Destination == listPalletsMap[i].consignee_code && c.Pallet_number == listPalletsMap[i].pallet_no && c.Status == false).FirstOrDefault();
                            tempVolume = tempPcPallet.Volume;
                            tempPcPallet.Status = true;
                            tempPcPallet.ImportDataId = listPalletsMap[i].id;
                            volumOcupat = volumOcupat + tempPcPallet.Volume;
                        }
                        else if (_context.PartCenterPallets.Any(c => c.ImportDataId == listPalletsMap[i].id))
                        {
                            var tempPcPallet = _context.PartCenterPallets.Where(c => c.ImportDataId == listPalletsMap[i].id).FirstOrDefault();
                            tempVolume = tempPcPallet.Volume;
                            volumOcupat = volumOcupat + tempPcPallet.Volume;
                        }
                        else
                        {
                            tempVolume = 0;

                        }
                    }
                    else
                    {
                        volumOcupat = volumOcupat + listPalletsMap[i].volume * listPalletsMap[i].picking_qty;
                    }

                    var tempMVID = new ModelViewCreatePallet { OrderNoApp = listPalletsApp[i].OrderNo, PalletMap = listPalletsMap[i], VolumPcPallet =  tempVolume};
                    mvcp.Add(tempMVID);

                }
                for (; i < listPalletsMap.Count; i++)
                {
                    if (listPalletsMap[i].serial_from == 0)
                    {
                        if (_context.PartCenterPallets.Any(c => c.Destination == listPalletsMap[i].consignee_code  && c.Pallet_number == listPalletsMap[i].pallet_no && c.Status == false))
                        {
                            var tempPcPallet = _context.PartCenterPallets.Where(c => c.Destination == listPalletsMap[i].consignee_code && c.Pallet_number == listPalletsMap[i].pallet_no).FirstOrDefault();
                            tempPcPallet.ImportDataId = listPalletsMap[i].id;
                            tempPcPallet.Status = true;
                            tempVolume = tempPcPallet.Volume;


                        }
                        else if(_context.PartCenterPallets.Any(c=>c.ImportDataId == listPalletsMap[i].id))
                        {
                            var tempPcPallet = _context.PartCenterPallets.Where(c => c.ImportDataId == listPalletsMap[i].id).FirstOrDefault();
                            tempVolume = tempPcPallet.Volume;
                            volumOcupat = volumOcupat + tempPcPallet.Volume;
                        }
                        else
                        {
                            tempVolume = 0;

                        }
                    }

                    var tempMVID = new ModelViewCreatePallet { OrderNoApp = -1, PalletMap = listPalletsMap[i], VolumPcPallet = tempVolume };
                    //listPalletsMap[i].Pallet = null;

                    mvcp.Add(tempMVID);
                }


                _context.UpdateRange(listPalletsApp);
                _context.SaveChanges();

                foreach (var item in switches)
                {
                    var firstPalletTemp = listPalletsApp.Where(c => c.Id == item.FirstPalletId).FirstOrDefault();
                    var secondPalletTemp = listPalletsApp.Where(c => c.Id == item.SecondPalletId).FirstOrDefault();
                    var auxid = firstPalletTemp.PalletImportDataId;
                    firstPalletTemp.PalletImportDataId = secondPalletTemp.PalletImportDataId;
                    secondPalletTemp.PalletImportDataId = auxid;
                    _context.Update(firstPalletTemp);
                    _context.Update(secondPalletTemp);
                    _context.SaveChanges();

                }


                var listPalletsApp2 = _context.Pallets.Include(x => x.PalletImportData).Where(c => c.Container2Id == containerId).OrderBy(c => c.OrderNo).ToList();
                List<Pallet> palletsMotrica = null;
                decimal motricaWeight = 0;
                if (listPalletsApp2.Any(x => x.Column <= 3))
                {
                    palletsMotrica = listPalletsApp2.Where(x => x.Column <= 3 && x.PalletImportData != null).ToList();

                    motricaWeight = palletsMotrica.Sum(x => x.PalletImportData.weight);
                    ViewBag.motricaWeight = motricaWeight;
                }

                return Json(new { listPallets = mvcp, motrica = motricaWeight, volumOcupat = volumOcupat.ToString("N2") });
            }
            else
            {
                var listPalletsMap2 = _context.ImportDataHistory.AsEnumerable().Where(c => containerAt.Select(c => c.Country.Abbreviation).ToList().Contains(c.consignee_code) && containerAt.Select(c => c.ContainerName).ToList().Contains(c.container_no)).OrderBy(c => c.loading_time).ToList();

                var listPalletsApp = _context.Pallets.Include(x => x.PalletImportDataHistory).Where(c => c.Container2Id == containerId).OrderBy(c => c.OrderNo).ToList();

                var mvcp = new List<ModelViewCreatePallet>();
                if (listPalletsMap2.Count == 0)
                {
                    return Json(new { listPallets = mvcp });

                }
                decimal tempVolume = 0;

                int i = 0;
                for (; i < listPalletsApp.Count && i < listPalletsMap2.Count; i++)
                {
                    listPalletsApp[i].PalletImportDataHistoryId = listPalletsMap2[i].id;

                    volumOcupat = volumOcupat + listPalletsMap2[i].volume * listPalletsMap2[i].picking_qty;
                    if (listPalletsMap2[i].serial_from == 0)
                    {
                        if (_context.PartCenterPallets.Any(c => c.Destination == listPalletsMap2[i].consignee_code  && c.Pallet_number == listPalletsMap2[i].pallet_no && c.Status == false))
                        {
                            var tempPcPallet = _context.PartCenterPallets.Where(c => c.Destination == listPalletsMap2[i].consignee_code && c.Pallet_number == listPalletsMap2[i].pallet_no).FirstOrDefault();
                            tempVolume = tempPcPallet.Volume;
                            tempPcPallet.Status = true;
                            tempPcPallet.ImportDataHistoryId = listPalletsMap2[i].id;
                            volumOcupat = volumOcupat + tempPcPallet.Volume;
                        }
                        else if (_context.PartCenterPallets.Any(c => c.ImportDataHistoryId == listPalletsMap2[i].id))
                        {
                            var tempPcPallet = _context.PartCenterPallets.Where(c => c.ImportDataHistoryId == listPalletsMap2[i].id).FirstOrDefault();
                            tempVolume = tempPcPallet.Volume;
                            volumOcupat = volumOcupat + tempPcPallet.Volume;
                        }
                        else
                        {
                            tempVolume = 0;

                        }
                    }
                    else
                    {
                        volumOcupat = volumOcupat + listPalletsMap2[i].volume * listPalletsMap2[i].picking_qty;
                    }
                    var tempMVID = new ModelViewCreatePallet { OrderNoApp = listPalletsApp[i].OrderNo, PalletMapHistory = listPalletsMap2[i],VolumPcPallet = tempVolume };

                    mvcp.Add(tempMVID);
                }
                for (; i < listPalletsMap2.Count; i++)
                {
                    if (listPalletsMap2[i].serial_from == 0)
                    {
                        if (_context.PartCenterPallets.Any(c => c.Destination == listPalletsMap2[i].consignee_code && c.Pallet_number == listPalletsMap2[i].pallet_no))
                        {
                            var tempPcPallet = _context.PartCenterPallets.Where(c => c.Destination == listPalletsMap2[i].consignee_code  && c.Pallet_number == listPalletsMap2[i].pallet_no && c.Status == false).FirstOrDefault();
                            tempVolume = tempPcPallet.Volume;
                            tempPcPallet.Status = true;
                            tempPcPallet.ImportDataHistoryId = listPalletsMap2[i].id;
                        }
                        else if (_context.PartCenterPallets.Any(c => c.ImportDataHistoryId == listPalletsMap2[i].id))
                        {
                            var tempPcPallet = _context.PartCenterPallets.Where(c => c.ImportDataHistoryId == listPalletsMap2[i].id).FirstOrDefault();
                            tempVolume = tempPcPallet.Volume;
                            volumOcupat = volumOcupat + tempPcPallet.Volume;
                        }
                        else
                        {
                            tempVolume = 0;

                        }
                    }

                    var tempMVID = new ModelViewCreatePallet { OrderNoApp = -1, PalletMapHistory = listPalletsMap2[i],VolumPcPallet=tempVolume };
                    //listPalletsMap[i].Pallet = null;

                    mvcp.Add(tempMVID);
                }
                _context.UpdateRange(listPalletsApp);
                _context.SaveChanges();

                foreach (var item in switches)
                {
                    var firstPalletTemp = listPalletsApp.Where(c => c.Id == item.FirstPalletId).FirstOrDefault();
                    var secondPalletTemp = listPalletsApp.Where(c => c.Id == item.SecondPalletId).FirstOrDefault();

                    var firstMapPallet = mvcp.Where(c => c.PalletMapHistory.id == firstPalletTemp.PalletImportDataHistoryId).Select(c => c.OrderNoApp).FirstOrDefault();
                    var secondMapPallet = mvcp.Where(c => c.PalletMapHistory.id == secondPalletTemp.PalletImportDataHistoryId).Select(c => c.OrderNoApp).FirstOrDefault();
                    mvcp.Where(c => c.PalletMapHistory.id == firstPalletTemp.PalletImportDataHistoryId).ToList().ForEach(c => c.OrderNoApp = secondMapPallet);
                    mvcp.Where(c => c.PalletMapHistory.id == secondPalletTemp.PalletImportDataHistoryId).ToList().ForEach(c => c.OrderNoApp = firstMapPallet);
                    var auxId = firstPalletTemp.PalletImportDataHistoryId;
                    var auxOrderNo = firstPalletTemp.OrderNo;
                    firstPalletTemp.PalletImportDataHistoryId = secondPalletTemp.PalletImportDataHistoryId;
                    secondPalletTemp.PalletImportDataHistoryId = auxId;
                    var x = mvcp.Where(c => c.PalletMapHistory.id == firstPalletTemp.PalletImportDataHistoryId).ToList();


                    _context.Update(firstPalletTemp);
                    _context.Update(secondPalletTemp);
                    _context.SaveChanges();

                }

                var listPalletsApp2 = _context.Pallets.Include(x => x.PalletImportDataHistory).AsEnumerable().Where(c => c.Container2Id == containerId).OrderBy(c => c.OrderNo).ToList();
                List<Pallet> palletsMotrica = null;
                decimal motricaWeight = 0;
                if (listPalletsApp2.Any(x => x.Column <= 3))
                {
                    palletsMotrica = listPalletsApp2.Where(x => x.Column <= 3 && x.PalletImportDataHistory != null).ToList();

                    motricaWeight = palletsMotrica.Sum(x => x.PalletImportDataHistory.weight);
                    ViewBag.motricaWeight = motricaWeight;
                }

                return Json(new { listPallets = mvcp, motrica = motricaWeight, volumOcupat = volumOcupat.ToString("N2") });

            }
        }


        // POST: Pallets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,TypeId")] Pallet pallet)
        {
            if (ModelState.IsValid)
            {
                string containerId = "", position = "";
                if (TempData.ContainsKey("containerId"))
                    containerId = TempData["containerId"].ToString();
                int idContainer = Convert.ToInt32(containerId);

                if (TempData.ContainsKey("position"))
                    position = TempData["position"].ToString();
                string rowAux = position.Split(",").First();
                string colAux = position.Split(",").Last();
                int row = Convert.ToInt32(rowAux);
                int col = Convert.ToInt32(colAux);

                PalletLoading.Models.Container container = _context.Containers.First(x => x.Id == idContainer);
                //container.PalletId = pallet.Id;

                /*                ICollection<Container> ContainerList = new Collection<Container>();
                                if (pallet.Containers != null)
                                {
                                    ContainerList = pallet.Containers;
                                }

                                ContainerList.Add(container);

                                pallet.Containers = ContainerList;*/
                if (_context.Pallets.Any(x => x.OrderNo == 1) && _context.Pallets.Any(x => x.Container2Id == container.Id))
                {

                    var lastPallet = _context.Pallets.OrderBy(x => x.OrderNo).Last(x => x.Container2Id == container.Id);
                    pallet.OrderNo = lastPallet.OrderNo + 1;
                }
                else
                    pallet.OrderNo = 1;

                pallet.Row = row;
                pallet.Column = col;

                pallet.Container2Id = idContainer;
                _context.Pallets.Add(pallet);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", new { id = idContainer });
            }
            return View(pallet);
        }

        public ActionResult AddCountry(string container, string country)
        {
            int containerId = Convert.ToInt32(container);
            int countryId = Convert.ToInt32(country);
            ContainerAT containerAT = new();
            containerAT.ContainerId = containerId;
            containerAT.CountryId = countryId;
            _context.ContainerATs.Add(containerAT);
            _context.SaveChanges();

            return RedirectToAction("Create", new { id = containerId });
        }

        public ActionResult SwitchPallets(string idContainer, string draggedPallet, string droppedPallet)
        {
            int containerId = Convert.ToInt32(idContainer);

            int idDragged = Convert.ToInt32(draggedPallet);
            int idDropped = Convert.ToInt32(droppedPallet);

            Pallet dragged = _context.Pallets.First(x => x.Id == idDragged);
            Pallet dropped = new();
            SwitchedPallet switchedPallets = new();
            if (_context.Pallets.Any(x => x.Id == idDropped))
                dropped = _context.Pallets.First(x => x.Id == idDropped);

            int? rowAux = 0, colAux = 0;
            int orderNoAux = 0;

            List<Pallet> pallets = _context.Pallets.Where(x => x.Container2Id == containerId).ToList();
            if (!pallets.Any(x => x.Row == rowAux && x.Column == colAux))
            {
                dropped.Row = dragged.Row;
                dropped.Column = dragged.Column;
                dropped.OrderNo = dragged.OrderNo;
                _context.Remove(dragged);
                _context.Add(dropped);
            }
            else
            {

                rowAux = dropped.Row;
                colAux = dropped.Column;
                orderNoAux = dropped.OrderNo;

                dropped.Row = dragged.Row;
                dropped.Column = dragged.Column;
                dropped.OrderNo = dragged.OrderNo;

                dragged.Row = rowAux;
                dragged.Column = colAux;
                dragged.OrderNo = orderNoAux;

                switchedPallets.FirstPallet = dragged.OrderNo.ToString();
                switchedPallets.SecondPallet = dropped.OrderNo.ToString();
                switchedPallets.FirstPalletId = dragged.Id;
                switchedPallets.SecondPalletId = dropped.Id;
                switchedPallets.IdContainer = containerId;

                _context.SwitchedPallets.Add(switchedPallets);
            }
            _context.SaveChanges();
            _context.Update(dragged);
            _context.Update(dropped);
            return RedirectToAction("GetTable", new { id = containerId });
        }

        public ActionResult AddPallet(string idContainer, string position, string palletName)
        {
            int containerId = Convert.ToInt32(idContainer);
            PalletLoading.Models.Container container = _context.Containers.First(x => x.Id == containerId);
            string rowAux = position.Split(",").First();
            string colAux = position.Split(",").Last();
            int row = Convert.ToInt32(rowAux);
            int col = Convert.ToInt32(colAux);

            Pallet pallet = new();
            pallet.Row = row;
            pallet.Column = col;
            pallet.Container2Id = containerId;
            pallet.Name = palletName;
            pallet.DataInsert = DateTime.Now;
            pallet.CreatedBy = User.Identity.Name.Replace("MMRMAKITA\\", "");
            pallet.OrderNo = _context.Pallets.Where(x => x.Container2Id == container.Id).OrderByDescending(c=>c.OrderNo).Select(c=>c.OrderNo).FirstOrDefault() + 1;

            //if (_context.Pallets.Any(x => x.OrderNo == 1) && _context.Pallets.Any(x => x.Container2Id == container.Id))
            //{

            //    var lastPallet = _context.Pallets.OrderBy(x => x.OrderNo).Last(x => x.Container2Id == container.Id);
            //    pallet.OrderNo = lastPallet.OrderNo + 1;
            //}
            //else
            //    pallet.OrderNo = 1;

            _context.Pallets.Add(pallet);
            _context.SaveChanges();



            return RedirectToAction("GetTable", new { id = containerId });
        }


        public ActionResult GetTable(int id)
        {
            var container2 = _context.Containers.Include(c => c.Country).First(x => x.Id == id);
            List<Pallet> pallets2 = _context.Pallets.Where(x => x.Container2Id == container2.Id).ToList();
            var viewModel = new AddContainerViewModel
            {
                Container = container2,
                Pallets = pallets2
            };

            return View(viewModel);
        }

        public ActionResult RemovePallet(string idContainer, string palletId)
        {
            int containerId = Convert.ToInt32(idContainer);
            PalletLoading.Models.Container container = _context.Containers.First(x => x.Id == containerId);

            int idPallet = Convert.ToInt32(palletId);
            Pallet pallet = _context.Pallets.First(x => x.Id == idPallet);
            var listSwitch = _context.SwitchedPallets.Where(c => c.FirstPalletId == idPallet || c.SecondPalletId == idPallet).ToList();
            _context.Pallets.Remove(pallet);
            _context.SwitchedPallets.RemoveRange(listSwitch);
            _context.SaveChanges();

            return RedirectToAction("GetTable", new { id = containerId });
        }

        public ActionResult AddRow(string idContainer)
        {
            int containerId = Convert.ToInt32(idContainer);
            PalletLoading.Models.Container container = _context.Containers.First(x => x.Id == containerId);

            if (container.NoOfRows < 3)
                container.NoOfRows++;
            _context.SaveChanges();
            return RedirectToAction("GetTable", new { id = containerId });
        }

        public ActionResult RemoveRow(string idContainer)
        {
            int containerId = Convert.ToInt32(idContainer);
            PalletLoading.Models.Container container = _context.Containers.First(x => x.Id == containerId);
            List<Pallet> pallets = new();
            var rowToRemove = container.NoOfRows - 1;
            if (_context.Pallets.Any(x => x.Row == rowToRemove && x.Container2Id == containerId))
            {
                pallets = _context.Pallets.Where(x => x.Row == rowToRemove && x.Container2Id == containerId).ToList();
                _context.RemoveRange(pallets);
            }

            if (container.NoOfRows > 1)
                container.NoOfRows--;
            _context.SaveChanges();
            return RedirectToAction("GetTable", new { id = containerId });
        }

        public ActionResult AddColumn(string idContainer)
        {
            int containerId = Convert.ToInt32(idContainer);
            PalletLoading.Models.Container container = _context.Containers.First(x => x.Id == containerId);

            container.NoOfColumns++;
            _context.SaveChanges();
            return RedirectToAction("GetTable", new { id = containerId });
        }

        public ActionResult RemoveColumn(string idContainer)
        {
            int containerId = Convert.ToInt32(idContainer);
            PalletLoading.Models.Container container = _context.Containers.First(x => x.Id == containerId);
            List<Pallet> pallets = new();
            var colToRemove = container.NoOfColumns - 1;
            if (_context.Pallets.Any(x => x.Column == colToRemove && x.Container2Id == containerId))
            {
                pallets = _context.Pallets.Where(x => x.Column == colToRemove && x.Container2Id == containerId).ToList();
                _context.RemoveRange(pallets);
            }

            if (container.NoOfColumns > 1)
                container.NoOfColumns--;
            _context.SaveChanges();
            return RedirectToAction("GetTable", new { id = containerId });
        }

        // GET: Pallets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pallet = await _context.Pallets.FindAsync(id);
            if (pallet == null)
            {
                return NotFound();
            }
            return View(pallet);
        }

        // POST: Pallets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TypeId")] Pallet pallet)
        {
            if (id != pallet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pallet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PalletExists(pallet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pallet);
        }

        // GET: Pallets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pallet = await _context.Pallets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pallet == null)
            {
                return NotFound();
            }

            return View(pallet);
        }

        // POST: Pallets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pallet = await _context.Pallets.FindAsync(id);
            _context.Pallets.Remove(pallet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteClient(int id, int idContainer)
        {
            var client = await _context.ContainerATs.FindAsync(id);
            _context.ContainerATs.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction("CountryList", new { id = idContainer });
        }

        private bool PalletExists(int id)
        {
            return _context.Pallets.Any(e => e.Id == id);
        }

        public ActionResult ChangePosition(string idContainer, string draggedPallet, string droppedPalletRow, string droppedPalletColumn)
        {
            int containerId = Convert.ToInt32(idContainer);
            int idPallet = Convert.ToInt32(draggedPallet);
            int idRow = Convert.ToInt32(droppedPalletRow);
            int idColumn = Convert.ToInt32(droppedPalletColumn);

            Pallet pallet = _context.Pallets.First(x => x.Id == idPallet);
            var pallets = _context.Pallets
                .Where(l => l.Container2Id == containerId && l.Row == idRow && l.Column == idColumn)
                .OrderBy(x => x.OrderNo)
                .ToList();
            pallets.Add(pallet);
            if (pallets.Count > 1)
            {
                List<int>OrderNumbers = new List<int>();
                foreach (var pal in pallets)
                {
                    OrderNumbers.Add(pal.OrderNo);
                }
                OrderNumbers.Sort();
                int i = 0;
                foreach (var pal in pallets)
                {
                    pal.OrderNo = OrderNumbers[i];
                    _context.Update(pal);
                    i++;
                }
            }

            pallet.Row = idRow;
            pallet.Column = idColumn;
            _context.Update(pallet);
            _context.SaveChanges();


            return RedirectToAction("GetTable", new { id = containerId });
        }
    }
}
