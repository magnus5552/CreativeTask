Set WshShell = WScript.CreateObject("WScript.Shell")

Return = WshShell.Run("tools\VMEmulator.bat", 1, true)

WScript.Sleep 2000

WshShell.SendKeys "%V"
WshShell.SendKeys "{ENTER}"
WshShell.SendKeys "{UP}"
WshShell.SendKeys "{ENTER}"

WScript.Sleep 500

WshShell.SendKeys "%f"
WshShell.SendKeys "{ENTER}"
WshShell.SendKeys "vm"
WshShell.SendKeys "{ENTER}"
WScript.Sleep 500
WshShell.SendKeys "{ENTER}"
