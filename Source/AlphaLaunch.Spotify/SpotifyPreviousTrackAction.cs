﻿using System;
using System.Collections.Generic;
using System.Linq;
using AlphaLaunch.Core.Actions;

namespace AlphaLaunch.Spotify
{
    public class SpotifyPreviousTrackAction : IStandaloneAction
    {
        public void RunAction()
        {
            new SpotifyClient().PreviousTrack();
        }

        public string Name
        {
            get { return "Spotify: Previous track"; }
        }

        public string BoostIdentifier
        {
            get { return typeof(SpotifyPreviousTrackAction).Name; }
        }
    }
}