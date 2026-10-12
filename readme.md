# Faster Biosculpter Pod (Revised)



A revised version of Inglix's [_Faster Biosculpter Mod_](https://github.com/Inglix/FasterBiosculpterPod) with following changes:

- Recompiled with Rimworld v1.6 (published 1.6 version did not have public source code)
- Replaced custom settings UI with a dependency-free vanilla one (custom one did not work for me)
- Simplified some settings, including:
  - numeric fields instead of sliders for exact input
  - no "recommended" values
- Added setting for general pod efficiency (based on in-game `BiosculpterPodSpeedFactor`)

This revision exists mainly because the settings menu of the latest version of the mod on [Steam](https://steamcommunity.com/sharedfiles/filedetails/?id=2576257954) did not appear at all in my mod settings list, so I was unable to configure it. I asked Claude Sonnet 5.5 to rework the UI entirely without the custom dependency it was based on. So yes, caveat user, this patch is more-or-less the product of gen AI, briefly reviewed and tested by me.

Another factor was that the mod had no published open source code for the 1.6 game version. There were no changes necessary at all for the update to succeed, I just moved the old source code under the 1.5 folder, recompiled with up-to-date game files, and added a `Loadfolders.xml`.

It should be clear by now that this mod is not my original work, credits go to [Inglix](https://github.com/Inglix).
