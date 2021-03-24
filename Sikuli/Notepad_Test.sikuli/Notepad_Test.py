click("1616599040730.png")
wait("Screenshot_1.png")
type("Notepad" + Key.ENTER)
wait("1616599515526.png")


text = "I love c#. Cool."
type(text)
click("1616602102815.png")
click("saveas.png")
type("File")
click("1616600735989.png")



if(exists("Allert.png")):
    click("Yes-1.png")



click("1616601063683.png")
doubleClick("1616601003188.png")


assert exists(text)

click("1616601063683.png")
    
