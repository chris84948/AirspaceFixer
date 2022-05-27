## Airspace Fixer

AirspaceFixer is a small lightweight project that only contains a single control - `AirspacePanel`.

If you've ever had to host any WinForms forms inside a WPF project, you might know about the dreaded Airspace problem. Basically, the WinForms control **must** always be on top. Even if you try to put a modal dialog on top of the WinForms control, this Airspace issue forces the WinForms control on top.

Now, you may be thinking, WinForms, gross, I'll never do that, well, me too, but I had to use the `WebBrowser` control recently, and since it's a legacy control (made in WinForms), the same Airspace problem applies.

`AirspacePanel` aims to fix this problem by allowing you to host any content you like inside of it, and swapping out the content for a screenshot of the content when you need to place a modal dialog on top of it. This swap is done by using the dependency property `FixAirspace`. Here's an example -

	xmlns:asf="clr-namespace:AirspaceFixer;assembly=AirspaceFixer"

    <asf:AirspacePanel FixAirspace="{Binding FixAirspace}">
        <WebBrowser x:Name="Browser" />
    </asf:AirspacePanel>
    
See the sample app for more info on how to use it.

AirspaceFixer is also available in Nuget to include as a package.