using KFZ_Konfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFZ_Konfigurator.ViewModels
{
    public class ViewModelConfiguration
    {
        public SimpleCarModelViewModel CarModel { get; set; }
        public int EngineSettingsId { get; set; } = -1;
        public int PaintId { get; set; } = -1;
        public int RimId { get; set; } = -1;

        private IEnumerable<int> _AccessoryIds;
        public IEnumerable<int> AccessoryIds
        {
            get => _AccessoryIds ?? Enumerable.Empty<int>();
            set => _AccessoryIds = value;
        }
        public string OrderLink { get; set; }
        public bool IsReadOnly
        {
            get => !string.IsNullOrEmpty(OrderLink);
        }

        public void Reset()
        {
            CarModel = null;
            EngineSettingsId = -1;
            PaintId = -1;
            RimId = -1;
            AccessoryIds = null;
            OrderLink = null;
        }

        public bool IsValid(int? id, out string error)
        {
            error = null;
            if (SessionData.ActiveConfiguration.CarModel == null)
            {
                error = "car model is not set";
            }
            else if (id.HasValue && SessionData.ActiveConfiguration.CarModel.Id != id.Value)
            {
                error = "selected car model does not match requested model";
            }
            else if (EngineSettingsId == -1)
            {
                error = "engine settings id not set";
            }
            else if (PaintId == -1)
            {
                error = "paint id not set";
            }
            else if (RimId == -1)
            {
                error = "rim id not set";
            }
            return error == null;
        }
    }
    public class ConfigurationLink
    {
        public string Url { get; set; }
        public int Id { get; set; }
    }
}