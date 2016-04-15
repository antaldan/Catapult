﻿using AlphaLaunch.Core.Icons;

namespace AlphaLaunch.Core.Indexes
{
    public interface IIndexable
    {
        string Name { get; }
        string BoostIdentifier { get; }
        object GetDetails();
        IIconResolver GetIconResolver();
    }
}