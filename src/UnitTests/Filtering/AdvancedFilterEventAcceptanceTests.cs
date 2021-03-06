﻿using System;
using AzureEventGridSimulator.Domain.Entities;
using AzureEventGridSimulator.Infrastructure.Extensions;
using AzureEventGridSimulator.Infrastructure.Settings;
using Shouldly;
using Xunit;

namespace UnitTests.Filtering
{
    public class AdvancedFilterEventAcceptanceTests
    {
        private static readonly EventGridEvent GridEvent = new EventGridEvent
        {
            Id = "EventId",
            Data = new { NumberValue = 1, IsTrue = true, Name = "StringValue", DoubleValue = 0.12345d, NumberMaxValue = ulong.MaxValue },
            DataVersion = "5.0",
            EventTime = DateTime.UtcNow.ToString("O"),
            EventType = "this.is.a.test.event.type",
            MetadataVersion = "2.3.4",
            Subject = "TheEventSubject",
            Topic = "THE_EVENT_TOPIC"
        };

        [Theory]
        [ClassData(typeof(PositiveFilterTestCaseContainer))]
        public void TestAdvancedFilteringSuccess(AdvancedFilterSetting filter)
        {
            var filterConfig = new FilterSetting { AdvancedFilters = new[] { filter } };

            filterConfig.AcceptsEvent(GridEvent).ShouldBeTrue($"{filter.Key} - {filter.OperatorType} - {filter.Value} - {filter.Values.Separate() }");
        }

        [Theory]
        [ClassData(typeof(NegativeFilterTestCaseContainer))]
        public void TestAdvancedFilteringFailure(AdvancedFilterSetting filter)
        {
            var filterConfig = new FilterSetting { AdvancedFilters = new[] { filter } };

            filterConfig.AcceptsEvent(GridEvent).ShouldBeFalse($"{filter.Key} - {filter.OperatorType} - {filter.Value} - {filter.Values.Separate() }");
        }
    }
}
