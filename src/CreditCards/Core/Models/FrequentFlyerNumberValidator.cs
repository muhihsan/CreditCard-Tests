﻿using CreditCards.Core.Interfaces;
using System;
using System.Globalization;
using System.Linq;

namespace CreditCards.Core.Models
{
    /// <summary>
    /// A frequent flyer number consists of 2 parts separated by a '-':
    /// [member number]-[scheme identifier]
    /// Member numbers consist of 6 numeric digits
    /// Scheme identifier are a single uppercase alphabetic character
    /// </summary>
    public class FrequentFlyerNumberValidator : IFrequentFlyerNumberValidator
    {
        private readonly char[] _validSchemeIdentifier = { 'A', 'Q', 'Y' };
        private const int _expectedTotalLength = 8;
        private const int _expectedMemberNumberLength = 6;

        public bool IsValid(string frequentFlyerNumber)
        {
            if (frequentFlyerNumber == null)
                throw new ArgumentNullException(nameof(frequentFlyerNumber));

            if (frequentFlyerNumber.Length != _expectedTotalLength)
                return false;

            var memberNumberPart = frequentFlyerNumber.Substring(0, _expectedMemberNumberLength);

            if (!int.TryParse(memberNumberPart, NumberStyles.None, null, out int _))
                return false;

            var schemeIdentifier = frequentFlyerNumber.Last();
            return _validSchemeIdentifier.Contains(schemeIdentifier);
        }
    }
}
