﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace AlphaLaunch.Core.Indexes
{
    public interface ISearcher
    {
        void IndexItems(IEnumerable<IIndexable> items);
        ImmutableList<SearchResult> Search(string search, ImmutableDictionary<string, EntryBoost> boostEntries);
    }
}