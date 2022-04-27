module KeepApi.Tests

open System
open KeepApi.Domain
open Xunit
open Xunit.Abstractions
open Security

module Security =
    
    type SecurityShould(output: ITestOutputHelper) =
    
        let write result =
                output.WriteLine $"Result: %s{result}"
            
        [<Fact>]
        let ValidPassword() =
            let user: User =
                { Id = Guid.Empty
                  Name = "Teste34441"
                  Email = "A@aa.com"
                  Password = "vIJP5xKSZjx89IaxWH+75fi64BnOdY8epwkzoe5rY7k="
                  IsVerified = true
                  }
                
            let settings:SecuritySettings = {
                JwtSecurityKey = "vH0YOQmKGDlGlQ0itKfBnHMpN8CRrg4N"
                JwtIssuer = "API-PVVmvfotoa"
                JwtAudience = "App-9LaXzT08Zj"
                TokenExpirationTime = 3
                Salt = "SOQbltfCDhyyviTAb1"
                Iterations = 13441
            }
            
            let result = validateUserPassword settings user "123"
            Assert.Equal(true, result)