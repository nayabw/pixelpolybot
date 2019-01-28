Imports Discord
Imports Discord.Commands

Public Class Commands 
    Inherits ModuleBase(Of SocketCommandContext)
    
    <Command("ping")>
    Public Async Function Ping() As Task
        Await ReplyAsync("pong!")
    End Function
    
    <Command("whoislel")>
    Public Async Function WhoIsLel() As Task
        Dim reflectronic = Context.Client.GetUser(384454726512672768)
        Dim embed = new EmbedBuilder With _
                {.Color = new Color(&H00ff00), 
                .Title = "LEL", 
                .Description = "Our Current Lel Is " & reflectronic.ToString(), 
                .ThumbnailUrl = reflectronic.GetAvatarUrl(size := 2048) } 
        
        Await ReplyAsync(embed := embed.Build())
    End Function

    <Command("help")>
    Public Async Function Help() As Task
        Await ReplyAsync("I'm a basic bot to count how many times @lel is pinged. Also I count Michael's daily complaints!")
    End Function

    <Command("lel"), [Alias]("lelcount")>
    Public Async Function LelCount() As Task
        Await ReplyAsync("Lel indeed. We've LELd " & Program.LelCount & " Times!")
    End Function

End Class