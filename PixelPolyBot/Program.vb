Imports System
Imports System.IO
Imports System.Reflection
Imports System.Runtime.Serialization.Formatters.Binary
Imports Discord
Imports Discord.Commands
Imports Discord.WebSocket
Imports Microsoft.Extensions.DependencyInjection

Module Program
    Private _client As DiscordSocketClient = New DiscordSocketClient(New DiscordSocketConfig() With {.LogLevel = LogSeverity.Info, .MessageCacheSize = 1000} )
    Private _services As IServiceCollection = New ServiceCollection()
    Private _commands As CommandService = new CommandService()
    Private _provider As IServiceProvider
    Public Property LelCount As ULong = 0
    
    Sub Main(args As String())
        MainAsync(args).GetAwaiter().GetResult()
    End Sub
    
    Async Function MainAsync(args As String()) As Task
        AddHandler _client.Log, AddressOf Log
        
        Try 
            Using fs = File.Open("count", FileMode.OpenOrCreate, FileAccess.ReadWrite)
                Dim bf = New BinaryFormatter()
                LelCount = DirectCast(bf.Deserialize(fs), ULong)
            End Using
            
        Catch ex As Exception
            Log(New LogMessage(LogSeverity.Info, ex.Source, ex.Message, ex))
            
        End Try
        
        
        
        Await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _provider)
        
        AddHandler _client.MessageReceived, AddressOf OnMessage
        Await _client.LoginAsync(TokenType.Bot, args(0))
        Await _client.StartAsync()
        
        Await Task.Delay(-1)
    End Function
    
    
    
    Async Function OnMessage(arg As SocketMessage) As Task
        If TypeOf arg IsNot SocketUserMessage Then
            Return ' ignore system messages
        End If
         
        Dim msg = DirectCast(arg, SocketUserMessage)
        
        
        Dim ids = From user in msg.MentionedUsers
                  Select user.Id

        If ids.Contains(384454726512672768)
            LelCount += 1UL
            Using fs = File.Open("count", FileMode.OpenOrCreate, FileAccess.ReadWrite)
                Dim bf = New BinaryFormatter()
                bf.Serialize(fs, LelCount)
            End Using
        End If
        
        Dim pos = 0
        
        If msg.HasStringPrefix("poly:", pos) OrElse msg.HasMentionPrefix(_client.CurrentUser, pos) Then
            Dim context = new SocketCommandContext(_client, msg)
            
            Dim result = Await _commands.ExecuteAsync(context, pos, _provider)
            
            If Not result.IsSuccess Then 
                Await msg.Channel.SendMessageAsync(result.ErrorReason)
            End If
        End If            
       
    End Function
    
    
    Async Function Log(logMessage As LogMessage) As Task
        Select logMessage.Severity
            Case LogSeverity.Info
                Console.ResetColor()
            Case LogSeverity.Debug
                Console.ForegroundColor = ConsoleColor.Magenta
            Case LogSeverity.Error
                Console.ForegroundColor = ConsoleColor.Red
            Case LogSeverity.Verbose
                Console.ForegroundColor = ConsoleColor.DarkGray
            Case LogSeverity.Warning
                Console.ForegroundColor = ConsoleColor.Yellow
            Case LogSeverity.Critical
                Console.ForegroundColor = ConsoleColor.DarkRed
        End Select
        
        Console.WriteLine(logMessage)
        Console.ResetColor()
    End Function
End Module
