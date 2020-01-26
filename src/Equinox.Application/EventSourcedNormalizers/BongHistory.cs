using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Equinox.Domain.Core.Events;

namespace Equinox.Application.EventSourcedNormalizers
{
    public class BongHistory
    {
        public static IList<BongHistoryData> HistoryData { get; set; }

        public static IList<BongHistoryData> ToJavaScriptBongHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<BongHistoryData>();
            BongHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.Timestamp);
            var list = new List<BongHistoryData>();
            var last = new BongHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new BongHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    Name = string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name
                        ? ""
                        : change.Name,
                    ReferenceNo = string.IsNullOrWhiteSpace(change.ReferenceNo) || change.ReferenceNo == last.ReferenceNo
                        ? ""
                        : change.ReferenceNo,
                    ArrivingInStock = string.IsNullOrWhiteSpace(change.ArrivingInStock) || change.ArrivingInStock == last.ArrivingInStock
                        ? ""
                        : change.ArrivingInStock.Substring(0,10),
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    Timestamp = change.Timestamp,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }
            return list;
        }

        private static void BongHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var historyData = JsonSerializer.Deserialize<BongHistoryData>(e.Data);
                historyData.Timestamp = DateTime.Parse(historyData.Timestamp).ToString("yyyy'-'MM'-'dd' - 'HH':'mm':'ss");

                switch (e.MessageType)
                {
                    case "BongRegisteredEvent":
                        historyData.Action = "Registered";
                        historyData.Who = e.User;
                        break;
                    case "BongUpdatedEvent":
                        historyData.Action = "Updated";
                        historyData.Who = e.User;
                        break;
                    case "BongRemovedEvent":
                        historyData.Action = "Removed";
                        historyData.Who = e.User;
                        break;
                }
                HistoryData.Add(historyData);
            }
        }
    }
}